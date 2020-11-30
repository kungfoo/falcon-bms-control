using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.F4Structs
{
    public enum CONDITIONS { SUNNY=1,FAIR,POOR,INCLEMENT,SUNNYFAIR,FAIRPOOR,POORINCLEMENT,NUM_CONDITIONS };
    public enum WeatherModel
    { 
	    PROBABILISTIC = 1,
	    DETERMINISTIC =2,
	    MAPMODEL = 3    
    };

}
