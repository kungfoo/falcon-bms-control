using System;
using System.IO;

namespace Common.Drawing.Imaging
{
    /// <summary>
    ///     Defines a graphic metafile. A metafile contains records that describe a sequence of graphics operations that
    ///     can be recorded (constructed) and played back (displayed). This class is not inheritable.
    /// </summary>
    [Serializable]
    public sealed class Metafile : Image
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     handle and a <see cref="T:Common.Drawing.Imaging.WmfPlaceableFileHeader" />.
        /// </summary>
        /// <param name="hmetafile">A windows handle to a <see cref="T:Common.Drawing.Imaging.Metafile" />. </param>
        /// <param name="wmfHeader">A <see cref="T:Common.Drawing.Imaging.WmfPlaceableFileHeader" />. </param>
        public Metafile(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(hmetafile, wmfHeader);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     handle and a <see cref="T:Common.Drawing.Imaging.WmfPlaceableFileHeader" />. Also, the
        ///     <paramref name="deleteWmf" /> parameter can be used to delete the handle when the metafile is deleted.
        /// </summary>
        /// <param name="hmetafile">A windows handle to a <see cref="T:Common.Drawing.Imaging.Metafile" />. </param>
        /// <param name="wmfHeader">A <see cref="T:Common.Drawing.Imaging.WmfPlaceableFileHeader" />. </param>
        /// <param name="deleteWmf">
        ///     true to delete the handle to the new <see cref="T:Common.Drawing.Imaging.Metafile" /> when the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> is deleted; otherwise, false.
        /// </param>
        public Metafile(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader, bool deleteWmf)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(hmetafile, wmfHeader, deleteWmf);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     handle.
        /// </summary>
        /// <param name="henhmetafile">A handle to an enhanced metafile. </param>
        /// <param name="deleteEmf">
        ///     true to delete the enhanced metafile handle when the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> is deleted; otherwise, false.
        /// </param>
        public Metafile(IntPtr henhmetafile, bool deleteEmf)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(henhmetafile, deleteEmf);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     file name.
        /// </summary>
        /// <param name="filename">
        ///     A <see cref="T:System.String" /> that represents the file name from which to create the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string filename)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(filename);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream.
        /// </summary>
        /// <param name="stream">
        ///     The <see cref="T:System.IO.Stream" /> from which to create the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="stream" /> is null.
        /// </exception>
        public Metafile(Stream stream)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     handle to a device context and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the
        ///     format of the <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="emfType">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, EmfType emfType)
        {
            WrappedMetafile =
                new System.Drawing.Imaging.Metafile(referenceHdc, (System.Drawing.Imaging.EmfType) emfType);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     handle to a device context and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the
        ///     format of the <see cref="T:Common.Drawing.Imaging.Metafile" />. A string can be supplied to name the file.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="emfType">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="description">A descriptive name for the new <see cref="T:Common.Drawing.Imaging.Metafile" />. </param>
        public Metafile(IntPtr referenceHdc, EmfType emfType, string description)
        {
            WrappedMetafile =
                new System.Drawing.Imaging.Metafile(referenceHdc, (System.Drawing.Imaging.EmfType) emfType,
                    description);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     device context, bounded by the specified rectangle.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, RectangleF frameRect)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(referenceHdc, frameRect);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     device context, bounded by the specified rectangle that uses the supplied unit of measure.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     device context, bounded by the specified rectangle that uses the supplied unit of measure, and an
        ///     <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     device context, bounded by the specified rectangle that uses the supplied unit of measure, and an
        ///     <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />. A string can be provided to name the file.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="description">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type,
            string description)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type,
                description);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     device context, bounded by the specified rectangle.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, Rectangle frameRect)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(referenceHdc, frameRect);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     device context, bounded by the specified rectangle that uses the supplied unit of measure.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     device context, bounded by the specified rectangle that uses the supplied unit of measure, and an
        ///     <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     device context, bounded by the specified rectangle that uses the supplied unit of measure, and an
        ///     <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />. A string can be provided to name the file.
        /// </summary>
        /// <param name="referenceHdc">The handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="desc">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type,
            string desc)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type, desc);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        public Metafile(string fileName, IntPtr referenceHdc)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration
        ///     that specifies the format of the <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, EmfType type)
        {
            WrappedMetafile =
                new System.Drawing.Imaging.Metafile(fileName, referenceHdc, (System.Drawing.Imaging.EmfType) type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration
        ///     that specifies the format of the <see cref="T:Common.Drawing.Imaging.Metafile" />. A descriptive string can be
        ///     added, as well.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="description">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, EmfType type, string description)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc,
                (System.Drawing.Imaging.EmfType) type, description);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, and a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, and the supplied
        ///     unit of measure.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, the supplied unit of
        ///     measure, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit,
            EmfType type)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, and the supplied
        ///     unit of measure. A descriptive string can also be added.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="desc">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit,
            string desc)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, desc);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, the supplied unit of
        ///     measure, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />. A descriptive string can also be added.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="description">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit,
            EmfType type, string description)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type,
                description);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, and a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, and the supplied
        ///     unit of measure.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that represents the rectangle that bounds
        ///     the new <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, the supplied unit of
        ///     measure, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit,
            EmfType type)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, and the supplied
        ///     unit of measure. A descriptive string can also be added.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="description">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit,
            string description)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, description);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class with the specified
        ///     file name, a Windows handle to a device context, a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, the supplied unit of
        ///     measure, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />. A descriptive string can also be added.
        /// </summary>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> that represents the file name of the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="description">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit,
            EmfType type, string description)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(fileName, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type,
                description);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        public Metafile(Stream stream, IntPtr referenceHdc)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, and an <see cref="T:Common.Drawing.Imaging.EmfType" />
        ///     enumeration that specifies the format of the <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, EmfType type)
        {
            WrappedMetafile =
                new System.Drawing.Imaging.Metafile(stream, referenceHdc, (System.Drawing.Imaging.EmfType) type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, and an <see cref="T:Common.Drawing.Imaging.EmfType" />
        ///     enumeration that specifies the format of the <see cref="T:Common.Drawing.Imaging.Metafile" />. Also, a string that
        ///     contains a descriptive name for the new <see cref="T:Common.Drawing.Imaging.Metafile" /> can be added.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="description">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, EmfType type, string description)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc,
                (System.Drawing.Imaging.EmfType) type, description);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, and a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc, frameRect);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, and the supplied
        ///     unit of measure.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, the supplied unit of
        ///     measure, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit,
            EmfType type)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, a <see cref="T:Common.Drawing.RectangleF" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, the supplied unit of
        ///     measure, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />. A string that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> can be added.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="description">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit,
            EmfType type, string description)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type,
                description);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, and a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc, frameRect);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, and the supplied
        ///     unit of measure.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit);
        }


        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, the supplied unit of
        ///     measure, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit,
            EmfType type)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Metafile" /> class from the specified
        ///     data stream, a Windows handle to a device context, a <see cref="T:Common.Drawing.Rectangle" /> structure that
        ///     represents the rectangle that bounds the new <see cref="T:Common.Drawing.Imaging.Metafile" />, the supplied unit of
        ///     measure, and an <see cref="T:Common.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />. A string that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> can be added.
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="referenceHdc">A Windows handle to a device context. </param>
        /// <param name="frameRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="frameUnit">
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure
        ///     for <paramref name="frameRect" />.
        /// </param>
        /// <param name="type">
        ///     An <see cref="T:Common.Drawing.Imaging.EmfType" /> that specifies the format of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        /// <param name="description">
        ///     A <see cref="T:System.String" /> that contains a descriptive name for the new
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </param>
        public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit,
            EmfType type, string description)
        {
            WrappedMetafile = new System.Drawing.Imaging.Metafile(stream, referenceHdc, frameRect,
                (System.Drawing.Imaging.MetafileFrameUnit) frameUnit, (System.Drawing.Imaging.EmfType) type,
                description);
        }

        private Metafile(System.Drawing.Imaging.Metafile metafile)
        {
            WrappedMetafile = metafile;
        }

        private Metafile()
        {
        }

        private System.Drawing.Imaging.Metafile WrappedMetafile { get; }

        /// <summary>Returns a Windows handle to an enhanced <see cref="T:Common.Drawing.Imaging.Metafile" />.</summary>
        /// <returns>A Windows handle to this enhanced <see cref="T:Common.Drawing.Imaging.Metafile" />.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public IntPtr GetHenhmetafile()
        {
            return WrappedMetafile.GetHenhmetafile();
        }

        /// <summary>
        ///     Returns the <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with the specified
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with the specified
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <param name="hmetafile">
        ///     The handle to the <see cref="T:Common.Drawing.Imaging.Metafile" /> for which to return a
        ///     header.
        /// </param>
        /// <param name="wmfHeader">A <see cref="T:Common.Drawing.Imaging.WmfPlaceableFileHeader" />. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static MetafileHeader GetMetafileHeader(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader)
        {
            return System.Drawing.Imaging.Metafile.GetMetafileHeader(hmetafile, wmfHeader);
        }

        /// <summary>
        ///     Returns the <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with the specified
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with the specified
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <param name="henhmetafile">
        ///     The handle to the enhanced <see cref="T:Common.Drawing.Imaging.Metafile" /> for which a
        ///     header is returned.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static MetafileHeader GetMetafileHeader(IntPtr henhmetafile)
        {
            return System.Drawing.Imaging.Metafile.GetMetafileHeader(henhmetafile);
        }

        /// <summary>
        ///     Returns the <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with the specified
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with the specified
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <param name="fileName">
        ///     A <see cref="T:System.String" /> containing the name of the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> for which a header is retrieved.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static MetafileHeader GetMetafileHeader(string fileName)
        {
            return System.Drawing.Imaging.Metafile.GetMetafileHeader(fileName);
        }

        /// <summary>
        ///     Returns the <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with the specified
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with the specified
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> containing the
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> for which a header is retrieved.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static MetafileHeader GetMetafileHeader(Stream stream)
        {
            return System.Drawing.Imaging.Metafile.GetMetafileHeader(stream);
        }

        /// <summary>
        ///     Returns the <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> associated with this
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public MetafileHeader GetMetafileHeader()
        {
            return WrappedMetafile.GetMetafileHeader();
        }

        /// <summary>Plays an individual metafile record.</summary>
        /// <param name="recordType">
        ///     Element of the <see cref="T:Common.Drawing.Imaging.EmfPlusRecordType" /> that specifies the
        ///     type of metafile record being played.
        /// </param>
        /// <param name="flags">A set of flags that specify attributes of the record. </param>
        /// <param name="dataSize">The number of bytes in the record data. </param>
        /// <param name="data">An array of bytes that contains the record data. </param>
        public void PlayRecord(EmfPlusRecordType recordType, int flags, int dataSize, byte[] data)
        {
            WrappedMetafile.PlayRecord((System.Drawing.Imaging.EmfPlusRecordType) recordType, flags, dataSize, data);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.Metafile" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.Metafile" /> that results from the conversion.</returns>
        /// <param name="metafile">The <see cref="T:System.Drawing.Imaging.Metafile" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Metafile(System.Drawing.Imaging.Metafile metafile)
        {
            return metafile == null ? null : new Metafile(metafile);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.Metafile" /> to a
        ///     <see cref="T:System.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.Metafile" /> that results from the conversion.</returns>
        /// <param name="metafile">The <see cref="T:Common.Drawing.Imaging.Metafile" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.Metafile(Metafile metafile)
        {
            return metafile?.WrappedMetafile;
        }
    }
}