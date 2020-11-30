using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.F4Structs
{
    public class TacticData 
    {
        public short id = 0;
        public char[] name = new char[30];		// Tactic name
        public byte team = 0;				    // Teams which are allowed to use this tactic
        public byte domainType = 0;				// Domain of units which can use this tactic
        public byte unitSize = 0;				// Size of units which can use this tactic
        public short minRangeToDest = 0;		// How far from our target we gotta be (no target, mission dest)
        public short maxRangeToDest = 0;		// How far from out target we can be
        public short distToFront = 0;		    // Minimum distance to the front
        public byte[] actionList = new byte[10];// Actions/orders this tactic's ok with (0 in slot 0 = any)
        public byte broken = 0;				    // Can we do this while broken?
        public byte engaged = 0;				// Can we do this while engaged?
        public byte combat = 0;					// Can we do this while in combat?
        public byte losses = 0;					// Can we do this while taking losses?
        public byte retreating = 0;				// Can we do this while retreating?
        public byte owned = 0;					// Is our assigned objective owned by us?
        public byte airborne = 0;				// Is our unit air-mobile?
        public byte marine = 0;					// Is our unit marine-capibile?
        public byte minOdds = 0;				// Minimum odds we're willing to do this with (minOdds:10)
        public byte role = 0;					// Any special role we need to do this.
        public byte fuel = 0;					// How much extra fuel we gotta have
        public byte weapons = 0;				// 0 = none req, 1 = req for target, 2 = req for target and mission obj
        public byte priority = 0;				// Relative ranking of this tactic
        public byte formation = 0;				// Formation to do this in
        public byte special = 0;				// Any private stuff we want to check
        public TacticData() { }
        public TacticData(Stream stream)
        {
            Read(stream);
        }

        private void Read(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                id = reader.ReadInt16();
                name = reader.ReadChars(30);
                team = reader.ReadByte();
                domainType = reader.ReadByte();
                unitSize = reader.ReadByte();
                minRangeToDest = reader.ReadInt16();
                maxRangeToDest = reader.ReadInt16();
                distToFront = reader.ReadInt16();
                actionList = reader.ReadBytes(10);
                broken = reader.ReadByte();
                engaged = reader.ReadByte();
                combat = reader.ReadByte();
                losses = reader.ReadByte();
                retreating = reader.ReadByte();
                owned = reader.ReadByte();
                airborne = reader.ReadByte();
                marine = reader.ReadByte();
                minOdds = reader.ReadByte();
                role = reader.ReadByte();
                fuel = reader.ReadByte();
                weapons = reader.ReadByte();
                priority = reader.ReadByte();
                formation = reader.ReadByte();
                special = reader.ReadByte();
            }
        }
        public void Write(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(id);
                writer.Write(name);
                writer.Write(team);
                writer.Write(domainType);
                writer.Write(unitSize);
                writer.Write(minRangeToDest);
                writer.Write(maxRangeToDest);
                writer.Write(distToFront);
                writer.Write(actionList);
                writer.Write(broken);
                writer.Write(engaged);
                writer.Write(combat);
                writer.Write(losses);
                writer.Write(retreating);
                writer.Write(owned);
                writer.Write(airborne);
                writer.Write(marine);
                writer.Write(minOdds);
                writer.Write(role);
                writer.Write(fuel);
                writer.Write(weapons);
                writer.Write(priority);
                writer.Write(formation);
                writer.Write(special);
            }
        }
	};

}
