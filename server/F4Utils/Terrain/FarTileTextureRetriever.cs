using System;
using Common.Drawing;
using Common.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Common.Imaging;
using Common.Win32;
using System.Collections.Generic;

namespace F4Utils.Terrain
{
    internal interface IFarTileTextureRetriever
    {
        Bitmap GetFarTileTexture(uint textureId);
    }
    internal class FarTileTextureRetriever:IFarTileTextureRetriever
    {
        private readonly TerrainDB _terrainDB;
        public FarTileTextureRetriever(TerrainDB terrainDB)
        {
            _terrainDB = terrainDB;
        }
        public Bitmap GetFarTileTexture(uint textureId)
        {
            if (String.IsNullOrEmpty(_terrainDB.FarTilesDotDdsFilePath) && (String.IsNullOrEmpty(_terrainDB.FarTilesDotRawFilePath)))
                return null;

            if (_terrainDB.FarTilesDotDdsFilePath != null)
            {
                var fileInfo = new FileInfo(_terrainDB.FarTilesDotDdsFilePath);
                var useDDS = true;
                if (!fileInfo.Exists)
                {
                    useDDS = false;
                    fileInfo = new FileInfo(_terrainDB.FarTilesDotRawFilePath);
                    if (!fileInfo.Exists) return null;
                }


                Bitmap bitmap;
                if (useDDS)
                {
                    using (var stream = File.OpenRead(_terrainDB.FarTilesDotDdsFilePath))
                    {
                        var headerSize = Marshal.SizeOf(typeof(NativeMethods.DDSURFACEDESC2));
                        var header = new byte[headerSize];
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Read(header, 0, headerSize);

                        var pinnedHeader = GCHandle.Alloc(header, GCHandleType.Pinned);
                        var surfaceDesc =
                            (NativeMethods.DDSURFACEDESC2)
                            Marshal.PtrToStructure(pinnedHeader.AddrOfPinnedObject(),
                                                   typeof(NativeMethods.DDSURFACEDESC2));
                        pinnedHeader.Free();

                        var imageSize = ((surfaceDesc.dwFlags & NativeMethods.DDSD_PITCH) == NativeMethods.DDSD_PITCH)
                                            ? surfaceDesc.dwHeight * surfaceDesc.lPitch
                                            : surfaceDesc.dwLinearSize;
                        var ddsBytes = new byte[headerSize + 4 + imageSize];
                        ddsBytes[0] = 0x44;
                        ddsBytes[1] = 0x44;
                        ddsBytes[2] = 0x53;
                        ddsBytes[3] = 0x20;
                        Array.Copy(header, 0, ddsBytes, 4, header.Length);
                        stream.Seek((imageSize * textureId), SeekOrigin.Current);
                        stream.Read(ddsBytes, headerSize + 4, imageSize);
                        bitmap = DDS.GetBitmapFromDDSFileBytes(ddsBytes);

                        stream.Close();
                    }
                }
                else
                {
                    bitmap = new Bitmap(32, 32, PixelFormat.Format8bppIndexed);
                    var pal = bitmap.Palette;
                    for (var i = 0; i < 256; i++)
                    {
                        pal.Entries[i] = _terrainDB.FarTilesDotPal.pallete[i];
                    }
                    bitmap.Palette = pal;
                    using (var stream = File.OpenRead(_terrainDB.FarTilesDotRawFilePath))
                    {
                        const int imageSizeBytes = 32 * 32;
                        stream.Seek(imageSizeBytes * textureId, SeekOrigin.Begin);
                        var bytesRead = new byte[imageSizeBytes];
                        stream.Read(bytesRead, 0, imageSizeBytes);
                        var lockData = bitmap.LockBits(new Rectangle(0, 0, 32, 32), ImageLockMode.WriteOnly,
                                                       bitmap.PixelFormat);
                        var scan0 = lockData.Scan0;
                        var height = lockData.Height;
                        var width = lockData.Width;
                        Marshal.Copy(bytesRead, 0, scan0, width * height);
                        bitmap.UnlockBits(lockData);
                        stream.Close();
                    }
                }
                return bitmap;
            }
            return null;
        }


    }
}
