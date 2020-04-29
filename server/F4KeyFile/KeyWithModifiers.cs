using System;
using System.Runtime.InteropServices;
using System.Text;

namespace F4KeyFile
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Serializable]
    public sealed class KeyWithModifiers
    {
        public KeyWithModifiers() { }

        public KeyWithModifiers(int scanCode, KeyModifiers modifers)
        {
            ScanCode = scanCode;
            Modifiers = modifers;
        }

        public int ScanCode { get; set; }
        public KeyModifiers Modifiers { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (ScanCode  == (int)ScanCodes.NotAssigned)
            {
                sb.Append("0XFFFFFFFF");
            }
            else if (ScanCode == 0)
            {
                sb.Append("0");
            }
            else
            {
                sb.Append("0x");
                sb.Append(ScanCode.ToString("X").TrimStart('0'));
            }
            sb.Append(" ");
            sb.Append((int)Modifiers);
            return sb.ToString();
        }
    }
}