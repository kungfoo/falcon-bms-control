namespace Common.Drawing.Drawing2D
{
    /// <summary>Specifies the different patterns available for <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> objects.</summary>
    public enum HatchStyle
    {
        /// <summary>A pattern of horizontal lines.</summary>
        Horizontal,

        /// <summary>A pattern of vertical lines.</summary>
        Vertical,

        /// <summary>A pattern of lines on a diagonal from upper left to lower right.</summary>
        ForwardDiagonal,

        /// <summary>A pattern of lines on a diagonal from upper right to lower left.</summary>
        BackwardDiagonal,

        /// <summary>Specifies horizontal and vertical lines that cross.</summary>
        Cross,

        /// <summary>A pattern of crisscross diagonal lines.</summary>
        DiagonalCross,

        /// <summary>Specifies a 5-percent hatch. The ratio of foreground color to background color is 5:95.</summary>
        Percent05,

        /// <summary>Specifies a 10-percent hatch. The ratio of foreground color to background color is 10:90.</summary>
        Percent10,

        /// <summary>Specifies a 20-percent hatch. The ratio of foreground color to background color is 20:80.</summary>
        Percent20,

        /// <summary>Specifies a 25-percent hatch. The ratio of foreground color to background color is 25:75.</summary>
        Percent25,

        /// <summary>Specifies a 30-percent hatch. The ratio of foreground color to background color is 30:70.</summary>
        Percent30,

        /// <summary>Specifies a 40-percent hatch. The ratio of foreground color to background color is 40:60.</summary>
        Percent40,

        /// <summary>Specifies a 50-percent hatch. The ratio of foreground color to background color is 50:50.</summary>
        Percent50,

        /// <summary>Specifies a 60-percent hatch. The ratio of foreground color to background color is 60:40.</summary>
        Percent60,

        /// <summary>Specifies a 70-percent hatch. The ratio of foreground color to background color is 70:30.</summary>
        Percent70,

        /// <summary>Specifies a 75-percent hatch. The ratio of foreground color to background color is 75:25.</summary>
        Percent75,

        /// <summary>Specifies a 80-percent hatch. The ratio of foreground color to background color is 80:100.</summary>
        Percent80,

        /// <summary>Specifies a 90-percent hatch. The ratio of foreground color to background color is 90:10.</summary>
        Percent90,

        /// <summary>
        ///     Specifies diagonal lines that slant to the right from top points to bottom points and are spaced 50 percent
        ///     closer together than <see cref="F:Common.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, but are not antialiased.
        /// </summary>
        LightDownwardDiagonal,

        /// <summary>
        ///     Specifies diagonal lines that slant to the left from top points to bottom points and are spaced 50 percent
        ///     closer together than <see cref="F:Common.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, but they are not
        ///     antialiased.
        /// </summary>
        LightUpwardDiagonal,

        /// <summary>
        ///     Specifies diagonal lines that slant to the right from top points to bottom points, are spaced 50 percent
        ///     closer together than, and are twice the width of
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />. This hatch pattern is not antialiased.
        /// </summary>
        DarkDownwardDiagonal,

        /// <summary>
        ///     Specifies diagonal lines that slant to the left from top points to bottom points, are spaced 50 percent closer
        ///     together than <see cref="F:Common.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are twice its width, but
        ///     the lines are not antialiased.
        /// </summary>
        DarkUpwardDiagonal,

        /// <summary>
        ///     Specifies diagonal lines that slant to the right from top points to bottom points, have the same spacing as
        ///     hatch style <see cref="F:Common.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, and are triple its width, but are
        ///     not antialiased.
        /// </summary>
        WideDownwardDiagonal,

        /// <summary>
        ///     Specifies diagonal lines that slant to the left from top points to bottom points, have the same spacing as
        ///     hatch style <see cref="F:Common.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are triple its width, but
        ///     are not antialiased.
        /// </summary>
        WideUpwardDiagonal,

        /// <summary>
        ///     Specifies vertical lines that are spaced 50 percent closer together than
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Vertical" />.
        /// </summary>
        LightVertical,

        /// <summary>
        ///     Specifies horizontal lines that are spaced 50 percent closer together than
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Horizontal" />.
        /// </summary>
        LightHorizontal,

        /// <summary>
        ///     Specifies vertical lines that are spaced 75 percent closer together than hatch style
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Vertical" /> (or 25 percent closer together than
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.LightVertical" />).
        /// </summary>
        NarrowVertical,

        /// <summary>
        ///     Specifies horizontal lines that are spaced 75 percent closer together than hatch style
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Horizontal" /> (or 25 percent closer together than
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.LightHorizontal" />).
        /// </summary>
        NarrowHorizontal,

        /// <summary>
        ///     Specifies vertical lines that are spaced 50 percent closer together than
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Vertical" /> and are twice its width.
        /// </summary>
        DarkVertical,

        /// <summary>
        ///     Specifies horizontal lines that are spaced 50 percent closer together than
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Horizontal" /> and are twice the width of
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Horizontal" />.
        /// </summary>
        DarkHorizontal,

        /// <summary>Specifies dashed diagonal lines, that slant to the right from top points to bottom points.</summary>
        DashedDownwardDiagonal,

        /// <summary>Specifies dashed diagonal lines, that slant to the left from top points to bottom points.</summary>
        DashedUpwardDiagonal,

        /// <summary>Specifies dashed horizontal lines.</summary>
        DashedHorizontal,

        /// <summary>Specifies dashed vertical lines.</summary>
        DashedVertical,

        /// <summary>Specifies a hatch that has the appearance of confetti.</summary>
        SmallConfetti,

        /// <summary>
        ///     Specifies a hatch that has the appearance of confetti, and is composed of larger pieces than
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.SmallConfetti" />.
        /// </summary>
        LargeConfetti,

        /// <summary>Specifies horizontal lines that are composed of zigzags.</summary>
        ZigZag,

        /// <summary>Specifies horizontal lines that are composed of tildes.</summary>
        Wave,

        /// <summary>
        ///     Specifies a hatch that has the appearance of layered bricks that slant to the left from top points to bottom
        ///     points.
        /// </summary>
        DiagonalBrick,

        /// <summary>Specifies a hatch that has the appearance of horizontally layered bricks.</summary>
        HorizontalBrick,

        /// <summary>Specifies a hatch that has the appearance of a woven material.</summary>
        Weave,

        /// <summary>Specifies a hatch that has the appearance of a plaid material.</summary>
        Plaid,

        /// <summary>Specifies a hatch that has the appearance of divots.</summary>
        Divot,

        /// <summary>Specifies horizontal and vertical lines, each of which is composed of dots, that cross.</summary>
        DottedGrid,

        /// <summary>Specifies forward diagonal and backward diagonal lines, each of which is composed of dots, that cross.</summary>
        DottedDiamond,

        /// <summary>
        ///     Specifies a hatch that has the appearance of diagonally layered shingles that slant to the right from top
        ///     points to bottom points.
        /// </summary>
        Shingle,

        /// <summary>Specifies a hatch that has the appearance of a trellis.</summary>
        Trellis,

        /// <summary>Specifies a hatch that has the appearance of spheres laid adjacent to one another.</summary>
        Sphere,

        /// <summary>
        ///     Specifies horizontal and vertical lines that cross and are spaced 50 percent closer together than hatch style
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Cross" />.
        /// </summary>
        SmallGrid,

        /// <summary>Specifies a hatch that has the appearance of a checkerboard.</summary>
        SmallCheckerBoard,

        /// <summary>
        ///     Specifies a hatch that has the appearance of a checkerboard with squares that are twice the size of
        ///     <see cref="F:Common.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard" />.
        /// </summary>
        LargeCheckerBoard,

        /// <summary>Specifies forward diagonal and backward diagonal lines that cross but are not antialiased.</summary>
        OutlinedDiamond,

        /// <summary>Specifies a hatch that has the appearance of a checkerboard placed diagonally.</summary>
        SolidDiamond,

        /// <summary>Specifies the hatch style <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
        LargeGrid = 4,

        /// <summary>Specifies hatch style <see cref="F:Common.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
        Min = 0,

        /// <summary>Specifies hatch style <see cref="F:Common.Drawing.Drawing2D.HatchStyle.SolidDiamond" />.</summary>
        Max = 4
    }
}