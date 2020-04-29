namespace Common.Drawing.Drawing2D
{
    /// <summary>Specifies the available cap styles with which a <see cref="T:Common.Drawing.Pen" /> object can end a line.</summary>
    public enum LineCap
    {
        /// <summary>Specifies a flat line cap.</summary>
        Flat,

        /// <summary>Specifies a square line cap.</summary>
        Square,

        /// <summary>Specifies a round line cap.</summary>
        Round,

        /// <summary>Specifies a triangular line cap.</summary>
        Triangle,

        /// <summary>Specifies no anchor.</summary>
        NoAnchor = 16,

        /// <summary>Specifies a square anchor line cap.</summary>
        SquareAnchor,

        /// <summary>Specifies a round anchor cap.</summary>
        RoundAnchor,

        /// <summary>Specifies a diamond anchor cap.</summary>
        DiamondAnchor,

        /// <summary>Specifies an arrow-shaped anchor cap.</summary>
        ArrowAnchor,

        /// <summary>Specifies a custom line cap.</summary>
        Custom = 255,

        /// <summary>Specifies a mask used to check whether a line cap is an anchor cap.</summary>
        AnchorMask = 240
    }
}