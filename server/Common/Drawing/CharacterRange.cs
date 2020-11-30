namespace Common.Drawing
{
    /// <summary>Specifies a range of character positions within a string.</summary>
    /// <filterpriority>1</filterpriority>
    public struct CharacterRange
    {
        private System.Drawing.CharacterRange WrappedCharacterRange;

        private CharacterRange(System.Drawing.CharacterRange characterRange)
        {
            WrappedCharacterRange = characterRange;
        }

        /// <summary>
        ///     Gets or sets the position in the string of the first character of this
        ///     <see cref="T:Common.Drawing.CharacterRange" />.
        /// </summary>
        /// <returns>The first position of this <see cref="T:Common.Drawing.CharacterRange" />.</returns>
        /// <filterpriority>1</filterpriority>
        public int First
        {
            get => WrappedCharacterRange.First;
            set => WrappedCharacterRange.First = value;
        }

        /// <summary>Gets or sets the number of positions in this <see cref="T:Common.Drawing.CharacterRange" />.</summary>
        /// <returns>The number of positions in this <see cref="T:Common.Drawing.CharacterRange" />.</returns>
        /// <filterpriority>1</filterpriority>
        public int Length
        {
            get => WrappedCharacterRange.First;
            set => WrappedCharacterRange.First = value;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.CharacterRange" /> structure, specifying a range
        ///     of character positions within a string.
        /// </summary>
        /// <param name="First">
        ///     The position of the first character in the range. For example, if <paramref name="First" /> is set
        ///     to 0, the first position of the range is position 0 in the string.
        /// </param>
        /// <param name="Length">The number of positions in the range. </param>
        public CharacterRange(int First, int Length)
        {
            WrappedCharacterRange = new System.Drawing.CharacterRange(First, Length);
        }

        /// <summary>Gets a value indicating whether this object is equivalent to the specified object.</summary>
        /// <returns>
        ///     true to indicate the specified object is an instance with the same
        ///     <see cref="P:Common.Drawing.CharacterRange.First" /> and <see cref="P:Common.Drawing.CharacterRange.Length" />
        ///     value as this instance; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare to for equality.</param>
        public override bool Equals(object obj)
        {
            return WrappedCharacterRange.Equals(obj);
        }

        /// <summary>
        ///     Compares two <see cref="T:Common.Drawing.CharacterRange" /> objects. Gets a value indicating whether the
        ///     <see cref="P:Common.Drawing.CharacterRange.First" /> and <see cref="P:Common.Drawing.CharacterRange.Length" />
        ///     values of the two <see cref="T:Common.Drawing.CharacterRange" /> objects are equal.
        /// </summary>
        /// <returns>
        ///     true to indicate the two <see cref="T:Common.Drawing.CharacterRange" /> objects have the same
        ///     <see cref="P:Common.Drawing.CharacterRange.First" /> and <see cref="P:Common.Drawing.CharacterRange.Length" />
        ///     values; otherwise, false.
        /// </returns>
        /// <param name="cr1">A <see cref="T:Common.Drawing.CharacterRange" /> to compare for equality.</param>
        /// <param name="cr2">A <see cref="T:Common.Drawing.CharacterRange" /> to compare for equality.</param>
        public static bool operator ==(CharacterRange cr1, CharacterRange cr2)
        {
            return cr1.WrappedCharacterRange == cr2.WrappedCharacterRange;
        }

        /// <summary>
        ///     Compares two <see cref="T:Common.Drawing.CharacterRange" /> objects. Gets a value indicating whether the
        ///     <see cref="P:Common.Drawing.CharacterRange.First" /> or <see cref="P:Common.Drawing.CharacterRange.Length" />
        ///     values of the two <see cref="T:Common.Drawing.CharacterRange" /> objects are not equal.
        /// </summary>
        /// <returns>
        ///     true to indicate the either the <see cref="P:Common.Drawing.CharacterRange.First" /> or
        ///     <see cref="P:Common.Drawing.CharacterRange.Length" /> values of the two
        ///     <see cref="T:Common.Drawing.CharacterRange" /> objects differ; otherwise, false.
        /// </returns>
        /// <param name="cr1">A <see cref="T:Common.Drawing.CharacterRange" /> to compare for inequality.</param>
        /// <param name="cr2">A <see cref="T:Common.Drawing.CharacterRange" /> to compare for inequality.</param>
        public static bool operator !=(CharacterRange cr1, CharacterRange cr2)
        {
            return cr1.WrappedCharacterRange != cr2.WrappedCharacterRange;
        }

        public override int GetHashCode()
        {
            return WrappedCharacterRange.GetHashCode();
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.CharacterRange" /> to a
        ///     <see cref="T:Common.Drawing.CharacterRange" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.CharacterRange" /> that results from the conversion.</returns>
        /// <param name="characterRange">The <see cref="T:System.Drawing.CharacterRange" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator CharacterRange(System.Drawing.CharacterRange characterRange)
        {
            return new CharacterRange(characterRange);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.CharacterRange" /> to a
        ///     <see cref="T:System.Drawing.CharacterRange" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.CharacterRange" /> that results from the conversion.</returns>
        /// <param name="characterRange">The <see cref="T:Common.Drawing.CharacterRange" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.CharacterRange(CharacterRange characterRange)
        {
            return characterRange.WrappedCharacterRange;
        }
    }
}