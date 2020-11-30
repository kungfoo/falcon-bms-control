using System;
using System.Linq;

namespace Common.Drawing.Drawing2D
{
    /// <summary>Represents a series of connected lines and curves. This class cannot be inherited.</summary>
    public sealed class GraphicsPath : MarshalByRefObject, ICloneable, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> class with a
        ///     <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.FillMode" /> value of
        ///     <see cref="F:Common.Drawing.Drawing2D.FillMode.Alternate" />.
        /// </summary>
        public GraphicsPath()
        {
            WrappedGraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> class with the
        ///     specified <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration.
        /// </summary>
        /// <param name="fillMode">
        ///     The <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines how the
        ///     interior of this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> is filled.
        /// </param>
        public GraphicsPath(FillMode fillMode)
        {
            WrappedGraphicsPath =
                new System.Drawing.Drawing2D.GraphicsPath((System.Drawing.Drawing2D.FillMode) fillMode);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> array with the
        ///     specified <see cref="T:Common.Drawing.Drawing2D.PathPointType" /> and <see cref="T:Common.Drawing.PointF" />
        ///     arrays.
        /// </summary>
        /// <param name="pts">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that defines the coordinates of the
        ///     points that make up this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </param>
        /// <param name="types">
        ///     An array of <see cref="T:Common.Drawing.Drawing2D.PathPointType" /> enumeration elements that
        ///     specifies the type of each corresponding point in the <paramref name="pts" /> array.
        /// </param>
        public GraphicsPath(PointF[] pts, byte[] types)
        {
            WrappedGraphicsPath =
                new System.Drawing.Drawing2D.GraphicsPath(pts.Convert<System.Drawing.PointF>().ToArray(), types);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> array with the
        ///     specified <see cref="T:Common.Drawing.Drawing2D.PathPointType" /> and <see cref="T:Common.Drawing.PointF" /> arrays
        ///     and with the specified <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration element.
        /// </summary>
        /// <param name="pts">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that defines the coordinates of the
        ///     points that make up this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </param>
        /// <param name="types">
        ///     An array of <see cref="T:Common.Drawing.Drawing2D.PathPointType" /> enumeration elements that
        ///     specifies the type of each corresponding point in the <paramref name="pts" /> array.
        /// </param>
        /// <param name="fillMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the
        ///     interiors of shapes in this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> are filled.
        /// </param>
        public GraphicsPath(PointF[] pts, byte[] types, FillMode fillMode)
        {
            WrappedGraphicsPath =
                new System.Drawing.Drawing2D.GraphicsPath(pts.Convert<System.Drawing.PointF>().ToArray(), types,
                    (System.Drawing.Drawing2D.FillMode) fillMode);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> class with the
        ///     specified <see cref="T:Common.Drawing.Drawing2D.PathPointType" /> and <see cref="T:Common.Drawing.Point" /> arrays.
        /// </summary>
        /// <param name="pts">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that defines the coordinates of the
        ///     points that make up this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </param>
        /// <param name="types">
        ///     An array of <see cref="T:Common.Drawing.Drawing2D.PathPointType" /> enumeration elements that
        ///     specifies the type of each corresponding point in the <paramref name="pts" /> array.
        /// </param>
        public GraphicsPath(Point[] pts, byte[] types)
        {
            WrappedGraphicsPath =
                new System.Drawing.Drawing2D.GraphicsPath(pts.Convert<System.Drawing.Point>().ToArray(), types);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> class with the
        ///     specified <see cref="T:Common.Drawing.Drawing2D.PathPointType" /> and <see cref="T:Common.Drawing.Point" /> arrays
        ///     and with the specified <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration element.
        /// </summary>
        /// <param name="pts">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that defines the coordinates of the
        ///     points that make up this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </param>
        /// <param name="types">
        ///     An array of <see cref="T:Common.Drawing.Drawing2D.PathPointType" /> enumeration elements that
        ///     specifies the type of each corresponding point in the <paramref name="pts" /> array.
        /// </param>
        /// <param name="fillMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the
        ///     interiors of shapes in this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> are filled.
        /// </param>
        public GraphicsPath(Point[] pts, byte[] types, FillMode fillMode)
        {
            WrappedGraphicsPath =
                new System.Drawing.Drawing2D.GraphicsPath(pts.Convert<System.Drawing.Point>().ToArray(), types,
                    (System.Drawing.Drawing2D.FillMode) fillMode);
        }

        private GraphicsPath(System.Drawing.Drawing2D.GraphicsPath graphicsPath)
        {
            WrappedGraphicsPath = graphicsPath;
        }

        /// <summary>
        ///     Gets or sets a <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that determines how the
        ///     interiors of shapes in this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> are filled.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in
        ///     this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> are filled.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public FillMode FillMode
        {
            get => (FillMode) WrappedGraphicsPath.FillMode;
            set => WrappedGraphicsPath.FillMode = (System.Drawing.Drawing2D.FillMode) value;
        }

        /// <summary>
        ///     Gets a <see cref="T:Common.Drawing.Drawing2D.PathData" /> that encapsulates arrays of points (
        ///     <paramref name="points" />) and types (<paramref name="types" />) for this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.PathData" /> that encapsulates arrays for both the points and types
        ///     for this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PathData PathData => WrappedGraphicsPath.PathData;

        /// <summary>Gets the points in the path.</summary>
        /// <returns>An array of <see cref="T:Common.Drawing.PointF" /> objects that represent the path.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PointF[] PathPoints => WrappedGraphicsPath.PathPoints.Convert<PointF>().ToArray();

        /// <summary>
        ///     Gets the types of the corresponding points in the
        ///     <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array.
        /// </summary>
        /// <returns>An array of bytes that specifies the types of the corresponding points in the path.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public byte[] PathTypes => WrappedGraphicsPath.PathTypes;

        /// <summary>
        ///     Gets the number of elements in the <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the
        ///     <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.
        /// </summary>
        /// <returns>
        ///     An integer that specifies the number of elements in the
        ///     <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the
        ///     <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int PointCount => WrappedGraphicsPath.PointCount;

        private System.Drawing.Drawing2D.GraphicsPath WrappedGraphicsPath { get; }

        /// <summary>Creates an exact copy of this path.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> this method creates, cast as an object.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public object Clone()
        {
            return new GraphicsPath((System.Drawing.Drawing2D.GraphicsPath) WrappedGraphicsPath.Clone());
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
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

        ~GraphicsPath()
        {
            Dispose(false);
        }

        /// <summary>Appends an elliptical arc to the current figure.</summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangular bounds of the ellipse
        ///     from which the arc is taken.
        /// </param>
        /// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis. </param>
        /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc. </param>
        public void AddArc(RectangleF rect, float startAngle, float sweepAngle)
        {
            WrappedGraphicsPath.AddArc(rect, startAngle, sweepAngle);
        }

        /// <summary>Appends an elliptical arc to the current figure.</summary>
        /// <param name="x">
        ///     The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which
        ///     the arc is drawn.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which
        ///     the arc is drawn.
        /// </param>
        /// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn. </param>
        /// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn. </param>
        /// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis. </param>
        /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc. </param>
        public void AddArc(float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            WrappedGraphicsPath.AddArc(x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>Appends an elliptical arc to the current figure.</summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangular bounds of the ellipse
        ///     from which the arc is taken.
        /// </param>
        /// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis. </param>
        /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc. </param>
        public void AddArc(Rectangle rect, float startAngle, float sweepAngle)
        {
            WrappedGraphicsPath.AddArc(rect, startAngle, sweepAngle);
        }

        /// <summary>Appends an elliptical arc to the current figure.</summary>
        /// <param name="x">
        ///     The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which
        ///     the arc is drawn.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which
        ///     the arc is drawn.
        /// </param>
        /// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn. </param>
        /// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn. </param>
        /// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis. </param>
        /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc. </param>
        public void AddArc(int x, int y, int width, int height, float startAngle, float sweepAngle)
        {
            WrappedGraphicsPath.AddArc(x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>Adds a cubic Bézier curve to the current figure.</summary>
        /// <param name="pt1">A <see cref="T:Common.Drawing.PointF" /> that represents the starting point of the curve. </param>
        /// <param name="pt2">A <see cref="T:Common.Drawing.PointF" /> that represents the first control point for the curve. </param>
        /// <param name="pt3">A <see cref="T:Common.Drawing.PointF" /> that represents the second control point for the curve. </param>
        /// <param name="pt4">A <see cref="T:Common.Drawing.PointF" /> that represents the endpoint of the curve. </param>
        public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        {
            WrappedGraphicsPath.AddBezier(pt1, pt2, pt3, pt4);
        }

        /// <summary>Adds a cubic Bézier curve to the current figure.</summary>
        /// <param name="x1">The x-coordinate of the starting point of the curve. </param>
        /// <param name="y1">The y-coordinate of the starting point of the curve. </param>
        /// <param name="x2">The x-coordinate of the first control point for the curve. </param>
        /// <param name="y2">The y-coordinate of the first control point for the curve. </param>
        /// <param name="x3">The x-coordinate of the second control point for the curve. </param>
        /// <param name="y3">The y-coordinate of the second control point for the curve. </param>
        /// <param name="x4">The x-coordinate of the endpoint of the curve. </param>
        /// <param name="y4">The y-coordinate of the endpoint of the curve. </param>
        public void AddBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            WrappedGraphicsPath.AddBezier(x1, y1, x2, y2, x3, y3, x4, y4);
        }

        /// <summary>Adds a cubic Bézier curve to the current figure.</summary>
        /// <param name="pt1">A <see cref="T:Common.Drawing.Point" /> that represents the starting point of the curve. </param>
        /// <param name="pt2">A <see cref="T:Common.Drawing.Point" /> that represents the first control point for the curve. </param>
        /// <param name="pt3">A <see cref="T:Common.Drawing.Point" /> that represents the second control point for the curve. </param>
        /// <param name="pt4">A <see cref="T:Common.Drawing.Point" /> that represents the endpoint of the curve. </param>
        public void AddBezier(Point pt1, Point pt2, Point pt3, Point pt4)
        {
            WrappedGraphicsPath.AddBezier(pt1, pt2, pt3, pt4);
        }

        /// <summary>Adds a cubic Bézier curve to the current figure.</summary>
        /// <param name="x1">The x-coordinate of the starting point of the curve. </param>
        /// <param name="y1">The y-coordinate of the starting point of the curve. </param>
        /// <param name="x2">The x-coordinate of the first control point for the curve. </param>
        /// <param name="y2">The y-coordinate of the first control point for the curve. </param>
        /// <param name="x3">The x-coordinate of the second control point for the curve. </param>
        /// <param name="y3">The y-coordinate of the second control point for the curve. </param>
        /// <param name="x4">The x-coordinate of the endpoint of the curve. </param>
        /// <param name="y4">The y-coordinate of the endpoint of the curve. </param>
        public void AddBezier(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            WrappedGraphicsPath.AddBezier(x1, y1, x2, y2, x3, y3, x4, y4);
        }

        /// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that
        ///     define the curves.
        /// </param>
        public void AddBeziers(PointF[] points)
        {
            WrappedGraphicsPath.AddBeziers(points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that
        ///     define the curves.
        /// </param>
        public void AddBeziers(params Point[] points)
        {
            WrappedGraphicsPath.AddBeziers(points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of
        ///     the points in the array.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        public void AddClosedCurve(PointF[] points)
        {
            WrappedGraphicsPath.AddClosedCurve(points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of
        ///     the points in the array.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        /// <param name="tension">
        ///     A value between from 0 through 1 that specifies the amount that the curve bends between points,
        ///     with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve.
        /// </param>
        public void AddClosedCurve(PointF[] points, float tension)
        {
            WrappedGraphicsPath.AddClosedCurve(points.Convert<System.Drawing.PointF>().ToArray(), tension);
        }

        /// <summary>
        ///     Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of
        ///     the points in the array.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        public void AddClosedCurve(Point[] points)
        {
            WrappedGraphicsPath.AddClosedCurve(points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of
        ///     the points in the array.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        /// <param name="tension">
        ///     A value between from 0 through 1 that specifies the amount that the curve bends between points,
        ///     with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve.
        /// </param>
        public void AddClosedCurve(Point[] points, float tension)
        {
            WrappedGraphicsPath.AddClosedCurve(points.Convert<System.Drawing.Point>().ToArray(), tension);
        }

        /// <summary>
        ///     Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through
        ///     each of the points in the array.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        public void AddCurve(PointF[] points)
        {
            WrappedGraphicsPath.AddCurve(points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>Adds a spline curve to the current figure.</summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        /// <param name="tension">
        ///     A value that specifies the amount that the curve bends between control points. Values greater
        ///     than 1 produce unpredictable results.
        /// </param>
        public void AddCurve(PointF[] points, float tension)
        {
            WrappedGraphicsPath.AddCurve(points.Convert<System.Drawing.PointF>().ToArray(), tension);
        }

        /// <summary>Adds a spline curve to the current figure.</summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        /// <param name="offset">
        ///     The index of the element in the <paramref name="points" /> array that is used as the first point
        ///     in the curve.
        /// </param>
        /// <param name="numberOfSegments">
        ///     The number of segments used to draw the curve. A segment can be thought of as a line
        ///     connecting two points.
        /// </param>
        /// <param name="tension">
        ///     A value that specifies the amount that the curve bends between control points. Values greater
        ///     than 1 produce unpredictable results.
        /// </param>
        public void AddCurve(PointF[] points, int offset, int numberOfSegments, float tension)
        {
            WrappedGraphicsPath.AddCurve(points.Convert<System.Drawing.PointF>().ToArray(), offset, numberOfSegments,
                tension);
        }

        /// <summary>
        ///     Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through
        ///     each of the points in the array.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        public void AddCurve(Point[] points)
        {
            WrappedGraphicsPath.AddCurve(points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>Adds a spline curve to the current figure.</summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        /// <param name="tension">
        ///     A value that specifies the amount that the curve bends between control points. Values greater
        ///     than 1 produce unpredictable results.
        /// </param>
        public void AddCurve(Point[] points, float tension)
        {
            WrappedGraphicsPath.AddCurve(points.Convert<System.Drawing.Point>().ToArray(), tension);
        }

        /// <summary>Adds a spline curve to the current figure.</summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that
        ///     define the curve.
        /// </param>
        /// <param name="offset">
        ///     The index of the element in the <paramref name="points" /> array that is used as the first point
        ///     in the curve.
        /// </param>
        /// <param name="numberOfSegments">
        ///     A value that specifies the amount that the curve bends between control points. Values
        ///     greater than 1 produce unpredictable results.
        /// </param>
        /// <param name="tension">
        ///     A value that specifies the amount that the curve bends between control points. Values greater
        ///     than 1 produce unpredictable results.
        /// </param>
        public void AddCurve(Point[] points, int offset, int numberOfSegments, float tension)
        {
            WrappedGraphicsPath.AddCurve(points.Convert<System.Drawing.Point>().ToArray(), offset, numberOfSegments,
                tension);
        }

        /// <summary>Adds an ellipse to the current path.</summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the bounding rectangle that defines the
        ///     ellipse.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void AddEllipse(RectangleF rect)
        {
            WrappedGraphicsPath.AddEllipse(rect);
        }

        /// <summary>Adds an ellipse to the current path.</summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="y">The y-coordinate of the upper left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">The width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="height">The height of the bounding rectangle that defines the ellipse. </param>
        public void AddEllipse(float x, float y, float width, float height)
        {
            WrappedGraphicsPath.AddEllipse(x, y, width, height);
        }

        /// <summary>Adds an ellipse to the current path.</summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the bounding rectangle that defines the
        ///     ellipse.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void AddEllipse(Rectangle rect)
        {
            WrappedGraphicsPath.AddEllipse(rect);
        }

        /// <summary>Adds an ellipse to the current path.</summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">The width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="height">The height of the bounding rectangle that defines the ellipse. </param>
        public void AddEllipse(int x, int y, int width, int height)
        {
            WrappedGraphicsPath.AddEllipse(x, y, width, height);
        }

        /// <summary>Appends a line segment to this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <param name="pt1">A <see cref="T:Common.Drawing.PointF" /> that represents the starting point of the line. </param>
        /// <param name="pt2">A <see cref="T:Common.Drawing.PointF" /> that represents the endpoint of the line. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void AddLine(PointF pt1, PointF pt2)
        {
            WrappedGraphicsPath.AddLine(pt1, pt2);
        }

        /// <summary>Appends a line segment to this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <param name="x1">The x-coordinate of the starting point of the line. </param>
        /// <param name="y1">The y-coordinate of the starting point of the line. </param>
        /// <param name="x2">The x-coordinate of the endpoint of the line. </param>
        /// <param name="y2">The y-coordinate of the endpoint of the line. </param>
        public void AddLine(float x1, float y1, float x2, float y2)
        {
            WrappedGraphicsPath.AddLine(x1, y1, x2, y2);
        }

        /// <summary>Appends a line segment to this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <param name="pt1">A <see cref="T:Common.Drawing.Point" /> that represents the starting point of the line. </param>
        /// <param name="pt2">A <see cref="T:Common.Drawing.Point" /> that represents the endpoint of the line. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void AddLine(Point pt1, Point pt2)
        {
            WrappedGraphicsPath.AddLine(pt1, pt2);
        }

        /// <summary>Appends a line segment to the current figure.</summary>
        /// <param name="x1">The x-coordinate of the starting point of the line. </param>
        /// <param name="y1">The y-coordinate of the starting point of the line. </param>
        /// <param name="x2">The x-coordinate of the endpoint of the line. </param>
        /// <param name="y2">The y-coordinate of the endpoint of the line. </param>
        public void AddLine(int x1, int y1, int x2, int y2)
        {
            WrappedGraphicsPath.AddLine(x1, y1, x2, y2);
        }

        /// <summary>
        ///     Appends a series of connected line segments to the end of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that
        ///     define the line segments to add.
        /// </param>
        public void AddLines(PointF[] points)
        {
            WrappedGraphicsPath.AddLines(points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Appends a series of connected line segments to the end of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that
        ///     define the line segments to add.
        /// </param>
        public void AddLines(Point[] points)
        {
            WrappedGraphicsPath.AddLines(points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>Appends the specified <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to this path.</summary>
        /// <param name="addingPath">The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to add. </param>
        /// <param name="connect">
        ///     A Boolean value that specifies whether the first figure in the added path is part of the last
        ///     figure in this path. A value of true specifies that (if possible) the first figure in the added path is part of the
        ///     last figure in this path. A value of false specifies that the first figure in the added path is separate from the
        ///     last figure in this path.
        /// </param>
        public void AddPath(GraphicsPath addingPath, bool connect)
        {
            WrappedGraphicsPath.AddPath(addingPath, connect);
        }

        /// <summary>Adds the outline of a pie shape to this path.</summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the bounding rectangle that defines the
        ///     ellipse from which the pie is drawn.
        /// </param>
        /// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis. </param>
        /// <param name="sweepAngle">
        ///     The angle between <paramref name="startAngle" /> and the end of the pie section, measured in
        ///     degrees clockwise from <paramref name="startAngle" />.
        /// </param>
        public void AddPie(Rectangle rect, float startAngle, float sweepAngle)
        {
            WrappedGraphicsPath.AddPie(rect, startAngle, sweepAngle);
        }

        /// <summary>Adds the outline of a pie shape to this path.</summary>
        /// <param name="x">
        ///     The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie is drawn.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie is drawn.
        /// </param>
        /// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
        /// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
        /// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis. </param>
        /// <param name="sweepAngle">
        ///     The angle between <paramref name="startAngle" /> and the end of the pie section, measured in
        ///     degrees clockwise from <paramref name="startAngle" />.
        /// </param>
        public void AddPie(float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            WrappedGraphicsPath.AddPie(x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>Adds the outline of a pie shape to this path.</summary>
        /// <param name="x">
        ///     The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie is drawn.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which
        ///     the pie is drawn.
        /// </param>
        /// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
        /// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
        /// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis. </param>
        /// <param name="sweepAngle">
        ///     The angle between <paramref name="startAngle" /> and the end of the pie section, measured in
        ///     degrees clockwise from <paramref name="startAngle" />.
        /// </param>
        public void AddPie(int x, int y, int width, int height, float startAngle, float sweepAngle)
        {
            WrappedGraphicsPath.AddPie(x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>Adds a polygon to this path.</summary>
        /// <param name="points">An array of <see cref="T:Common.Drawing.PointF" /> structures that defines the polygon to add. </param>
        public void AddPolygon(PointF[] points)
        {
            WrappedGraphicsPath.AddPolygon(points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>Adds a polygon to this path.</summary>
        /// <param name="points">An array of <see cref="T:Common.Drawing.Point" /> structures that defines the polygon to add. </param>
        public void AddPolygon(Point[] points)
        {
            WrappedGraphicsPath.AddPolygon(points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>Adds a rectangle to this path.</summary>
        /// <param name="rect">A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle to add. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void AddRectangle(RectangleF rect)
        {
            WrappedGraphicsPath.AddRectangle(rect);
        }

        /// <summary>Adds a rectangle to this path.</summary>
        /// <param name="rect">A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle to add. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void AddRectangle(Rectangle rect)
        {
            WrappedGraphicsPath.AddRectangle(rect);
        }

        /// <summary>Adds a series of rectangles to this path.</summary>
        /// <param name="rects">
        ///     An array of <see cref="T:Common.Drawing.RectangleF" /> structures that represents the rectangles to
        ///     add.
        /// </param>
        public void AddRectangles(RectangleF[] rects)
        {
            WrappedGraphicsPath.AddRectangles(rects.Convert<System.Drawing.RectangleF>().ToArray());
        }

        /// <summary>Adds a series of rectangles to this path.</summary>
        /// <param name="rects">
        ///     An array of <see cref="T:Common.Drawing.Rectangle" /> structures that represents the rectangles to
        ///     add.
        /// </param>
        public void AddRectangles(Rectangle[] rects)
        {
            WrappedGraphicsPath.AddRectangles(rects.Convert<System.Drawing.Rectangle>().ToArray());
        }

        /// <summary>Adds a text string to this path.</summary>
        /// <param name="s">The <see cref="T:System.String" /> to add. </param>
        /// <param name="family">
        ///     A <see cref="T:Common.Drawing.FontFamily" /> that represents the name of the font with which the
        ///     test is drawn.
        /// </param>
        /// <param name="style">
        ///     A <see cref="T:Common.Drawing.FontStyle" /> enumeration that represents style information about the
        ///     text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).
        /// </param>
        /// <param name="emSize">The height of the em square box that bounds the character. </param>
        /// <param name="origin">A <see cref="T:Common.Drawing.PointF" /> that represents the point where the text starts. </param>
        /// <param name="format">
        ///     A <see cref="T:Common.Drawing.StringFormat" /> that specifies text formatting information, such as
        ///     line spacing and alignment.
        /// </param>
        public void AddString(string s, FontFamily family, int style, float emSize, PointF origin, StringFormat format)
        {
            WrappedGraphicsPath.AddString(s, family, style, emSize, origin, format);
        }

        /// <summary>Adds a text string to this path.</summary>
        /// <param name="s">The <see cref="T:System.String" /> to add. </param>
        /// <param name="family">
        ///     A <see cref="T:Common.Drawing.FontFamily" /> that represents the name of the font with which the
        ///     test is drawn.
        /// </param>
        /// <param name="style">
        ///     A <see cref="T:Common.Drawing.FontStyle" /> enumeration that represents style information about the
        ///     text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).
        /// </param>
        /// <param name="emSize">The height of the em square box that bounds the character. </param>
        /// <param name="origin">A <see cref="T:Common.Drawing.Point" /> that represents the point where the text starts. </param>
        /// <param name="format">
        ///     A <see cref="T:Common.Drawing.StringFormat" /> that specifies text formatting information, such as
        ///     line spacing and alignment.
        /// </param>
        public void AddString(string s, FontFamily family, int style, float emSize, Point origin, StringFormat format)
        {
            WrappedGraphicsPath.AddString(s, family, style, emSize, origin, format);
        }

        /// <summary>Adds a text string to this path.</summary>
        /// <param name="s">The <see cref="T:System.String" /> to add. </param>
        /// <param name="family">
        ///     A <see cref="T:Common.Drawing.FontFamily" /> that represents the name of the font with which the
        ///     test is drawn.
        /// </param>
        /// <param name="style">
        ///     A <see cref="T:Common.Drawing.FontStyle" /> enumeration that represents style information about the
        ///     text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).
        /// </param>
        /// <param name="emSize">The height of the em square box that bounds the character. </param>
        /// <param name="layoutRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that bounds the
        ///     text.
        /// </param>
        /// <param name="format">
        ///     A <see cref="T:Common.Drawing.StringFormat" /> that specifies text formatting information, such as
        ///     line spacing and alignment.
        /// </param>
        public void AddString(string s, FontFamily family, int style, float emSize, RectangleF layoutRect,
            StringFormat format)
        {
            WrappedGraphicsPath.AddString(s, family, style, emSize, layoutRect, format);
        }

        /// <summary>Adds a text string to this path.</summary>
        /// <param name="s">The <see cref="T:System.String" /> to add. </param>
        /// <param name="family">
        ///     A <see cref="T:Common.Drawing.FontFamily" /> that represents the name of the font with which the
        ///     test is drawn.
        /// </param>
        /// <param name="style">
        ///     A <see cref="T:Common.Drawing.FontStyle" /> enumeration that represents style information about the
        ///     text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).
        /// </param>
        /// <param name="emSize">The height of the em square box that bounds the character. </param>
        /// <param name="layoutRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the rectangle that bounds the
        ///     text.
        /// </param>
        /// <param name="format">
        ///     A <see cref="T:Common.Drawing.StringFormat" /> that specifies text formatting information, such as
        ///     line spacing and alignment.
        /// </param>
        public void AddString(string s, FontFamily family, int style, float emSize, Rectangle layoutRect,
            StringFormat format)
        {
            WrappedGraphicsPath.AddString(s, family, style, emSize, layoutRect, format);
        }

        /// <summary>Clears all markers from this path.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearMarkers()
        {
            WrappedGraphicsPath.ClearMarkers();
        }

        /// <summary>
        ///     Closes all open figures in this path and starts a new figure. It closes each open figure by connecting a line
        ///     from its endpoint to its starting point.
        /// </summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void CloseAllFigures()
        {
            WrappedGraphicsPath.CloseAllFigures();
        }

        /// <summary>
        ///     Closes the current figure and starts a new figure. If the current figure contains a sequence of connected
        ///     lines and curves, the method closes the loop by connecting a line from the endpoint to the starting point.
        /// </summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void CloseFigure()
        {
            WrappedGraphicsPath.CloseFigure();
        }

        /// <summary>Converts each curve in this path into a sequence of connected line segments.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Flatten()
        {
            WrappedGraphicsPath.Flatten();
        }

        /// <summary>
        ///     Applies the specified transform and then converts each curve in this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.
        /// </summary>
        /// <param name="matrix">
        ///     A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by which to transform this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> before flattening.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Flatten(Matrix matrix)
        {
            WrappedGraphicsPath.Flatten(matrix);
        }

        /// <summary>
        ///     Converts each curve in this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> into a sequence of
        ///     connected line segments.
        /// </summary>
        /// <param name="matrix">
        ///     A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by which to transform this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> before flattening.
        /// </param>
        /// <param name="flatness">
        ///     Specifies the maximum permitted error between the curve and its flattened approximation. A value
        ///     of 0.25 is the default. Reducing the flatness value will increase the number of line segments in the approximation.
        /// </param>
        public void Flatten(Matrix matrix, float flatness)
        {
            WrappedGraphicsPath.Flatten(matrix, flatness);
        }

        /// <summary>Returns a rectangle that bounds this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents a rectangle that bounds this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RectangleF GetBounds()
        {
            return WrappedGraphicsPath.GetBounds();
        }

        /// <summary>
        ///     Returns a rectangle that bounds this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when this path is
        ///     transformed by the specified <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents a rectangle that bounds this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </returns>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied
        ///     to this path before the bounding rectangle is calculated. This path is not permanently transformed; the
        ///     transformation is used only during the process of calculating the bounding rectangle.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RectangleF GetBounds(Matrix matrix)
        {
            return WrappedGraphicsPath.GetBounds(matrix);
        }

        /// <summary>
        ///     Returns a rectangle that bounds this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when the current
        ///     path is transformed by the specified <see cref="T:Common.Drawing.Drawing2D.Matrix" /> and drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents a rectangle that bounds this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </returns>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied
        ///     to this path before the bounding rectangle is calculated. This path is not permanently transformed; the
        ///     transformation is used only during the process of calculating the bounding rectangle.
        /// </param>
        /// <param name="pen">
        ///     The <see cref="T:Common.Drawing.Pen" /> with which to draw the
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </param>
        public RectangleF GetBounds(Matrix matrix, Pen pen)
        {
            return WrappedGraphicsPath.GetBounds(matrix, pen);
        }

        /// <summary>
        ///     Gets the last point in the <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.PointF" /> that represents the last point in this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PointF GetLastPoint()
        {
            return WrappedGraphicsPath.GetLastPoint();
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to test. </param>
        public bool IsOutlineVisible(float x, float y, Pen pen)
        {
            return WrappedGraphicsPath.IsOutlineVisible(x, y, pen);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />; otherwise, false.
        /// </returns>
        /// <param name="point">A <see cref="T:Common.Drawing.PointF" /> that specifies the location to test. </param>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to test. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsOutlineVisible(PointF point, Pen pen)
        {
            return WrappedGraphicsPath.IsOutlineVisible(point, pen);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" /> and using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to test. </param>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> for which to test visibility. </param>
        public bool IsOutlineVisible(float x, float y, Pen pen, Graphics graphics)
        {
            return WrappedGraphicsPath.IsOutlineVisible(x, y, pen, graphics);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" /> and using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />; otherwise, false.
        /// </returns>
        /// <param name="pt">A <see cref="T:Common.Drawing.PointF" /> that specifies the location to test. </param>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to test. </param>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> for which to test visibility. </param>
        public bool IsOutlineVisible(PointF pt, Pen pen, Graphics graphics)
        {
            return WrappedGraphicsPath.IsOutlineVisible(pt, pen, graphics);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to test. </param>
        public bool IsOutlineVisible(int x, int y, Pen pen)
        {
            return WrappedGraphicsPath.IsOutlineVisible(x, y, pen);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />; otherwise, false.
        /// </returns>
        /// <param name="point">A <see cref="T:Common.Drawing.Point" /> that specifies the location to test. </param>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to test. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsOutlineVisible(Point point, Pen pen)
        {
            return WrappedGraphicsPath.IsOutlineVisible(point, pen);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" /> and using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to test. </param>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> for which to test visibility. </param>
        public bool IsOutlineVisible(int x, int y, Pen pen, Graphics graphics)
        {
            return WrappedGraphicsPath.IsOutlineVisible(x, y, pen, graphics);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within (under) the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" /> and using the specified <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within the outline of this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified
        ///     <see cref="T:Common.Drawing.Pen" />; otherwise, false.
        /// </returns>
        /// <param name="pt">A <see cref="T:Common.Drawing.Point" /> that specifies the location to test. </param>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to test. </param>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> for which to test visibility. </param>
        public bool IsOutlineVisible(Point pt, Pen pen, Graphics graphics)
        {
            return WrappedGraphicsPath.IsOutlineVisible(pt, pen, graphics);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        public bool IsVisible(float x, float y)
        {
            return WrappedGraphicsPath.IsVisible(x, y);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.
        /// </returns>
        /// <param name="point">A <see cref="T:Common.Drawing.PointF" /> that represents the point to test. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(PointF point)
        {
            return WrappedGraphicsPath.IsVisible(point);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> in the visible clip region of the specified
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> for which to test visibility. </param>
        public bool IsVisible(float x, float y, Graphics graphics)
        {
            return WrappedGraphicsPath.IsVisible(x, y, graphics);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>This method returns true if the specified point is contained within this; otherwise, false.</returns>
        /// <param name="pt">A <see cref="T:Common.Drawing.PointF" /> that represents the point to test. </param>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> for which to test visibility. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(PointF pt, Graphics graphics)
        {
            return WrappedGraphicsPath.IsVisible(pt, graphics);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        public bool IsVisible(int x, int y)
        {
            return WrappedGraphicsPath.IsVisible(x, y);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.
        /// </returns>
        /// <param name="point">A <see cref="T:Common.Drawing.Point" /> that represents the point to test. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Point point)
        {
            return WrappedGraphicsPath.IsVisible(point);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />, using the specified
        ///     <see cref="T:Common.Drawing.Graphics" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> for which to test visibility. </param>
        public bool IsVisible(int x, int y, Graphics graphics)
        {
            return WrappedGraphicsPath.IsVisible(x, y, graphics);
        }

        /// <summary>
        ///     Indicates whether the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if the specified point is contained within this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.
        /// </returns>
        /// <param name="pt">A <see cref="T:Common.Drawing.Point" /> that represents the point to test. </param>
        /// <param name="graphics">The <see cref="T:Common.Drawing.Graphics" /> for which to test visibility. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Point pt, Graphics graphics)
        {
            return WrappedGraphicsPath.IsVisible(pt, graphics);
        }

        /// <summary>
        ///     Empties the <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathPoints" /> and
        ///     <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathTypes" /> arrays and sets the
        ///     <see cref="T:Common.Drawing.Drawing2D.FillMode" /> to <see cref="F:Common.Drawing.Drawing2D.FillMode.Alternate" />.
        /// </summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Reset()
        {
            WrappedGraphicsPath.Reset();
        }

        /// <summary>
        ///     Reverses the order of points in the <see cref="P:Common.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of
        ///     this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Reverse()
        {
            WrappedGraphicsPath.Reverse();
        }

        /// <summary>Sets a marker on this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetMarkers()
        {
            WrappedGraphicsPath.SetMarkers();
        }

        /// <summary>
        ///     Starts a new figure without closing the current figure. All subsequent points added to the path are added to
        ///     this new figure.
        /// </summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void StartFigure()
        {
            WrappedGraphicsPath.StartFigure();
        }

        /// <summary>Applies a transform matrix to this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <param name="matrix">A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that represents the transformation to apply. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Transform(Matrix matrix)
        {
            WrappedGraphicsPath.Transform(matrix);
        }

        /// <summary>
        ///     Applies a warp transform, defined by a rectangle and a parallelogram, to this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="destPoints">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram to
        ///     which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or
        ///     four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the
        ///     first three points.
        /// </param>
        /// <param name="srcRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that is transformed to
        ///     the parallelogram defined by <paramref name="destPoints" />.
        /// </param>
        public void Warp(PointF[] destPoints, RectangleF srcRect)
        {
            WrappedGraphicsPath.Warp(destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect);
        }

        /// <summary>
        ///     Applies a warp transform, defined by a rectangle and a parallelogram, to this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="destPoints">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram to
        ///     which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or
        ///     four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the
        ///     first three points.
        /// </param>
        /// <param name="srcRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that is transformed to
        ///     the parallelogram defined by <paramref name="destPoints" />.
        /// </param>
        /// <param name="matrix">
        ///     A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply
        ///     to the path.
        /// </param>
        public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix)
        {
            WrappedGraphicsPath.Warp(destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect, matrix);
        }

        /// <summary>
        ///     Applies a warp transform, defined by a rectangle and a parallelogram, to this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="destPoints">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that defines a parallelogram to
        ///     which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or
        ///     four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the
        ///     first three points.
        /// </param>
        /// <param name="srcRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that is transformed to
        ///     the parallelogram defined by <paramref name="destPoints" />.
        /// </param>
        /// <param name="matrix">
        ///     A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply
        ///     to the path.
        /// </param>
        /// <param name="warpMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this
        ///     warp operation uses perspective or bilinear mode.
        /// </param>
        public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode)
        {
            WrappedGraphicsPath.Warp(destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect, matrix,
                (System.Drawing.Drawing2D.WarpMode) warpMode);
        }

        /// <summary>
        ///     Applies a warp transform, defined by a rectangle and a parallelogram, to this
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <param name="destPoints">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that define a parallelogram to
        ///     which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or
        ///     four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the
        ///     first three points.
        /// </param>
        /// <param name="srcRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents the rectangle that is transformed to
        ///     the parallelogram defined by <paramref name="destPoints" />.
        /// </param>
        /// <param name="matrix">
        ///     A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply
        ///     to the path.
        /// </param>
        /// <param name="warpMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this
        ///     warp operation uses perspective or bilinear mode.
        /// </param>
        /// <param name="flatness">
        ///     A value from 0 through 1 that specifies how flat the resulting path is. For more information,
        ///     see the <see cref="M:Common.Drawing.Drawing2D.GraphicsPath.Flatten" /> methods.
        /// </param>
        public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode, float flatness)
        {
            WrappedGraphicsPath.Warp(destPoints.Convert<System.Drawing.PointF>().ToArray(), srcRect, matrix,
                (System.Drawing.Drawing2D.WarpMode) warpMode, flatness);
        }

        /// <summary>Adds an additional outline to the path.</summary>
        /// <param name="pen">
        ///     A <see cref="T:Common.Drawing.Pen" /> that specifies the width between the original outline of the
        ///     path and the new outline this method creates.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Widen(Pen pen)
        {
            WrappedGraphicsPath.Widen(pen);
        }

        /// <summary>Adds an additional outline to the <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.</summary>
        /// <param name="pen">
        ///     A <see cref="T:Common.Drawing.Pen" /> that specifies the width between the original outline of the
        ///     path and the new outline this method creates.
        /// </param>
        /// <param name="matrix">
        ///     A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path
        ///     before widening.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Widen(Pen pen, Matrix matrix)
        {
            WrappedGraphicsPath.Widen(pen, matrix);
        }

        /// <summary>
        ///     Replaces this <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> with curves that enclose the area that is
        ///     filled when this path is drawn by the specified pen.
        /// </summary>
        /// <param name="pen">
        ///     A <see cref="T:Common.Drawing.Pen" /> that specifies the width between the original outline of the
        ///     path and the new outline this method creates.
        /// </param>
        /// <param name="matrix">
        ///     A <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path
        ///     before widening.
        /// </param>
        /// <param name="flatness">A value that specifies the flatness for curves. </param>
        public void Widen(Pen pen, Matrix matrix, float flatness)
        {
            WrappedGraphicsPath.Widen(pen, matrix, flatness);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedGraphicsPath.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> that results from the conversion.</returns>
        /// <param name="graphicsPath">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator GraphicsPath(System.Drawing.Drawing2D.GraphicsPath graphicsPath)
        {
            return graphicsPath == null ? null : new GraphicsPath(graphicsPath);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that results from the conversion.</returns>
        /// <param name="graphicsPath">The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.GraphicsPath(GraphicsPath graphicsPath)
        {
            return graphicsPath?.WrappedGraphicsPath;
        }
    }
}