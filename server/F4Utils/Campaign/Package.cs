using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class Package : AirUnit
    {
        #region Public Fields
        public byte elements;
        public VU_ID[] element;
        public VU_ID interceptor;
        public VU_ID awacs;
        public VU_ID jstar;
        public VU_ID ecm;
        public VU_ID tanker;
        public byte wait_cycles;
        public byte flights;
        public short wait_for;
        public short iax;
        public short iay;
        public short eax;
        public short eay;
        public short bpx;
        public short bpy;
        public short tpx;
        public short tpy;
        public uint takeoff;
        public uint tp_time;
        public uint package_flags;
        public short caps;
        public short requests;
        public short threat_stats;
        public short responses;
        public byte num_ingress_waypoints;
        public Waypoint[] ingress_waypoints;
        public byte num_egress_waypoints;
        public Waypoint[] egress_waypoints;
        public MissionRequest mis_request;
        #endregion
        protected Package()
            : base()
        {
        }
        public Package(Stream stream, int version)
            : base(stream, version)
        {
            ReadPackage(stream, version);
        }

        protected void ReadPackage(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                elements = reader.ReadByte();
                element = new VU_ID[elements];
                for (int i = 0; i < elements; i++)
                {
                    VU_ID thisElement = new VU_ID();
                    thisElement.num_ = reader.ReadUInt32();
                    thisElement.creator_ = reader.ReadUInt32();
                    element[i] = thisElement;
                }
                interceptor = new VU_ID();
                interceptor.num_ = reader.ReadUInt32();
                interceptor.creator_ = reader.ReadUInt32();
                if (version >= 7)
                {
                    awacs = new VU_ID();
                    awacs.num_ = reader.ReadUInt32();
                    awacs.creator_ = reader.ReadUInt32();

                    jstar = new VU_ID();
                    jstar.num_ = reader.ReadUInt32();
                    jstar.creator_ = reader.ReadUInt32();

                    ecm = new VU_ID();
                    ecm.num_ = reader.ReadUInt32();
                    ecm.creator_ = reader.ReadUInt32();

                    tanker = new VU_ID();
                    tanker.num_ = reader.ReadUInt32();
                    tanker.creator_ = reader.ReadUInt32();

                }
                wait_cycles = reader.ReadByte();

                mis_request = new MissionRequest();

                if (Final && wait_cycles == 0)
                {
                    requests = reader.ReadInt16();

                    if (version < 35)
                    {
                        threat_stats = reader.ReadInt16();
                    }

                    responses = reader.ReadInt16();

                    mis_request.mission = (byte)reader.ReadInt16();
                    mis_request.context = (byte)reader.ReadInt16();

                    mis_request.requesterID = new VU_ID();
                    mis_request.requesterID.num_ = reader.ReadUInt32();
                    mis_request.requesterID.creator_ = reader.ReadUInt32();

                    mis_request.targetID = new VU_ID();
                    mis_request.targetID.num_ = reader.ReadUInt32();
                    mis_request.targetID.creator_ = reader.ReadUInt32();

                    if (version >= 26)
                    {
                        mis_request.tot = reader.ReadUInt32();
                    }
                    else if (version >= 16)
                    {
                        mis_request.tot = reader.ReadUInt32();
                    }
                    if (version >= 35)
                    {
                        mis_request.action_type = reader.ReadByte();
                    }
                    else
                    {
                        mis_request.action_type = 0;
                    }

                    if (version >= 41)
                    {
                        mis_request.priority = reader.ReadInt16();
                    }
                    else
                    {
                        mis_request.priority = 1;
                    }
                    package_flags = 0;
                }
                else
                {
                    flights = reader.ReadByte();
                    wait_for = reader.ReadInt16();
                    iax = reader.ReadInt16();
                    iay = reader.ReadInt16();
                    eax = reader.ReadInt16();
                    eay = reader.ReadInt16();
                    bpx = reader.ReadInt16();
                    bpy = reader.ReadInt16();
                    tpx = reader.ReadInt16();
                    tpy = reader.ReadInt16();
                    takeoff = reader.ReadUInt32();
                    tp_time = reader.ReadUInt32();
                    package_flags = reader.ReadUInt32();
                    caps = reader.ReadInt16();
                    requests = reader.ReadInt16();

                    if (version < 35)
                    {
                        threat_stats = reader.ReadInt16();
                    }

                    responses = reader.ReadInt16();
                    num_ingress_waypoints = reader.ReadByte();

                    ingress_waypoints = new Waypoint[num_ingress_waypoints];
                    for (int j = 0; j < num_ingress_waypoints; j++)
                    {
                        ingress_waypoints[j] = new Waypoint(stream, version);
                    }

                    num_egress_waypoints = reader.ReadByte();
                    egress_waypoints = new Waypoint[num_egress_waypoints];
                    for (int j = 0; j < num_egress_waypoints; j++)
                    {
                        egress_waypoints[j] = new Waypoint(stream, version);
                    }

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

                    reader.ReadBytes(2); //align on int32 boundary

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

                    if (!(version < 35))
                    {

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
                    }
                }
            }
        }
        public void WritePackage(Stream stream, int version)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(elements);
                for (int i = 0; i < elements; i++)
                {
                    var thisElement = element[i];
                    writer.Write(thisElement.num_);
                    writer.Write(thisElement.creator_);
                }
                writer.Write(interceptor.num_);
                writer.Write(interceptor.creator_);
                if (version >= 7)
                {
                    writer.Write(awacs.num_);
                    writer.Write(awacs.creator_);

                    writer.Write(jstar.num_);
                    writer.Write(jstar.creator_);

                    writer.Write(ecm.num_);
                    writer.Write(ecm.creator_);

                    writer.Write(tanker.num_);
                    writer.Write(tanker.creator_);

                }
                writer.Write(wait_cycles);

                if (Final && wait_cycles == 0)
                {
                    writer.Write(requests);

                    if (version < 35)
                    {
                        writer.Write(threat_stats);
                    }

                    writer.Write(responses);

                    writer.Write((short)mis_request.mission);
                    writer.Write((short)mis_request.context);

                    writer.Write(mis_request.requesterID.num_);
                    writer.Write(mis_request.requesterID.creator_);

                    writer.Write(mis_request.targetID.num_);
                    writer.Write(mis_request.targetID.creator_);

                    if (version >= 26)
                    {
                        writer.Write(mis_request.tot);
                    }
                    else if (version >= 16)
                    {
                        writer.Write(mis_request.tot);
                    }
                    if (version >= 35)
                    {
                        writer.Write(mis_request.action_type);
                    }
                    if (version >= 41)
                    {
                        writer.Write(mis_request.priority);
                    }
                }
                else
                {
                    writer.Write(flights);
                    writer.Write(wait_for);
                    writer.Write(iax);
                    writer.Write(iay);
                    writer.Write(eax);
                    writer.Write(eay);
                    writer.Write(bpx);
                    writer.Write(bpy);
                    writer.Write(tpx);
                    writer.Write(tpy);
                    writer.Write(takeoff);
                    writer.Write(tp_time);
                    writer.Write(package_flags);
                    writer.Write(caps);
                    writer.Write(requests);

                    if (version < 35)
                    {
                        writer.Write(threat_stats);
                    }

                    writer.Write(responses);
                    writer.Write(num_ingress_waypoints);

                    for (int j = 0; j < num_ingress_waypoints; j++)
                    {
                        ingress_waypoints[j].Write(stream, version);
                    }

                    writer.Write(num_egress_waypoints);
                    for (int j = 0; j < num_egress_waypoints; j++)
                    {
                        egress_waypoints[j].Write(stream, version);
                    }

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

                    writer.Write(new byte[2]); //align on int32 boundary

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

                    if (!(version < 35))
                    {

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