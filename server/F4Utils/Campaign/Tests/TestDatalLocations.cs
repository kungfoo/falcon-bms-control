using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.Tests
{
    internal class TestDatalLocations
    {
        public const string BMSBasePath = @"C:\Falcon BMS 4.33 RC6 (Internal)\";
        public const string BMSDataFolder = @"Data\";
        public const string TerrainBaseFolder = BMSBasePath + BMSDataFolder + @"Terrdata\";
        public const string TerrainObjectsFolder = TerrainBaseFolder + @"Objects\";
        public const string AcdFile= TerrainObjectsFolder + @"FALCON4.ACD";
        public const string DdpFile = TerrainObjectsFolder + @"FALCON4.DDP";
        public const string ClassTable = TerrainObjectsFolder + @"FALCON4.CT";
        public const string TestDataOutputFolder = @"C:\lightningtools_testjunk\";
    }
}
