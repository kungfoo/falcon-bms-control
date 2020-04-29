using System.Windows.Forms;
using SlimDX.DirectInput;

namespace Common.InputSupport.DirectInput
{
    public static class KeyConverter
    {
        /// <summary>
        ///     Convert the first pressed letter or number in a Keys enum to a Key value
        /// </summary>
        /// <param name="keys">The System.Windows.Forms.Keys enumerator</param>
        /// <returns>A Key value</returns>
        /// <remarks>
        ///     Not all keys are supported.
        ///     Letters, numbers, arrows, Function keys, and many other keys should work
        /// </remarks>
        public static Key ConvertFrom(Keys keys)
        {
            switch (keys)
            {
                #region Key Mappings from Keys to Key

                case Keys.A:
                    return Key.A;
                case Keys.B:
                    return Key.B;
                case Keys.C:
                    return Key.C;
                case Keys.D:
                    return Key.D;
                case Keys.E:
                    return Key.E;
                case Keys.F:
                    return Key.F;
                case Keys.G:
                    return Key.G;
                case Keys.H:
                    return Key.H;
                case Keys.I:
                    return Key.I;
                case Keys.J:
                    return Key.J;
                case Keys.K:
                    return Key.K;
                case Keys.L:
                    return Key.L;
                case Keys.M:
                    return Key.M;
                case Keys.N:
                    return Key.N;
                case Keys.O:
                    return Key.O;
                case Keys.P:
                    return Key.P;
                case Keys.Q:
                    return Key.Q;
                case Keys.R:
                    return Key.R;
                case Keys.S:
                    return Key.S;
                case Keys.T:
                    return Key.T;
                case Keys.U:
                    return Key.U;
                case Keys.V:
                    return Key.V;
                case Keys.W:
                    return Key.W;
                case Keys.X:
                    return Key.X;
                case Keys.Y:
                    return Key.Y;
                case Keys.Z:
                    return Key.Z;
                case Keys.D0:
                    return Key.D0;
                case Keys.D1:
                    return Key.D1;
                case Keys.D2:
                    return Key.D2;
                case Keys.D3:
                    return Key.D3;
                case Keys.D4:
                    return Key.D4;
                case Keys.D5:
                    return Key.D5;
                case Keys.D6:
                    return Key.D6;
                case Keys.D7:
                    return Key.D7;
                case Keys.D8:
                    return Key.D8;
                case Keys.D9:
                    return Key.D9;
                case Keys.NumPad0:
                    return Key.NumberPad0;
                case Keys.NumPad1:
                    return Key.NumberPad1;
                case Keys.NumPad2:
                    return Key.NumberPad2;
                case Keys.NumPad3:
                    return Key.NumberPad3;
                case Keys.NumPad4:
                    return Key.NumberPad4;
                case Keys.NumPad5:
                    return Key.NumberPad5;
                case Keys.NumPad6:
                    return Key.NumberPad6;
                case Keys.NumPad7:
                    return Key.NumberPad7;
                case Keys.NumPad8:
                    return Key.NumberPad8;
                case Keys.NumPad9:
                    return Key.NumberPad9;
                case Keys.F1:
                    return Key.F1;
                case Keys.F2:
                    return Key.F2;
                case Keys.F3:
                    return Key.F3;
                case Keys.F4:
                    return Key.F4;
                case Keys.F5:
                    return Key.F5;
                case Keys.F6:
                    return Key.F6;
                case Keys.F7:
                    return Key.F7;
                case Keys.F8:
                    return Key.F8;
                case Keys.F9:
                    return Key.F9;
                case Keys.F10:
                    return Key.F10;
                case Keys.F11:
                    return Key.F11;
                case Keys.F12:
                    return Key.F12;
                case Keys.Escape:
                    return Key.Escape;
                case Keys.Return:
                    return Key.Return;
                case Keys.Left:
                    return Key.LeftArrow;
                case Keys.Right:
                    return Key.RightArrow;
                case Keys.Up:
                    return Key.UpArrow;
                case Keys.Down:
                    return Key.DownArrow;
                case Keys.Tab:
                    return Key.Tab;
                case Keys.Space:
                    return Key.Space;
                case Keys.PageUp:
                    return Key.PageUp;
                case Keys.PageDown:
                    return Key.PageDown;
                case Keys.End:
                    return Key.End;
                case Keys.Home:
                    return Key.Home;
                case Keys.Delete:
                    return Key.Delete;
                case Keys.Insert:
                    return Key.Insert;
                case Keys.Multiply:
                    return Key.NumberPadStar;
                case Keys.Add:
                    return Key.NumberPadPlus;
                case Keys.Divide:
                    return Key.NumberPadSlash;
                case Keys.Subtract:
                    return Key.NumberPadMinus;
                case Keys.Separator:
                    return Key.NumberPadComma;
                case Keys.Decimal:
                    return Key.NumberPadPeriod;
                case Keys.OemPeriod:
                    return Key.Period;
                case Keys.Oemcomma:
                    return Key.Comma;
                case Keys.Oemplus:
                    return Key.NumberPadPlus;
                case Keys.OemMinus:
                    return Key.NumberPadMinus;
                case Keys.OemSemicolon:
                    return Key.Semicolon;
                case Keys.OemQuotes:
                    return Key.Apostrophe;
                case Keys.OemOpenBrackets:
                    return Key.LeftBracket;
                case Keys.OemCloseBrackets:
                    return Key.RightBracket;

                default:
                    return 0;

                #endregion
            }
        }
    }
}