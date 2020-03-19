using System;
using System.Text;

namespace F4SharedMem.Headers
{
    // - NOTE: Check DrawingAreaSize in FalconSharedMemoryArea2 for the actual size of this area!
    // - NOTE: Treat this shared memory area as a pure "char*", not as "DrawingData*", since the size is not fixed!

    // changelog:
    // 1: initial BMS 4.35 version: added export of 2D drawing commands for HUD, RWR, and HMS in the following string variables
    //     HUD_commands  (only populated if g_bExportDrawingCommandsForHUD is set to 1 in Falcon BMS config)
    //     RWR_commands  (only populated if g_bExportDrawingCommandsForRWR is set to 1 in Falcon BMS config)
    //     HMS_commands  (only populated if g_bExportDrawingCommandsForHMS is set to 1 in Falcon BMS config)
    //
    //         Common data format for 2D drawing command strings:
    //         Command strings consist of a symbol indicating the command type, followed by a colon, followed by a comma-delimited list of arguments for that commmand, all followed by a terminating semicolon. e.g;
    //	           COMMAND: arg1, arg2, ..., argN;  
    //
    //         The following table lists all the available commands and their arguments. 
    //
    //		   Command       Arguments          Type              Description
    //         -------       ---------          ---------        -------------------------------------------------------------------
    //          R:                                               Set canvas resolution
    //                       width,               int                canvas width
    //                       height;              int                canvas height
    //
    //			F:                                               Set font            
    //                       "fontFile";          string              font texture file
    //
    //			P:                                               Draw point           
    //                       x,                   float               X coordinate
    //                       y;                   float               Y coordinate
    //
    //			L:                                               Draw line           
    //                       x1,                  float               starting X coordinate
    //                       y1,                  float               starting Y coordinate
    //                       x2,                  float               ending X coordinate
    //                       y2;                  float               ending Y coordinate
    //
    //			T:                                               Draw filled triangle           
    //                       x1,                  float               vertex 1 X coordinate
    //                       y1,                  float               vertex 1 Y coordinate
    //                       x2,                  float               vertex 2 X coordinate
    //                       y2,                  float               vertex 2 Y coordinate
    //                       x3,                  float               vertex 3 X coordinate
    //                       y3;                  float               vertex 3 Y coordinate
    //
    //			S:                                               Draw string
    //                       xLeft,               float               X coordinate 
    //                       yTop,                float               Y coordinate 
    //                       "textString",        string              string to draw
    //                       invert;              unsigned char       0=draw text normally; 1=draw text inverted
    //
    //			SR:                                               Draw string with rotated text
    //                       xLeft,               float               X coordinate
    //                       yTop,                float               Y coordinate 
    //                       "textString",        string              string to draw
    //                       angle;               float               rotation angle (radians)
    //
    //			FG:                                              Set foreground (text and line) color
    //                       packedABGR;          unsigned int        foreground color in packed Alpha-Blue-Green-Red bit order (8 bits for each component)
    //                                                                   alpha: bits 24-31 (most significant 8 bits)
    //                                                                   blue:  bits 16-23
    //                                                                   green: bits 8-15
    //                                                                   red:   bits 0-7 (least significant 8 bits)
    //
    //			BG:                                              Set background (text and line) color
    //                       packedABGR;          unsigned int        foreground color in packed Alpha-Blue-Green-Red bit order (8 bits for each component)
    //                                                                   alpha: bits 24-31 (most significant 8 bits)
    //                                                                   blue:  bits 16-23
    //                                                                   green: bits 8-15
    //                                                                   red:   bits 0-7 (least significant 8 bits)

    // *** "FalconSharedMemoryAreaDrawing" ***
    [Serializable]
    public class DrawingData
    {
        public const uint DRAWINGDATA_AREA_SIZE_MAX = 1024 * 1024;

        public uint VersionNum;      // Version of the DrawingData shared memory area

        public uint HUD_length;      // The length of the string in "HUD_commands", *without* termination, note that HUD_commands *does* have termination
        public string HUD_commands;  

        public uint RWR_length;      // The length of the string in "RWR_commands", *without* termination, note that RWR_commands *does* have termination
        public string RWR_commands; 

        public uint HMS_length;      // The length of the string in "HMS_commands", *without* termination, note that HMS_commands *does* have termination
        public string HMS_commands;  

        public static DrawingData GetDrawingData(byte[] rawDrawingData)
        {
            var result = new DrawingData();
            var offset = 0;
            result.VersionNum = BitConverter.ToUInt32(rawDrawingData, offset);
            offset += sizeof(uint);

            result.HUD_length = BitConverter.ToUInt32(rawDrawingData, offset);
            offset += sizeof(uint);
            if (offset + result.HUD_length + 1> rawDrawingData.Length) return result;
            result.HUD_commands = Encoding.Default.GetString(rawDrawingData, offset, (int)result.HUD_length);
            offset += (int)result.HUD_length +1;

            result.RWR_length = BitConverter.ToUInt32(rawDrawingData, offset);
            offset += sizeof(uint);
            if (offset + result.RWR_length + 1 > rawDrawingData.Length) return result;
            result.RWR_commands = Encoding.Default.GetString(rawDrawingData, offset, (int)result.RWR_length);
            offset += (int)result.RWR_length + 1;

            result.HMS_length = BitConverter.ToUInt32(rawDrawingData, offset);
            offset += sizeof(uint);
            if (offset + result.HMS_length + 1 > rawDrawingData.Length) return result;
            result.HMS_commands = Encoding.Default.GetString(rawDrawingData, offset, (int)result.HMS_length);

            return result;
        }
    }
}