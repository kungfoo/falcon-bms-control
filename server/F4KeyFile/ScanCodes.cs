using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [ComVisible(true)]
    [Serializable]
    public enum ScanCodes
    {
        [Description("")]
        NotAssigned = -1,
        [Description("ESC")]
        Escape = 0x01,
        [Description("1")]
        One = 0x02,
        [Description("2")]
        Two = 0x03,
        [Description("3")]
        Three = 0x04,
        [Description("4")]
        Four = 0x05,
        [Description("5")]
        Five = 0x06,
        [Description("6")]
        Six = 0x07,
        [Description("7")]
        Seven = 0x08,
        [Description("8")]
        Eight = 0x09,
        [Description("9")]
        Nine = 0x0A,
        [Description("0")]
        Zero = 0x0B,
        [Description("-")]
        Minus = 0x0C,
        [Description("=")]
        Equals = 0x0D,
        [Description("BKSP")]
        Backspace = 0x0E,
        [Description("TAB")]
        Tab = 0x0F,
        [Description("Q")]
        Q = 0x10,
        [Description("W")]
        W = 0x11,
        [Description("E")]
        E = 0x12,
        [Description("R")]
        R = 0x13,
        [Description("T")]
        T = 0x14,
        [Description("Y")]
        Y = 0x15,
        [Description("U")]
        U = 0x16,
        [Description("I")]
        I = 0x17,
        [Description("O")]
        O = 0x18,
        [Description("P")]
        P = 0x19,
        [Description("[")]
        LBracket = 0x1A,
        [Description("]")]
        RBracket = 0x1B,
        [Description("ENTER")]
        Return = 0x1C,
        [Description("LCTRL")]
        LControl = 0x1D,
        [Description("A")]
        A = 0x1E,
        [Description("S")]
        S = 0x1F,
        [Description("D")]
        D = 0x20,
        [Description("F")]
        F = 0x21,
        [Description("G")]
        G = 0x22,
        [Description("H")]
        H = 0x23,
        [Description("J")]
        J = 0x24,
        [Description("K")]
        K = 0x25,
        [Description("L")]
        L = 0x26,
        [Description(";")]
        Semicolon = 0x27,
        [Description("'")]
        Apostrophe = 0x28,
        [Description("`")]
        Grave = 0x29,
        [Description("LSHIFT")]
        LShift = 0x2A,
        [Description("\\")]
        Backslash = 0x2B,
        [Description("Z")]
        Z = 0x2C,
        [Description("X")]
        X = 0x2D,
        [Description("C")]
        C = 0x2E,
        [Description("V")]
        V = 0x2F,
        [Description("B")]
        B = 0x30,
        [Description("N")]
        N = 0x31,
        [Description("M")]
        M = 0x32,
        [Description(",")]
        Comma = 0x33,
        [Description(".")]
        Period = 0x34,
        [Description("/")]
        Slash = 0x35,
        [Description("RSHIFT")]
        RShift = 0x36,
        [Description("KP*")]
        Multiply = 0x37,
        [Description("LALT")]
        LMenu = 0x38,
        [Description("SPACE")]
        Space = 0x39,
        [Description("CAPSLOCK")]
        CapsLock = 0x3A,
        [Description("F1")]
        F1 = 0x3B,
        [Description("F2")]
        F2 = 0x3C,
        [Description("F3")]
        F3 = 0x3D,
        [Description("F4")]
        F4 = 0x3E,
        [Description("F5")]
        F5 = 0x3F,
        [Description("F6")]
        F6 = 0x40,
        [Description("F7")]
        F7 = 0x41,
        [Description("F8")]
        F8 = 0x42,
        [Description("F9")]
        F9 = 0x43,
        [Description("F10")]
        F10 = 0x44,
        [Description("NUMLOCK")]
        NumLock = 0x45,
        [Description("SCROLLLOCK")]
        ScrollLock = 0x46,
        [Description("KP7")]
        NumPad7 = 0x47,
        [Description("KP8")]
        NumPad8 = 0x48,
        [Description("KP9")]
        NumPad9 = 0x49,
        [Description("KP-")]
        Subtract = 0x4A,
        [Description("KP4")]
        NumPad4 = 0x4B,
        [Description("KP5")]
        NumPad5 = 0x4C,
        [Description("KP6")]
        NumPad6 = 0x4D,
        [Description("KP+")]
        Add = 0x4E,
        [Description("KP1")]
        NumPad1 = 0x4F,
        [Description("KP2")]
        NumPad2 = 0x50,
        [Description("KP3")]
        NumPad3 = 0x51,
        [Description("KP0")]
        NumPad0 = 0x52,
        [Description("KP.")]
        Decimal = 0x53,
        [Description("F11")]
        F11 = 0x57,
        [Description("F12")]
        F12 = 0x58,
        [Description("F13")]
        F13 = 0x64,
        [Description("F14")]
        F14 = 0x65,
        [Description("F15")]
        F15 = 0x66,
        [Description("KANA")]
        Kana = 0x70,
        [Description("CONVERT")]
        Convert = 0x79,
        [Description("NOCONVERT")]
        NoConvert = 0x7B,
        [Description("YEN")]
        Yen = 0x7D,
        [Description("KP=")]
        NumPadEquals = 0x8D,
        [Description("^")]
        Circumflex = 0x90,
        [Description("@")]
        At = 0x91,
        [Description(":")]
        Colon = 0x92,
        [Description("_")]
        Underline = 0x93,
        [Description("KANJI")]
        Kanji = 0x94,
        [Description("STOP")]
        Stop = 0x95,
        [Description("AX")]
        Ax = 0x96,
        [Description("UNLABELED")]
        Unlabeled = 0x97,
        [Description("KPENTER")]
        NumPadEnter = 0x9C,
        [Description("RCTRL")]
        RControl = 0x9D,
        [Description("KP,")]
        NumPadComma = 0xB3,
        [Description("KP/")]
        Divide = 0xB5,
        [Description("PRTSCR")]
        SysRq = 0xB7,
        [Description("RALT")]
        RMenu = 0xB8,
        [Description("HOME")]
        Home = 0xC7,
        [Description("UARROW")]
        Up = 0xC8,
        [Description("PGUP")]
        Prior = 0xC9,
        [Description("LARROW")]
        Left = 0xCB,
        [Description("RARROW")]
        Right = 0xCD,
        [Description("END")]
        End = 0xCF,
        [Description("DARROW")]
        Down = 0xD0,
        [Description("PGDOWN")]
        Next = 0xD1,
        [Description("INSERT")]
        Insert = 0xD2,
        [Description("DEL")]
        Delete = 0xD3,
        [Description("LWIN")]
        LWin = 0xDB,
        [Description("RWIN")]
        RWin = 0xDC,
        [Description("APPS")]
        Apps = 0xDD
    }
}