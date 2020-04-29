using F4Utils.Campaign.F4Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace F4Utils.Campaign.Save
{
    public class HisFileRecord
    {
        public uint Time { get; internal set; }
        public UnitHistoryType[] UnitHistory { get; internal set; }
    }
    public class HisFile
    {
        public HisFileRecord[] HistoryRecords { get; internal set; }
        public HisFile(string fileName)
        {
            LoadHisFile(fileName);
        }

         private void LoadHisFile(string fileName)
         {
             List<HisFileRecord> records = new List<HisFileRecord>();
             //reads HIS file
             using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
             using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen:true))
             {
                 while (stream.Position != stream.Length)
                 {
                     var rec = new HisFileRecord();
                     rec.Time = reader.ReadUInt32();
                     var count = reader.ReadInt16();
                     if (count > 0)
                     {
                         rec.UnitHistory = new UnitHistoryType[count];
                         for (var i = 0; i < count; i++)
                         {
                            rec.UnitHistory[i] = new UnitHistoryType(stream);
                         }
                     }
                     records.Add(rec);
                 }
             }
             HistoryRecords = records.ToArray();
         }
         public void Save(string fileName)
         {
             //writes HIS file
             using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
             using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
             {
                 foreach (var rec in HistoryRecords)
                 {
                     writer.Write(rec.Time);
                     writer.Write((short)rec.UnitHistory.Length);
                     if (rec.UnitHistory !=null)
                     {
                         for (var i = 0; i < rec.UnitHistory.Length; i++)
                         {
                             rec.UnitHistory[i].Write(stream);
                         }
                     }
                 }
             }
         }
    }
}
