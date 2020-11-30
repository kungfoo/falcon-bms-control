using F4Utils.Campaign.F4Structs;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
namespace F4Utils.Campaign.Save
{
    public class TwxFile
    {
        public uint Version { get; private set; }
        public int LastCheck { get; private set; }
        public int BasicOldCondition { get; private set; }
        public int BasicCondition { get; private set; }
        public int BasicConditionCounter { get; private set; }
        public ConditionPreset CurCondition { get; private set; }
        public int ConditionChangeInterval { get; private set; }
        public int Model { get; private set; }
        public int MapUpdates { get; private set; }
        public int MinInterval { get; private set; }
        public int MaxInterval { get; private set; }
        public float WxProbSunny { get; private set; }
        public float WxProbFair{ get; private set; }
        public float WxProbPoor{ get; private set; }
        public float WxProbInclement{ get; private set; }
        public WeatherShifts[] WthUserShift { get; private set; }
        public int Wind_Heading_Model { get; private set; }
        public int Wind_Heading { get; private set; }
        public int Map_Wind_Heading { get; private set; }
        public float Map_Wind_Speed{ get; private set; }
        public Wind[] WthWind { get; private set; }
        public WthTurbu[] WthTurbu { get; private set; }
        public int MechLayer { get; private set; }
        public int HeatLayer { get; private set; }
        public int Occurrence { get; private set; }
        public int Duration { get; private set; }
        public float[] FogStart { get; private set; }
        public float[] FogEnd{ get; private set; }
        public int[] StratusLayer { get; private set; }
        public int[] StratusThick{ get; private set; }
        public int[] CumulusLayer { get; private set; }
        public int[] ContrailLayer { get; private set; }
        public int CumulusDensity { get; private set; }
        public float CumulusSize{ get; private set; }
        public Temp[] WthTemp { get; private set; }
        public Press[] WthPress{ get; private set; }
        public TwxFile(string fileName)
        {
            Init();
            LoadTwxFile(fileName);
        }
        private void Init()
        {
            WthUserShift = new WeatherShifts[WeatherConstants.MAXUSERSHIFT];
            WthWind = new Wind[5];
            WthTemp = new Temp[5];
            WthPress = new Press[5];
            WthTurbu = new WthTurbu[7];
            FogStart = new float[4];
            FogEnd= new float[4];
            StratusLayer = new int[4];
            StratusThick= new int[4];
            CumulusLayer = new int[4];
            ContrailLayer = new int[4];
        }

        private void LoadTwxFile(string fileName)
        {
            //reads TWX file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                Version = reader.ReadUInt32();

		        if( (Version > WeatherConstants.campFileWeatherInfoFileVersion) || (Version < 2) )
		        {
			        return;
		        }

                LastCheck = reader.ReadInt32();
                BasicOldCondition = reader.ReadInt32();
                BasicCondition = reader.ReadInt32();
                BasicConditionCounter = reader.ReadInt32();
                CurCondition = new ConditionPreset(stream);
                ConditionChangeInterval = reader.ReadInt32();
                Model = reader.ReadInt32();
                MapUpdates = reader.ReadInt32();
                MinInterval = reader.ReadInt32();
                MaxInterval = reader.ReadInt32();
                WxProbSunny = reader.ReadSingle();
                WxProbFair= reader.ReadSingle();
                WxProbPoor= reader.ReadSingle();
                WxProbInclement= reader.ReadSingle();
			
			    for (var i=1 ; i <WeatherConstants.MAXUSERSHIFT ; i++)
			    {
                    WthUserShift[i].ShiftTime=reader.ReadUInt32();
                    WthUserShift[i].ShiftnOldCondition=reader.ReadInt32();
			    }
                Wind_Heading_Model = reader.ReadInt32();
                Wind_Heading= reader.ReadInt32();
                Map_Wind_Heading= reader.ReadInt32();
                Map_Wind_Speed= reader.ReadSingle();

			    for (var i=1 ; i <5 ; i++)
			    {
                    WthWind[i].NightSpeed = reader.ReadInt32();
                    WthWind[i].DawnSpeed = reader.ReadInt32();
                    WthWind[i].DaySpeed = reader.ReadInt32();
                    WthWind[i].BurstInterval = reader.ReadInt32();
                    WthWind[i].BurstDuration = reader.ReadInt32();
                    WthWind[i].BurstSpeed= reader.ReadInt32();
                    WthWind[i].BurstDirection= reader.ReadInt32();
			    }
			    for (var i=1 ; i <7 ; i++)
			    {
                    WthTurbu[i].RotXWind = reader.ReadInt32();
                    WthTurbu[i].RotYWind = reader.ReadInt32();
                    WthTurbu[i].RotZWind = reader.ReadInt32();
                    WthTurbu[i].RotXRot = reader.ReadInt32();
                    WthTurbu[i].RotYRot = reader.ReadInt32();
                    WthTurbu[i].RotZRot = reader.ReadInt32();
			    }
                
                MechLayer = reader.ReadInt32();
                HeatLayer = reader.ReadInt32();
                Occurrence = reader.ReadInt32();
                Duration = reader.ReadInt32();
			
			    for (var i=0 ; i <4 ; i++)
			    {
                    FogStart[i] = reader.ReadSingle();
			    }

			    for (var i=0 ; i <4 ; i++)
			    {
                    FogEnd[i] = reader.ReadSingle();
			    }

			    for (var i=0 ; i <4 ; i++)
			    {
                    StratusLayer[i] = reader.ReadInt32();
			    }

			    for (var i=0 ; i <4 ; i++)
			    {
                    StratusThick[i] = reader.ReadInt32();
			    }

			    for (var i=0 ; i <4 ; i++)
			    {
                    CumulusLayer[i] = reader.ReadInt32();
			    }

			    for (var i=0 ; i <4 ; i++)
			    {
                    ContrailLayer[i] = reader.ReadInt32();
			    }

                CumulusDensity = reader.ReadInt32();
                CumulusSize= reader.ReadSingle();

			    for (int i=1 ; i <5 ; i++)
			    {
                    WthTemp[i].NightTemp = reader.ReadInt32();
                    WthTemp[i].DawnTemp = reader.ReadInt32();
                    WthTemp[i].DayTemp = reader.ReadInt32();
			    }

			    for (int i=1 ; i <5 ; i++)
			    {
                    WthPress[i].NightPress = reader.ReadInt32();
                    WthPress[i].DawnPress = reader.ReadInt32();
                    WthPress[i].DayPress = reader.ReadInt32();
			    }
		    }	
		
        }
        public void Save(string fileName)
        {
            //writes TWX file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Version);
                writer.Write(LastCheck);
                writer.Write(BasicOldCondition);
                writer.Write(BasicCondition);
                writer.Write(BasicConditionCounter);
                CurCondition.Write(stream);
                writer.Write(ConditionChangeInterval);
                writer.Write(Model);
                writer.Write(MapUpdates);
                writer.Write(MinInterval);
                writer.Write(MaxInterval);
                writer.Write(WxProbSunny);
                writer.Write(WxProbFair);
                writer.Write(WxProbPoor);
                writer.Write(WxProbInclement);

                for (var i = 1; i < WeatherConstants.MAXUSERSHIFT; i++)
                {
                    writer.Write(WthUserShift[i].ShiftTime);
                    writer.Write(WthUserShift[i].ShiftnOldCondition);
                }
                writer.Write(Wind_Heading_Model);
                writer.Write(Wind_Heading);
                writer.Write(Map_Wind_Heading);
                writer.Write(Map_Wind_Speed);

                for (var i = 1; i < 5; i++)
                {
                    writer.Write(WthWind[i].NightSpeed);
                    writer.Write(WthWind[i].DawnSpeed);
                    writer.Write(WthWind[i].DaySpeed);
                    writer.Write(WthWind[i].BurstInterval);
                    writer.Write(WthWind[i].BurstDuration);
                    writer.Write(WthWind[i].BurstSpeed);
                    writer.Write(WthWind[i].BurstDirection);
                }
                for (var i = 1; i < 7; i++)
                {
                    writer.Write(WthTurbu[i].RotXWind);
                    writer.Write(WthTurbu[i].RotYWind);
                    writer.Write(WthTurbu[i].RotZWind);
                    writer.Write(WthTurbu[i].RotXRot);
                    writer.Write(WthTurbu[i].RotYRot);
                    writer.Write(WthTurbu[i].RotZRot);
                }

                writer.Write(MechLayer);
                writer.Write(HeatLayer);
                writer.Write(Occurrence);
                writer.Write(Duration);

                for (var i = 0; i < 4; i++)
                {
                    writer.Write(FogStart[i]);
                }

                for (var i = 0; i < 4; i++)
                {
                    writer.Write(FogEnd[i]);
                }

                for (var i = 0; i < 4; i++)
                {
                    writer.Write(StratusLayer[i]);
                }

                for (var i = 0; i < 4; i++)
                {
                    writer.Write(StratusThick[i]);
                }

                for (var i = 0; i < 4; i++)
                {
                    writer.Write(CumulusLayer[i]);
                }

                for (var i = 0; i < 4; i++)
                {
                    writer.Write(ContrailLayer[i]);
                }

                writer.Write(CumulusDensity);
                writer.Write(CumulusSize);

                for (int i = 1; i < 5; i++)
                {
                    writer.Write(WthTemp[i].NightTemp);
                    writer.Write(WthTemp[i].DawnTemp);
                    writer.Write(WthTemp[i].DayTemp);
                }

                for (int i = 1; i < 5; i++)
                {
                    writer.Write(WthPress[i].NightPress);
                    writer.Write(WthPress[i].DawnPress);
                    writer.Write(WthPress[i].DayPress);
                }
            }

        }
    }
}
