using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FlightData2
    {
        // changelog:
        // 1: initial BMS 4.33 version
        // 2: added AltCalReading, altBits, BupUhfPreset, powerBits, blinkBits, cmdsMode
        // 3: added VersionNum, hydPressureA/B, cabinAlt, BupUhfFreq, currentTime, vehicleACD
        // 4: added fuelflow2
        // 5: added RwrInfo, lefPos, tefPos
        // 6: added vtolPos
        // 7: bit fields are now unsigned instead of signed
        // 8: increased RwrInfo size to 512
        // 9: added human pilot names and their status in a session
        // 10: added bump intensity while taxiing/rolling
        // 11: added latitude/longitude
        // 12: added RTT info
        // 13: added IFF panel backup digits
        // 14: added instrument backlight brightness
        // 15: added MiscBits, BettyBits, radar altitude, bingo fuel, cara alow, bullseye, BMS version information, string area size/time, drawing area size
        // 16: added turn rate
        // 17: added Flcs_Flcc, SolenoidStatus to MiscBits

        public const int RWRINFO_SIZE = 512;
        public const int MAX_CALLSIGNS = 32;
        public const int CALLSIGN_LEN = 12;


        // VERSION 1
        public float nozzlePos2;   // Ownship engine nozzle2 percent open (0-100)
        public float rpm2;         // Ownship engine rpm2 (Percent 0-103)
        public float ftit2;        // Ownship Forward Turbine Inlet Temp2 (Degrees C)
        public float oilPressure2; // Ownship Oil Pressure2 (Percent 0-100)
        public byte navMode;  // current mode selected for HSI/eHSI (added in BMS4)
        public float aauz; // Ownship barometric altitude given by AAU (depends on calibration)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)TacanSources.NUMBER_OF_SOURCES)]
        public byte[] tacanInfo;      // Tacan band/mode settings for UFC and AUX COMM

        // VERSION 2
        public int AltCalReading;	// barometric altitude calibration (depends on CalType)
        public uint altBits;		// various altimeter bits, see AltBits enum for details
        public uint powerBits;		// Ownship power bus / generator states, see PowerBits enum for details
        public uint blinkBits;		// Cockpit indicator lights blink status, see BlinkBits enum for details
        // NOTE: these bits indicate only *if* a lamp is blinking, in addition to the
        // existing on/off bits. It's up to the external program to implement the
        // *actual* blinking.
        public int cmdsMode;		// Ownship CMDS mode state, see CmdsModes enum for details
        public int BupUhfPreset;	// BUP UHF channel preset

        // VERSION 3
        public int BupUhfFreq;		// BUP UHF channel frequency
        public float cabinAlt;		// Ownship cabin altitude
        public float hydPressureA;	// Ownship Hydraulic Pressure A
        public float hydPressureB;	// Ownship Hydraulic Pressure B
        public int currentTime;	// Current time in seconds (max 60 * 60 * 24)
        public short vehicleACD;	// Ownship ACD index number, i.e. which aircraft type are we flying.
        public int VersionNum2;		// Version of FlightData2 mem area

        // VERSION 4
        public float fuelFlow2;    // Ownship fuel flow2 (Lbs/Hour)

        // VERSION 5 / 8
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RWRINFO_SIZE)]
        public byte[] RwrInfo;     //[512] New RWR Info
        public float lefPos;       // Ownship LEF position
        public float tefPos;       // Ownship TEF position

        // VERSION 6
        public float vtolPos;      // Ownship VTOL exhaust angle

        // VERSION 9
        public byte pilotsOnline;                  // Number of pilots in an MP session

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CALLSIGNS)]
        public Callsign_LineOfText[] pilotsCallsign;        // [MAX_CALLSIGNS][CALLSIGN_LEN] List of pilots callsign connected to an MP session

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CALLSIGNS)]
        public byte[] pilotsStatus;                // [MAX_CALLSIGNS] Status of the MP pilots, see enum FlyStates


        //VERSION 10
        public float bumpIntensity; // Intensity of a "bump" while taxiing/rolling, 0..1

        //VERSION 11
        public float latitude;      // Ownship latitude in degrees (as known by avionics)
        public float longitude;     // Ownship longitude in degrees (as known by avionics)

        //VERSION 12
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public ushort[] RTT_size;                 // RTT overall width and height
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)RTT_areas.RTT_noOfAreas * 4)]
        public ushort[] RTT_area;  // For each area: left/top/right/bottom

        // VERSION 13
        public byte iffBackupMode1Digit1;                     // IFF panel backup Mode1 digit 1
        public byte iffBackupMode1Digit2;                     // IFF panel backup Mode1 digit 2
        public byte iffBackupMode3ADigit1;                    // IFF panel backup Mode3A digit 1
        public byte iffBackupMode3ADigit2;                    // IFF panel backup Mode3A digit 2

        // VERSION 14
        public byte instrLight;  // (unsigned char) current instrument backlight brightness setting, see InstrLight enum for details

        // VERSION 15
        public uint bettyBits;      // see BettyBits enum for details
        public uint miscBits;        // see MiscBits enum for details
        public float RALT;                  // radar altitude (only valid/ reliable if MiscBit "RALT_Valid" is set)
        public float bingoFuel;             // bingo fuel level
        public float caraAlow;              // cara alow setting
        public float bullseyeX;             // bullseye X in sim coordinates (same as ownship, i.e. North (Ft))
        public float bullseyeY;             // bullseye Y in sim coordinates (same as ownship, i.e. East (Ft))
        public int BMSVersionMajor;         // E.g.  4.
        public int BMSVersionMinor;         //         34.
        public int BMSVersionMicro;         //            1
        public int BMSBuildNumber;          //              build 20050
        public uint StringAreaSize; // the overall size of the StringData/FalconSharedMemoryAreaString shared memory area
        public uint StringAreaTime; // last time the StringData/FalconSharedMemoryAreaString shared memory area has been changed - you only need to re-read the string shared mem if this changes
        public uint DrawingAreaSize;// the overall size of the DrawingData/FalconSharedMemoryAreaDrawing area

        // VERSION 16
        float turnRate;              // actual turn rate (no delay or dampening) in degrees/second

    }

}
