using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class Flight : AirUnit
    {
        #region Public Fields

        public int fuel_burnt;
        public uint last_move;
        public uint last_combat;
        public uint time_on_target;
        public uint mission_over_time;
        public short mission_target;
        public sbyte use_loadout;
        public byte[] weapons;
        public byte loadouts;
        public LoadoutStruct[] loadout;
        public short[] weapon;
        public byte mission;
        public byte old_mission;
        public byte last_direction;
        public byte priority;
        public byte mission_id;
        public byte dummy;
        public byte eval_flags;
        public byte mission_context;
        public VU_ID package;
        public VU_ID squadron;
        public VU_ID requester;
        public byte[] slots;
        public byte[] pilots;
        public byte[] plane_stats;
        public byte[] player_slots;
        public byte last_player_slot;
        public byte callsign_id;
        public byte callsign_num;
        public uint refuelQuantity;
        #endregion
        private const int WEAPON_IDS_WIDENED_VERSION = 73;
        protected Flight()
            : base()
        {
        }
        public Flight(Stream stream, int version)
            : base(stream, version)
        {
            ReadFlight(stream, version);
        }

        private void ReadFlight(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                z = reader.ReadSingle();
                fuel_burnt = reader.ReadInt32();

                if (version < 65)
                {
                    fuel_burnt = 0;
                }

                last_move = reader.ReadUInt32();
                last_combat = reader.ReadUInt32();
                time_on_target = reader.ReadUInt32();
                mission_over_time = reader.ReadUInt32();
                mission_target = reader.ReadInt16();

                loadouts = 0;
                if (version < 24)
                {
                    use_loadout = 0;
                    weapons = new byte[16];
                    loadouts = 1;
                    loadout = new LoadoutStruct[loadouts];
                    if (version >= 8)
                    {
                        use_loadout = reader.ReadSByte();

                        if (use_loadout != 0)
                        {
                            LoadoutArray junk = new LoadoutArray();
                            junk.Stores = new LoadoutStruct[5];
                            for (int j = 0; j < 5; j++)
                            {
                                LoadoutStruct thisStore = junk.Stores[j];
                                thisStore.WeaponID = new ushort[16];
                                for (int k = 0; k < 16; k++)
                                {
                                    thisStore.WeaponID[k] = reader.ReadByte();
                                }

                                thisStore.WeaponCount = new byte[16];
                                for (int k = 0; k < 16; k++)
                                {
                                    thisStore.WeaponCount[k] = reader.ReadByte();
                                }

                            }
                            loadout[0] = junk.Stores[0];
                        }
                    }
                    weapon = new short[16];
                    if (version < 18)
                    {
                        for (int j = 0; j < 16; j++)
                        {
                            weapon[j] = reader.ReadInt16();
                        }
                        if (use_loadout == 0)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                loadout[0].WeaponID[j] = (byte)weapon[j];
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 16; j++)
                        {
                            weapon[j] = reader.ReadByte();
                        }
                        if (use_loadout == 0)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                loadout[0].WeaponID[j] = (byte)weapon[j];
                            }
                        }
                    }
                    for (int j = 0; j < 16; j++)
                    {
                        weapons[j] = reader.ReadByte();
                    }
                    if (use_loadout == 0)
                    {
                        for (int j = 0; j < 16; j++)
                        {
                            loadout[0].WeaponCount[j] = weapons[j];
                        }
                    }
                }
                else
                {
                    loadouts = reader.ReadByte();
                    loadout = new LoadoutStruct[loadouts];
                    for (int j = 0; j < loadouts; j++)
                    {
                        LoadoutStruct thisLoadout = new LoadoutStruct();
                        thisLoadout.WeaponID = new ushort[16];
                        for (int k = 0; k < 16; k++)
                        {
                            if (version >= WEAPON_IDS_WIDENED_VERSION)
                            {
                                thisLoadout.WeaponID[k] = reader.ReadUInt16();
                            }
                            else
                            {
                                thisLoadout.WeaponID[k] = reader.ReadByte();
                            }
                        }
                        thisLoadout.WeaponCount = new byte[16];
                        for (int k = 0; k < 16; k++)
                        {
                            thisLoadout.WeaponCount[k] = reader.ReadByte();
                        }
                        loadout[j] = thisLoadout;
                    }
                }
                mission = reader.ReadByte();

                if (version > 65)
                {
                    old_mission = reader.ReadByte();
                }
                else
                {
                    old_mission = mission;
                }
                last_direction = reader.ReadByte();

                priority = reader.ReadByte();

                mission_id = reader.ReadByte();

                if (version < 14)
                {
                    dummy = reader.ReadByte();
                }
                eval_flags = reader.ReadByte();

                if (version > 65)
                {
                    mission_context = reader.ReadByte();
                }
                else
                {
                    mission_context = 0;
                }

                package = new VU_ID();
                package.num_ = reader.ReadUInt32();
                package.creator_ = reader.ReadUInt32();

                squadron = new VU_ID();
                squadron.num_ = reader.ReadUInt32();
                squadron.creator_ = reader.ReadUInt32();

                if (version > 65)
                {
                    requester = new VU_ID();
                    requester.num_ = reader.ReadUInt32();
                    requester.creator_ = reader.ReadUInt32();
                }
                else
                {
                    requester = new VU_ID();
                }

                slots = new byte[4];
                for (int j = 0; j < 4; j++)
                {
                    slots[j] = reader.ReadByte();
                }

                pilots = new byte[4];
                for (int j = 0; j < 4; j++)
                {
                    pilots[j] = reader.ReadByte();
                }

                plane_stats = new byte[4];
                for (int j = 0; j < 4; j++)
                {
                    plane_stats[j] = reader.ReadByte();
                }

                player_slots = new byte[4];
                for (int j = 0; j < 4; j++)
                {
                    player_slots[j] = reader.ReadByte();
                }

                last_player_slot = reader.ReadByte();
                callsign_id = reader.ReadByte();
                callsign_num = reader.ReadByte();

                if (version >= 72)
                {
                    refuelQuantity = reader.ReadUInt32();
                }
                else
                {
                    refuelQuantity = 0;
                }
            }
        }
        public void WriteFlight(Stream stream, int version)
        {
            base.WriteAirUnit(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(z);
                writer.Write(fuel_burnt);
                writer.Write(last_move);
                writer.Write(last_combat);
                writer.Write(time_on_target); 
                writer.Write(mission_over_time);
                writer.Write(mission_target);

                if (version < 24)
                {
                    if (version >= 8)
                    {
                        writer.Write(use_loadout); 
                        if (use_loadout != 0)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                var thisStore = loadout[j];
                                for (int k = 0; k < 16; k++)
                                {
                                    writer.Write((byte)thisStore.WeaponID[k]);
                                }

                                for (int k = 0; k < 16; k++)
                                {
                                    writer.Write((byte)thisStore.WeaponCount[k]);
                                }

                            }
                        }
                    }
                    if (version < 18)
                    {
                        for (int j = 0; j < 16; j++)
                        {
                            writer.Write(weapon[j]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 16; j++)
                        {
                            writer.Write((byte)weapon[j]);
                        }
                    }
                    for (int j = 0; j < 16; j++)
                    {
                        writer.Write((byte)weapons[j]);
                    }
                }
                else
                {
                    writer.Write(loadouts);
                    for (int j = 0; j < loadouts; j++)
                    {
                        var thisLoadout = loadout[j];
                        for (int k = 0; k < 16; k++)
                        {
                            if (version >= WEAPON_IDS_WIDENED_VERSION)
                            {
                                writer.Write(thisLoadout.WeaponID[k]);
                            }
                            else
                            {
                                writer.Write((byte)thisLoadout.WeaponID[k]);
                            }
                        }
                        for (int k = 0; k < 16; k++)
                        {
                            writer.Write(thisLoadout.WeaponCount[k]);
                        }
                        
                    }
                }
                writer.Write(mission);

                if (version > 65)
                {
                    writer.Write(old_mission);
                }

                writer.Write(last_direction);
                writer.Write(priority);
                writer.Write(mission_id);

                if (version < 14)
                {
                    writer.Write((byte)0x00);//dummy
                }
                writer.Write(eval_flags);

                if (version > 65)
                {
                    writer.Write(mission_context);
                }

                writer.Write(package.num_ );
                writer.Write( package.creator_);

                writer.Write(squadron.num_);
                writer.Write(squadron.creator_);

                if (version > 65)
                {
                    writer.Write(requester.num_);
                    writer.Write(requester.creator_);
                }

                for (int j = 0; j < 4; j++)
                {
                    writer.Write(slots[j]);
                }

                for (int j = 0; j < 4; j++)
                {
                    writer.Write(pilots[j]);
                }

                for (int j = 0; j < 4; j++)
                {
                    writer.Write(plane_stats[j]);
                }

                for (int j = 0; j < 4; j++)
                {
                    writer.Write(player_slots[j]);
                }

                writer.Write(last_player_slot);
                writer.Write(callsign_id);
                writer.Write(callsign_num);

                if (version >= 72)
                {
                    writer.Write(refuelQuantity);
                }
            }
        }
    }
}