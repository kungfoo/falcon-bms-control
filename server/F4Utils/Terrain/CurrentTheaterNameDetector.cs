using System;
using System.IO;
using F4Utils.Process;
using log4net;
using Microsoft.Win32;

namespace F4Utils.Terrain
{
    public interface ICurrentTheaterNameDetector
    {
        string DetectCurrentTheaterName(string bmsBaseDirectory);
    }
    public class CurrentTheaterNameDetector : ICurrentTheaterNameDetector
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CurrentTheaterNameDetector));
        public string DetectCurrentTheaterName(string bmsBaseDirectory)
        {
            string theaterName = null;
           
            try
            {
                var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Benchmark Sims");
                if (key != null)
                {
                    var subkeys = key.GetSubKeyNames();
                    if (subkeys != null && subkeys.Length > 0)
                    {
                        foreach (var subkey in subkeys)
                        {
                            var toRead = key.OpenSubKey(subkey, false);
                            if (toRead != null)
                            {
                                var baseDir = (string)toRead.GetValue("baseDir", null);
                                var bmsBaseDirectoryFI = new FileInfo(bmsBaseDirectory);
                                if (baseDir != null && bmsBaseDirectoryFI.FullName.TrimEnd('\\').Equals(baseDir.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase))
                                {
                                    theaterName = (string)toRead.GetValue("curTheater", null);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(theaterName)) theaterName = theaterName.Trim();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                theaterName = null;
            }
            return theaterName;
        }
    }
}
