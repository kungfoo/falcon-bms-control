using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class EvtFile
    {
        #region Public Fields
        public short numEvents;
        public CampEvent[] campEvents;
        #endregion

        public EvtFile(Stream stream, int version)
        {
            Read(stream);
        }

        private void Read(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                numEvents = reader.ReadInt16();

                campEvents = new CampEvent[numEvents];
                for (int i = 0; i < numEvents; i++)
                {
                    CampEvent thisEvent = new CampEvent();
                    thisEvent.id = reader.ReadInt16();
                    thisEvent.flags = reader.ReadInt16();
                    campEvents[i] = thisEvent;
                }
            }
        }
        public void Write(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(numEvents);
                for (int i = 0; i < numEvents; i++)
                {
                    var thisEvent = campEvents[i];
                    writer.Write(thisEvent.id );
                    writer.Write(thisEvent.flags);
                }
            }
        }
    }
}