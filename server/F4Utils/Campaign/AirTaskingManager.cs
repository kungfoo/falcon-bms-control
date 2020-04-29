using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class AirTaskingManager : CampaignManager
    {
        #region Public Fields
        public short flags;
        public short averageCAStrength;
        public short averageCAMissions;
        public byte sampleCycles;
        public byte numAirbases;
        public ATMAirbase[] airbases;
        public byte cycle;
        public short numMissionRequests;
        public MissionRequest[] missionRequests;
        #endregion

        protected AirTaskingManager(){}
        public AirTaskingManager(Stream stream, int version):base(stream, version)
        {
            ReadAirTaskingManager(stream, version);
        }

        protected void ReadAirTaskingManager(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                flags = reader.ReadInt16();

                if (version >= 28)
                {
                    if (version >= 63)
                    {
                        averageCAStrength = reader.ReadInt16();
                    }
                    averageCAMissions = reader.ReadInt16();
                    sampleCycles = reader.ReadByte();
                }
                if (version < 63)
                {
                    averageCAMissions = 500;
                    averageCAStrength = 500;
                    sampleCycles = 10;

                }
                numAirbases = reader.ReadByte();

                airbases = new ATMAirbase[numAirbases];
                for (int j = 0; j < numAirbases; j++)
                {
                    airbases[j] = new ATMAirbase(stream, version);
                }

                cycle = reader.ReadByte();

                numMissionRequests = reader.ReadInt16();

                missionRequests = new MissionRequest[numMissionRequests];
                if (version < 35)
                {
                    for (int j = 0; j < numMissionRequests; j++)
                    {
                        reader.ReadBytes(64);
                    }
                }
                else
                {
                    for (int j = 0; j < numMissionRequests; j++)
                    {
                        var mis_request = new MissionRequest();

                        mis_request.requesterID = new VU_ID();
                        mis_request.requesterID.num_ = reader.ReadUInt32();
                        mis_request.requesterID.creator_ = reader.ReadUInt32();

                        mis_request.targetID = new VU_ID();
                        mis_request.targetID.num_ = reader.ReadUInt32();
                        mis_request.targetID.creator_ = reader.ReadUInt32();

                        mis_request.secondaryID = new VU_ID();
                        mis_request.secondaryID.num_ = reader.ReadUInt32();
                        mis_request.secondaryID.creator_ = reader.ReadUInt32();

                        mis_request.pakID = new VU_ID();
                        mis_request.pakID.num_ = reader.ReadUInt32();
                        mis_request.pakID.creator_ = reader.ReadUInt32();

                        mis_request.who = reader.ReadByte();
                        mis_request.vs = reader.ReadByte();
                        reader.ReadBytes(2);//align on int32 boundary

                        mis_request.tot = reader.ReadUInt32();
                        mis_request.tx = reader.ReadInt16();
                        mis_request.ty = reader.ReadInt16();
                        mis_request.flags = reader.ReadUInt32();
                        mis_request.caps = reader.ReadInt16();
                        mis_request.target_num = reader.ReadInt16();
                        mis_request.speed = reader.ReadInt16();
                        mis_request.match_strength = reader.ReadInt16();
                        mis_request.priority = reader.ReadInt16();
                        mis_request.tot_type = reader.ReadByte();
                        mis_request.action_type = reader.ReadByte();
                        mis_request.mission = reader.ReadByte();
                        mis_request.aircraft = reader.ReadByte();
                        mis_request.context = reader.ReadByte();
                        mis_request.roe_check = reader.ReadByte();
                        mis_request.delayed = reader.ReadByte();
                        mis_request.start_block = reader.ReadByte();
                        mis_request.final_block = reader.ReadByte();

                        mis_request.slots = new byte[4];
                        for (int k = 0; k < 4; k++)
                        {
                            mis_request.slots[k] = reader.ReadByte();
                        }

                        mis_request.min_to = reader.ReadSByte();
                        mis_request.max_to = reader.ReadSByte();
                        reader.ReadBytes(3);// align on int32 boundary
                        missionRequests[j] = mis_request;

                    }
                }
            }
        }
        public void WriteAirTaskingManager(Stream stream, int version)
        {
            base.WriteCampaignManager(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(flags);

                if (version >= 28)
                {
                    if (version >= 63)
                    {
                        writer.Write(averageCAStrength);
                    }
                    writer.Write(averageCAMissions);
                    writer.Write(sampleCycles);
                }
                writer.Write(numAirbases);

                for (int j = 0; j < numAirbases; j++)
                {
                    airbases[j].Write(stream, version);
                }

                writer.Write(cycle);

                writer.Write(numMissionRequests);

                if (version < 35)
                {
                    for (int j = 0; j < numMissionRequests; j++)
                    {
                        writer.Write(new byte[64]);
                    }
                }
                else
                {
                    for (int j = 0; j < numMissionRequests; j++)
                    {
                        var mis_request = missionRequests[j];

                        writer.Write(mis_request.requesterID.num_);
                        writer.Write(mis_request.requesterID.creator_);

                        writer.Write(mis_request.targetID.num_);
                        writer.Write(mis_request.targetID.creator_);

                        writer.Write(mis_request.secondaryID.num_);
                        writer.Write(mis_request.secondaryID.creator_);

                        writer.Write(mis_request.pakID.num_);
                        writer.Write(mis_request.pakID.creator_);

                        writer.Write(mis_request.who);
                        writer.Write(mis_request.vs);
                        writer.Write(new byte[2]);//align on int32 boundary

                        writer.Write(mis_request.tot);
                        writer.Write(mis_request.tx);
                        writer.Write(mis_request.ty);
                        writer.Write(mis_request.flags);
                        writer.Write(mis_request.caps);
                        writer.Write(mis_request.target_num);
                        writer.Write(mis_request.speed);
                        writer.Write(mis_request.match_strength);
                        writer.Write(mis_request.priority);
                        writer.Write(mis_request.tot_type);
                        writer.Write(mis_request.action_type);
                        writer.Write(mis_request.mission);
                        writer.Write(mis_request.aircraft);
                        writer.Write(mis_request.context);
                        writer.Write(mis_request.roe_check);
                        writer.Write(mis_request.delayed);
                        writer.Write(mis_request.start_block);
                        writer.Write(mis_request.final_block);

                        for (int k = 0; k < 4; k++)
                        {
                            writer.Write(mis_request.slots[k]);
                        }

                        writer.Write(mis_request.min_to);
                        writer.Write(mis_request.max_to);
                        writer.Write(new byte[3]);// align on int32 boundary
                    }
                }
            }
        }
    }
}