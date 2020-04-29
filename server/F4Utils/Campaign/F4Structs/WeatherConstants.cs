using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.F4Structs
{
    public class WeatherConstants
    {
        public const float R360 = .0027777777777778f;
        public const float HTMS = 3600000;
        public const float MSTH = 2.7777777777777778e-7f;
        public const float HTD = 15;
        public const float HTR = .2617993877991494f;

        public const int PHIMAX = 91;
        public const int HORIZONPHI = 86;
        public const int THETAMAX = 361;
        public const int SKYTEXTURESIZE = 512;
        public const int MAXLEVELS = 5;
        public const int MAXCUMULUSNBR = 13;
        public const int CUMULUSPOLYS = 20;
        public const int CUMULUSPOINTS = 40;
        public const int LIGHTNINGPOINTS = 33;
        public const float LIGHTNINGRADIUS = 15000f;
        public const int STRATUSTEXTURES = 4;
        public const int CUMULUSTEXTURES = 10;
        public const int SHADOWCELL = 2;
        public const int DRAWABLECELL = 1;
        public const int NUMCELLS = 9;
        public const int CELLSIZE = 57344;
        public const int HALFCELLSIZE = CELLSIZE / 2;
        public const int HALFCELLS = (NUMCELLS - 1) / 2;
        public const int VPSHIFT = HALFCELLS * CELLSIZE;
        public const float PUFFRADIUS = 3000f;
        public const float CUMULUSRADIUS = 10000f;
        public const float STRATUSRADIUS = 80000f;
        public const float NVG_LIGHT_LEVEL = .703125f;
        public const int NUMSTARS = 1348;
        public const float MAXRANGE = 2.5f;
        public const int MAXSHIFT = 16;
        public const int MAXUSERSHIFT = 6;
        public const int CLOUD = 1;
        public const int TROPO = 2;
        public const int MECH = 3;
        public const int HEATWATER = 4;
        public const int HEATCITIES = 5;
        public const int HEATOTHERS = 6;
        public const int STRATUSTHICKNESS = 2000;
        public const int CIRRUSTHICKNESS = 100;

        public const uint FilteredWaterNormalMapTextureIndex = 5;
        public const uint SkyEnvMapTextureIndex = 4;
        public const uint campFileWeatherInfoFileVersion = 2;
        public const uint campFileMapWeatherInfoFileVersion = 3;

    }
}
