//  Copyright 2014 Craig Courtney
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NLog;

namespace FalconBMSUniversalServer
{
    public static class KeyboardEmulator
    {
        private static readonly IntPtr KeyboardLayout;
        private static readonly KeyboardThread KeyboardThread;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("KeyboardEmulator");

        private static readonly Dictionary<string, ushort> Keycodes = new Dictionary<string, ushort>
        {
            {"BACKSPACE", 0x08},
            {"TAB", 0x09},
            {"CLEAR", 0x0C},
            {"RETURN", 0x0D},
            {"LSHIFT", 0xA0},
            {"RSHIFT", 0xA1},
            {"LCONTROL", 0xA2},
            {"RCONTROL", 0xA3},
            {"LALT", 0xA4},
            {"RALT", 0xA5},
            {"PAUSE", 0x13},
            {"CAPSLOCK", 0x14},
            {"ESCAPE", 0x1B},
            {"SPACE", 0x20},
            {"PAGEUP", 0x21},
            {"PAGEDOWN", 0x22},
            {"END", 0x23},
            {"HOME", 0x24},
            {"LEFT", 0x25},
            {"UP", 0x26},
            {"RIGHT", 0x27},
            {"DOWN", 0x28},
            {"PRINTSCREEN", 0x2C},
            {"INSERT", 0x2D},
            {"DELETE", 0x2E},
            {"LWIN", 0x5B},
            {"RWIN", 0x5C},
            {"APPS", 0x5D},
            {"NUMPAD0", 0x60},
            {"NUMPAD1", 0x61},
            {"NUMPAD2", 0x62},
            {"NUMPAD3", 0x63},
            {"NUMPAD4", 0x64},
            {"NUMPAD5", 0x65},
            {"NUMPAD6", 0x66},
            {"NUMPAD7", 0x67},
            {"NUMPAD8", 0x68},
            {"NUMPAD9", 0x69},
            {"MULTIPLY", 0x6A},
            {"ADD", 0x6B},
            {"SEPARATOR", 0x6C},
            {"SUBTRACT", 0x6D},
            {"DECIMAL", 0x6E},
            {"DIVIDE", 0x6F},
            {"F1", 0x70},
            {"F2", 0x71},
            {"F3", 0x72},
            {"F4", 0x73},
            {"F5", 0x74},
            {"F6", 0x75},
            {"F7", 0x76},
            {"F8", 0x77},
            {"F9", 0x78},
            {"F10", 0x79},
            {"F11", 0x7A},
            {"F12", 0x7B},
            {"F13", 0x7C},
            {"F14", 0x7D},
            {"F15", 0x7E},
            {"F16", 0x7F},
            {"F17", 0x80},
            {"F18", 0x81},
            {"F19", 0x82},
            {"F20", 0x83},
            {"F21", 0x84},
            {"F22", 0x85},
            {"F23", 0x86},
            {"F24", 0x87},
            {"NUMLOCK", 0x90},
            {"SCROLLLOCK", 0x91},
            {"ENTER", 0x0D},
            // high byte set is numpad
            {"NUMENTER", 0x10D},
            {"NUMPADENTER", 0x10D}
        };

        static KeyboardEmulator()
        {
            KeyboardThread = new KeyboardThread(30);
            // BMS actually binds to keycodes, not key names, so using qwerty is actually correct. Fallback present, in case anything goes wrong.
            KeyboardLayout = FindQwertyLayout() ?? NativeMethods.GetKeyboardLayout(0);
        }


        private static IntPtr? FindQwertyLayout()
        {
            Int32 allocSize = NativeMethods.GetKeyboardLayoutList(0, null);

            // array of int ptrs, which are not actually pointers but machine word-sized integer handles 
            // containing padded int32 values that we don't have to deallocate
            IntPtr[] hkls = new IntPtr[allocSize];

            // native method to fill array with static handles
            Int32 count = NativeMethods.GetKeyboardLayoutList(hkls.Length, hkls);
            if (count != allocSize)
            {
                // broken native method wrapper? don't use the values
                return null;
            }

            foreach (IntPtr hkl in hkls)
            {
                Logger.Debug(
                    $"KeyboardEmulator: keyboard layout 0x{((UInt32) hkl):X8} detected (not necessarily active)");
                if ((((UInt32) hkl) & 0xffff0000) == 0x04090000)
                {
                    Logger.Debug("Found qwerty layout for keycode conversion.");
                    return hkl;
                }
            }

            return null;
        }


        private static List<NativeMethods.Input> CreateEvents(string keys, bool keyDown, bool reverse)
        {
            List<NativeMethods.Input> eventList = new List<NativeMethods.Input>();
            int length = keys.Length;
            for (int index = 0; index < length; index++)
            {
                char character = keys[index];
                if (character.Equals('{'))
                {
                    int endIndex = keys.IndexOf('}', index + 1);
                    if (endIndex > -1)
                    {
                        string keycode = keys.Substring(index + 1, endIndex - index - 1).ToUpper();
                        if (Keycodes.ContainsKey(keycode))
                        {
                            eventList.Add(CreateInput(Keycodes[keycode], keyDown));
                        }

                        index = endIndex;
                    }
                    else
                    {
                        index = length + 1;
                    }
                }
                else
                {
                    eventList.Add(CreateInput((ushort) NativeMethods.VkKeyScanEx(character, KeyboardLayout), keyDown));
                }
            }

            if (reverse)
            {
                eventList.Reverse();
            }

            return eventList;
        }

        private static NativeMethods.Input CreateInput(ushort virtualKeyCode, bool keyDown)
        {
            ushort ourCode = virtualKeyCode;
            if (ourCode > 0xff)
            {
                virtualKeyCode = (ushort) (virtualKeyCode & 0x00ff);
            }

            NativeMethods.Input input = new NativeMethods.Input();
            input.Type = NativeMethods.InputKeyboard;
            input.inputs.ki.wVk = virtualKeyCode;
            input.inputs.ki.time = 0;
            input.inputs.ki.wScan = 0;
            input.inputs.ki.dwExtraInfo = IntPtr.Zero;
            input.inputs.ki.dwFlags = NativeMethods.KeyScanCode;

            if (ourCode > 0xff ||
                (virtualKeyCode >= 0x21 && virtualKeyCode <= 0x28) ||
                virtualKeyCode == 0x2D ||
                virtualKeyCode == 0x2E ||
                (virtualKeyCode >= 0x5B && virtualKeyCode <= 0x5D) ||
                virtualKeyCode == 0x6F ||
                virtualKeyCode == 0x90 ||
                virtualKeyCode == 0xA3 ||
                virtualKeyCode == 0xA5)
            {
                input.inputs.ki.dwFlags |= NativeMethods.KeyExtended;
            }

            uint scanCode = NativeMethods.MapVirtualKeyEx(virtualKeyCode, 0, KeyboardLayout);
            if (virtualKeyCode == 0x13)
            {
                scanCode = 0x04C5; // extended scancode for Pause
            }

            if (keyDown)
            {
                input.inputs.ki.wScan = (ushort) (scanCode & 0xFF);
            }
            else
            {
                input.inputs.ki.wScan = (ushort) scanCode;
                input.inputs.ki.dwFlags |= NativeMethods.KeyUp;
            }

            return input;
        }

        public static void KeyDown(string keys)
        {
            string[] keyList = Regex.Split(keys, @"\s+");
            foreach (string keyCombo in keyList)
            {
                KeyboardThread.AddEvents(CreateEvents(keys, true, false));
            }
        }

        public static void KeyUp(string keys)
        {
            string[] keyList = Regex.Split(keys, @"\s+");
            foreach (string keyCombo in keyList)
            {
                KeyboardThread.AddEvents(CreateEvents(keys, false, false));
            }
        }

        public static void KeyPress(string keys)
        {
            List<NativeMethods.Input> events = new List<NativeMethods.Input>();

            string[] keyList = Regex.Split(keys, @"\s+");
            foreach (string keyCombo in keyList)
            {
                events.AddRange(CreateEvents(keyCombo, true, false));
                events.AddRange(CreateEvents(keyCombo, false, true));
            }

            KeyboardThread.AddEvents(events);
        }
    }
}