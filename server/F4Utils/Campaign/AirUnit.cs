using System;
using System.IO;
namespace F4Utils.Campaign
{
    public class AirUnit : Unit
    {
        protected AirUnit()
            : base()
        {
        }
        public AirUnit(Stream stream, int version)
            : base(stream, version)
        {

        }
        public void WriteAirUnit(Stream stream, int version)
        {
            base.WriteUnit(stream, version);
        }
    }
}