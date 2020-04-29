using System;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;

namespace Common.Drawing.Imaging
{
    /// <summary>Contains information about how bitmap and metafile colors are manipulated during rendering. </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class ImageAttributes : ICloneable, IDisposable
    {
        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> class.</summary>
        public ImageAttributes()
        {
            WrappedImageAttributes = new System.Drawing.Imaging.ImageAttributes();
        }

        private ImageAttributes(System.Drawing.Imaging.ImageAttributes imageAttributes)
        {
            WrappedImageAttributes = imageAttributes;
        }

        private System.Drawing.Imaging.ImageAttributes WrappedImageAttributes { get; }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> object.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> object this class creates, cast as an object.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public object Clone()
        {
            return new ImageAttributes((System.Drawing.Imaging.ImageAttributes) WrappedImageAttributes.Clone());
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> object.</summary>
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

        ~ImageAttributes()
        {
            Dispose(false);
        }

        /// <summary>Clears the brush color-remap table of this <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> object.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearBrushRemapTable()
        {
            WrappedImageAttributes.ClearBrushRemapTable();
        }

        /// <summary>Clears the color key (transparency range) for the default category.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearColorKey()
        {
            WrappedImageAttributes.ClearColorKey();
        }

        /// <summary>Clears the color key (transparency range) for a specified category.</summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the color key is cleared.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearColorKey(ColorAdjustType type)
        {
            WrappedImageAttributes.ClearColorKey((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Clears the color-adjustment matrix for the default category.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearColorMatrix()
        {
            WrappedImageAttributes.ClearColorMatrix();
        }

        /// <summary>Clears the color-adjustment matrix for a specified category.</summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the color-adjustment matrix is cleared.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearColorMatrix(ColorAdjustType type)
        {
            WrappedImageAttributes.ClearColorMatrix((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Disables gamma correction for the default category.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearGamma()
        {
            WrappedImageAttributes.ClearGamma();
        }

        /// <summary>Disables gamma correction for a specified category.</summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which gamma correction is disabled.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearGamma(ColorAdjustType type)
        {
            WrappedImageAttributes.ClearGamma((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Clears the NoOp setting for the default category.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearNoOp()
        {
            WrappedImageAttributes.ClearNoOp();
        }

        /// <summary>Clears the NoOp setting for a specified category.</summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the NoOp setting is cleared.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearNoOp(ColorAdjustType type)
        {
            WrappedImageAttributes.ClearNoOp((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Clears the CMYK (cyan-magenta-yellow-black) output channel setting for the default category.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearOutputChannel()
        {
            WrappedImageAttributes.ClearOutputChannel();
        }

        /// <summary>Clears the (cyan-magenta-yellow-black) output channel setting for a specified category.</summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the output channel setting is cleared.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearOutputChannel(ColorAdjustType type)
        {
            WrappedImageAttributes.ClearOutputChannel((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Clears the output channel color profile setting for the default category.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearOutputChannelColorProfile()
        {
            WrappedImageAttributes.ClearOutputChannelColorProfile();
        }

        /// <summary>Clears the output channel color profile setting for a specified category.</summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the output channel profile setting is cleared.
        /// </param>
        public void ClearOutputChannelColorProfile(ColorAdjustType type)
        {
            WrappedImageAttributes.ClearOutputChannelColorProfile((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Clears the color-remap table for the default category.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearRemapTable()
        {
            WrappedImageAttributes.ClearRemapTable();
        }

        /// <summary>Clears the color-remap table for a specified category.</summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the remap table is cleared.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearRemapTable(ColorAdjustType type)
        {
            WrappedImageAttributes.ClearRemapTable((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Clears the threshold value for the default category.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearThreshold()
        {
            WrappedImageAttributes.ClearThreshold();
        }

        /// <summary>Clears the threshold value for a specified category.</summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the threshold is cleared.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ClearThreshold(ColorAdjustType type)
        {
            WrappedImageAttributes.ClearThreshold((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Adjusts the colors in a palette according to the adjustment settings of a specified category.</summary>
        /// <param name="palette">
        ///     A <see cref="T:Common.Drawing.Imaging.ColorPalette" /> that on input contains the palette to be
        ///     adjusted, and on output contains the adjusted palette.
        /// </param>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     whose adjustment settings will be applied to the palette.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void GetAdjustedPalette(ColorPalette palette, ColorAdjustType type)
        {
            WrappedImageAttributes.GetAdjustedPalette(palette, (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Sets the color-remap table for the brush category.</summary>
        /// <param name="map">An array of <see cref="T:Common.Drawing.Imaging.ColorMap" /> objects. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetBrushRemapTable(ColorMap[] map)
        {
            WrappedImageAttributes.SetBrushRemapTable(map.Convert<System.Drawing.Imaging.ColorMap>().ToArray());
        }

        /// <summary>Sets the color key for the default category.</summary>
        /// <param name="colorLow">The low color-key value. </param>
        /// <param name="colorHigh">The high color-key value. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetColorKey(Color colorLow, Color colorHigh)
        {
            WrappedImageAttributes.SetColorKey(colorLow, colorHigh);
        }

        /// <summary>Sets the color key (transparency range) for a specified category.</summary>
        /// <param name="colorLow">The low color-key value. </param>
        /// <param name="colorHigh">The high color-key value. </param>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the color key is set.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetColorKey(Color colorLow, Color colorHigh, ColorAdjustType type)
        {
            WrappedImageAttributes.SetColorKey(colorLow, colorHigh, (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
        /// <param name="newColorMatrix">The color-adjustment matrix. </param>
        /// <param name="grayMatrix">The grayscale-adjustment matrix. </param>
        public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix)
        {
            WrappedImageAttributes.SetColorMatrices(newColorMatrix, grayMatrix);
        }

        /// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
        /// <param name="newColorMatrix">The color-adjustment matrix. </param>
        /// <param name="grayMatrix">The grayscale-adjustment matrix. </param>
        /// <param name="flags">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of
        ///     image and color that will be affected by the color-adjustment and grayscale-adjustment matrices.
        /// </param>
        public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag flags)
        {
            WrappedImageAttributes.SetColorMatrices(newColorMatrix, grayMatrix,
                (System.Drawing.Imaging.ColorMatrixFlag) flags);
        }

        /// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for a specified category.</summary>
        /// <param name="newColorMatrix">The color-adjustment matrix. </param>
        /// <param name="grayMatrix">The grayscale-adjustment matrix. </param>
        /// <param name="mode">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of
        ///     image and color that will be affected by the color-adjustment and grayscale-adjustment matrices.
        /// </param>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the color-adjustment and grayscale-adjustment matrices are set.
        /// </param>
        public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag mode,
            ColorAdjustType type)
        {
            WrappedImageAttributes.SetColorMatrices(newColorMatrix, grayMatrix,
                (System.Drawing.Imaging.ColorMatrixFlag) mode, (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Sets the color-adjustment matrix for the default category.</summary>
        /// <param name="newColorMatrix">The color-adjustment matrix. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetColorMatrix(ColorMatrix newColorMatrix)
        {
            WrappedImageAttributes.SetColorMatrix(newColorMatrix);
        }

        /// <summary>Sets the color-adjustment matrix for the default category.</summary>
        /// <param name="newColorMatrix">The color-adjustment matrix. </param>
        /// <param name="flags">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of
        ///     image and color that will be affected by the color-adjustment matrix.
        /// </param>
        public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag flags)
        {
            WrappedImageAttributes.SetColorMatrix(newColorMatrix, (System.Drawing.Imaging.ColorMatrixFlag) flags);
        }

        /// <summary>Sets the color-adjustment matrix for a specified category.</summary>
        /// <param name="newColorMatrix">The color-adjustment matrix. </param>
        /// <param name="mode">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of
        ///     image and color that will be affected by the color-adjustment matrix.
        /// </param>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the color-adjustment matrix is set.
        /// </param>
        public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag mode, ColorAdjustType type)
        {
            WrappedImageAttributes.SetColorMatrix(newColorMatrix, (System.Drawing.Imaging.ColorMatrixFlag) mode,
                (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Sets the gamma value for the default category.</summary>
        /// <param name="gamma">The gamma correction value. </param>
        public void SetGamma(float gamma)
        {
            WrappedImageAttributes.SetGamma(gamma);
        }

        /// <summary>Sets the gamma value for a specified category.</summary>
        /// <param name="gamma">The gamma correction value. </param>
        /// <param name="type">
        ///     An element of the <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> enumeration that specifies
        ///     the category for which the gamma value is set.
        /// </param>
        public void SetGamma(float gamma, ColorAdjustType type)
        {
            WrappedImageAttributes.SetGamma(gamma, (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>
        ///     Turns off color adjustment for the default category. You can call the
        ///     <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment
        ///     settings that were in place before the call to the
        ///     <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.
        /// </summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetNoOp()
        {
            WrappedImageAttributes.SetNoOp();
        }

        /// <summary>
        ///     Turns off color adjustment for a specified category. You can call the
        ///     <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment
        ///     settings that were in place before the call to the
        ///     <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.
        /// </summary>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which color correction is turned off.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetNoOp(ColorAdjustType type)
        {
            WrappedImageAttributes.SetNoOp((System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for the default category.</summary>
        /// <param name="flags">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorChannelFlag" /> that specifies the output
        ///     channel.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetOutputChannel(ColorChannelFlag flags)
        {
            WrappedImageAttributes.SetOutputChannel((System.Drawing.Imaging.ColorChannelFlag) flags);
        }

        /// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for a specified category.</summary>
        /// <param name="flags">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorChannelFlag" /> that specifies the output
        ///     channel.
        /// </param>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the output channel is set.
        /// </param>
        public void SetOutputChannel(ColorChannelFlag flags, ColorAdjustType type)
        {
            WrappedImageAttributes.SetOutputChannel((System.Drawing.Imaging.ColorChannelFlag) flags,
                (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Sets the output channel color-profile file for the default category.</summary>
        /// <param name="colorProfileFilename">
        ///     The path name of a color-profile file. If the color-profile file is in the
        ///     %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter
        ///     must be the fully qualified path name.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetOutputChannelColorProfile(string colorProfileFilename)
        {
            WrappedImageAttributes.SetOutputChannelColorProfile(colorProfileFilename);
        }

        /// <summary>Sets the output channel color-profile file for a specified category.</summary>
        /// <param name="colorProfileFilename">
        ///     The path name of a color-profile file. If the color-profile file is in the
        ///     %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter
        ///     must be the fully qualified path name.
        /// </param>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the output channel color-profile file is set.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetOutputChannelColorProfile(string colorProfileFilename, ColorAdjustType type)
        {
            WrappedImageAttributes.SetOutputChannelColorProfile(colorProfileFilename,
                (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Sets the color-remap table for the default category.</summary>
        /// <param name="map">
        ///     An array of color pairs of type <see cref="T:Common.Drawing.Imaging.ColorMap" />. Each color pair
        ///     contains an existing color (the first value) and the color that it will be mapped to (the second value).
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetRemapTable(ColorMap[] map)
        {
            WrappedImageAttributes.SetRemapTable(map.Convert<System.Drawing.Imaging.ColorMap>().ToArray());
        }

        /// <summary>Sets the color-remap table for a specified category.</summary>
        /// <param name="map">
        ///     An array of color pairs of type <see cref="T:Common.Drawing.Imaging.ColorMap" />. Each color pair
        ///     contains an existing color (the first value) and the color that it will be mapped to (the second value).
        /// </param>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the color-remap table is set.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetRemapTable(ColorMap[] map, ColorAdjustType type)
        {
            WrappedImageAttributes.SetRemapTable(map.Convert<System.Drawing.Imaging.ColorMap>().ToArray(),
                (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>Sets the threshold (transparency range) for the default category.</summary>
        /// <param name="threshold">A real number that specifies the threshold value. </param>
        public void SetThreshold(float threshold)
        {
            WrappedImageAttributes.SetThreshold(threshold);
        }

        /// <summary>Sets the threshold (transparency range) for a specified category.</summary>
        /// <param name="threshold">
        ///     A threshold value from 0.0 to 1.0 that is used as a breakpoint to sort colors that will be
        ///     mapped to either a maximum or a minimum value.
        /// </param>
        /// <param name="type">
        ///     An element of <see cref="T:Common.Drawing.Imaging.ColorAdjustType" /> that specifies the category
        ///     for which the color threshold is set.
        /// </param>
        public void SetThreshold(float threshold, ColorAdjustType type)
        {
            WrappedImageAttributes.SetThreshold(threshold, (System.Drawing.Imaging.ColorAdjustType) type);
        }

        /// <summary>
        ///     Sets the wrap mode that is used to decide how to tile a texture across a shape, or at shape boundaries. A
        ///     texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.
        /// </summary>
        /// <param name="mode">
        ///     An element of <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies
        ///     of an image are used to tile an area.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetWrapMode(WrapMode mode)
        {
            WrappedImageAttributes.SetWrapMode(mode);
        }

        /// <summary>
        ///     Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A
        ///     texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.
        /// </summary>
        /// <param name="mode">
        ///     An element of <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies
        ///     of an image are used to tile an area.
        /// </param>
        /// <param name="color">
        ///     An <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> object that specifies the color of
        ///     pixels outside of a rendered image. This color is visible if the mode parameter is set to
        ///     <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to
        ///     <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetWrapMode(WrapMode mode, Color color)
        {
            WrappedImageAttributes.SetWrapMode(mode, color);
        }

        /// <summary>
        ///     Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A
        ///     texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.
        /// </summary>
        /// <param name="mode">
        ///     An element of <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies
        ///     of an image are used to tile an area.
        /// </param>
        /// <param name="color">
        ///     A color object that specifies the color of pixels outside of a rendered image. This color is
        ///     visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source
        ///     rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself.
        /// </param>
        /// <param name="clamp">This parameter has no effect. Set it to false. </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void SetWrapMode(WrapMode mode, Color color, bool clamp)
        {
            WrappedImageAttributes.SetWrapMode(mode, color, clamp);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedImageAttributes.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.ImageAttributes" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> that results from the conversion.</returns>
        /// <param name="imageAttributes">The <see cref="T:System.Drawing.Imaging.ImageAttributes" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator ImageAttributes(System.Drawing.Imaging.ImageAttributes imageAttributes)
        {
            return imageAttributes == null ? null : new ImageAttributes(imageAttributes);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> to a
        ///     <see cref="T:System.Drawing.Imaging.ImageAttributes" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that results from the conversion.</returns>
        /// <param name="imageAttributes">The <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.ImageAttributes(ImageAttributes imageAttributes)
        {
            return imageAttributes?.WrappedImageAttributes;
        }
    }
}