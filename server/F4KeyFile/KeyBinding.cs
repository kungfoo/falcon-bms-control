using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace F4KeyFile
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Serializable]
    public sealed class KeyBinding : IBinding
    {

        public KeyBinding() { }

        public KeyBinding(
            string callback,
            int soundId,
            KeyWithModifiers key,
            KeyWithModifiers comboKey,
            UIVisibility uiVisibility,
            string description
            )
        {
            Callback = callback;
            SoundId = soundId;
            Key = key;
            ComboKey = comboKey;
            UIVisibility = uiVisibility;
            Description = description;
        }

        public int SoundId { get; set; }
        public KeyWithModifiers Key { get; set; }
        public KeyWithModifiers ComboKey { get; set; }
        public UIVisibility UIVisibility { get; set; }
        public string Description { get; set; }
        public int LineNum { get; set; }
        public string Callback { get; set; }
        public void SendCallback()
        {
            CallbackSender.SendKeystrokesForKeyBinding(this);
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Callback);
            sb.Append(" ");
            sb.Append(SoundId);
            sb.Append(" ");
            sb.Append("0");
            sb.Append(" ");
            if (Key != null && Key.ScanCode != 0)
            {
                sb.Append(Key);
            }
            else
            {
                sb.Append("0 0");
            }
            sb.Append(" ");
            if (ComboKey != null && ComboKey.ScanCode != 0)
            {
                sb.Append(ComboKey);
            }
            else
            {
                sb.Append("0 0");
            }
            sb.Append(" ");
            if (UIVisibility != UIVisibility.Locked)
            {
                sb.Append((int) UIVisibility);
            }
            else
            {
                sb.Append("-" + (int) UIVisibility);
            }
            sb.Append(" ");
            if (Description != null)
            {
                if (Description.StartsWith("\""))
                {
                    sb.Append(Description);
                }
                else
                {
                    sb.Append("\"" + Description);
                }
                if (Description.EndsWith("\""))
                {
                }
                else
                {
                    sb.Append("\"");
                }
            }
            else
            {
                sb.Append("\"\"");
            }
            return sb.ToString();
        }

        public static bool TryParse(string input, out KeyBinding keyBinding)
        {
            keyBinding = null;
            if (input == null) return false;

            var tokenList = Util.Tokenize(input);
            if (tokenList.Count < 8) return false;

            var callback = tokenList[0];

            int soundId;
            var parsed = Int32.TryParse(tokenList[1],out soundId);
            if (!parsed) return false;
            

            int keyScancode;
            if (tokenList[3] !=null && tokenList[3].StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                parsed = Int32.TryParse(tokenList[3].ToLower().Replace("0x", string.Empty),
                                      NumberStyles.HexNumber, CultureInfo.InvariantCulture, out keyScancode);
            }
            else
            {
                parsed = Int32.TryParse(tokenList[3], out keyScancode);
            }
            if (!parsed) return false;

            int keyModifiers;
            parsed = Int32.TryParse(tokenList[4], out keyModifiers);
            if (!parsed) return false;

            var keyWithModifiers = new KeyWithModifiers(keyScancode, (KeyModifiers)keyModifiers);

            int comboKeyScancode;
            if (tokenList[5].StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                parsed =Int32.TryParse(tokenList[5].ToLowerInvariant().Replace("0x", string.Empty),
                                       NumberStyles.HexNumber, CultureInfo.InvariantCulture, out comboKeyScancode);
            }
            else
            {
                parsed =Int32.TryParse(tokenList[5], out comboKeyScancode);
            }
            if (!parsed) return false;

            int comboKeyModifiers;
            parsed=  Int32.TryParse(tokenList[6], out comboKeyModifiers);
            if (!parsed) return false;

            var comboKeyWithModifiers = new KeyWithModifiers(comboKeyScancode, (KeyModifiers)comboKeyModifiers);

            int uiVisibility;
            parsed = Int32.TryParse(tokenList[7], out uiVisibility);
            if (!parsed) return false;

            var description ="\"\"";
            if (tokenList.Count > 8)
            {
                var firstQuote = input.IndexOf("\"", StringComparison.OrdinalIgnoreCase);
                if (firstQuote > 0)
                {
                    description = input.Substring(firstQuote);
                }
                else
                {
                    return false;
                }
                
            }
            keyBinding = new KeyBinding(callback, soundId, keyWithModifiers, comboKeyWithModifiers, (UIVisibility)uiVisibility,description);
            return true;
        }
    }
}