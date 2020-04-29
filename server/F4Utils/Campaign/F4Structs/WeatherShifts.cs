using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.F4Structs
{
    public class WeatherShifts 
    {
	    public WeatherShifts()
	    {
		    ShiftTime=1;
		    ShiftnOldCondition=1; 
		    ShiftnCondition=1; 
		    Counter=1;
	    }
	    public uint ShiftTime {get;internal set;}
	    public int ShiftnOldCondition {get; internal set;}
	    public int ShiftnCondition {get; internal set;}
	    public int Counter {get;internal set;}
    }
}
