using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using Common.Drawing.Drawing2D;
using Common.Drawing.Imaging;
using Common.Drawing.Text;

namespace Common.Drawing
{
    /// <summary>Encapsulates a GDI+ drawing surface. This class cannot be inherited.</summary>
    /// <filterpriority>1</filterpriority>
    public sealed class Graphics : MarshalByRefObject, IDisposable, IDeviceContext
    {
        /// <summary>
        ///     Provides a callback method for deciding when the <see cref="Overload:Common.Drawing.Graphics.DrawImage" />
        ///     method should prematurely cancel execution and stop drawing an image.
        /// </summary>
        /// <returns>
        ///     This method returns true if it decides that the <see cref="Overload:Common.Drawing.Graphics.DrawImage" />
        ///     method should prematurely stop execution. Otherwise it returns false to indicate that the
        ///     <see cref="Overload:Common.Drawing.Graphics.DrawImage" /> method should continue execution.
        /// </returns>
        /// <param name="callbackdata">
        ///     Internal pointer that specifies data for the callback method. This parameter is not passed
        ///     by all <see cref="Overload:Common.Drawing.Graphics.DrawImage" /> overloads. You can test for its absence by
        ///     checking for the value <see cref="F:System.IntPtr.Zero" />.
        /// </param>
        public delegate bool DrawImageAbort(IntPtr callbackdata);

        /// <summary>Provides a callback method for the <see cref="Overload:Common.Drawing.Graphics.EnumerateMetafile" /> method.</summary>
        /// <returns>Return true if you want to continue enumerating records; otherwise, false.</returns>
        /// <param name="recordType">
        ///     Member of the <see cref="T:Common.Drawing.Imaging.EmfPlusRecordType" /> enumeration that
        ///     specifies the type of metafile record.
        /// </param>
        /// <param name="flags">Set of flags that specify attributes of the record. </param>
        /// <param name="dataSize">Number of bytes in the record data. </param>
        /// <param name="data">Pointer to a buffer that contains the record data. </param>
        /// <param name="callbackData">Not used. </param>
        public delegate bool EnumerateMetafileProc(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data,
            PlayRecordCallback callbackData);

        private Graphics(System.Drawing.Graphics graphics)
        {
            WrappedGraphics = graphics;
        }

        /// <summary>
        ///     Gets or sets a <see cref="T:Common.Drawing.Region" /> that limits the drawing region of this
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Region" /> that limits the portion of this
        ///     <see cref="T:Common.Drawing.Graphics" /> that is currently available for drawing.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Region Clip
        {
            get => WrappedGraphics.Clip;
            set => WrappedGraphics.Clip = value;
        }

        /// <summary>
        ///     Gets a <see cref="T:Common.Drawing.RectangleF" /> structure that bounds the clipping region of this
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that represents a bounding rectangle for the clipping
        ///     region of this <see cref="T:Common.Drawing.Graphics" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RectangleF ClipBounds => WrappedGraphics.ClipBounds;

        /// <summary>Gets a value that specifies how composited images are drawn to this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>
        ///     This property specifies a member of the <see cref="T:Common.Drawing.Drawing2D.CompositingMode" /> enumeration.
        ///     The default is <see cref="F:Common.Drawing.Drawing2D.CompositingMode.SourceOver" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public CompositingMode CompositingMode
        {
            get => (CompositingMode) WrappedGraphics.CompositingMode;
            set => WrappedGraphics.CompositingMode = (System.Drawing.Drawing2D.CompositingMode) value;
        }

        /// <summary>
        ///     Gets or sets the rendering quality of composited images drawn to this <see cref="T:Common.Drawing.Graphics" />
        ///     .
        /// </summary>
        /// <returns>
        ///     This property specifies a member of the <see cref="T:Common.Drawing.Drawing2D.CompositingQuality" />
        ///     enumeration. The default is <see cref="F:Common.Drawing.Drawing2D.CompositingQuality.Default" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public CompositingQuality CompositingQuality
        {
            get => (CompositingQuality) WrappedGraphics.CompositingQuality;
            set => WrappedGraphics.CompositingQuality = (System.Drawing.Drawing2D.CompositingQuality) value;
        }

        /// <summary>Gets the horizontal resolution of this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>
        ///     The value, in dots per inch, for the horizontal resolution supported by this
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float DpiX => WrappedGraphics.DpiX;

        /// <summary>Gets the vertical resolution of this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>
        ///     The value, in dots per inch, for the vertical resolution supported by this
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float DpiY => WrappedGraphics.DpiX;

        /// <summary>Gets or sets the interpolation mode associated with this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>One of the <see cref="T:Common.Drawing.Drawing2D.InterpolationMode" /> values.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public InterpolationMode InterpolationMode
        {
            get => (InterpolationMode) WrappedGraphics.InterpolationMode;
            set => WrappedGraphics.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode) value;
        }

        /// <summary>Gets a value indicating whether the clipping region of this <see cref="T:Common.Drawing.Graphics" /> is empty.</summary>
        /// <returns>true if the clipping region of this <see cref="T:Common.Drawing.Graphics" /> is empty; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsClipEmpty => WrappedGraphics.IsClipEmpty;

        /// <summary>
        ///     Gets a value indicating whether the visible clipping region of this <see cref="T:Common.Drawing.Graphics" />
        ///     is empty.
        /// </summary>
        /// <returns>
        ///     true if the visible portion of the clipping region of this <see cref="T:Common.Drawing.Graphics" /> is empty;
        ///     otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisibleClipEmpty => WrappedGraphics.IsVisibleClipEmpty;

        /// <summary>Gets or sets the scaling between world units and page units for this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>
        ///     This property specifies a value for the scaling between world units and page units for this
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float PageScale
        {
            get => WrappedGraphics.PageScale;
            set => WrappedGraphics.PageScale = value;
        }

        /// <summary>Gets or sets the unit of measure used for page coordinates in this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>
        ///     One of the <see cref="T:Common.Drawing.GraphicsUnit" /> values other than
        ///     <see cref="F:Common.Drawing.GraphicsUnit.World" />.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     <see cref="P:Common.Drawing.Graphics.PageUnit" /> is set to <see cref="F:Common.Drawing.GraphicsUnit.World" />,
        ///     which is not a physical unit.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public GraphicsUnit PageUnit
        {
            get => (GraphicsUnit) WrappedGraphics.PageUnit;
            set => WrappedGraphics.PageUnit = (System.Drawing.GraphicsUnit) value;
        }

        /// <summary>
        ///     Gets or set a value specifying how pixels are offset during rendering of this
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>This property specifies a member of the <see cref="T:Common.Drawing.Drawing2D.PixelOffsetMode" /> enumeration </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PixelOffsetMode PixelOffsetMode
        {
            get => (PixelOffsetMode) WrappedGraphics.PixelOffsetMode;
            set => WrappedGraphics.PixelOffsetMode = (System.Drawing.Drawing2D.PixelOffsetMode) value;
        }

        /// <summary>
        ///     Gets or sets the rendering origin of this <see cref="T:Common.Drawing.Graphics" /> for dithering and for hatch
        ///     brushes.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Point" /> structure that represents the dither origin for 8-bits-per-pixel and
        ///     16-bits-per-pixel dithering and is also used to set the origin for hatch brushes.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Point RenderingOrigin
        {
            get => WrappedGraphics.RenderingOrigin;
            set => WrappedGraphics.RenderingOrigin = value;
        }

        /// <summary>Gets or sets the rendering quality for this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>One of the <see cref="T:Common.Drawing.Drawing2D.SmoothingMode" /> values.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public SmoothingMode SmoothingMode
        {
            get => (SmoothingMode) WrappedGraphics.SmoothingMode;
            set => WrappedGraphics.SmoothingMode = (System.Drawing.Drawing2D.SmoothingMode) value;
        }

        /// <summary>Gets or sets the gamma correction value for rendering text.</summary>
        /// <returns>The gamma correction value used for rendering antialiased and ClearType text.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int TextContrast
        {
            get => WrappedGraphics.TextContrast;
            set => WrappedGraphics.TextContrast = value;
        }

        /// <summary>Gets or sets the rendering mode for text associated with this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>One of the <see cref="T:Common.Drawing.Text.TextRenderingHint" /> values.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public TextRenderingHint TextRenderingHint
        {
            get => (TextRenderingHint) WrappedGraphics.TextRenderingHint;
            set => WrappedGraphics.TextRenderingHint = (System.Drawing.Text.TextRenderingHint) value;
        }

        /// <summary>Gets or sets a copy of the geometric world transformation for this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>
        ///     A copy of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that represents the geometric world
        ///     transformation for this <see cref="T:Common.Drawing.Graphics" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Matrix Transform
        {
            get => WrappedGraphics.Transform;
            set => WrappedGraphics.Transform = value;
        }

        /// <summary>Gets the bounding rectangle of the visible clipping region of this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that represents a bounding rectangle for the visible
        ///     clipping region of this <see cref="T:Common.Drawing.Graphics" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="Common.Drawing.Printing.PrintingPermission, Common.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public RectangleF VisibleClipBounds => WrappedGraphics.VisibleClipBounds;

        private System.Drawing.Graphics WrappedGraphics { get; }

        /// <summary>Gets the handle to the device context associated with this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>Handle to the device context associated with this <see cref="T:Common.Drawing.Graphics" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public IntPtr GetHdc()
        {
            return WrappedGraphics.GetHdc();
        }

        /// <summary>
        ///     Releases a device context handle obtained by a previous call to the
        ///     <see cref="M:Common.Drawing.Graphics.GetHdc" /> method of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public void ReleaseHdc()
        {
            WrappedGraphics.ReleaseHdc();
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Graphics()
        {
            Dispose(false);
        }

        /// <summary>Adds a comment to the current <see cref="T:Common.Drawing.Imaging.Metafile" />.</summary>
        /// <param name="data">Array of bytes that contains the comment. </param>
        /// <filterpriority>1</filterpriority>
        public void AddMetafileComment(byte[] data)
        {
            WrappedGraphics.AddMetafileComment(data);
        }

        /// <summary>
        ///     Saves a graphics container with the current state of this <see cref="T:Common.Drawing.Graphics" /> and opens
        ///     and uses a new graphics container with the specified scale transformation.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of
        ///     this <see cref="T:Common.Drawing.Graphics" /> at the time of the method call.
        /// </returns>
        /// <param name="dstrect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that, together with the <paramref name="srcrect" /> parameter,
        ///     specifies a scale transformation for the new graphics container.
        /// </param>
        /// <param name="srcrect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that, together with the <paramref name="dstrect" /> parameter,
        ///     specifies a scale transformation for the new graphics container.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure for the container.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
        {
            return WrappedGraphics.BeginContainer(dstrect, srcrect, (System.Drawing.GraphicsUnit) unit);
        }

        /// <summary>
        ///     Saves a graphics container with the current state of this <see cref="T:Common.Drawing.Graphics" /> and opens
        ///     and uses a new graphics container.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of
        ///     this <see cref="T:Common.Drawing.Graphics" /> at the time of the method call.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public GraphicsContainer BeginContainer()
        {
            return WrappedGraphics.BeginContainer();
        }

        /// <summary>
        ///     Saves a graphics container with the current state of this <see cref="T:Common.Drawing.Graphics" /> and opens
        ///     and uses a new graphics container with the specified scale transformation.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of
        ///     this <see cref="T:Common.Drawing.Graphics" /> at the time of the method call.
        /// </returns>
        /// <param name="dstrect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that, together with the <paramref name="srcrect" /> parameter,
        ///     specifies a scale transformation for the container.
        /// </param>
        /// <param name="srcrect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that, together with the <paramref name="dstrect" /> parameter,
        ///     specifies a scale transformation for the container.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure for the container.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
        {
            return WrappedGraphics.BeginContainer(dstrect, srcrect, (System.Drawing.GraphicsUnit) unit);
        }

        /// <summary>Clears the entire drawing surface and fills it with the specified background color.</summary>
        /// <param name="color">
        ///     <see cref="T:Common.Drawing.Color" /> structure that represents the background color of the drawing surface.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void Clear(Color color)
        {
            WrappedGraphics.Clear(color);
        }

        /// <summary>
        ///     Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the
        ///     drawing surface of the <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
        /// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="AllWindows" />
        /// </PermissionSet>
        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
        {
            WrappedGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize);
        }

        /// <summary>
        ///     Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to
        ///     the drawing surface of the <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="AllWindows" />
        /// </PermissionSet>
        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize)
        {
            WrappedGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize);
        }

        /// <summary>
        ///     Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the
        ///     drawing surface of the <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
        /// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <param name="copyPixelOperation">One of the <see cref="T:Common.Drawing.CopyPixelOperation" /> values.</param>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     <paramref name="copyPixelOperation" /> is not a member of <see cref="T:Common.Drawing.CopyPixelOperation" />.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="AllWindows" />
        /// </PermissionSet>
        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize,
            CopyPixelOperation copyPixelOperation)
        {
            WrappedGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize,
                (System.Drawing.CopyPixelOperation) copyPixelOperation);
        }

        /// <summary>
        ///     Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to
        ///     the drawing surface of the <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle</param>
        /// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <param name="copyPixelOperation">One of the <see cref="T:Common.Drawing.CopyPixelOperation" /> values.</param>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     <paramref name="copyPixelOperation" /> is not a member of <see cref="T:Common.Drawing.CopyPixelOperation" />.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="AllWindows" />
        /// </PermissionSet>
        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize,
            CopyPixelOperation copyPixelOperation)
        {
            WrappedGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize,
                (System.Drawing.CopyPixelOperation) copyPixelOperation);
        }

        /// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the arc.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the rectangle that defines the ellipse. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to
        ///     ending point of the arc.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            WrappedGraphics.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws an arc representing a portion of an ellipse specified by a <see cref="T:Common.Drawing.RectangleF" />
        ///     structure.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the arc.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.
        /// </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to
        ///     ending point of the arc.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            WrappedGraphics.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        /// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the arc.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the rectangle that defines the ellipse. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to
        ///     ending point of the arc.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="rects" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="rects" /> is a zero-length array.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            WrappedGraphics.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws an arc representing a portion of an ellipse specified by a <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the arc.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.
        /// </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to
        ///     ending point of the arc.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        {
            WrappedGraphics.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        /// <summary>Draws a Bézier spline defined by four ordered pairs of coordinates that represent points.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the curve.
        /// </param>
        /// <param name="x1">The x-coordinate of the starting point of the curve. </param>
        /// <param name="y1">The y-coordinate of the starting point of the curve. </param>
        /// <param name="x2">The x-coordinate of the first control point of the curve. </param>
        /// <param name="y2">The y-coordinate of the first control point of the curve. </param>
        /// <param name="x3">The x-coordinate of the second control point of the curve. </param>
        /// <param name="y3">The y-coordinate of the second control point of the curve. </param>
        /// <param name="x4">The x-coordinate of the ending point of the curve. </param>
        /// <param name="y4">The y-coordinate of the ending point of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            WrappedGraphics.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
        }

        /// <summary>Draws a Bézier spline defined by four <see cref="T:Common.Drawing.PointF" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the curve.
        /// </param>
        /// <param name="pt1">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that represents the starting point of the curve.
        /// </param>
        /// <param name="pt2">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that represents the first control point for the curve.
        /// </param>
        /// <param name="pt3">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that represents the second control point for the curve.
        /// </param>
        /// <param name="pt4">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that represents the ending point of the curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        {
            WrappedGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        /// <summary>Draws a Bézier spline defined by four <see cref="T:Common.Drawing.Point" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> structure that determines the color, width, and style of the curve.
        /// </param>
        /// <param name="pt1">
        ///     <see cref="T:Common.Drawing.Point" /> structure that represents the starting point of the curve.
        /// </param>
        /// <param name="pt2">
        ///     <see cref="T:Common.Drawing.Point" /> structure that represents the first control point for the curve.
        /// </param>
        /// <param name="pt3">
        ///     <see cref="T:Common.Drawing.Point" /> structure that represents the second control point for the curve.
        /// </param>
        /// <param name="pt4">
        ///     <see cref="T:Common.Drawing.Point" /> structure that represents the ending point of the curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
        {
            WrappedGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        /// <summary>Draws a series of Bézier splines from an array of <see cref="T:Common.Drawing.PointF" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the curve.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.PointF" /> structures that represent the points that
        ///     determine the curve. The number of points in the array should be a multiple of 3 plus 1, such as 4, 7, or 10.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBeziers(Pen pen, PointF[] points)
        {
            WrappedGraphics.DrawBeziers(pen, points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>Draws a series of Bézier splines from an array of <see cref="T:Common.Drawing.Point" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the curve.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.Point" /> structures that represent the points that determine
        ///     the curve. The number of points in the array should be a multiple of 3 plus 1, such as 4, 7, or 10.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBeziers(Pen pen, Point[] points)
        {
            WrappedGraphics.DrawBeziers(pen, points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:Common.Drawing.PointF" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawClosedCurve(Pen pen, PointF[] points)
        {
            WrappedGraphics.DrawClosedCurve(pen, points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Draws a closed cardinal spline defined by an array of <see cref="T:Common.Drawing.PointF" /> structures using
        ///     a specified tension.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that define the spline. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="fillmode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines how
        ///     the curve is filled. This parameter is required but is ignored.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
        {
            WrappedGraphics.DrawClosedCurve(pen, points.Convert<System.Drawing.PointF>().ToArray(), tension,
                (System.Drawing.Drawing2D.FillMode) fillmode);
        }

        /// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:Common.Drawing.Point" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawClosedCurve(Pen pen, Point[] points)
        {
            WrappedGraphics.DrawClosedCurve(pen, points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Draws a closed cardinal spline defined by an array of <see cref="T:Common.Drawing.Point" /> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that define the spline. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="fillmode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines how
        ///     the curve is filled. This parameter is required but ignored.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
        {
            WrappedGraphics.DrawClosedCurve(pen, points.Convert<System.Drawing.Point>().ToArray(), tension,
                (System.Drawing.Drawing2D.FillMode) fillmode);
        }

        /// <summary>Draws a cardinal spline through a specified array of <see cref="T:Common.Drawing.PointF" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, PointF[] points)
        {
            WrappedGraphics.DrawCurve(pen, points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="T:Common.Drawing.PointF" /> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.PointF" /> structures that represent the points that define
        ///     the curve.
        /// </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, PointF[] points, float tension)
        {
            WrappedGraphics.DrawCurve(pen, points.Convert<System.Drawing.PointF>().ToArray(), tension);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="T:Common.Drawing.PointF" /> structures. The
        ///     drawing begins offset from the beginning of the array.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that define the spline. </param>
        /// <param name="offset">
        ///     Offset from the first element in the array of the <paramref name="points" /> parameter to the
        ///     starting point in the curve.
        /// </param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
        {
            WrappedGraphics.DrawCurve(pen, points.Convert<System.Drawing.PointF>().ToArray(), offset, numberOfSegments);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="T:Common.Drawing.PointF" /> structures using a
        ///     specified tension. The drawing begins offset from the beginning of the array.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that define the spline. </param>
        /// <param name="offset">
        ///     Offset from the first element in the array of the <paramref name="points" /> parameter to the
        ///     starting point in the curve.
        /// </param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
        {
            WrappedGraphics.DrawCurve(pen, points.Convert<System.Drawing.PointF>().ToArray(), offset, numberOfSegments,
                tension);
        }

        /// <summary>Draws a cardinal spline through a specified array of <see cref="T:Common.Drawing.Point" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, Point[] points)
        {
            WrappedGraphics.DrawCurve(pen, points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="T:Common.Drawing.Point" /> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that define the spline. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, Point[] points, float tension)
        {
            WrappedGraphics.DrawCurve(pen, points.Convert<System.Drawing.Point>().ToArray(), tension);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="T:Common.Drawing.Point" /> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and height of the curve.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that define the spline. </param>
        /// <param name="offset">
        ///     Offset from the first element in the array of the <paramref name="points" /> parameter to the
        ///     starting point in the curve.
        /// </param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
        {
            WrappedGraphics.DrawCurve(pen, points.Convert<System.Drawing.Point>().ToArray(), offset, numberOfSegments,
                tension);
        }

        /// <summary>Draws an ellipse defined by a bounding <see cref="T:Common.Drawing.RectangleF" />.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the ellipse.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            WrappedGraphics.DrawEllipse(pen, rect);
        }

        /// <summary>Draws an ellipse defined by a bounding rectangle specified by a pair of coordinates, a height, and a width.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the ellipse.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            WrappedGraphics.DrawEllipse(pen, x, y, width, height);
        }

        /// <summary>Draws an ellipse specified by a bounding <see cref="T:Common.Drawing.Rectangle" /> structure.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the ellipse.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that defines the boundaries of the ellipse.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawEllipse(Pen pen, Rectangle rect)
        {
            WrappedGraphics.DrawEllipse(pen, rect);
        }

        /// <summary>
        ///     Draws an ellipse defined by a bounding rectangle specified by coordinates for the upper-left corner of the
        ///     rectangle, a height, and a width.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the ellipse.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawEllipse(Pen pen, int x, int y, int width, int height)
        {
            WrappedGraphics.DrawEllipse(pen, x, y, width, height);
        }

        /// <summary>
        ///     Draws the image represented by the specified <see cref="T:Common.Drawing.Icon" /> at the specified
        ///     coordinates.
        /// </summary>
        /// <param name="icon">
        ///     <see cref="T:Common.Drawing.Icon" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="icon" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawIcon(Icon icon, int x, int y)
        {
            WrappedGraphics.DrawIcon(icon, x, y);
        }

        /// <summary>
        ///     Draws the image represented by the specified <see cref="T:Common.Drawing.Icon" /> within the area specified by
        ///     a <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="icon">
        ///     <see cref="T:Common.Drawing.Icon" /> to draw.
        /// </param>
        /// <param name="targetRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image on
        ///     the display surface. The image contained in the <paramref name="icon" /> parameter is scaled to the dimensions of
        ///     this rectangular area.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="icon" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawIcon(Icon icon, Rectangle targetRect)
        {
            WrappedGraphics.DrawIcon(icon, targetRect);
        }

        /// <summary>Draws the image represented by the specified <see cref="T:Common.Drawing.Icon" /> without scaling the image.</summary>
        /// <param name="icon">
        ///     <see cref="T:Common.Drawing.Icon" /> to draw.
        /// </param>
        /// <param name="targetRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image.
        ///     The image is not scaled to fit this rectangle, but retains its original size. If the image is larger than the
        ///     rectangle, it is clipped to fit inside it.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="icon" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
        {
            WrappedGraphics.DrawIconUnstretched(icon, targetRect);
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" />, using its original physical size, at the specified
        ///     location.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="point">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that represents the upper-left corner of the drawn image.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImage(Image image, PointF point)
        {
            WrappedGraphics.DrawImage(image, point);
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" />, using its original physical size, at the specified
        ///     location.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, float x, float y)
        {
            WrappedGraphics.DrawImage(image, x, y);
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" /> at the specified location and with the specified
        ///     size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location and size of the drawn image.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImage(Image image, RectangleF rect)
        {
            WrappedGraphics.DrawImage(image, rect);
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" /> at the specified location and with the specified
        ///     size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="width">Width of the drawn image. </param>
        /// <param name="height">Height of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, float x, float y, float width, float height)
        {
            WrappedGraphics.DrawImage(image, x, y, width, height);
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" />, using its original physical size, at the specified
        ///     location.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="point">
        ///     <see cref="T:Common.Drawing.Point" /> structure that represents the location of the upper-left corner of the drawn
        ///     image.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImage(Image image, Point point)
        {
            WrappedGraphics.DrawImage(image, point);
        }

        /// <summary>Draws the specified image, using its original physical size, at the location specified by a coordinate pair.</summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, int x, int y)
        {
            WrappedGraphics.DrawImage(image, x, y);
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" /> at the specified location and with the specified
        ///     size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImage(Image image, Rectangle rect)
        {
            WrappedGraphics.DrawImage(image, rect);
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" /> at the specified location and with the specified
        ///     size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="width">Width of the drawn image. </param>
        /// <param name="height">Height of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            WrappedGraphics.DrawImage(image, x, y, width, height);
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" /> at the specified location and with the specified
        ///     shape and size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Draws the specified <see cref="T:Common.Drawing.Image" /> at the specified location and with the specified
        ///     shape and size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>Draws a portion of an image at a specified location.</summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            WrappedGraphics.DrawImage(image, x, y, srcRect, (System.Drawing.GraphicsUnit) srcUnit);
        }

        /// <summary>Draws a portion of an image at a specified location.</summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            WrappedGraphics.DrawImage(image, x, y, srcRect, (System.Drawing.GraphicsUnit) srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            WrappedGraphics.DrawImage(image, destRect, srcRect, (System.Drawing.GraphicsUnit) srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            WrappedGraphics.DrawImage(image, destRect, srcRect, (System.Drawing.GraphicsUnit) srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit,
            ImageAttributes imageAttr)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttr);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing
        ///     of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.PointF[],Common.Drawing.RectangleF,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort)" />
        ///     method according to application-determined criteria.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit,
            ImageAttributes imageAttr, DrawImageAbort callback)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttr, new System.Drawing.Graphics.DrawImageAbort(callback));
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing
        ///     of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.PointF[],Common.Drawing.RectangleF,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort,System.Int32)" />
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="callbackData">
        ///     Value specifying additional data for the <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate to use
        ///     when checking whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.PointF[],Common.Drawing.RectangleF,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort,System.Int32)" />
        ///     method.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit,
            ImageAttributes imageAttr, DrawImageAbort callback, int callbackData)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttr, new System.Drawing.Graphics.DrawImageAbort(callback),
                callbackData);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.Point>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit);
        }

        /// <summary>Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location.</summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit,
            ImageAttributes imageAttr)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.Point>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttr);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing
        ///     of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.Point[],Common.Drawing.Rectangle,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort)" />
        ///     method according to application-determined criteria.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit,
            ImageAttributes imageAttr, DrawImageAbort callback)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.Point>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttr, new System.Drawing.Graphics.DrawImageAbort(callback));
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destPoints">Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" />
        ///     object to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used by the <paramref name="srcRect" /> parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing
        ///     of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.Point[],Common.Drawing.Rectangle,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort,System.Int32)" />
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="callbackData">
        ///     Value specifying additional data for the <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate to use
        ///     when checking whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.Point[],Common.Drawing.Rectangle,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort,System.Int32)" />
        ///     method.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit,
            ImageAttributes imageAttr, DrawImageAbort callback, int callbackData)
        {
            WrappedGraphics.DrawImage(image, destPoints.Convert<System.Drawing.Point>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttr, new System.Drawing.Graphics.DrawImageAbort(callback),
                callbackData);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight,
            GraphicsUnit srcUnit)
        {
            WrappedGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight,
                (System.Drawing.GraphicsUnit) srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="imageAttrs">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight,
            GraphicsUnit srcUnit, ImageAttributes imageAttrs)
        {
            WrappedGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttrs);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="imageAttrs">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing
        ///     of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort)" />
        ///     method according to application-determined criteria.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight,
            GraphicsUnit srcUnit, ImageAttributes imageAttrs, DrawImageAbort callback)
        {
            WrappedGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttrs,
                new System.Drawing.Graphics.DrawImageAbort(callback));
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="imageAttrs">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing
        ///     of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort,System.IntPtr)" />
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="callbackData">
        ///     Value specifying additional data for the
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of
        ///     the DrawImage method.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight,
            GraphicsUnit srcUnit, ImageAttributes imageAttrs, DrawImageAbort callback, IntPtr callbackData)
        {
            WrappedGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttrs, new System.Drawing.Graphics.DrawImageAbort(callback),
                callbackData);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight,
            GraphicsUnit srcUnit)
        {
            WrappedGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight,
                (System.Drawing.GraphicsUnit) srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight,
            GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            WrappedGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttr);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for
        ///     <paramref name="image" />.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing
        ///     of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort)" />
        ///     method according to application-determined criteria.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight,
            GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback)
        {
            WrappedGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttr, new System.Drawing.Graphics.DrawImageAbort(callback));
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="T:Common.Drawing.Image" /> at the specified location
        ///     and with the specified size.
        /// </summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The
        ///     image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="imageAttrs">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the
        ///     <paramref name="image" /> object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing
        ///     of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.DrawImage(Common.Drawing.Image,Common.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,Common.Drawing.GraphicsUnit,Common.Drawing.Imaging.ImageAttributes,Common.Drawing.Graphics.DrawImageAbort,System.IntPtr)" />
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="callbackData">
        ///     Value specifying additional data for the
        ///     <see cref="T:Common.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of
        ///     the DrawImage method.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight,
            GraphicsUnit srcUnit, ImageAttributes imageAttrs, DrawImageAbort callback, IntPtr callbackData)
        {
            WrappedGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight,
                (System.Drawing.GraphicsUnit) srcUnit, imageAttrs, new System.Drawing.Graphics.DrawImageAbort(callback),
                callbackData);
        }

        /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="point">
        ///     <see cref="T:Common.Drawing.Point" /> structure that specifies the upper-left corner of the drawn image.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImageUnscaled(Image image, Point point)
        {
            WrappedGraphics.DrawImageUnscaled(image, point);
        }

        /// <summary>Draws the specified image using its original physical size at the location specified by a coordinate pair.</summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImageUnscaled(Image image, int x, int y)
        {
            WrappedGraphics.DrawImageUnscaled(image, x, y);
        }

        /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> that specifies the upper-left corner of the drawn image. The X and Y
        ///     properties of the rectangle specify the upper-left corner. The Width and Height properties are ignored.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImageUnscaled(Image image, Rectangle rect)
        {
            WrappedGraphics.DrawImageUnscaled(image, rect);
        }

        /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> to draw.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="width">Not used. </param>
        /// <param name="height">Not used. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
        {
            WrappedGraphics.DrawImageUnscaled(image, x, y, width, height);
        }

        /// <summary>Draws the specified image without scaling and clips it, if necessary, to fit in the specified rectangle.</summary>
        /// <param name="image">The <see cref="T:Common.Drawing.Image" /> to draw.</param>
        /// <param name="rect">The <see cref="T:Common.Drawing.Rectangle" /> in which to draw the image.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
        {
            WrappedGraphics.DrawImageUnscaledAndClipped(image, rect);
        }

        /// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the line.
        /// </param>
        /// <param name="x1">The x-coordinate of the first point. </param>
        /// <param name="y1">The y-coordinate of the first point. </param>
        /// <param name="x2">The x-coordinate of the second point. </param>
        /// <param name="y2">The y-coordinate of the second point. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            WrappedGraphics.DrawLine(pen, x1, y1, x2, y2);
        }

        /// <summary>Draws a line connecting two <see cref="T:Common.Drawing.PointF" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the line.
        /// </param>
        /// <param name="pt1">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that represents the first point to connect.
        /// </param>
        /// <param name="pt2">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that represents the second point to connect.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            WrappedGraphics.DrawLine(pen, pt1, pt2);
        }

        /// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the line.
        /// </param>
        /// <param name="x1">The x-coordinate of the first point. </param>
        /// <param name="y1">The y-coordinate of the first point. </param>
        /// <param name="x2">The x-coordinate of the second point. </param>
        /// <param name="y2">The y-coordinate of the second point. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            WrappedGraphics.DrawLine(pen, x1, y1, x2, y2);
        }

        /// <summary>Draws a line connecting two <see cref="T:Common.Drawing.Point" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the line.
        /// </param>
        /// <param name="pt1">
        ///     <see cref="T:Common.Drawing.Point" /> structure that represents the first point to connect.
        /// </param>
        /// <param name="pt2">
        ///     <see cref="T:Common.Drawing.Point" /> structure that represents the second point to connect.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            WrappedGraphics.DrawLine(pen, pt1, pt2);
        }

        /// <summary>Draws a series of line segments that connect an array of <see cref="T:Common.Drawing.PointF" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the line segments.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that represent the points to connect. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawLines(Pen pen, PointF[] points)
        {
            WrappedGraphics.DrawLines(pen, points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>Draws a series of line segments that connect an array of <see cref="T:Common.Drawing.Point" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the line segments.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that represent the points to connect. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawLines(Pen pen, Point[] points)
        {
            WrappedGraphics.DrawLines(pen, points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>Draws a <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the path.
        /// </param>
        /// <param name="path">
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="path" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawPath(Pen pen, GraphicsPath path)
        {
            WrappedGraphics.DrawPath(pen, path);
        }

        /// <summary>
        ///     Draws a pie shape defined by an ellipse specified by a <see cref="T:Common.Drawing.RectangleF" /> structure
        ///     and two radial lines.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the pie shape.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that represents the bounding rectangle that defines the
        ///     ellipse from which the pie shape comes.
        /// </param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
        /// <param name="sweepAngle">
        ///     Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the
        ///     second side of the pie shape.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            WrappedGraphics.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial
        ///     lines.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the pie shape.
        /// </param>
        /// <param name="x">
        ///     The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie shape comes.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie shape comes.
        /// </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
        /// <param name="sweepAngle">
        ///     Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the
        ///     second side of the pie shape.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            WrappedGraphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a pie shape defined by an ellipse specified by a <see cref="T:Common.Drawing.Rectangle" /> structure and
        ///     two radial lines.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the pie shape.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse
        ///     from which the pie shape comes.
        /// </param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
        /// <param name="sweepAngle">
        ///     Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the
        ///     second side of the pie shape.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        {
            WrappedGraphics.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial
        ///     lines.
        /// </summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the pie shape.
        /// </param>
        /// <param name="x">
        ///     The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie shape comes.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie shape comes.
        /// </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
        /// <param name="sweepAngle">
        ///     Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the
        ///     second side of the pie shape.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            WrappedGraphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>Draws a polygon defined by an array of <see cref="T:Common.Drawing.PointF" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the polygon.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.PointF" /> structures that represent the vertices of the
        ///     polygon.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPolygon(Pen pen, PointF[] points)
        {
            WrappedGraphics.DrawPolygon(pen, points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>Draws a polygon defined by an array of <see cref="T:Common.Drawing.Point" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the polygon.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.Point" /> structures that represent the vertices of the
        ///     polygon.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPolygon(Pen pen, Point[] points)
        {
            WrappedGraphics.DrawPolygon(pen, points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>Draws a rectangle specified by a <see cref="T:Common.Drawing.Rectangle" /> structure.</summary>
        /// <param name="pen">A <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the rectangle. </param>
        /// <param name="rect">A <see cref="T:Common.Drawing.Rectangle" /> structure that represents the rectangle to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawRectangle(Pen pen, Rectangle rect)
        {
            WrappedGraphics.DrawRectangle(pen, rect);
        }

        /// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
        /// <param name="pen">A <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the rectangle. </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw. </param>
        /// <param name="width">The width of the rectangle to draw. </param>
        /// <param name="height">The height of the rectangle to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawRectangle(Pen pen, float x, float y, float width, float height)
        {
            WrappedGraphics.DrawRectangle(pen, x, y, width, height);
        }

        /// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the rectangle.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw. </param>
        /// <param name="width">Width of the rectangle to draw. </param>
        /// <param name="height">Height of the rectangle to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            WrappedGraphics.DrawRectangle(pen, x, y, width, height);
        }

        /// <summary>Draws a series of rectangles specified by <see cref="T:Common.Drawing.RectangleF" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the outlines of the rectangles.
        /// </param>
        /// <param name="rects">
        ///     Array of <see cref="T:Common.Drawing.RectangleF" /> structures that represent the rectangles to
        ///     draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="rects" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="rects" /> is a zero-length array.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawRectangles(Pen pen, RectangleF[] rects)
        {
            WrappedGraphics.DrawRectangles(pen, rects.Convert<System.Drawing.RectangleF>().ToArray());
        }

        /// <summary>Draws a series of rectangles specified by <see cref="T:Common.Drawing.Rectangle" /> structures.</summary>
        /// <param name="pen">
        ///     <see cref="T:Common.Drawing.Pen" /> that determines the color, width, and style of the outlines of the rectangles.
        /// </param>
        /// <param name="rects">
        ///     Array of <see cref="T:Common.Drawing.Rectangle" /> structures that represent the rectangles to
        ///     draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="pen" /> is null.-or-<paramref name="rects" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="rects" /> is a zero-length array.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawRectangles(Pen pen, Rectangle[] rects)
        {
            WrappedGraphics.DrawRectangles(pen, rects.Convert<System.Drawing.Rectangle>().ToArray());
        }

        /// <summary>
        ///     Draws the specified text string at the specified location with the specified
        ///     <see cref="T:Common.Drawing.Brush" /> and <see cref="T:Common.Drawing.Font" /> objects.
        /// </summary>
        /// <param name="s">String to draw. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the color and texture of the drawn text.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn text. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn text. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            WrappedGraphics.DrawString(s, font, brush, x, y);
        }

        /// <summary>
        ///     Draws the specified text string at the specified location with the specified
        ///     <see cref="T:Common.Drawing.Brush" /> and <see cref="T:Common.Drawing.Font" /> objects.
        /// </summary>
        /// <param name="s">String to draw. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the color and texture of the drawn text.
        /// </param>
        /// <param name="point">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, PointF point)
        {
            WrappedGraphics.DrawString(s, font, brush, point);
        }

        /// <summary>
        ///     Draws the specified text string at the specified location with the specified
        ///     <see cref="T:Common.Drawing.Brush" /> and <see cref="T:Common.Drawing.Font" /> objects using the formatting
        ///     attributes of the specified <see cref="T:Common.Drawing.StringFormat" />.
        /// </summary>
        /// <param name="s">String to draw. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the color and texture of the drawn text.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn text. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn text. </param>
        /// <param name="format">
        ///     <see cref="T:Common.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and
        ///     alignment, that are applied to the drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
        {
            WrappedGraphics.DrawString(s, font, brush, x, y, format);
        }

        /// <summary>
        ///     Draws the specified text string at the specified location with the specified
        ///     <see cref="T:Common.Drawing.Brush" /> and <see cref="T:Common.Drawing.Font" /> objects using the formatting
        ///     attributes of the specified <see cref="T:Common.Drawing.StringFormat" />.
        /// </summary>
        /// <param name="s">String to draw. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the color and texture of the drawn text.
        /// </param>
        /// <param name="point">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text.
        /// </param>
        /// <param name="format">
        ///     <see cref="T:Common.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and
        ///     alignment, that are applied to the drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            WrappedGraphics.DrawString(s, font, brush, point, format);
        }

        /// <summary>
        ///     Draws the specified text string in the specified rectangle with the specified
        ///     <see cref="T:Common.Drawing.Brush" /> and <see cref="T:Common.Drawing.Font" /> objects.
        /// </summary>
        /// <param name="s">String to draw. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the color and texture of the drawn text.
        /// </param>
        /// <param name="layoutRectangle">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location of the drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            WrappedGraphics.DrawString(s, font, brush, layoutRectangle);
        }

        /// <summary>
        ///     Draws the specified text string in the specified rectangle with the specified
        ///     <see cref="T:Common.Drawing.Brush" /> and <see cref="T:Common.Drawing.Font" /> objects using the formatting
        ///     attributes of the specified <see cref="T:Common.Drawing.StringFormat" />.
        /// </summary>
        /// <param name="s">String to draw. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the color and texture of the drawn text.
        /// </param>
        /// <param name="layoutRectangle">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location of the drawn text.
        /// </param>
        /// <param name="format">
        ///     <see cref="T:Common.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and
        ///     alignment, that are applied to the drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            WrappedGraphics.DrawString(s, font, brush, layoutRectangle, format);
        }

        /// <summary>
        ///     Closes the current graphics container and restores the state of this <see cref="T:Common.Drawing.Graphics" />
        ///     to the state saved by a call to the <see cref="M:Common.Drawing.Graphics.BeginContainer" /> method.
        /// </summary>
        /// <param name="container">
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsContainer" /> that represents the container this method restores.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void EndContainer(GraphicsContainer container)
        {
            WrappedGraphics.EndContainer(container);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display at a specified point.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display at a specified point.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, EnumerateMetafileProc callback,
            IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display at a specified point using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, EnumerateMetafileProc callback,
            IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display at a specified point.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display at a specified point.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, EnumerateMetafileProc callback,
            IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display at a specified point using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, EnumerateMetafileProc callback,
            IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, EnumerateMetafileProc callback,
            IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified rectangle using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, EnumerateMetafileProc callback,
            IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, EnumerateMetafileProc callback,
            IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified rectangle using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, EnumerateMetafileProc callback,
            IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.PointF>().ToArray(),
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, EnumerateMetafileProc callback,
            IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.PointF>().ToArray(),
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified parallelogram using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, EnumerateMetafileProc callback,
            IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.PointF>().ToArray(),
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.Point>().ToArray(),
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, EnumerateMetafileProc callback,
            IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.Point>().ToArray(),
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a time, to a
        ///     callback method for display in a specified parallelogram using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, EnumerateMetafileProc callback,
            IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.Point>().ToArray(),
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display at a specified point.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint, srcRect, (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display at a specified point.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback, IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint, srcRect, (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display at a specified point using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit unit,
            EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint, srcRect, (System.Drawing.GraphicsUnit) unit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display at a specified point.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint, srcRect, (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display at a specified point.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback, IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint, srcRect, (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display at a specified point using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="T:Common.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn
        ///     metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit unit,
            EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoint, srcRect, (System.Drawing.GraphicsUnit) unit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect, srcRect, (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback, IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect, srcRect, (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified rectangle using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit unit,
            EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect, srcRect, (System.Drawing.GraphicsUnit) unit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect, srcRect, (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback, IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect, srcRect, (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified rectangle using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit unit,
            EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destRect, srcRect, (System.Drawing.GraphicsUnit) unit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structures that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback, IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified parallelogram using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit unit,
            EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) unit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.Point>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)));
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit,
            EnumerateMetafileProc callback, IntPtr callbackData)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.Point>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) srcUnit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="T:Common.Drawing.Imaging.Metafile" />, one at a
        ///     time, to a callback method for display in a specified parallelogram using specified image attributes.
        /// </summary>
        /// <param name="metafile">
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> to enumerate.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="T:Common.Drawing.Point" /> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its
        ///     upper-left corner, to draw.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the
        ///     <paramref name="srcRect" /> parameter contains.
        /// </param>
        /// <param name="callback">
        ///     <see cref="T:Common.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the
        ///     metafile records are sent.
        /// </param>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero" /> for this parameter.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn
        ///     image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit unit,
            EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            WrappedGraphics.EnumerateMetafile(metafile, destPoints.Convert<System.Drawing.Point>().ToArray(), srcRect,
                (System.Drawing.GraphicsUnit) unit,
                (a, b, c, d, e) => callback((EmfPlusRecordType) a, b, c, d,
                    (aa, bb, cc, dd) => e((System.Drawing.Imaging.EmfPlusRecordType) aa, bb, cc, dd)), callbackData,
                imageAttr);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="T:Common.Drawing.Graphics" /> to exclude the area specified by a
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the rectangle to exclude from the clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ExcludeClip(Rectangle rect)
        {
            WrappedGraphics.ExcludeClip(rect);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="T:Common.Drawing.Graphics" /> to exclude the area specified by a
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="region">
        ///     <see cref="T:Common.Drawing.Region" /> that specifies the region to exclude from the clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ExcludeClip(Region region)
        {
            WrappedGraphics.ExcludeClip(region);
        }

        /// <summary>
        ///     Fills the interior of a closed cardinal spline curve defined by an array of
        ///     <see cref="T:Common.Drawing.PointF" /> structures.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, PointF[] points)
        {
            WrappedGraphics.FillClosedCurve(brush, points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Fills the interior of a closed cardinal spline curve defined by an array of
        ///     <see cref="T:Common.Drawing.PointF" /> structures using the specified fill mode.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that define the spline. </param>
        /// <param name="fillmode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines how
        ///     the curve is filled.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
        {
            WrappedGraphics.FillClosedCurve(brush, points.Convert<System.Drawing.PointF>().ToArray(),
                (System.Drawing.Drawing2D.FillMode) fillmode);
        }

        /// <summary>
        ///     Fills the interior of a closed cardinal spline curve defined by an array of
        ///     <see cref="T:Common.Drawing.PointF" /> structures using the specified fill mode and tension.
        /// </summary>
        /// <param name="brush">A <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill. </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.PointF" /> structures that define the spline. </param>
        /// <param name="fillmode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines how
        ///     the curve is filled.
        /// </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
        {
            WrappedGraphics.FillClosedCurve(brush, points.Convert<System.Drawing.PointF>().ToArray(),
                (System.Drawing.Drawing2D.FillMode) fillmode, tension);
        }

        /// <summary>
        ///     Fills the interior of a closed cardinal spline curve defined by an array of
        ///     <see cref="T:Common.Drawing.Point" /> structures.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, Point[] points)
        {
            WrappedGraphics.FillClosedCurve(brush, points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Fills the interior of a closed cardinal spline curve defined by an array of
        ///     <see cref="T:Common.Drawing.Point" /> structures using the specified fill mode.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that define the spline. </param>
        /// <param name="fillmode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines how
        ///     the curve is filled.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
        {
            WrappedGraphics.FillClosedCurve(brush, points.Convert<System.Drawing.Point>().ToArray(),
                (System.Drawing.Drawing2D.FillMode) fillmode);
        }

        /// <summary>
        ///     Fills the interior of a closed cardinal spline curve defined by an array of
        ///     <see cref="T:Common.Drawing.Point" /> structures using the specified fill mode and tension.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">Array of <see cref="T:Common.Drawing.Point" /> structures that define the spline. </param>
        /// <param name="fillmode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines how
        ///     the curve is filled.
        /// </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
        {
            WrappedGraphics.FillClosedCurve(brush, points.Convert<System.Drawing.Point>().ToArray(),
                (System.Drawing.Drawing2D.FillMode) fillmode, tension);
        }

        /// <summary>
        ///     Fills the interior of an ellipse defined by a bounding rectangle specified by a
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that represents the bounding rectangle that defines the
        ///     ellipse.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillEllipse(Brush brush, RectangleF rect)
        {
            WrappedGraphics.FillEllipse(brush, rect);
        }

        /// <summary>
        ///     Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width,
        ///     and a height.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            WrappedGraphics.FillEllipse(brush, x, y, width, height);
        }

        /// <summary>
        ///     Fills the interior of an ellipse defined by a bounding rectangle specified by a
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the
        ///     ellipse.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillEllipse(Brush brush, Rectangle rect)
        {
            WrappedGraphics.FillEllipse(brush, rect);
        }

        /// <summary>
        ///     Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width,
        ///     and a height.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        {
            WrappedGraphics.FillEllipse(brush, x, y, width, height);
        }

        /// <summary>Fills the interior of a <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="path">
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> that represents the path to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="path" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillPath(Brush brush, GraphicsPath path)
        {
            WrappedGraphics.FillPath(brush, path);
        }

        /// <summary>
        ///     Fills the interior of a pie section defined by an ellipse specified by a
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure and two radial lines.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse
        ///     from which the pie section comes.
        /// </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the
        ///     second side of the pie section.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
        {
            WrappedGraphics.FillPie(brush, rect, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a
        ///     height, and two radial lines.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="x">
        ///     The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie section comes.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie section comes.
        /// </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the
        ///     second side of the pie section.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle,
            float sweepAngle)
        {
            WrappedGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a
        ///     height, and two radial lines.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="x">
        ///     The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie section comes.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie section comes.
        /// </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the
        ///     second side of the pie section.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            WrappedGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Fills the interior of a polygon defined by an array of points specified by
        ///     <see cref="T:Common.Drawing.PointF" /> structures.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.PointF" /> structures that represent the vertices of the
        ///     polygon to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillPolygon(Brush brush, PointF[] points)
        {
            WrappedGraphics.FillPolygon(brush, points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Fills the interior of a polygon defined by an array of points specified by
        ///     <see cref="T:Common.Drawing.PointF" /> structures using the specified fill mode.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.PointF" /> structures that represent the vertices of the
        ///     polygon to fill.
        /// </param>
        /// <param name="fillMode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines the
        ///     style of the fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
        {
            WrappedGraphics.FillPolygon(brush, points.Convert<System.Drawing.PointF>().ToArray(),
                (System.Drawing.Drawing2D.FillMode) fillMode);
        }

        /// <summary>
        ///     Fills the interior of a polygon defined by an array of points specified by
        ///     <see cref="T:Common.Drawing.Point" /> structures.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.Point" /> structures that represent the vertices of the
        ///     polygon to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillPolygon(Brush brush, Point[] points)
        {
            WrappedGraphics.FillPolygon(brush, points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Fills the interior of a polygon defined by an array of points specified by
        ///     <see cref="T:Common.Drawing.Point" /> structures using the specified fill mode.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="points">
        ///     Array of <see cref="T:Common.Drawing.Point" /> structures that represent the vertices of the
        ///     polygon to fill.
        /// </param>
        /// <param name="fillMode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines the
        ///     style of the fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
        {
            WrappedGraphics.FillPolygon(brush, points.Convert<System.Drawing.Point>().ToArray(),
                (System.Drawing.Drawing2D.FillMode) fillMode);
        }

        /// <summary>Fills the interior of a rectangle specified by a <see cref="T:Common.Drawing.RectangleF" /> structure.</summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that represents the rectangle to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillRectangle(Brush brush, RectangleF rect)
        {
            WrappedGraphics.FillRectangle(brush, rect);
        }

        /// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill. </param>
        /// <param name="width">Width of the rectangle to fill. </param>
        /// <param name="height">Height of the rectangle to fill. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            WrappedGraphics.FillRectangle(brush, x, y, width, height);
        }

        /// <summary>Fills the interior of a rectangle specified by a <see cref="T:Common.Drawing.Rectangle" /> structure.</summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that represents the rectangle to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillRectangle(Brush brush, Rectangle rect)
        {
            WrappedGraphics.FillRectangle(brush, rect);
        }

        /// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill. </param>
        /// <param name="width">Width of the rectangle to fill. </param>
        /// <param name="height">Height of the rectangle to fill. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            WrappedGraphics.FillRectangle(brush, x, y, width, height);
        }

        /// <summary>
        ///     Fills the interiors of a series of rectangles specified by <see cref="T:Common.Drawing.RectangleF" />
        ///     structures.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="rects">
        ///     Array of <see cref="T:Common.Drawing.RectangleF" /> structures that represent the rectangles to
        ///     fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="rects" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="Rects" /> is a zero-length array.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillRectangles(Brush brush, RectangleF[] rects)
        {
            WrappedGraphics.FillRectangles(brush, rects.Convert<System.Drawing.RectangleF>().ToArray());
        }

        /// <summary>
        ///     Fills the interiors of a series of rectangles specified by <see cref="T:Common.Drawing.Rectangle" />
        ///     structures.
        /// </summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="rects">
        ///     Array of <see cref="T:Common.Drawing.Rectangle" /> structures that represent the rectangles to
        ///     fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="rects" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="rects" /> is a zero-length array.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void FillRectangles(Brush brush, Rectangle[] rects)
        {
            WrappedGraphics.FillRectangles(brush, rects.Convert<System.Drawing.Rectangle>().ToArray());
        }

        /// <summary>Fills the interior of a <see cref="T:Common.Drawing.Region" />.</summary>
        /// <param name="brush">
        ///     <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of the fill.
        /// </param>
        /// <param name="region">
        ///     <see cref="T:Common.Drawing.Region" /> that represents the area to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.-or-<paramref name="region" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillRegion(Brush brush, Region region)
        {
            WrappedGraphics.FillRegion(brush, region);
        }

        /// <summary>
        ///     Forces execution of all pending graphics operations and returns immediately without waiting for the operations
        ///     to finish.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Flush()
        {
            WrappedGraphics.Flush();
        }

        /// <summary>
        ///     Forces execution of all pending graphics operations with the method waiting or not waiting, as specified, to
        ///     return before the operations finish.
        /// </summary>
        /// <param name="intention">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.FlushIntention" /> enumeration that
        ///     specifies whether the method returns immediately or waits for any existing operations to finish.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Flush(FlushIntention intention)
        {
            WrappedGraphics.Flush((System.Drawing.Drawing2D.FlushIntention) intention);
        }

        /// <summary>Creates a new <see cref="T:Common.Drawing.Graphics" /> from the specified handle to a device context.</summary>
        /// <returns>This method returns a new <see cref="T:Common.Drawing.Graphics" /> for the specified device context.</returns>
        /// <param name="hdc">Handle to a device context. </param>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static Graphics FromHdc(IntPtr hdc)
        {
            return new Graphics(System.Drawing.Graphics.FromHdc(hdc));
        }

        /// <summary>
        ///     Creates a new <see cref="T:Common.Drawing.Graphics" /> from the specified handle to a device context and
        ///     handle to a device.
        /// </summary>
        /// <returns>
        ///     This method returns a new <see cref="T:Common.Drawing.Graphics" /> for the specified device context and
        ///     device.
        /// </returns>
        /// <param name="hdc">Handle to a device context. </param>
        /// <param name="hdevice">Handle to a device. </param>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static Graphics FromHdc(IntPtr hdc, IntPtr hdevice)
        {
            return new Graphics(System.Drawing.Graphics.FromHdc(hdc, hdevice));
        }

        /// <summary>Returns a <see cref="T:Common.Drawing.Graphics" /> for the specified device context.</summary>
        /// <returns>A <see cref="T:Common.Drawing.Graphics" /> for the specified device context.</returns>
        /// <param name="hdc">Handle to a device context. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static Graphics FromHdcInternal(IntPtr hdc)
        {
            return new Graphics(System.Drawing.Graphics.FromHdcInternal(hdc));
        }

        /// <summary>Creates a new <see cref="T:Common.Drawing.Graphics" /> from the specified handle to a window.</summary>
        /// <returns>This method returns a new <see cref="T:Common.Drawing.Graphics" /> for the specified window handle.</returns>
        /// <param name="hwnd">Handle to a window. </param>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static Graphics FromHwnd(IntPtr hwnd)
        {
            return new Graphics(System.Drawing.Graphics.FromHwnd(hwnd));
        }

        /// <summary>Creates a new <see cref="T:Common.Drawing.Graphics" /> for the specified windows handle.</summary>
        /// <returns>A <see cref="T:Common.Drawing.Graphics" /> for the specified window handle.</returns>
        /// <param name="hwnd">Handle to a window. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static Graphics FromHwndInternal(IntPtr hwnd)
        {
            return new Graphics(System.Drawing.Graphics.FromHwndInternal(hwnd));
        }

        /// <summary>
        ///     Creates a new <see cref="T:Common.Drawing.Graphics" /> from the specified
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </summary>
        /// <returns>
        ///     This method returns a new <see cref="T:Common.Drawing.Graphics" /> for the specified
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </returns>
        /// <param name="image">
        ///     <see cref="T:Common.Drawing.Image" /> from which to create the new <see cref="T:Common.Drawing.Graphics" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <exception cref="T:System.Exception">
        ///     <paramref name="image" /> has an indexed pixel format or its format is undefined.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static Graphics FromImage(Image image)
        {
            return new Graphics(System.Drawing.Graphics.FromImage(image));
        }

        /// <summary>Gets the cumulative graphics context.</summary>
        /// <returns>An <see cref="T:System.Object" /> representing the cumulative graphics context.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [StrongNameIdentityPermission(SecurityAction.LinkDemand, Name = "System.Windows.Forms",
            PublicKey = "0x00000000000000000400000000000000")]
        public object GetContextInfo()
        {
            return WrappedGraphics.GetContextInfo();
        }

        /// <summary>Gets a handle to the current Windows halftone palette.</summary>
        /// <returns>Internal pointer that specifies the handle to the palette.</returns>
        /// <filterpriority>1</filterpriority>
        public static IntPtr GetHalftonePalette()
        {
            return System.Drawing.Graphics.GetHalftonePalette();
        }

        /// <summary>Gets the nearest color to the specified <see cref="T:Common.Drawing.Color" /> structure.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the nearest color to the one specified with
        ///     the <paramref name="color" /> parameter.
        /// </returns>
        /// <param name="color">
        ///     <see cref="T:Common.Drawing.Color" /> structure for which to find a match.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public Color GetNearestColor(Color color)
        {
            return WrappedGraphics.GetNearestColor(color);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="T:Common.Drawing.Graphics" /> to the intersection of the current
        ///     clip region and the specified <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure to intersect with the current clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void IntersectClip(Rectangle rect)
        {
            WrappedGraphics.IntersectClip(rect);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="T:Common.Drawing.Graphics" /> to the intersection of the current
        ///     clip region and the specified <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure to intersect with the current clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void IntersectClip(RectangleF rect)
        {
            WrappedGraphics.IntersectClip(rect);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="T:Common.Drawing.Graphics" /> to the intersection of the current
        ///     clip region and the specified <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="region">
        ///     <see cref="T:Common.Drawing.Region" /> to intersect with the current region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void IntersectClip(Region region)
        {
            WrappedGraphics.IntersectClip(region);
        }

        /// <summary>
        ///     Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of
        ///     this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true if the point defined by the <paramref name="x" /> and <paramref name="y" /> parameters is contained
        ///     within the visible clip region of this <see cref="T:Common.Drawing.Graphics" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test for visibility. </param>
        /// <param name="y">The y-coordinate of the point to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(int x, int y)
        {
            return WrappedGraphics.IsVisible(x, y);
        }

        /// <summary>
        ///     Indicates whether the specified <see cref="T:Common.Drawing.Point" /> structure is contained within the
        ///     visible clip region of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true if the point specified by the <paramref name="point" /> parameter is contained within the visible clip
        ///     region of this <see cref="T:Common.Drawing.Graphics" />; otherwise, false.
        /// </returns>
        /// <param name="point">
        ///     <see cref="T:Common.Drawing.Point" /> structure to test for visibility.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Point point)
        {
            return WrappedGraphics.IsVisible(point);
        }

        /// <summary>
        ///     Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of
        ///     this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true if the point defined by the <paramref name="x" /> and <paramref name="y" /> parameters is contained
        ///     within the visible clip region of this <see cref="T:Common.Drawing.Graphics" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test for visibility. </param>
        /// <param name="y">The y-coordinate of the point to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(float x, float y)
        {
            return IsVisible(new PointF(x, y));
        }

        /// <summary>
        ///     Indicates whether the specified <see cref="T:Common.Drawing.PointF" /> structure is contained within the
        ///     visible clip region of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true if the point specified by the <paramref name="point" /> parameter is contained within the visible clip
        ///     region of this <see cref="T:Common.Drawing.Graphics" />; otherwise, false.
        /// </returns>
        /// <param name="point">
        ///     <see cref="T:Common.Drawing.PointF" /> structure to test for visibility.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(PointF point)
        {
            return WrappedGraphics.IsVisible(point);
        }

        /// <summary>
        ///     Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within
        ///     the visible clip region of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true if the rectangle defined by the <paramref name="x" />, <paramref name="y" />, <paramref name="width" />,
        ///     and <paramref name="height" /> parameters is contained within the visible clip region of this
        ///     <see cref="T:Common.Drawing.Graphics" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility. </param>
        /// <param name="width">Width of the rectangle to test for visibility. </param>
        /// <param name="height">Height of the rectangle to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(int x, int y, int width, int height)
        {
            return WrappedGraphics.IsVisible(x, y, width, height);
        }

        /// <summary>
        ///     Indicates whether the rectangle specified by a <see cref="T:Common.Drawing.Rectangle" /> structure is
        ///     contained within the visible clip region of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true if the rectangle specified by the <paramref name="rect" /> parameter is contained within the visible clip
        ///     region of this <see cref="T:Common.Drawing.Graphics" />; otherwise, false.
        /// </returns>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure to test for visibility.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Rectangle rect)
        {
            return WrappedGraphics.IsVisible(rect);
        }

        /// <summary>
        ///     Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within
        ///     the visible clip region of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true if the rectangle defined by the <paramref name="x" />, <paramref name="y" />, <paramref name="width" />,
        ///     and <paramref name="height" /> parameters is contained within the visible clip region of this
        ///     <see cref="T:Common.Drawing.Graphics" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility. </param>
        /// <param name="width">Width of the rectangle to test for visibility. </param>
        /// <param name="height">Height of the rectangle to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(float x, float y, float width, float height)
        {
            return WrappedGraphics.IsVisible(x, y, width, height);
        }

        /// <summary>
        ///     Indicates whether the rectangle specified by a <see cref="T:Common.Drawing.RectangleF" /> structure is
        ///     contained within the visible clip region of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true if the rectangle specified by the <paramref name="rect" /> parameter is contained within the visible clip
        ///     region of this <see cref="T:Common.Drawing.Graphics" />; otherwise, false.
        /// </returns>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure to test for visibility.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(RectangleF rect)
        {
            return WrappedGraphics.IsVisible(rect);
        }

        /// <summary>
        ///     Gets an array of <see cref="T:Common.Drawing.Region" /> objects, each of which bounds a range of character
        ///     positions within the specified string.
        /// </summary>
        /// <returns>
        ///     This method returns an array of <see cref="T:Common.Drawing.Region" /> objects, each of which bounds a range
        ///     of character positions within the specified string.
        /// </returns>
        /// <param name="text">String to measure. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="layoutRect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the layout rectangle for the string.
        /// </param>
        /// <param name="stringFormat">
        ///     <see cref="T:Common.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the
        ///     string.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
        {
            return WrappedGraphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat)
                .Convert<Region>()
                .ToArray();
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="T:Common.Drawing.Font" /> and formatted
        ///     with the specified <see cref="T:Common.Drawing.StringFormat" />.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.SizeF" /> structure that represents the size of the string,
        ///     in the units specified by the <see cref="P:Common.Drawing.Graphics.PageUnit" /> property, of the
        ///     <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the
        ///     <paramref name="stringFormat" /> parameter.
        /// </returns>
        /// <param name="text">String to measure. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="layoutArea">
        ///     <see cref="T:Common.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.
        /// </param>
        /// <param name="stringFormat">
        ///     <see cref="T:Common.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the
        ///     string.
        /// </param>
        /// <param name="charactersFitted">Number of characters in the string. </param>
        /// <param name="linesFilled">Number of text lines in the string. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="font" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat,
            out int charactersFitted, out int linesFilled)
        {
            return WrappedGraphics.MeasureString(text, font, layoutArea, stringFormat, out charactersFitted,
                out linesFilled);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="T:Common.Drawing.Font" /> and formatted
        ///     with the specified <see cref="T:Common.Drawing.StringFormat" />.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.SizeF" /> structure that represents the size, in the units
        ///     specified by the <see cref="P:Common.Drawing.Graphics.PageUnit" /> property, of the string specified by the
        ///     <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the
        ///     <paramref name="stringFormat" /> parameter.
        /// </returns>
        /// <param name="text">String to measure. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> defines the text format of the string.
        /// </param>
        /// <param name="origin">
        ///     <see cref="T:Common.Drawing.PointF" /> structure that represents the upper-left corner of the string.
        /// </param>
        /// <param name="stringFormat">
        ///     <see cref="T:Common.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the
        ///     string.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="font" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
        {
            return WrappedGraphics.MeasureString(text, font, origin, stringFormat);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="T:Common.Drawing.Font" /> within the
        ///     specified layout area.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.SizeF" /> structure that represents the size, in the units
        ///     specified by the <see cref="P:Common.Drawing.Graphics.PageUnit" /> property, of the string specified by the
        ///     <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.
        /// </returns>
        /// <param name="text">String to measure. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> defines the text format of the string.
        /// </param>
        /// <param name="layoutArea">
        ///     <see cref="T:Common.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="font" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, SizeF layoutArea)
        {
            return WrappedGraphics.MeasureString(text, font, layoutArea);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="T:Common.Drawing.Font" /> and formatted
        ///     with the specified <see cref="T:Common.Drawing.StringFormat" />.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.SizeF" /> structure that represents the size, in the units
        ///     specified by the <see cref="P:Common.Drawing.Graphics.PageUnit" /> property, of the string specified in the
        ///     <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the
        ///     <paramref name="stringFormat" /> parameter.
        /// </returns>
        /// <param name="text">String to measure. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> defines the text format of the string.
        /// </param>
        /// <param name="layoutArea">
        ///     <see cref="T:Common.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.
        /// </param>
        /// <param name="stringFormat">
        ///     <see cref="T:Common.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the
        ///     string.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="font" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
        {
            return WrappedGraphics.MeasureString(text, font, layoutArea, stringFormat);
        }

        /// <summary>Measures the specified string when drawn with the specified <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.SizeF" /> structure that represents the size, in the units
        ///     specified by the <see cref="P:Common.Drawing.Graphics.PageUnit" /> property, of the string specified by the
        ///     <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.
        /// </returns>
        /// <param name="text">String to measure. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="font" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font)
        {
            return WrappedGraphics.MeasureString(text, font);
        }

        /// <summary>Measures the specified string when drawn with the specified <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.SizeF" /> structure that represents the size, in the units
        ///     specified by the <see cref="P:Common.Drawing.Graphics.PageUnit" /> property, of the string specified in the
        ///     <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.
        /// </returns>
        /// <param name="text">String to measure. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the format of the string.
        /// </param>
        /// <param name="width">Maximum width of the string in pixels. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="font" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, int width)
        {
            return WrappedGraphics.MeasureString(text, font, width);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="T:Common.Drawing.Font" /> and formatted
        ///     with the specified <see cref="T:Common.Drawing.StringFormat" />.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.SizeF" /> structure that represents the size, in the units
        ///     specified by the <see cref="P:Common.Drawing.Graphics.PageUnit" /> property, of the string specified in the
        ///     <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the
        ///     <paramref name="stringFormat" /> parameter.
        /// </returns>
        /// <param name="text">String to measure. </param>
        /// <param name="font">
        ///     <see cref="T:Common.Drawing.Font" /> that defines the text format of the string.
        /// </param>
        /// <param name="width">Maximum width of the string. </param>
        /// <param name="format">
        ///     <see cref="T:Common.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the
        ///     string.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="font" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, int width, StringFormat format)
        {
            return WrappedGraphics.MeasureString(text, font, width, format);
        }

        /// <summary>
        ///     Multiplies the world transformation of this <see cref="T:Common.Drawing.Graphics" /> and specified the
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <param name="matrix">4x4 <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that multiplies the world transformation. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void MultiplyTransform(Matrix matrix)
        {
            WrappedGraphics.MultiplyTransform(matrix);
        }

        /// <summary>
        ///     Multiplies the world transformation of this <see cref="T:Common.Drawing.Graphics" /> and specified the
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the specified order.
        /// </summary>
        /// <param name="matrix">4x4 <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that multiplies the world transformation. </param>
        /// <param name="order">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that determines the
        ///     order of the multiplication.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            WrappedGraphics.MultiplyTransform(matrix, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Releases a device context handle obtained by a previous call to the
        ///     <see cref="M:Common.Drawing.Graphics.GetHdc" /> method of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="hdc">
        ///     Handle to a device context obtained by a previous call to the
        ///     <see cref="M:Common.Drawing.Graphics.GetHdc" /> method of this <see cref="T:Common.Drawing.Graphics" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void ReleaseHdc(IntPtr hdc)
        {
            WrappedGraphics.ReleaseHdc(hdc);
        }

        /// <summary>Releases a handle to a device context.</summary>
        /// <param name="hdc">Handle to a device context. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public void ReleaseHdcInternal(IntPtr hdc)
        {
            WrappedGraphics.ReleaseHdcInternal(hdc);
        }

        /// <summary>Resets the clip region of this <see cref="T:Common.Drawing.Graphics" /> to an infinite region.</summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ResetClip()
        {
            WrappedGraphics.ResetClip();
        }

        /// <summary>
        ///     Resets the world transformation matrix of this <see cref="T:Common.Drawing.Graphics" /> to the identity
        ///     matrix.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ResetTransform()
        {
            WrappedGraphics.ResetTransform();
        }

        /// <summary>
        ///     Restores the state of this <see cref="T:Common.Drawing.Graphics" /> to the state represented by a
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsState" />.
        /// </summary>
        /// <param name="gstate">
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsState" /> that represents the state to which to restore this
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Restore(GraphicsState gstate)
        {
            WrappedGraphics.Restore(gstate);
        }

        /// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <param name="angle">Angle of rotation in degrees. </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle)
        {
            WrappedGraphics.RotateTransform(angle);
        }

        /// <summary>
        ///     Applies the specified rotation to the transformation matrix of this <see cref="T:Common.Drawing.Graphics" />
        ///     in the specified order.
        /// </summary>
        /// <param name="angle">Angle of rotation in degrees. </param>
        /// <param name="order">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies
        ///     whether the rotation is appended or prepended to the matrix transformation.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle, MatrixOrder order)
        {
            WrappedGraphics.RotateTransform(angle, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Saves the current state of this <see cref="T:Common.Drawing.Graphics" /> and identifies the saved state with a
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsState" />.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="T:Common.Drawing.Drawing2D.GraphicsState" /> that represents the saved state
        ///     of this <see cref="T:Common.Drawing.Graphics" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public GraphicsState Save()
        {
            return WrappedGraphics.Save();
        }

        /// <summary>
        ///     Applies the specified scaling operation to the transformation matrix of this
        ///     <see cref="T:Common.Drawing.Graphics" /> by prepending it to the object's transformation matrix.
        /// </summary>
        /// <param name="sx">Scale factor in the x direction. </param>
        /// <param name="sy">Scale factor in the y direction. </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy)
        {
            WrappedGraphics.ScaleTransform(sx, sy);
        }

        /// <summary>
        ///     Applies the specified scaling operation to the transformation matrix of this
        ///     <see cref="T:Common.Drawing.Graphics" /> in the specified order.
        /// </summary>
        /// <param name="sx">Scale factor in the x direction. </param>
        /// <param name="sy">Scale factor in the y direction. </param>
        /// <param name="order">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies
        ///     whether the scaling operation is prepended or appended to the transformation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            WrappedGraphics.ScaleTransform(sx, sy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the Clip property of the
        ///     specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="g">
        ///     <see cref="T:Common.Drawing.Graphics" /> from which to take the new clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Graphics g)
        {
            WrappedGraphics.SetClip(g);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the result of the specified
        ///     combining operation of the current clip region and the <see cref="P:Common.Drawing.Graphics.Clip" /> property of
        ///     the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="g">
        ///     <see cref="T:Common.Drawing.Graphics" /> that specifies the clip region to combine.
        /// </param>
        /// <param name="combineMode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.CombineMode" /> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Graphics g, CombineMode combineMode)
        {
            WrappedGraphics.SetClip(g, (System.Drawing.Drawing2D.CombineMode) combineMode);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the rectangle specified by a
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that represents the new clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Rectangle rect)
        {
            WrappedGraphics.SetClip(rect);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the result of the specified
        ///     operation combining the current clip region and the rectangle specified by a
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure to combine.
        /// </param>
        /// <param name="combineMode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.CombineMode" /> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Rectangle rect, CombineMode combineMode)
        {
            WrappedGraphics.SetClip(rect, (System.Drawing.Drawing2D.CombineMode) combineMode);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the rectangle specified by a
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that represents the new clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(RectangleF rect)
        {
            WrappedGraphics.SetClip(rect);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the result of the specified
        ///     operation combining the current clip region and the rectangle specified by a
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure to combine.
        /// </param>
        /// <param name="combineMode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.CombineMode" /> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(RectangleF rect, CombineMode combineMode)
        {
            WrappedGraphics.SetClip(rect, (System.Drawing.Drawing2D.CombineMode) combineMode);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="path">
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> that represents the new clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(GraphicsPath path)
        {
            WrappedGraphics.SetClip(path);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the result of the specified
        ///     operation combining the current clip region and the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="path">
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to combine.
        /// </param>
        /// <param name="combineMode">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.CombineMode" /> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void SetClip(GraphicsPath path, CombineMode combineMode)
        {
            WrappedGraphics.SetClip(path, (System.Drawing.Drawing2D.CombineMode) combineMode);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="T:Common.Drawing.Graphics" /> to the result of the specified
        ///     operation combining the current clip region and the specified <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="region">
        ///     <see cref="T:Common.Drawing.Region" /> to combine.
        /// </param>
        /// <param name="combineMode">
        ///     Member from the <see cref="T:Common.Drawing.Drawing2D.CombineMode" /> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Region region, CombineMode combineMode)
        {
            WrappedGraphics.SetClip(region, (System.Drawing.Drawing2D.CombineMode) combineMode);
        }

        /// <summary>
        ///     Transforms an array of points from one coordinate space to another using the current world and page
        ///     transformations of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="destSpace">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.CoordinateSpace" /> enumeration that
        ///     specifies the destination coordinate space.
        /// </param>
        /// <param name="srcSpace">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.CoordinateSpace" /> enumeration that
        ///     specifies the source coordinate space.
        /// </param>
        /// <param name="pts">Array of <see cref="T:Common.Drawing.PointF" /> structures that represent the points to transform. </param>
        /// <filterpriority>1</filterpriority>
        public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
        {
            WrappedGraphics.TransformPoints((System.Drawing.Drawing2D.CoordinateSpace) destSpace,
                (System.Drawing.Drawing2D.CoordinateSpace) srcSpace, pts.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Transforms an array of points from one coordinate space to another using the current world and page
        ///     transformations of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="destSpace">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.CoordinateSpace" /> enumeration that
        ///     specifies the destination coordinate space.
        /// </param>
        /// <param name="srcSpace">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.CoordinateSpace" /> enumeration that
        ///     specifies the source coordinate space.
        /// </param>
        /// <param name="pts">
        ///     Array of <see cref="T:Common.Drawing.Point" /> structures that represents the points to
        ///     transformation.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
        {
            WrappedGraphics.TransformPoints((System.Drawing.Drawing2D.CoordinateSpace) destSpace,
                (System.Drawing.Drawing2D.CoordinateSpace) srcSpace, pts.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Translates the clipping region of this <see cref="T:Common.Drawing.Graphics" /> by specified amounts in the
        ///     horizontal and vertical directions.
        /// </summary>
        /// <param name="dx">The x-coordinate of the translation. </param>
        /// <param name="dy">The y-coordinate of the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateClip(float dx, float dy)
        {
            WrappedGraphics.TranslateClip(dx, dy);
        }

        /// <summary>
        ///     Translates the clipping region of this <see cref="T:Common.Drawing.Graphics" /> by specified amounts in the
        ///     horizontal and vertical directions.
        /// </summary>
        /// <param name="dx">The x-coordinate of the translation. </param>
        /// <param name="dy">The y-coordinate of the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateClip(int dx, int dy)
        {
            WrappedGraphics.TranslateClip(dx, dy);
        }

        /// <summary>
        ///     Changes the origin of the coordinate system by prepending the specified translation to the transformation
        ///     matrix of this <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <param name="dx">The x-coordinate of the translation. </param>
        /// <param name="dy">The y-coordinate of the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy)
        {
            WrappedGraphics.TranslateTransform(dx, dy);
        }

        /// <summary>
        ///     Changes the origin of the coordinate system by applying the specified translation to the transformation matrix
        ///     of this <see cref="T:Common.Drawing.Graphics" /> in the specified order.
        /// </summary>
        /// <param name="dx">The x-coordinate of the translation. </param>
        /// <param name="dy">The y-coordinate of the translation. </param>
        /// <param name="order">
        ///     Member of the <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies
        ///     whether the translation is prepended or appended to the transformation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            WrappedGraphics.TranslateTransform(dx, dy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedGraphics.Dispose();
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Graphics" /> to a <see cref="T:Common.Drawing.Graphics" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Graphics" /> that results from the conversion.</returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Graphics(System.Drawing.Graphics graphics)
        {
            return graphics == null ? null : new Graphics(graphics);
        }

        /// <summary>Converts the specified <see cref="T:Common.Drawing.Graphics" /> to a <see cref="T:System.Drawing.Graphics" />.</summary>
        /// <returns>The <see cref="T:System.Drawing.Graphics" /> that results from the conversion.</returns>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Graphics(Graphics graphics)
        {
            return graphics?.WrappedGraphics;
        }
    }
}