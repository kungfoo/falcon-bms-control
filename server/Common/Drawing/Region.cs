using System;
using System.Linq;
using Common.Drawing.Drawing2D;

namespace Common.Drawing
{
    /// <summary>Describes the interior of a graphics shape composed of rectangles and paths. This class cannot be inherited.</summary>
    /// <filterpriority>1</filterpriority>
    public sealed class Region : MarshalByRefObject, IDisposable
    {
        /// <summary>Initializes a new <see cref="T:Common.Drawing.Region" />.</summary>
        public Region()
        {
            WrappedRegion = new System.Drawing.Region();
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.Region" /> from the specified
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that defines the interior of the new
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        public Region(RectangleF rect)
        {
            WrappedRegion = new System.Drawing.Region(rect);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.Region" /> from the specified
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that defines the interior of the new
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        public Region(Rectangle rect)
        {
            WrappedRegion = new System.Drawing.Region(rect);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.Region" /> with the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="path">
        ///     A <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> that defines the new
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> isnull.
        /// </exception>
        public Region(GraphicsPath path)
        {
            WrappedRegion = new System.Drawing.Region(path);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Region" /> from the specified data.</summary>
        /// <param name="rgnData">
        ///     A <see cref="T:Common.Drawing.Drawing2D.RegionData" /> that defines the interior of the new
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="rgnData" /> isnull.
        /// </exception>
        public Region(RegionData rgnData)
        {
            WrappedRegion = new System.Drawing.Region(rgnData);
        }

        private Region(System.Drawing.Region region)
        {
            WrappedRegion = region;
        }

        private System.Drawing.Region WrappedRegion { get; }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Region" />.</summary>
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

        ~Region()
        {
            Dispose(false);
        }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Region" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Region" /> that this method creates.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Region Clone()
        {
            return new Region(WrappedRegion.Clone());
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to contain the portion of the specified
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure that does not intersect with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.RectangleF" /> structure to complement this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Complement(RectangleF rect)
        {
            WrappedRegion.Complement(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to contain the portion of the specified
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure that does not intersect with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure to complement this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Complement(Rectangle rect)
        {
            WrappedRegion.Complement(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to contain the portion of the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> that does not intersect with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="path">
        ///     The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to complement this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> isnull.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Complement(GraphicsPath path)
        {
            WrappedRegion.Complement(path);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to contain the portion of the specified
        ///     <see cref="T:Common.Drawing.Region" /> that does not intersect with this <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="region">
        ///     The <see cref="T:Common.Drawing.Region" /> object to complement this
        ///     <see cref="T:Common.Drawing.Region" /> object.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="region" /> isnull.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Complement(Region region)
        {
            WrappedRegion.Complement(region);
        }

        /// <summary>
        ///     Tests whether the specified <see cref="T:Common.Drawing.Region" /> is identical to this
        ///     <see cref="T:Common.Drawing.Region" /> on the specified drawing surface.
        /// </summary>
        /// <returns>
        ///     true if the interior of region is identical to the interior of this region when the transformation associated
        ///     with the <paramref name="g" /> parameter is applied; otherwise, false.
        /// </returns>
        /// <param name="region">The <see cref="T:Common.Drawing.Region" /> to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a drawing surface. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="g" /> or <paramref name="region" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool Equals(Region region, Graphics g)
        {
            return WrappedRegion.Equals(region, g);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to contain only the portion of its interior that does not
        ///     intersect with the specified <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.RectangleF" /> structure to exclude from this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Exclude(RectangleF rect)
        {
            WrappedRegion.Exclude(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to contain only the portion of its interior that does not
        ///     intersect with the specified <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure to exclude from this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Exclude(Rectangle rect)
        {
            WrappedRegion.Exclude(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to contain only the portion of its interior that does not
        ///     intersect with the specified <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="path">
        ///     The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to exclude from this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Exclude(GraphicsPath path)
        {
            WrappedRegion.Exclude(path);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to contain only the portion of its interior that does not
        ///     intersect with the specified <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="region">
        ///     The <see cref="T:Common.Drawing.Region" /> to exclude from this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="region" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Exclude(Region region)
        {
            WrappedRegion.Exclude(region);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Region" /> from a handle to the specified existing GDI region.</summary>
        /// <returns>The new <see cref="T:Common.Drawing.Region" />.</returns>
        /// <param name="hrgn">A handle to an existing <see cref="T:Common.Drawing.Region" />. </param>
        /// <filterpriority>1</filterpriority>
        public static Region FromHrgn(IntPtr hrgn)
        {
            return System.Drawing.Region.FromHrgn(hrgn);
        }

        /// <summary>
        ///     Gets a <see cref="T:Common.Drawing.RectangleF" /> structure that represents a rectangle that bounds this
        ///     <see cref="T:Common.Drawing.Region" /> on the drawing surface of a <see cref="T:Common.Drawing.Graphics" /> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that represents the bounding rectangle for this
        ///     <see cref="T:Common.Drawing.Region" /> on the specified drawing surface.
        /// </returns>
        /// <param name="g">
        ///     The <see cref="T:Common.Drawing.Graphics" /> on which this <see cref="T:Common.Drawing.Region" /> is
        ///     drawn.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="g" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RectangleF GetBounds(Graphics g)
        {
            return WrappedRegion.GetBounds(g);
        }

        /// <summary>Returns a Windows handle to this <see cref="T:Common.Drawing.Region" /> in the specified graphics context.</summary>
        /// <returns>A Windows handle to this <see cref="T:Common.Drawing.Region" />.</returns>
        /// <param name="g">
        ///     The <see cref="T:Common.Drawing.Graphics" /> on which this <see cref="T:Common.Drawing.Region" /> is
        ///     drawn.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="g" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public IntPtr GetHrgn(Graphics g)
        {
            return WrappedRegion.GetHrgn(g);
        }

        /// <summary>
        ///     Returns a <see cref="T:Common.Drawing.Drawing2D.RegionData" /> that represents the information that describes
        ///     this <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.RegionData" /> that represents the information that describes this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RegionData GetRegionData()
        {
            return WrappedRegion.GetRegionData();
        }

        /// <summary>
        ///     Returns an array of <see cref="T:Common.Drawing.RectangleF" /> structures that approximate this
        ///     <see cref="T:Common.Drawing.Region" /> after the specified matrix transformation is applied.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Common.Drawing.RectangleF" /> structures that approximate this
        ///     <see cref="T:Common.Drawing.Region" /> after the specified matrix transformation is applied.
        /// </returns>
        /// <param name="matrix">
        ///     A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that represents a geometric transformation to
        ///     apply to the region.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="matrix" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RectangleF[] GetRegionScans(Matrix matrix)
        {
            return WrappedRegion.GetRegionScans(matrix).Convert<RectangleF>().ToArray();
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the intersection of itself with the specified
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.RectangleF" /> structure to intersect with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Intersect(RectangleF rect)
        {
            WrappedRegion.Intersect(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the intersection of itself with the specified
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure to intersect with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Intersect(Rectangle rect)
        {
            WrappedRegion.Intersect(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the intersection of itself with the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="path">
        ///     The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to intersect with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Intersect(GraphicsPath path)
        {
            WrappedRegion.Intersect(path);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the intersection of itself with the specified
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="region">
        ///     The <see cref="T:Common.Drawing.Region" /> to intersect with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Intersect(Region region)
        {
            WrappedRegion.Intersect(region);
        }

        /// <summary>
        ///     Tests whether this <see cref="T:Common.Drawing.Region" /> has an empty interior on the specified drawing
        ///     surface.
        /// </summary>
        /// <returns>
        ///     true if the interior of this <see cref="T:Common.Drawing.Region" /> is empty when the transformation
        ///     associated with <paramref name="g" /> is applied; otherwise, false.
        /// </returns>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a drawing surface. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="g" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsEmpty(Graphics g)
        {
            return WrappedRegion.IsEmpty(g);
        }

        /// <summary>
        ///     Tests whether this <see cref="T:Common.Drawing.Region" /> has an infinite interior on the specified drawing
        ///     surface.
        /// </summary>
        /// <returns>
        ///     true if the interior of this <see cref="T:Common.Drawing.Region" /> is infinite when the transformation
        ///     associated with <paramref name="g" /> is applied; otherwise, false.
        /// </returns>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a drawing surface. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="g" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsInfinite(Graphics g)
        {
            return WrappedRegion.IsInfinite(g);
        }

        /// <summary>Tests whether the specified point is contained within this <see cref="T:Common.Drawing.Region" />.</summary>
        /// <returns>
        ///     true when the specified point is contained within this <see cref="T:Common.Drawing.Region" />; otherwise,
        ///     false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(float x, float y)
        {
            return WrappedRegion.IsVisible(x, y);
        }

        /// <summary>
        ///     Tests whether the specified <see cref="T:Common.Drawing.PointF" /> structure is contained within this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <returns>
        ///     true when <paramref name="point" /> is contained within this <see cref="T:Common.Drawing.Region" />;
        ///     otherwise, false.
        /// </returns>
        /// <param name="point">The <see cref="T:Common.Drawing.PointF" /> structure to test. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(PointF point)
        {
            return WrappedRegion.IsVisible(point);
        }

        /// <summary>
        ///     Tests whether the specified point is contained within this <see cref="T:Common.Drawing.Region" /> when drawn
        ///     using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true when the specified point is contained within this <see cref="T:Common.Drawing.Region" />; otherwise,
        ///     false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a graphics context. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(float x, float y, Graphics g)
        {
            return WrappedRegion.IsVisible(x, y, g);
        }

        /// <summary>
        ///     Tests whether the specified <see cref="T:Common.Drawing.PointF" /> structure is contained within this
        ///     <see cref="T:Common.Drawing.Region" /> when drawn using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true when <paramref name="point" /> is contained within this <see cref="T:Common.Drawing.Region" />;
        ///     otherwise, false.
        /// </returns>
        /// <param name="point">The <see cref="T:Common.Drawing.PointF" /> structure to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a graphics context. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(PointF point, Graphics g)
        {
            return WrappedRegion.IsVisible(point, g);
        }

        /// <summary>
        ///     Tests whether any portion of the specified rectangle is contained within this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <returns>
        ///     true when any portion of the specified rectangle is contained within this
        ///     <see cref="T:Common.Drawing.Region" /> object; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test. </param>
        /// <param name="width">The width of the rectangle to test. </param>
        /// <param name="height">The height of the rectangle to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(float x, float y, float width, float height)
        {
            return WrappedRegion.IsVisible(x, y, width, height);
        }

        /// <summary>
        ///     Tests whether any portion of the specified <see cref="T:Common.Drawing.RectangleF" /> structure is contained
        ///     within this <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <returns>
        ///     true when any portion of <paramref name="rect" /> is contained within this
        ///     <see cref="T:Common.Drawing.Region" />; otherwise, false.
        /// </returns>
        /// <param name="rect">The <see cref="T:Common.Drawing.RectangleF" /> structure to test. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(RectangleF rect)
        {
            return WrappedRegion.IsVisible(rect);
        }

        /// <summary>
        ///     Tests whether any portion of the specified rectangle is contained within this
        ///     <see cref="T:Common.Drawing.Region" /> when drawn using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true when any portion of the specified rectangle is contained within this
        ///     <see cref="T:Common.Drawing.Region" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test. </param>
        /// <param name="width">The width of the rectangle to test. </param>
        /// <param name="height">The height of the rectangle to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a graphics context. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(float x, float y, float width, float height, Graphics g)
        {
            return WrappedRegion.IsVisible(x, y, width, height, g);
        }

        /// <summary>
        ///     Tests whether any portion of the specified <see cref="T:Common.Drawing.RectangleF" /> structure is contained
        ///     within this <see cref="T:Common.Drawing.Region" /> when drawn using the specified
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true when <paramref name="rect" /> is contained within this <see cref="T:Common.Drawing.Region" />; otherwise,
        ///     false.
        /// </returns>
        /// <param name="rect">The <see cref="T:Common.Drawing.RectangleF" /> structure to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a graphics context. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(RectangleF rect, Graphics g)
        {
            return WrappedRegion.IsVisible(rect, g);
        }

        /// <summary>
        ///     Tests whether the specified point is contained within this <see cref="T:Common.Drawing.Region" /> object when
        ///     drawn using the specified <see cref="T:Common.Drawing.Graphics" /> object.
        /// </summary>
        /// <returns>
        ///     true when the specified point is contained within this <see cref="T:Common.Drawing.Region" />; otherwise,
        ///     false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a graphics context. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(int x, int y, Graphics g)
        {
            return WrappedRegion.IsVisible(x, y, g);
        }

        /// <summary>
        ///     Tests whether the specified <see cref="T:Common.Drawing.Point" /> structure is contained within this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <returns>
        ///     true when <paramref name="point" /> is contained within this <see cref="T:Common.Drawing.Region" />;
        ///     otherwise, false.
        /// </returns>
        /// <param name="point">The <see cref="T:Common.Drawing.Point" /> structure to test. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Point point)
        {
            return WrappedRegion.IsVisible(point);
        }

        /// <summary>
        ///     Tests whether the specified <see cref="T:Common.Drawing.Point" /> structure is contained within this
        ///     <see cref="T:Common.Drawing.Region" /> when drawn using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true when <paramref name="point" /> is contained within this <see cref="T:Common.Drawing.Region" />;
        ///     otherwise, false.
        /// </returns>
        /// <param name="point">The <see cref="T:Common.Drawing.Point" /> structure to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a graphics context. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Point point, Graphics g)
        {
            return WrappedRegion.IsVisible(point, g);
        }

        /// <summary>
        ///     Tests whether any portion of the specified rectangle is contained within this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <returns>
        ///     true when any portion of the specified rectangle is contained within this
        ///     <see cref="T:Common.Drawing.Region" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test. </param>
        /// <param name="width">The width of the rectangle to test. </param>
        /// <param name="height">The height of the rectangle to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(int x, int y, int width, int height)
        {
            return WrappedRegion.IsVisible(x, y, width, height);
        }

        /// <summary>
        ///     Tests whether any portion of the specified <see cref="T:Common.Drawing.Rectangle" /> structure is contained
        ///     within this <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <returns>
        ///     This method returns true when any portion of <paramref name="rect" /> is contained within this
        ///     <see cref="T:Common.Drawing.Region" />; otherwise, false.
        /// </returns>
        /// <param name="rect">The <see cref="T:Common.Drawing.Rectangle" /> structure to test. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Rectangle rect)
        {
            return WrappedRegion.IsVisible(rect);
        }

        /// <summary>
        ///     Tests whether any portion of the specified rectangle is contained within this
        ///     <see cref="T:Common.Drawing.Region" /> when drawn using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true when any portion of the specified rectangle is contained within this
        ///     <see cref="T:Common.Drawing.Region" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test. </param>
        /// <param name="width">The width of the rectangle to test. </param>
        /// <param name="height">The height of the rectangle to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a graphics context. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(int x, int y, int width, int height, Graphics g)
        {
            return WrappedRegion.IsVisible(x, y, width, height, g);
        }

        /// <summary>
        ///     Tests whether any portion of the specified <see cref="T:Common.Drawing.Rectangle" /> structure is contained
        ///     within this <see cref="T:Common.Drawing.Region" /> when drawn using the specified
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     true when any portion of the <paramref name="rect" /> is contained within this
        ///     <see cref="T:Common.Drawing.Region" />; otherwise, false.
        /// </returns>
        /// <param name="rect">The <see cref="T:Common.Drawing.Rectangle" /> structure to test. </param>
        /// <param name="g">A <see cref="T:Common.Drawing.Graphics" /> that represents a graphics context. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Rectangle rect, Graphics g)
        {
            return WrappedRegion.IsVisible(rect, g);
        }

        /// <summary>Initializes this <see cref="T:Common.Drawing.Region" /> to an empty interior.</summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void MakeEmpty()
        {
            WrappedRegion.MakeEmpty();
        }

        /// <summary>Initializes this <see cref="T:Common.Drawing.Region" /> object to an infinite interior.</summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void MakeInfinite()
        {
            WrappedRegion.MakeInfinite();
        }

        /// <summary>Releases the handle of the <see cref="T:Common.Drawing.Region" />.</summary>
        /// <param name="regionHandle">The handle to the <see cref="T:Common.Drawing.Region" />.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="regionHandle" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void ReleaseHrgn(IntPtr regionHandle)
        {
            WrappedRegion.ReleaseHrgn(regionHandle);
        }

        /// <summary>
        ///     Transforms this <see cref="T:Common.Drawing.Region" /> by the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by which to transform this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="matrix" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Transform(Matrix matrix)
        {
            WrappedRegion.Transform(matrix);
        }

        /// <summary>Offsets the coordinates of this <see cref="T:Common.Drawing.Region" /> by the specified amount.</summary>
        /// <param name="dx">The amount to offset this <see cref="T:Common.Drawing.Region" /> horizontally. </param>
        /// <param name="dy">The amount to offset this <see cref="T:Common.Drawing.Region" /> vertically. </param>
        /// <filterpriority>1</filterpriority>
        public void Translate(float dx, float dy)
        {
            WrappedRegion.Translate(dx, dy);
        }

        /// <summary>Offsets the coordinates of this <see cref="T:Common.Drawing.Region" /> by the specified amount.</summary>
        /// <param name="dx">The amount to offset this <see cref="T:Common.Drawing.Region" /> horizontally. </param>
        /// <param name="dy">The amount to offset this <see cref="T:Common.Drawing.Region" /> vertically. </param>
        /// <filterpriority>1</filterpriority>
        public void Translate(int dx, int dy)
        {
            WrappedRegion.Translate(dx, dy);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the union of itself and the specified
        ///     <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.RectangleF" /> structure to unite with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Union(RectangleF rect)
        {
            WrappedRegion.Union(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the union of itself and the specified
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure to unite with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Union(Rectangle rect)
        {
            WrappedRegion.Union(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the union of itself and the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="path">
        ///     The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to unite with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Union(GraphicsPath path)
        {
            WrappedRegion.Union(path);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the union of itself and the specified
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="region">
        ///     The <see cref="T:Common.Drawing.Region" /> to unite with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="region" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Union(Region region)
        {
            WrappedRegion.Union(region);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the union minus the intersection of itself with the
        ///     specified <see cref="T:Common.Drawing.RectangleF" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.RectangleF" /> structure to
        ///     <see cref="M:Common.Drawing.Region.Xor(Common.Drawing.Drawing2D.GraphicsPath)" /> with this
        ///     <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Xor(RectangleF rect)
        {
            WrappedRegion.Xor(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the union minus the intersection of itself with the
        ///     specified <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure to
        ///     <see cref="Overload:Common.Drawing.Region.Xor" /> with this <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Xor(Rectangle rect)
        {
            WrappedRegion.Xor(rect);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the union minus the intersection of itself with the
        ///     specified <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="path">
        ///     The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to
        ///     <see cref="Overload:Common.Drawing.Region.Xor" /> with this <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Xor(GraphicsPath path)
        {
            WrappedRegion.Xor(path);
        }

        /// <summary>
        ///     Updates this <see cref="T:Common.Drawing.Region" /> to the union minus the intersection of itself with the
        ///     specified <see cref="T:Common.Drawing.Region" />.
        /// </summary>
        /// <param name="region">
        ///     The <see cref="T:Common.Drawing.Region" /> to <see cref="Overload:Common.Drawing.Region.Xor" />
        ///     with this <see cref="T:Common.Drawing.Region" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="region" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Xor(Region region)
        {
            WrappedRegion.Xor(region);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedRegion.Dispose();
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Region" /> to a <see cref="T:Common.Drawing.Region" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Region" /> that results from the conversion.</returns>
        /// <param name="region">The <see cref="T:System.Drawing.Region" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Region(System.Drawing.Region region)
        {
            return region == null ? null : new Region(region);
        }

        /// <summary>Converts the specified <see cref="T:Common.Drawing.Region" /> to a <see cref="T:System.Drawing.Region" />.</summary>
        /// <returns>The <see cref="T:System.Drawing.Region" /> that results from the conversion.</returns>
        /// <param name="region">The <see cref="T:Common.Drawing.Region" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Region(Region region)
        {
            return region?.WrappedRegion;
        }
    }
}