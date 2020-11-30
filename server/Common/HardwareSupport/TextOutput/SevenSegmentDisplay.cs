using System;
using Common.MacroProgramming;

namespace Common.HardwareSupport.TextOutput
{
    [Serializable]
    [Flags]
    public enum SevenSegmentBits : byte
    {
        /// <summary>
        ///     Bitmask for all segments off
        /// </summary>
        None = 0x00,

        /// <summary>
        ///     Bitmask for segment A
        /// </summary>
        SegmentA = 0x80,

        /// <summary>
        ///     Bitmask for segment B
        /// </summary>
        SegmentB = 0x40,

        /// <summary>
        ///     Bitmask for segment C
        /// </summary>
        SegmentC = 0x20,

        /// <summary>
        ///     Bitmask for segment D
        /// </summary>
        SegmentD = 0x08,

        /// <summary>
        ///     Bitmask for segment E
        /// </summary>
        SegmentE = 0x04,

        /// <summary>
        ///     Bitmask for segment F
        /// </summary>
        SegmentF = 0x02,

        /// <summary>
        ///     Bitmask for segment G
        /// </summary>
        SegmentG = 0x01,

        /// <summary>
        ///     Bitmask for decimal point segment
        /// </summary>
        SegmentDP = 0x10,

        /// <summary>
        ///     Bitmask for all segments on
        /// </summary>
        All = 0xFF
    }

    [Serializable]
    public class SevenSegmentDisplay : SegmentedDisplay
    {
        public SevenSegmentDisplay()
        {
            CreateDefaultSegments();
        }

        public SevenSegmentDisplay(DigitalSignal segA, DigitalSignal segB, DigitalSignal segC, DigitalSignal segD,
            DigitalSignal segE, DigitalSignal segF, DigitalSignal segG, DigitalSignal segDP)
            : this()
        {
            AssignSegments(segA, segB, segC, segD, segE, segF, segG, segDP);
        }

        public SevenSegmentDisplay(TextSignal displayText) : base(displayText)
        {
            CreateDefaultSegments();
        }

        public SevenSegmentDisplay(DigitalSignal segA, DigitalSignal segB, DigitalSignal segC, DigitalSignal segD,
            DigitalSignal segE, DigitalSignal segF, DigitalSignal segG, DigitalSignal segDP,
            TextSignal displayText) : this(displayText)
        {
            AssignSegments(segA, segB, segC, segD, segE, segF, segG, segDP);
        }

        public DigitalSignal SegmentA { get; set; }
        public DigitalSignal SegmentB { get; set; }
        public DigitalSignal SegmentC { get; set; }
        public DigitalSignal SegmentD { get; set; }
        public DigitalSignal SegmentDP { get; set; }
        public DigitalSignal SegmentE { get; set; }
        public DigitalSignal SegmentF { get; set; }
        public DigitalSignal SegmentG { get; set; }

        /// <summary>
        ///     Produces a 7-segment bitmask with the appropriate bits set, to represent a specific Latin character
        /// </summary>
        /// <param name="charToConvert">a Latin character to produce a seven-segment display bitmask from</param>
        /// <returns>
        ///     a <see cref="SevenSegmentBits" /> flags value whose bits are set appropriately for sending to a seven-segment
        ///     display
        /// </returns>
        public static SevenSegmentBits CharTo7Seg(char charToConvert)
        {
            switch (charToConvert)
            {
                case '0':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case '1':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC;
                case '2':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentG |
                           SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentD;
                case '3':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentG;
                case '4':
                    return SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG | SevenSegmentBits.SegmentB |
                           SevenSegmentBits.SegmentC;
                case '5':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG |
                           SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD;
                case '6':
                    return SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentG;
                case '7':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC;
                case '8':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF |
                           SevenSegmentBits.SegmentG;
                case '9':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case '.':
                    return SevenSegmentBits.SegmentDP;
                case '-':
                    return SevenSegmentBits.SegmentG;
                case '+':
                    return SevenSegmentBits.SegmentG | SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentE;
                case '!':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentDP;
                case '@':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case '#':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF;
                case '$':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case '%':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentG;
                case '^':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentF;
                case '&':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF |
                           SevenSegmentBits.SegmentG;
                case '*':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case '(':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF;
                case ')':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD;
                case '_':
                    return SevenSegmentBits.SegmentD;
                case '=':
                    return SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentG;
                case '[':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF;
                case ']':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD;
                case '{':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF;
                case '}':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD;
                case '|':
                    return SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case '\\':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case ':':
                    return SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case ';':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentD;
                case '\'':
                    return SevenSegmentBits.SegmentB;
                case '"':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentF;
                case '<':
                    return SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case '>':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentG;
                case ',':
                    return SevenSegmentBits.SegmentE;
                case '/':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case '?':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentG | SevenSegmentBits.SegmentDP;
                case '`':
                    return SevenSegmentBits.SegmentF;
                case '~':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;

                case 'A':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'a':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case 'B':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF |
                           SevenSegmentBits.SegmentG;
                case 'b':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'C':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF;
                case 'c':
                    return SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case 'D':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case 'd':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case 'E':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'e':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'F':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF |
                           SevenSegmentBits.SegmentG;
                case 'f':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF |
                           SevenSegmentBits.SegmentG;
                case 'G':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case 'g':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'H':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'h':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF |
                           SevenSegmentBits.SegmentG;
                case 'I':
                    return SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case 'i':
                    return SevenSegmentBits.SegmentC;
                case 'J':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentE;
                case 'j':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD;
                case 'K':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'k':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'L':
                    return SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case 'l':
                    return SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE;
                case 'M':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentF;
                case 'm':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentF;
                case 'N':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case 'n':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case 'O':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case 'o':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentG;
                case 'P':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'p':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'Q':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'q':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'R':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case 'r':
                    return SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case 'S':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 's':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'T':
                    return SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF |
                           SevenSegmentBits.SegmentG;
                case 't':
                    return SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF |
                           SevenSegmentBits.SegmentG;
                case 'U':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentF;
                case 'u':
                    return SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentE;
                case 'V':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentD | SevenSegmentBits.SegmentF;
                case 'v':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'W':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentE;
                case 'w':
                    return SevenSegmentBits.SegmentA | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentE;
                case 'X':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'x':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentE |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'Y':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'y':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentC | SevenSegmentBits.SegmentD |
                           SevenSegmentBits.SegmentF | SevenSegmentBits.SegmentG;
                case 'Z':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                case 'z':
                    return SevenSegmentBits.SegmentB | SevenSegmentBits.SegmentE | SevenSegmentBits.SegmentG;
                default:
                    return SevenSegmentBits.None;
            }
        }

        protected override void UpdateOutputSignals()
        {
            if (!string.IsNullOrEmpty(DisplayText?.State))
            {
                var toSend = DisplayText.State[0];
                var toSet = CharTo7Seg(toSend);
                SegmentA.State = (toSet & SevenSegmentBits.SegmentA) == SevenSegmentBits.SegmentA && SegmentA != null;
                SegmentB.State = (toSet & SevenSegmentBits.SegmentB) == SevenSegmentBits.SegmentB && SegmentB != null;
                SegmentC.State = (toSet & SevenSegmentBits.SegmentC) == SevenSegmentBits.SegmentC && SegmentC != null;
                SegmentD.State = (toSet & SevenSegmentBits.SegmentD) == SevenSegmentBits.SegmentD && SegmentD != null;
                SegmentE.State = (toSet & SevenSegmentBits.SegmentE) == SevenSegmentBits.SegmentE && SegmentE != null;
                SegmentF.State = (toSet & SevenSegmentBits.SegmentF) == SevenSegmentBits.SegmentF && SegmentF != null;
                SegmentG.State = (toSet & SevenSegmentBits.SegmentG) == SevenSegmentBits.SegmentG && SegmentG != null;
                SegmentDP.State = (toSet & SevenSegmentBits.SegmentDP) == SevenSegmentBits.SegmentDP &&
                                  SegmentDP != null;
            }
            else
            {
                SegmentA.State = false;
                SegmentB.State = false;
                SegmentC.State = false;
                SegmentD.State = false;
                SegmentE.State = false;
                SegmentF.State = false;
                SegmentG.State = false;
                SegmentDP.State = false;
            }
        }

        private void AssignSegments(DigitalSignal segA, DigitalSignal segB, DigitalSignal segC, DigitalSignal segD,
            DigitalSignal segE, DigitalSignal segF, DigitalSignal segG, DigitalSignal segDP)
        {
            SegmentA = segA;
            SegmentB = segB;
            SegmentC = segC;
            SegmentD = segD;
            SegmentE = segE;
            SegmentF = segF;
            SegmentG = segG;
            SegmentDP = segDP;
        }

        private void CreateDefaultSegments()
        {
            SegmentA = new DigitalSignal();
            SegmentB = new DigitalSignal();
            SegmentC = new DigitalSignal();
            SegmentD = new DigitalSignal();
            SegmentE = new DigitalSignal();
            SegmentF = new DigitalSignal();
            SegmentG = new DigitalSignal();
            SegmentDP = new DigitalSignal();
        }
    }
}