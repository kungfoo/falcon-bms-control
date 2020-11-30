using F4Utils.Campaign.F4Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.Save
{
    public class PriFiles
    {
        byte[][] DefaultObjtypePriority=new byte[(int)AirTacticTypeEnum.TAT_CAS][];		// AI's suggested settings
        byte[][] DefaultUnittypePriority=new byte[(int)AirTacticTypeEnum.TAT_CAS][];		
        byte[][] DefaultMissionPriority=new byte[(int)AirTacticTypeEnum.TAT_CAS][];		

        public PriFiles(string campaignSaveFolderPath) 
        {
            LoadPriFiles(campaignSaveFolderPath);
        }
        public void LoadPriFiles(string campaignSaveFolderPath) 
        {
            for (var t=0; t<(int)AirTacticTypeEnum.TAT_CAS; t++)
		    {
                PriFile priFile;
		        switch (t+1)
			    {
			        case (int)AirTacticTypeEnum.TAT_DEFENSIVE:
				        priFile = new PriFile(Path.Combine(campaignSaveFolderPath, "defense.pri"));
				        break;
			        case (int)AirTacticTypeEnum.TAT_OFFENSIVE:
                        priFile = new PriFile(Path.Combine(campaignSaveFolderPath, "offense.pri"));
				        break;
			        case (int)AirTacticTypeEnum.TAT_ATTRITION:
                        priFile = new PriFile(Path.Combine(campaignSaveFolderPath, "attrit.pri"));
				        break;
			        case (int)AirTacticTypeEnum.TAT_CAS:
                        priFile = new PriFile(Path.Combine(campaignSaveFolderPath, "cas.pri"));
				        break;
			        default:
                        priFile = new PriFile(Path.Combine(campaignSaveFolderPath, "intdict.pri"));
				        break;

                }
                DefaultObjtypePriority[t] = priFile.ObjectiveTargetPriorities;
                DefaultUnittypePriority[t] = priFile.UnitTypePriorities;
                DefaultMissionPriority[t] = priFile.MissionPriorities;
            }

        }
    }
}
