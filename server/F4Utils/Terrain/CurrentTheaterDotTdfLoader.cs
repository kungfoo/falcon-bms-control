using System.IO;
using F4Utils.Terrain.Structs;

namespace F4Utils.Terrain
{
    public interface ICurrentTheaterDotTdfLoader
    {
        TheaterDotTdfFileInfo GetCurrentTheaterDotTdf(string bmsBaseDirectory);
    }
    public class CurrentTheaterDotTdfLoader:ICurrentTheaterDotTdfLoader
    {
        private readonly ICurrentTheaterNameDetector _currentTheaterNameDetector;
        private readonly ITheaterDotTdfFileReader _theaterDotTdfFileReader;
        public CurrentTheaterDotTdfLoader(ICurrentTheaterNameDetector currentTheaterNameDetector = null,
            ITheaterDotTdfFileReader theaterDotTdfFileReader = null)
        {
            _currentTheaterNameDetector = currentTheaterNameDetector ?? new CurrentTheaterNameDetector();
            _theaterDotTdfFileReader = theaterDotTdfFileReader ?? new TheaterDotTdfFileReader();
        }
        public TheaterDotTdfFileInfo GetCurrentTheaterDotTdf(string bmsBaseDirectory)
        {
            if (bmsBaseDirectory == null) return null;
            var currentTheaterName = _currentTheaterNameDetector.DetectCurrentTheaterName(bmsBaseDirectory);
            if (currentTheaterName == null) return null;
            var theaterDotLstFI =
                new FileInfo(new DirectoryInfo(bmsBaseDirectory).FullName + Path.DirectorySeparatorChar +
                                "data" + Path.DirectorySeparatorChar + 
                                "terrdata" + Path.DirectorySeparatorChar + 
                                "theaterdefinition" + Path.DirectorySeparatorChar + 
                                "theater.lst");

            if (theaterDotLstFI.Exists)
            {
                using (var fs = new FileStream(theaterDotLstFI.FullName, FileMode.Open, FileAccess.Read))
                using (var sw = new StreamReader(fs))
                {
                    while (!sw.EndOfStream)
                    {
                        var thisLine = sw.ReadLine();
                        var tdfDetailsThisLine = _theaterDotTdfFileReader.ReadTheaterDotTdfFile(bmsBaseDirectory + Path.DirectorySeparatorChar + "data" + Path.DirectorySeparatorChar + thisLine);
                        if (tdfDetailsThisLine != null)
                        {
                            if (tdfDetailsThisLine.theaterName != null &&
                                tdfDetailsThisLine.theaterName.ToLower().Trim() ==
                                currentTheaterName.ToLower().Trim())
                            {
                                return tdfDetailsThisLine;
                            }
                        }
                    }
                }
            }
            return null;
        }

    }
}
