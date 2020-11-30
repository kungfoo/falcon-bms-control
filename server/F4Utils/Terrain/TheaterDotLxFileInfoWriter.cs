using System;
using System.IO;
using F4Utils.Terrain.Structs;

namespace F4Utils.Terrain
{
    internal interface ITheaterDotLxFileInfoWriter
    {
        void WriteTheaterDotOxAndLxFiles(TheaterDotLxFileInfo theaterDotLxFileInfo, string theaterDotMapFilePath, string tileset= null);
    }

    internal class TheaterDotLxFileInfoWriter : ITheaterDotLxFileInfoWriter
    {
        public void WriteTheaterDotOxAndLxFiles(TheaterDotLxFileInfo theaterDotLxFileInfo, string theaterDotMapFilePath, string tileset = null)
        {
            if (String.IsNullOrEmpty(theaterDotMapFilePath)) throw new ArgumentNullException(nameof(theaterDotMapFilePath));
            if (theaterDotLxFileInfo.L == null || theaterDotLxFileInfo.L.Length == 0) throw new InvalidOperationException(nameof(theaterDotLxFileInfo) + ".L[] is null or zero-length. Nothing to write!");
            if (theaterDotLxFileInfo.O == null || theaterDotLxFileInfo.O.Length == 0) throw new InvalidOperationException(nameof(theaterDotLxFileInfo) + ".O[] is null or zero-length. Nothing to write!");

            var lFileInfo =
                new FileInfo(Path.GetDirectoryName(theaterDotMapFilePath) + Path.DirectorySeparatorChar
                    + "THEATER"
                    + (tileset != null ? "_" + tileset : "")
                    + ".L" + theaterDotLxFileInfo.LoDLevel);

            WriteTheaterDotLxFile(theaterDotLxFileInfo.L, lFileInfo.FullName, theaterDotLxFileInfo.LRecordSizeBytes >0 ? theaterDotLxFileInfo.LRecordSizeBytes : 9);

            var oFileInfo =
                new FileInfo(Path.GetDirectoryName(theaterDotMapFilePath) + Path.DirectorySeparatorChar
                    + "THEATER"
                    + (tileset != null ? "_" + tileset : "")
                    + ".O" + theaterDotLxFileInfo.LoDLevel);
            WriteTheaterDotOxFile(theaterDotLxFileInfo.O, oFileInfo.FullName, theaterDotLxFileInfo.LRecordSizeBytes == 7 ? 2u : 4u);
        }

        private static void WriteTheaterDotOxFile(TheaterDotOxFileRecord[] oRecords, string filePath, uint oRecordSize)
        {
            if (oRecordSize != 2 && oRecordSize != 4) throw new ArgumentOutOfRangeException(nameof(oRecordSize));

            using (var writer = new BinaryWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write)))
            {
                foreach (var oRecord in oRecords)
                {
                    if (oRecordSize == 4)
                    {
                        writer.Write(oRecord.LRecordStartingOffset);
                    }
                    else if (oRecordSize == 2)
                    {
                        writer.Write((ushort)oRecord.LRecordStartingOffset);
                    }
                }
                writer.Flush();
                writer.Close();
            }
        }
        private static void WriteTheaterDotLxFile(TheaterDotLxFileRecord[] lRecords, string filePath, uint lRecordSize)
        {
            if (lRecordSize != 7 && lRecordSize != 9) throw new ArgumentOutOfRangeException(nameof(lRecordSize));

            using (var writer = new BinaryWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write)))
            {
                foreach (var lRecord in lRecords)
                {
                    if (lRecordSize == 9)
                    {
                        writer.Write(lRecord.TextureId);
                    }
                    else if (lRecordSize == 7)
                    {
                        writer.Write((ushort)lRecord.TextureId);
                    }
                    writer.Write(lRecord.Elevation);
                    writer.Write(lRecord.Pallete);
                    writer.Write(lRecord.X1);
                    writer.Write(lRecord.X2);
                }
                writer.Flush();
                writer.Close();
            }
        }
    }
}
