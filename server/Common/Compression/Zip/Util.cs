using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;

namespace Common.Compression.Zip
{
    public static class Util
    {
        public static Dictionary<string, ZipEntry> GetZipFileEntries(ZipFile zipFile)
        {
            var toReturn = new Dictionary<string, ZipEntry>();
            var zipEntries = zipFile.GetEnumerator();
            while (zipEntries.MoveNext())
            {
                var thisEntry = (ZipEntry) zipEntries.Current;
                if (thisEntry != null) toReturn.Add(thisEntry.Name, thisEntry);
            }
            return toReturn;
        }
    }
}