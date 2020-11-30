using F4Utils.Campaign.F4Structs;
using System;
using System.IO;
using System.Text;

namespace F4Utils.Campaign.Save
{
    public class PriFile
    {
        public byte[] ObjectiveTargetPriorities=new byte[TeamConstants.MAX_TGTTYPE];	
        public byte[] UnitTypePriorities = new byte[TeamConstants.MAX_UNITTYPE];		
        public byte[] MissionPriorities = new byte[(int)MissionTypeEnum.AMIS_OTHER];		

        public PriFile(string fileName)
        {
            LoadPriFile(fileName);
        }

        private void LoadPriFile(string fileName)
        {
            int n=0;
            //reads PRI file
            using (var stream = new FileStream(fileName, FileMode.Open))
            using (var reader = new StreamReader(stream))
            {

                n = ParseInt(GetNextToken(reader));		// index into objective target priorities table
                while (n >= 0)
                {
                    ObjectiveTargetPriorities[n] = (byte)ParseInt(GetNextToken(reader));
                    n = ParseInt(GetNextToken(reader));
                }
                n = ParseInt(GetNextToken(reader));		// index into unit target priorities table
                while (n >= 0)
                {
                    UnitTypePriorities[n] = (byte)ParseInt(GetNextToken(reader));
                    n = ParseInt(GetNextToken(reader));
                }
                n = ParseInt(GetNextToken(reader));		// index into mission priorities table
                while (n >= 0)
                {
                    MissionPriorities[n] = (byte)ParseInt(GetNextToken(reader));
                    n = ParseInt(GetNextToken(reader));
                }

            }
        }
        private int ParseInt(string a)
        {
            int toReturn = 0;
            Int32.TryParse(a.Trim(), out toReturn);
            return toReturn;
        }
        private string GetNextToken(StreamReader reader)
        {
            var sb = new StringBuilder();
            while (reader.Peek() >= 0)
            {
                char nextChar = (char)reader.Read();
                if (nextChar == ';' || nextChar == '#' || nextChar == '\r' || nextChar == '\n') //if this is a newline or it's the start of a comment
                {
                    reader.ReadLine(); //skip to end of line
                    if (sb.Length > 0)
                    {
                        break;
                    }
                }
                else if (char.IsWhiteSpace(nextChar))
                {
                    if (sb.Length > 0)
                    {
                        break;
                    }
                }
                else 
                {
                    sb.Append(nextChar);
                }
            }
            return sb.ToString().Trim();
        }
       

    }
}
