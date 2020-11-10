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

namespace FalconBMSUniversalServer
{
    public class FalconKeyCallback : IComparable<FalconKeyCallback>
    {
        private static readonly Dictionary<int, string> _keyCodes = new Dictionary<int, string>{
            {1, "{ESCAPE}"},
            {2, "1"},
            {3, "2"},
            {4, "3"},
            {5, "4"},
            {6, "5"},
            {7, "6"},
            {8, "7"},
            {9, "8"},
            {10, "9"},
            {11, "0"},
            {12, "-"},  
            {13, "="},  
            {14, "{BACKSPACE}"},  
            {15, "{TAB}"},
            {16, "q"},
            {17, "w"},
            {18, "e"},
            {19, "r"},
            {20, "t"},
            {21, "y"},
            {22, "u"},
            {23, "i"},
            {24, "o"},
            {25, "p"},
            {26, "["},
            {27, "]"},
            {28, "{RETURN}"},
            {29, "`"},
            {30, "a"},
            {31, "s"},
            {32, "d"},
            {33, "f"},
            {34, "g"},
            {35, "h"},
            {36, "j"},
            {37, "k"},
            {38, "l"},
            {39, ";"},
            {40, "'"},
            {43, "\\"},
            {44, "z"},
            {45, "x"},
            {46, "c"},
            {47, "v"},
            {48, "b"},
            {49, "n"},
            {50, "m"},
            {51, ","},
            {52, "."},
            {53, "/"},
            {55, "{MULTIPLY}"},
            {57, "{SPACE}"},
            {58, "{CAPSLOCK}"},
            {59, "{F1}"},
            {60, "{F2}"},
            {61, "{F3}"},
            {62, "{F4}"},
            {63, "{F5}"},
            {64, "{F6}"},
            {65, "{F7}"},
            {66, "{F8}"},
            {67, "{F9}"},
            {68, "{F10}"},
            {69, "{NUMLOCK}"},
            {70, "{SCROLLOCK}"},
            {71, "{NUMPAD7}"},
            {72, "{NUMPAD8}"},
            {73, "{NUMPAD9}"},
            {74, "{SUBTRACT}"},
            {75, "{NUMPAD4}"},
            {76, "{NUMPAD5}"},
            {77, "{NUMPAD6}"},
            {78, "{ADD}"},
            {79, "{NUMPAD1}"},
            {80, "{NUMPAD2}"},
            {81, "{NUMPAD3}"},
            {82, "{NUMPAD0}"},
            {83, "{DECIMAL}"},
            {87, "{F11}"},
            {88, "{F12}"},
            {100, "{F13}"},
            {101, "{F14}"},
            {102, "{F15}"},
            {156, "{NUMPADENTER}"},
            {157, "{RCONTROL}"},
            {181, "{DIVIDE}"},
            {199, "{HOME}"},
            {200, "{UP}"},
            {201, "{PAGEUP}"},
            {203, "{LEFT}"},
            {205, "{RIGHT}"},
            {207, "{END}"},
            {208, "{DOWN}"},
            {209, "{PAGEDOWN}"},
            {210, "{INSERT}"},
            {211, "{DELETE}"},
            {219, "{LWIN}"},
            {220, "{RWIN}"},
            {221, "{APPS}"}
        };

        private readonly string _callbackName;
        private string _description;
        private int _keyCode;
        private int _modifiers;
        private int _comboKeyCode;
        private int _comboModifiers;

        private bool _stringsGenerated = false;
        private string _keyString = "";
        private string _keyUpString = "";
        private string _comboKeyString = "";

        public FalconKeyCallback(string name)
        {
            _callbackName = name;
        }

        #region Properties

        public string Name
        {
            get { return _callbackName; }
        }

        public int KeyCode
        {
            get { return _keyCode; }
            set
            {
                if (!_keyCode.Equals(value))
                {
                    _keyCode = value;
                    _stringsGenerated = false;
                }
            }
        }

        public int Modifiers
        {
            get { return _modifiers; }
            set
            {
                if (!_modifiers.Equals(value))
                {
                    _modifiers = value;
                    _stringsGenerated = false;
                }
            }
        }

        public int ComboKeyCode
        {
            get { return _comboKeyCode; }
            set
            {
                if (!_comboKeyCode.Equals(value))
                {
                    _comboKeyCode = value;
                    _stringsGenerated = false;
                }
            }
        }

        public int ComboModifiers
        {
            get { return _comboModifiers; }
            set
            {
                if (!_comboModifiers.Equals(value))
                {
                    _comboModifiers = value;
                    _stringsGenerated = false;
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string DisplayDescription
        {
            get { return _callbackName + " - " + _description; }
        }

        public string KeyString
        {
            get
            {
                GenerateStrings();
                return _keyString;
            }
        }

        public string KeyUpString
        {
            get
            {
                GenerateStrings();
                return _keyUpString;
            }
        }

        #endregion

        #region Key Press String Generators

        private void GenerateStrings()
        {
            if (!_stringsGenerated)
            {
                if (_keyCodes.ContainsKey(ComboKeyCode))
                {
                    _comboKeyString = ModifierKeyString(ComboModifiers) + _keyCodes[ComboKeyCode];
                }
                else
                {
                    _comboKeyString = "";
                }

                if (_keyCodes.ContainsKey(KeyCode))
                {
                    _keyString = ModifierKeyString(Modifiers) + _keyCodes[KeyCode];
                    _keyUpString = _keyCodes[KeyCode] + ModifierKeyString(Modifiers);
                }
                else
                {
                    _keyString = "";
                }
                _stringsGenerated = true;
            }
        }

        private string ModifierKeyString(int modifiers)
        {
            return AppendModifier(modifiers, 1, "{LSHIFT}") + AppendModifier(modifiers, 2, "{LCONTROL}") + AppendModifier(modifiers, 4, "{LALT}");
        }

        private string AppendModifier(int modifiers, int flag, string value)
        {
            if ((modifiers & flag) == flag)
            {
                return value;
            }
            return "";
        }

        #endregion

        public void Press()
        {
            GenerateStrings();
            if (_keyString.Length <= 0) return;
            if (_comboKeyString.Length > 0)
            {
                KeyboardEmulator.KeyPress(_comboKeyString + " " + _keyString);
            }
            else
            {
                KeyboardEmulator.KeyPress(_keyString);
            }
        }

        public void Down()
        {
            GenerateStrings();
            if (_keyString.Length > 0)
            {
                if (_comboKeyString.Length > 0)
                {
                    KeyboardEmulator.KeyPress(_comboKeyString);
                    KeyboardEmulator.KeyDown(_keyString);
                }
                else
                {
                    KeyboardEmulator.KeyDown(_keyString);
                }
            }
        }

        public void Up()
        {
            GenerateStrings();
            if (_keyString.Length > 0)
            {
                KeyboardEmulator.KeyUp(_keyUpString);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is FalconKeyCallback other && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public int CompareTo(FalconKeyCallback other)
        {
            return string.Compare(Description, other.Description, StringComparison.Ordinal);
        }
    }
}
