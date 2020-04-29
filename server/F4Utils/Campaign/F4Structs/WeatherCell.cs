using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.F4Structs
{
    public class WeatherCell
    {
	    public bool[] OnScreen = new bool[WeatherConstants.MAXCUMULUSNBR];
        public int BasicCondition;
        public int InterpolatedCondition;

        public float Pressure;
        public float Temperature;
        public float WindSpeed;
        public float WindHeading;

        public float StratusBase;
        public float CumulusBase;


        public bool AboveLayer;
        public bool BelowLayer;
        public bool InsideLayer;

        public vector CloudPos;
        public vector[] ShadowPos = new vector[WeatherConstants.MAXCUMULUSNBR];
        public vector[] ShadowWorldPos = new vector[WeatherConstants.MAXCUMULUSNBR];
        public vector[] CumulusPos = new vector[WeatherConstants.MAXCUMULUSNBR];
        public float StratusU;
        public float StratusV;
        public int[] CumulusIdx = new int[WeatherConstants.CUMULUSPOLYS];
        public float[] CumulusU = new float[WeatherConstants.CUMULUSPOLYS];
        public float[] CumulusV = new float[WeatherConstants.CUMULUSPOLYS];
        public vector[] CumulusPolysPos = new vector[WeatherConstants.CUMULUSPOLYS]; // Necessary to store polys position when drawn for LOS checks
    }
}
