using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace F4Utils.Terrain
{
    internal interface ITileSetConfigValueReader
    {
        string TileSetConfigValue { get; }
    }

    internal class TileSetConfigValueReader : ITileSetConfigValueReader
    {
        private string _bmsBaseDir;
        public TileSetConfigValueReader(string bmsBaseDir) { _bmsBaseDir = bmsBaseDir; }
        public string TileSetConfigValue
        {
            get
            {
                var configLines = new List<string>();
                var configFolder = Path.Combine(_bmsBaseDir, @"\Config");
                var bmsConfigFile = Path.Combine(configFolder, "Falcon BMS.config");
                using (var sr = new StreamReader(bmsConfigFile))
                {
                    while (!sr.EndOfStream)
                    {
                        configLines.Add(sr.ReadLine());
                    }
                }
            
                var matchingConfigLine = configLines
                    .Select(Common.Strings.Util.Tokenize)
                    .Where(tokens =>
                        tokens.Count > 2 &&
                        tokens[0].ToLowerInvariant() == "set" &&
                        tokens[1].ToLowerInvariant() == "g_sTileSet".ToLowerInvariant())
                    .FirstOrDefault();

                if (matchingConfigLine ==null) return null;
                            
                return matchingConfigLine
                    .Skip(2)
                    .Take(1)
                    .Select(x=>x.TrimStart('\"').TrimEnd('\"').Trim())
                    .FirstOrDefault();
                    
            }
        }
    }
}
