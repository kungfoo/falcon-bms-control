namespace Common.Drawing.Imaging
{
    /// <summary>Encapsulates a metadata property to be included in an image file. Not inheritable.</summary>
    public sealed class PropertyItem
    {
        private PropertyItem(System.Drawing.Imaging.PropertyItem propertyItem)
        {
            WrappedPropertyItem = propertyItem;
        }

        private PropertyItem()
        {
        }

        /// <summary>Gets or sets the ID of the property.</summary>
        /// <returns>The integer that represents the ID of the property.</returns>
        public int Id
        {
            get => WrappedPropertyItem.Id;
            set => WrappedPropertyItem.Id = value;
        }

        /// <summary>Gets or sets the length (in bytes) of the <see cref="P:Common.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
        /// <returns>
        ///     An integer that represents the length (in bytes) of the
        ///     <see cref="P:Common.Drawing.Imaging.PropertyItem.Value" /> byte array.
        /// </returns>
        public int Len
        {
            get => WrappedPropertyItem.Len;
            set => WrappedPropertyItem.Len = value;
        }

        /// <summary>
        ///     Gets or sets an integer that defines the type of data contained in the
        ///     <see cref="P:Common.Drawing.Imaging.PropertyItem.Value" /> property.
        /// </summary>
        /// <returns>
        ///     An integer that defines the type of data contained in
        ///     <see cref="P:Common.Drawing.Imaging.PropertyItem.Value" />.
        /// </returns>
        public short Type
        {
            get => WrappedPropertyItem.Type;
            set => WrappedPropertyItem.Type = value;
        }

        /// <summary>Gets or sets the value of the property item.</summary>
        /// <returns>A byte array that represents the value of the property item.</returns>
        public byte[] Value
        {
            get => WrappedPropertyItem.Value;
            set => WrappedPropertyItem.Value = value;
        }

        private System.Drawing.Imaging.PropertyItem WrappedPropertyItem { get; }


        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.PropertyItem" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.PropertyItem" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.PropertyItem" /> that results from the conversion.</returns>
        /// <param name="propertyItem">The <see cref="T:System.Drawing.Imaging.PropertyItem" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator PropertyItem(System.Drawing.Imaging.PropertyItem propertyItem)
        {
            return propertyItem == null ? null : new PropertyItem(propertyItem);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.PropertyItem" /> to a
        ///     <see cref="T:System.Drawing.Imaging.PropertyItem" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.PropertyItem" /> that results from the conversion.</returns>
        /// <param name="propertyItem">The <see cref="T:Common.Drawing.Imaging.PropertyItem" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.PropertyItem(PropertyItem propertyItem)
        {
            return propertyItem?.WrappedPropertyItem;
        }
    }
}