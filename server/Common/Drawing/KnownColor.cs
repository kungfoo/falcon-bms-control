namespace Common.Drawing
{
    /// <summary>Specifies the known system colors.</summary>
    /// <filterpriority>2</filterpriority>
    public enum KnownColor
    {
        /// <summary>The system-defined color of the active window's border.</summary>
        ActiveBorder = 1,

        /// <summary>The system-defined color of the background of the active window's title bar.</summary>
        ActiveCaption,

        /// <summary>The system-defined color of the text in the active window's title bar.</summary>
        ActiveCaptionText,

        /// <summary>
        ///     The system-defined color of the application workspace. The application workspace is the area in a
        ///     multiple-document view that is not being occupied by documents.
        /// </summary>
        AppWorkspace,

        /// <summary>The system-defined face color of a 3-D element.</summary>
        Control,

        /// <summary>
        ///     The system-defined shadow color of a 3-D element. The shadow color is applied to parts of a 3-D element that
        ///     face away from the light source.
        /// </summary>
        ControlDark,

        /// <summary>
        ///     The system-defined color that is the dark shadow color of a 3-D element. The dark shadow color is applied to
        ///     the parts of a 3-D element that are the darkest color.
        /// </summary>
        ControlDarkDark,

        /// <summary>
        ///     The system-defined color that is the light color of a 3-D element. The light color is applied to parts of a
        ///     3-D element that face the light source.
        /// </summary>
        ControlLight,

        /// <summary>
        ///     The system-defined highlight color of a 3-D element. The highlight color is applied to the parts of a 3-D
        ///     element that are the lightest color.
        /// </summary>
        ControlLightLight,

        /// <summary>The system-defined color of text in a 3-D element.</summary>
        ControlText,

        /// <summary>The system-defined color of the desktop.</summary>
        Desktop,

        /// <summary>The system-defined color of dimmed text. Items in a list that are disabled are displayed in dimmed text.</summary>
        GrayText,

        /// <summary>
        ///     The system-defined color of the background of selected items. This includes selected menu items as well as
        ///     selected text.
        /// </summary>
        Highlight,

        /// <summary>The system-defined color of the text of selected items.</summary>
        HighlightText,

        /// <summary>
        ///     The system-defined color used to designate a hot-tracked item. Single-clicking a hot-tracked item executes the
        ///     item.
        /// </summary>
        HotTrack,

        /// <summary>The system-defined color of an inactive window's border.</summary>
        InactiveBorder,

        /// <summary>The system-defined color of the background of an inactive window's title bar.</summary>
        InactiveCaption,

        /// <summary>The system-defined color of the text in an inactive window's title bar.</summary>
        InactiveCaptionText,

        /// <summary>The system-defined color of the background of a ToolTip.</summary>
        Info,

        /// <summary>The system-defined color of the text of a ToolTip.</summary>
        InfoText,

        /// <summary>The system-defined color of a menu's background.</summary>
        Menu,

        /// <summary>The system-defined color of a menu's text.</summary>
        MenuText,

        /// <summary>The system-defined color of the background of a scroll bar.</summary>
        ScrollBar,

        /// <summary>The system-defined color of the background in the client area of a window.</summary>
        Window,

        /// <summary>The system-defined color of a window frame.</summary>
        WindowFrame,

        /// <summary>The system-defined color of the text in the client area of a window.</summary>
        WindowText,

        /// <summary>A system-defined color.</summary>
        Transparent,

        /// <summary>A system-defined color.</summary>
        AliceBlue,

        /// <summary>A system-defined color.</summary>
        AntiqueWhite,

        /// <summary>A system-defined color.</summary>
        Aqua,

        /// <summary>A system-defined color.</summary>
        Aquamarine,

        /// <summary>A system-defined color.</summary>
        Azure,

        /// <summary>A system-defined color.</summary>
        Beige,

        /// <summary>A system-defined color.</summary>
        Bisque,

        /// <summary>A system-defined color.</summary>
        Black,

        /// <summary>A system-defined color.</summary>
        BlanchedAlmond,

        /// <summary>A system-defined color.</summary>
        Blue,

        /// <summary>A system-defined color.</summary>
        BlueViolet,

        /// <summary>A system-defined color.</summary>
        Brown,

        /// <summary>A system-defined color.</summary>
        BurlyWood,

        /// <summary>A system-defined color.</summary>
        CadetBlue,

        /// <summary>A system-defined color.</summary>
        Chartreuse,

        /// <summary>A system-defined color.</summary>
        Chocolate,

        /// <summary>A system-defined color.</summary>
        Coral,

        /// <summary>A system-defined color.</summary>
        CornflowerBlue,

        /// <summary>A system-defined color.</summary>
        Cornsilk,

        /// <summary>A system-defined color.</summary>
        Crimson,

        /// <summary>A system-defined color.</summary>
        Cyan,

        /// <summary>A system-defined color.</summary>
        DarkBlue,

        /// <summary>A system-defined color.</summary>
        DarkCyan,

        /// <summary>A system-defined color.</summary>
        DarkGoldenrod,

        /// <summary>A system-defined color.</summary>
        DarkGray,

        /// <summary>A system-defined color.</summary>
        DarkGreen,

        /// <summary>A system-defined color.</summary>
        DarkKhaki,

        /// <summary>A system-defined color.</summary>
        DarkMagenta,

        /// <summary>A system-defined color.</summary>
        DarkOliveGreen,

        /// <summary>A system-defined color.</summary>
        DarkOrange,

        /// <summary>A system-defined color.</summary>
        DarkOrchid,

        /// <summary>A system-defined color.</summary>
        DarkRed,

        /// <summary>A system-defined color.</summary>
        DarkSalmon,

        /// <summary>A system-defined color.</summary>
        DarkSeaGreen,

        /// <summary>A system-defined color.</summary>
        DarkSlateBlue,

        /// <summary>A system-defined color.</summary>
        DarkSlateGray,

        /// <summary>A system-defined color.</summary>
        DarkTurquoise,

        /// <summary>A system-defined color.</summary>
        DarkViolet,

        /// <summary>A system-defined color.</summary>
        DeepPink,

        /// <summary>A system-defined color.</summary>
        DeepSkyBlue,

        /// <summary>A system-defined color.</summary>
        DimGray,

        /// <summary>A system-defined color.</summary>
        DodgerBlue,

        /// <summary>A system-defined color.</summary>
        Firebrick,

        /// <summary>A system-defined color.</summary>
        FloralWhite,

        /// <summary>A system-defined color.</summary>
        ForestGreen,

        /// <summary>A system-defined color.</summary>
        Fuchsia,

        /// <summary>A system-defined color.</summary>
        Gainsboro,

        /// <summary>A system-defined color.</summary>
        GhostWhite,

        /// <summary>A system-defined color.</summary>
        Gold,

        /// <summary>A system-defined color.</summary>
        Goldenrod,

        /// <summary>A system-defined color.</summary>
        Gray,

        /// <summary>A system-defined color.</summary>
        Green,

        /// <summary>A system-defined color.</summary>
        GreenYellow,

        /// <summary>A system-defined color.</summary>
        Honeydew,

        /// <summary>A system-defined color.</summary>
        HotPink,

        /// <summary>A system-defined color.</summary>
        IndianRed,

        /// <summary>A system-defined color.</summary>
        Indigo,

        /// <summary>A system-defined color.</summary>
        Ivory,

        /// <summary>A system-defined color.</summary>
        Khaki,

        /// <summary>A system-defined color.</summary>
        Lavender,

        /// <summary>A system-defined color.</summary>
        LavenderBlush,

        /// <summary>A system-defined color.</summary>
        LawnGreen,

        /// <summary>A system-defined color.</summary>
        LemonChiffon,

        /// <summary>A system-defined color.</summary>
        LightBlue,

        /// <summary>A system-defined color.</summary>
        LightCoral,

        /// <summary>A system-defined color.</summary>
        LightCyan,

        /// <summary>A system-defined color.</summary>
        LightGoldenrodYellow,

        /// <summary>A system-defined color.</summary>
        LightGray,

        /// <summary>A system-defined color.</summary>
        LightGreen,

        /// <summary>A system-defined color.</summary>
        LightPink,

        /// <summary>A system-defined color.</summary>
        LightSalmon,

        /// <summary>A system-defined color.</summary>
        LightSeaGreen,

        /// <summary>A system-defined color.</summary>
        LightSkyBlue,

        /// <summary>A system-defined color.</summary>
        LightSlateGray,

        /// <summary>A system-defined color.</summary>
        LightSteelBlue,

        /// <summary>A system-defined color.</summary>
        LightYellow,

        /// <summary>A system-defined color.</summary>
        Lime,

        /// <summary>A system-defined color.</summary>
        LimeGreen,

        /// <summary>A system-defined color.</summary>
        Linen,

        /// <summary>A system-defined color.</summary>
        Magenta,

        /// <summary>A system-defined color.</summary>
        Maroon,

        /// <summary>A system-defined color.</summary>
        MediumAquamarine,

        /// <summary>A system-defined color.</summary>
        MediumBlue,

        /// <summary>A system-defined color.</summary>
        MediumOrchid,

        /// <summary>A system-defined color.</summary>
        MediumPurple,

        /// <summary>A system-defined color.</summary>
        MediumSeaGreen,

        /// <summary>A system-defined color.</summary>
        MediumSlateBlue,

        /// <summary>A system-defined color.</summary>
        MediumSpringGreen,

        /// <summary>A system-defined color.</summary>
        MediumTurquoise,

        /// <summary>A system-defined color.</summary>
        MediumVioletRed,

        /// <summary>A system-defined color.</summary>
        MidnightBlue,

        /// <summary>A system-defined color.</summary>
        MintCream,

        /// <summary>A system-defined color.</summary>
        MistyRose,

        /// <summary>A system-defined color.</summary>
        Moccasin,

        /// <summary>A system-defined color.</summary>
        NavajoWhite,

        /// <summary>A system-defined color.</summary>
        Navy,

        /// <summary>A system-defined color.</summary>
        OldLace,

        /// <summary>A system-defined color.</summary>
        Olive,

        /// <summary>A system-defined color.</summary>
        OliveDrab,

        /// <summary>A system-defined color.</summary>
        Orange,

        /// <summary>A system-defined color.</summary>
        OrangeRed,

        /// <summary>A system-defined color.</summary>
        Orchid,

        /// <summary>A system-defined color.</summary>
        PaleGoldenrod,

        /// <summary>A system-defined color.</summary>
        PaleGreen,

        /// <summary>A system-defined color.</summary>
        PaleTurquoise,

        /// <summary>A system-defined color.</summary>
        PaleVioletRed,

        /// <summary>A system-defined color.</summary>
        PapayaWhip,

        /// <summary>A system-defined color.</summary>
        PeachPuff,

        /// <summary>A system-defined color.</summary>
        Peru,

        /// <summary>A system-defined color.</summary>
        Pink,

        /// <summary>A system-defined color.</summary>
        Plum,

        /// <summary>A system-defined color.</summary>
        PowderBlue,

        /// <summary>A system-defined color.</summary>
        Purple,

        /// <summary>A system-defined color.</summary>
        Red,

        /// <summary>A system-defined color.</summary>
        RosyBrown,

        /// <summary>A system-defined color.</summary>
        RoyalBlue,

        /// <summary>A system-defined color.</summary>
        SaddleBrown,

        /// <summary>A system-defined color.</summary>
        Salmon,

        /// <summary>A system-defined color.</summary>
        SandyBrown,

        /// <summary>A system-defined color.</summary>
        SeaGreen,

        /// <summary>A system-defined color.</summary>
        SeaShell,

        /// <summary>A system-defined color.</summary>
        Sienna,

        /// <summary>A system-defined color.</summary>
        Silver,

        /// <summary>A system-defined color.</summary>
        SkyBlue,

        /// <summary>A system-defined color.</summary>
        SlateBlue,

        /// <summary>A system-defined color.</summary>
        SlateGray,

        /// <summary>A system-defined color.</summary>
        Snow,

        /// <summary>A system-defined color.</summary>
        SpringGreen,

        /// <summary>A system-defined color.</summary>
        SteelBlue,

        /// <summary>A system-defined color.</summary>
        Tan,

        /// <summary>A system-defined color.</summary>
        Teal,

        /// <summary>A system-defined color.</summary>
        Thistle,

        /// <summary>A system-defined color.</summary>
        Tomato,

        /// <summary>A system-defined color.</summary>
        Turquoise,

        /// <summary>A system-defined color.</summary>
        Violet,

        /// <summary>A system-defined color.</summary>
        Wheat,

        /// <summary>A system-defined color.</summary>
        White,

        /// <summary>A system-defined color.</summary>
        WhiteSmoke,

        /// <summary>A system-defined color.</summary>
        Yellow,

        /// <summary>A system-defined color.</summary>
        YellowGreen,

        /// <summary>The system-defined face color of a 3-D element.</summary>
        ButtonFace,

        /// <summary>
        ///     The system-defined color that is the highlight color of a 3-D element. This color is applied to parts of a 3-D
        ///     element that face the light source.
        /// </summary>
        ButtonHighlight,

        /// <summary>
        ///     The system-defined color that is the shadow color of a 3-D element. This color is applied to parts of a 3-D
        ///     element that face away from the light source.
        /// </summary>
        ButtonShadow,

        /// <summary>The system-defined color of the lightest color in the color gradient of an active window's title bar.</summary>
        GradientActiveCaption,

        /// <summary>The system-defined color of the lightest color in the color gradient of an inactive window's title bar. </summary>
        GradientInactiveCaption,

        /// <summary>The system-defined color of the background of a menu bar.</summary>
        MenuBar,

        /// <summary>The system-defined color used to highlight menu items when the menu appears as a flat menu.</summary>
        MenuHighlight
    }
}