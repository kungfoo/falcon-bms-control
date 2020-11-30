using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class VcdFile
    {
        public VehicleClassDataType[] VehicleDataTable { get; private set; }
        public VcdFile(string fileName)
        {
            VehicleDataTable = LoadVehicleData(fileName);
        }

        private VehicleClassDataType[] LoadVehicleData(string fileName)
        {
            //reads VCD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var vehicleDataTable = new VehicleClassDataType[entries];
                for (var i = 0; i < vehicleDataTable.Length; i++)
                {
                    var entry = new VehicleClassDataType();
                    entry.Index = reader.ReadInt16();
                    entry.HitPoints = reader.ReadInt16();
                    entry.Flags = reader.ReadUInt32();
                    entry.Name = reader.ReadBytes(15);
                    entry.NCTR = reader.ReadBytes(5);
                    entry.RCSfactor = reader.ReadSingle();
                    entry.MaxWt = reader.ReadInt32();
                    entry.EmptyWt = reader.ReadInt32();
                    entry.FuelWt = reader.ReadInt32();
                    entry.FuelEcon = reader.ReadInt16();
                    entry.EngineSound = reader.ReadInt16();
                    entry.Union1 = reader.ReadInt16();
                    entry.LowAlt = reader.ReadInt16();
                    entry.CruiseAlt = reader.ReadInt16();
                    entry.MaxSpeed = reader.ReadInt16();
                    entry.RadarType = reader.ReadInt16();
                    entry.NumberOfPilots = reader.ReadInt16();
                    entry.Union2 = reader.ReadUInt16();
                    entry.VisibleFlags = reader.ReadUInt16();
                    entry.CallsignIndex = reader.ReadByte();
                    entry.CallsignSlots = reader.ReadByte();
                    entry.HitChance = reader.ReadBytes((int) MoveType.MOVEMENT_TYPES);
                    entry.Strength = reader.ReadBytes((int)MoveType.MOVEMENT_TYPES);
                    entry.Range = reader.ReadBytes((int)MoveType.MOVEMENT_TYPES);
                    entry.Detection = reader.ReadBytes((int)MoveType.MOVEMENT_TYPES);
                    entry.Weapon = new short[WeaponsConstants.HARDPOINT_MAX];
                    for (var j=0;j<entry.Weapon.Length;j++)
                    {
                        entry.Weapon[j] = reader.ReadInt16();
                    }
                    entry.Weapons = reader.ReadBytes(WeaponsConstants.HARDPOINT_MAX);
                    entry.DamageMod = reader.ReadBytes((int) DamageDataType.OtherDam + 1);
                    entry.visSignature = reader.ReadByte();
                    entry.irSignature = reader.ReadByte();
                    entry.pad1 = reader.ReadByte();
                    vehicleDataTable[i] = entry;
                }
                return vehicleDataTable;
            }

        }
        public void Save(string fileName)
        {
            //writes VCD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)VehicleDataTable.Length);
                for (var i = 0; i < VehicleDataTable.Length; i++)
                {
                    var entry = VehicleDataTable[i];
                    writer.Write(entry.Index);
                    writer.Write(entry.HitPoints);
                    writer.Write(entry.Flags);
                    writer.Write(entry.Name);
                    writer.Write(entry.NCTR);
                    writer.Write(entry.RCSfactor);
                    writer.Write(entry.MaxWt);
                    writer.Write(entry.EmptyWt);
                    writer.Write(entry.FuelWt);
                    writer.Write(entry.FuelEcon);
                    writer.Write(entry.EngineSound);
                    writer.Write(entry.Union1);
                    writer.Write(entry.LowAlt);
                    writer.Write(entry.CruiseAlt);
                    writer.Write(entry.MaxSpeed);
                    writer.Write(entry.RadarType);
                    writer.Write(entry.NumberOfPilots);
                    writer.Write(entry.Union2);
                    writer.Write(entry.VisibleFlags);
                    writer.Write(entry.CallsignIndex);
                    writer.Write(entry.CallsignSlots);
                    writer.Write(entry.HitChance);
                    writer.Write(entry.Strength);
                    writer.Write(entry.Range);
                    writer.Write(entry.Detection);
                    for (var j = 0; j < entry.Weapon.Length; j++)
                    {
                        writer.Write(entry.Weapon[j]);
                    }
                    writer.Write(entry.Weapons);
                    writer.Write(entry.DamageMod);
                    writer.Write(entry.visSignature);
                    writer.Write(entry.irSignature);
                    writer.Write(entry.pad1);
                }
            }

        }
    }
}
