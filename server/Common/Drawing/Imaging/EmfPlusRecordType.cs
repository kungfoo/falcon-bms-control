namespace Common.Drawing.Imaging
{
    /// <summary>Specifies the methods available for use with a metafile to read and write graphic commands. </summary>
    public enum EmfPlusRecordType
    {
        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfRecordBase = 65536,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetBkColor = 66049,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetBkMode = 65794,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetMapMode,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetROP2,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetRelAbs,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetPolyFillMode,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetStretchBltMode,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetTextCharExtra,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetTextColor = 66057,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetTextJustification,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetWindowOrg,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetWindowExt,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetViewportOrg,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetViewportExt,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfOffsetWindowOrg,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfScaleWindowExt = 66576,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfOffsetViewportOrg = 66065,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfScaleViewportExt = 66578,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfLineTo = 66067,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfMoveTo,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfExcludeClipRect = 66581,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfIntersectClipRect,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfArc = 67607,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfEllipse = 66584,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfFloodFill,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfPie = 67610,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfRectangle = 66587,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfRoundRect = 67100,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfPatBlt,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSaveDC = 65566,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetPixel = 66591,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfOffsetCilpRgn = 66080,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfTextOut = 66849,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfBitBlt = 67874,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfStretchBlt = 68387,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfPolygon = 66340,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfPolyline,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfEscape = 67110,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfRestoreDC = 65831,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfFillRegion = 66088,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfFrameRegion = 66601,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfInvertRegion = 65834,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfPaintRegion,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSelectClipRegion,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSelectObject,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetTextAlign,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfChord = 67632,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetMapperFlags = 66097,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfExtTextOut = 68146,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetDibToDev = 68915,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSelectPalette = 66100,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfRealizePalette = 65589,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfAnimatePalette = 66614,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetPalEntries = 65591,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfPolyPolygon = 66872,

        /// <summary>Increases or decreases the size of a logical palette based on the specified value.</summary>
        WmfResizePalette = 65849,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfDibBitBlt = 67904,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfDibStretchBlt = 68417,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfDibCreatePatternBrush = 65858,

        /// <summary>Copies the color data for a rectangle of pixels in a DIB to the specified destination rectangle.</summary>
        WmfStretchDib = 69443,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfExtFloodFill = 66888,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfSetLayout = 65865,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfDeleteObject = 66032,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfCreatePalette = 65783,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfCreatePatternBrush = 66041,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfCreatePenIndirect = 66298,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfCreateFontIndirect,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfCreateBrushIndirect,

        /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        WmfCreateRegion = 67327,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfHeader = 1,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyBezier,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolygon,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyline,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyBezierTo,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyLineTo,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyPolyline,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyPolygon,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetWindowExtEx,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetWindowOrgEx,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetViewportExtEx,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetViewportOrgEx,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetBrushOrgEx,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfEof,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetPixelV,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetMapperFlags,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetMapMode,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetBkMode,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetPolyFillMode,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetROP2,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetStretchBltMode,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetTextAlign,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetColorAdjustment,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetTextColor,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetBkColor,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfOffsetClipRgn,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfMoveToEx,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetMetaRgn,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfExcludeClipRect,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfIntersectClipRect,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfScaleViewportExtEx,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfScaleWindowExtEx,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSaveDC,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfRestoreDC,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetWorldTransform,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfModifyWorldTransform,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSelectObject,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfCreatePen,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfCreateBrushIndirect,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfDeleteObject,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfAngleArc,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfEllipse,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfRectangle,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfRoundRect,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfRoundArc,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfChord,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPie,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSelectPalette,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfCreatePalette,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetPaletteEntries,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfResizePalette,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfRealizePalette,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfExtFloodFill,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfLineTo,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfArcTo,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyDraw,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetArcDirection,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetMiterLimit,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfBeginPath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfEndPath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfCloseFigure,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfFillPath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfStrokeAndFillPath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfStrokePath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfFlattenPath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfWidenPath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSelectClipPath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfAbortPath,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfReserved069,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfGdiComment,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfFillRgn,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfFrameRgn,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfInvertRgn,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPaintRgn,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfExtSelectClipRgn,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfBitBlt,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfStretchBlt,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfMaskBlt,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPlgBlt,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetDIBitsToDevice,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfStretchDIBits,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfExtCreateFontIndirect,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfExtTextOutA,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfExtTextOutW,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyBezier16,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolygon16,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyline16,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyBezierTo16,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolylineTo16,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyPolyline16,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyPolygon16,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyDraw16,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfCreateMonoBrush,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfCreateDibPatternBrushPt,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfExtCreatePen,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyTextOutA,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPolyTextOutW,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetIcmMode,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfCreateColorSpace,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetColorSpace,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfDeleteColorSpace,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfGlsRecord,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfGlsBoundedRecord,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPixelFormat,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfDrawEscape,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfExtEscape,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfStartDoc,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSmallTextOut,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfForceUfiMapping,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfNamedEscpae,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfColorCorrectPalette,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetIcmProfileA,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetIcmProfileW,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfAlphaBlend,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetLayout,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfTransparentBlt,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfReserved117,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfGradientFill,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetLinkedUfis,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfSetTextJustification,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfColorMatchToTargetW,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfCreateColorSpaceW,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfMax = 122,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfMin = 1,

        /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
        EmfPlusRecordBase = 16384,

        /// <summary>Indicates invalid data.</summary>
        Invalid = 16384,

        /// <summary>Identifies a record that is the EMF+ header.</summary>
        Header,

        /// <summary>Identifies a record that marks the last EMF+ record of a metafile.</summary>
        EndOfFile,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.AddMetafileComment(System.Byte[])" />.</summary>
        Comment,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.GetHdc" />.</summary>
        GetDC,

        /// <summary>Marks the start of a multiple-format section.</summary>
        MultiFormatStart,

        /// <summary>Marks a multiple-format section.</summary>
        MultiFormatSection,

        /// <summary>Marks the end of a multiple-format section.</summary>
        MultiFormatEnd,

        /// <summary>Marks an object.</summary>
        Object,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.Clear(Common.Drawing.Color)" />.</summary>
        Clear,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.FillRectangles" /> methods.</summary>
        FillRects,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawRectangles" /> methods.</summary>
        DrawRects,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.FillPolygon" /> methods.</summary>
        FillPolygon,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawLines" /> methods.</summary>
        DrawLines,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.FillEllipse" /> methods.</summary>
        FillEllipse,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawEllipse" /> methods.</summary>
        DrawEllipse,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.FillPie" /> methods.</summary>
        FillPie,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawPie" /> methods.</summary>
        DrawPie,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawArc" /> methods.</summary>
        DrawArc,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.FillRegion(Common.Drawing.Brush,Common.Drawing.Region)" />.</summary>
        FillRegion,

        /// <summary>
        ///     See
        ///     <see cref="M:Common.Drawing.Graphics.FillPath(Common.Drawing.Brush,Common.Drawing.Drawing2D.GraphicsPath)" />.
        /// </summary>
        FillPath,

        /// <summary>
        ///     See
        ///     <see cref="M:Common.Drawing.Graphics.DrawPath(Common.Drawing.Pen,Common.Drawing.Drawing2D.GraphicsPath)" />.
        /// </summary>
        DrawPath,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.FillClosedCurve" /> methods.</summary>
        FillClosedCurve,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawClosedCurve" /> methods.</summary>
        DrawClosedCurve,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawCurve" /> methods.</summary>
        DrawCurve,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawBeziers" /> methods.</summary>
        DrawBeziers,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawImage" /> methods.</summary>
        DrawImage,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawImage" /> methods.</summary>
        DrawImagePoints,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.DrawString" /> methods.</summary>
        DrawString,

        /// <summary>See <see cref="P:Common.Drawing.Graphics.RenderingOrigin" />.</summary>
        SetRenderingOrigin,

        /// <summary>See <see cref="P:Common.Drawing.Graphics.SmoothingMode" />.</summary>
        SetAntiAliasMode,

        /// <summary>See <see cref="P:Common.Drawing.Graphics.TextRenderingHint" />.</summary>
        SetTextRenderingHint,

        /// <summary>See <see cref="P:Common.Drawing.Graphics.TextContrast" />.</summary>
        SetTextContrast,

        /// <summary>See <see cref="P:Common.Drawing.Graphics.InterpolationMode" />.</summary>
        SetInterpolationMode,

        /// <summary>See <see cref="P:Common.Drawing.Graphics.PixelOffsetMode" />.</summary>
        SetPixelOffsetMode,

        /// <summary>See <see cref="P:Common.Drawing.Graphics.CompositingMode" />.</summary>
        SetCompositingMode,

        /// <summary>See <see cref="P:Common.Drawing.Graphics.CompositingQuality" />.</summary>
        SetCompositingQuality,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.Save" />.</summary>
        Save,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.Restore(Common.Drawing.Drawing2D.GraphicsState)" />.</summary>
        Restore,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.BeginContainer" /> methods.</summary>
        BeginContainer,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.BeginContainer" /> methods.</summary>
        BeginContainerNoParams,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.EndContainer(Common.Drawing.Drawing2D.GraphicsContainer)" />.</summary>
        EndContainer,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.TransformPoints" /> methods.</summary>
        SetWorldTransform,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.ResetTransform" />.</summary>
        ResetWorldTransform,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.MultiplyTransform" /> methods.</summary>
        MultiplyWorldTransform,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.TransformPoints" /> methods.</summary>
        TranslateWorldTransform,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.ScaleTransform" /> methods.</summary>
        ScaleWorldTransform,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.RotateTransform" /> methods.</summary>
        RotateWorldTransform,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.TransformPoints" /> methods.</summary>
        SetPageTransform,

        /// <summary>See <see cref="M:Common.Drawing.Graphics.ResetClip" />.</summary>
        ResetClip,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.SetClip" /> methods.</summary>
        SetClipRect,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.SetClip" /> methods.</summary>
        SetClipPath,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.SetClip" /> methods.</summary>
        SetClipRegion,

        /// <summary>See <see cref="Overload:Common.Drawing.Graphics.TranslateClip" /> methods.</summary>
        OffsetClip,

        /// <summary>Specifies a character string, a location, and formatting information.</summary>
        DrawDriverString,

        /// <summary>Used internally.</summary>
        Total,

        /// <summary>The maximum value for this enumeration.</summary>
        Max = 16438,

        /// <summary>The minimum value for this enumeration.</summary>
        Min = 16385
    }
}