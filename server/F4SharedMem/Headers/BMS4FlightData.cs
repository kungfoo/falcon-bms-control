using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    // changelog:
    // 110: initial BMS 4.33 version
    // 111: added SysTest to LightBits3
    // 112: added MCAnnounced to LightBits3
    // 113: added AllLampBits2OnExceptCarapace to LightBits2 and AllLampBits3OnExceptCarapace to LightBits3
    // 114: renamed WOW LightBit to ONGROUND, added "real" (AFM) WOW to LightBits3
    // 115: renamed "real" WOW in MLGWOW, added NLGWOW
    // 116: bitfields are now unsigned instead of signed
    // 117: added ATF_Not_Engaged to LightBits3
    // 118: added Inlet_Icing to LightBits3

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BMS4FlightData
    {
        // These are outputs from the sim
        public float x;            // Ownship North (Ft)
        public float y;            // Ownship East (Ft)
        public float z;            // Ownship Down (Ft)
        public float xDot;         // Ownship North Rate (ft/sec)
        public float yDot;         // Ownship East Rate (ft/sec)
        public float zDot;         // Ownship Down Rate (ft/sec)
        public float alpha;        // Ownship AOA (Degrees)
        public float beta;         // Ownship Beta (Degrees)
        public float gamma;        // Ownship Gamma (Radians)
        public float pitch;        // Ownship Pitch (Radians)
        public float roll;         // Ownship Roll (Radians)
        public float yaw;          // Ownship Yaw (Radians)
        public float mach;         // Ownship Mach number
        public float kias;         // Ownship Indicated Airspeed (Knots)
        public float vt;           // Ownship True Airspeed (Ft/Sec)
        public float gs;           // Ownship Normal Gs
        public float windOffset;   // Wind delta to FPM (Radians)
        public float nozzlePos;    // Ownship engine nozzle percent open (0-100)
        //float nozzlePos2;   // MOVED TO FlightData2! Ownship engine nozzle2 percent open (0-100) 
        public float internalFuel; // Ownship internal fuel (Lbs)
        public float externalFuel; // Ownship external fuel (Lbs)
        public float fuelFlow;     // Ownship fuel flow (Lbs/Hour)
        public float rpm;          // Ownship engine rpm (Percent 0-103)
        //float rpm2;         // MOVED TO FlightData2! Ownship engine rpm2 (Percent 0-103)
        public float ftit;         // Ownship Forward Turbine Inlet Temp (Degrees C)
        //float ftit2;        // MOVED TO FlightData2! Ownship Forward Turbine Inlet Temp2 (Degrees C)
        public float gearPos;      // Ownship Gear position 0 = up, 1 = down;
        public float speedBrake;   // Ownship speed brake position 0 = closed, 1 = 60 Degrees open
        public float epuFuel;      // Ownship EPU fuel (Percent 0-100)
        public float oilPressure;  // Ownship Oil Pressure (Percent 0-100)
        //float oilPressure2; // MOVED TO FlightData2! Ownship Oil Pressure2 (Percent 0-100)
        public uint lightBits;      // Cockpit Indicator Lights, one bit per bulb. See enum

        // These are inputs. Use them carefully
        // NB: these do not work when TrackIR device is enabled
        // NB2: launch falcon with the '-head' command line parameter to activate !
        public float headPitch;    // Head pitch offset from design eye (radians)
        public float headRoll;     // Head roll offset from design eye (radians)
        public float headYaw;      // Head yaw offset from design eye (radians)

        // new lights
        public uint lightBits2;   // Cockpit Indicator Lights, one bit per bulb. See enum
        public uint lightBits3;   // Cockpit Indicator Lights, one bit per bulb. See enum

        // chaff/flare
        public float ChaffCount;   // Number of Chaff left
        public float FlareCount;   // Number of Flare left

        // landing gear
        public float NoseGearPos;  // Position of the nose landinggear; caution: full down values defined in dat files
        public float LeftGearPos;  // Position of the left landinggear; caution: full down values defined in dat files
        public float RightGearPos; // Position of the right landinggear; caution: full down values defined in dat files

        // ADI values
        public float AdiIlsHorPos; // Position of horizontal ILS bar
        public float AdiIlsVerPos; // Position of vertical ILS bar

        // HSI states
        public int courseState;    // HSI_STA_CRS_STATE
        public int headingState;   // HSI_STA_HDG_STATE
        public int totalStates;    // HSI_STA_TOTAL_STATES; never set

        // HSI values
        public float courseDeviation;  // HSI_VAL_CRS_DEVIATION
        public float desiredCourse;    // HSI_VAL_DESIRED_CRS
        public float distanceToBeacon;    // HSI_VAL_DISTANCE_TO_BEACON
        public float bearingToBeacon;  // HSI_VAL_BEARING_TO_BEACON
        public float currentHeading;      // HSI_VAL_CURRENT_HEADING
        public float desiredHeading;   // HSI_VAL_DESIRED_HEADING
        public float deviationLimit;      // HSI_VAL_DEV_LIMIT
        public float halfDeviationLimit;  // HSI_VAL_HALF_DEV_LIMIT
        public float localizerCourse;     // HSI_VAL_LOCALIZER_CRS
        public float airbaseX;            // HSI_VAL_AIRBASE_X
        public float airbaseY;            // HSI_VAL_AIRBASE_Y
        public float totalValues;         // HSI_VAL_TOTAL_VALUES; never set

        public float TrimPitch;  // Value of trim in pitch axis, -0.5 to +0.5
        public float TrimRoll;   // Value of trim in roll axis, -0.5 to +0.5
        public float TrimYaw;    // Value of trim in yaw axis, -0.5 to +0.5

        // HSI flags
        public uint hsiBits;      // HSI flags

        //DED Lines
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public DED_PFL_LineOfText[] DEDLines;  //25 usable chars
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public DED_PFL_LineOfText[] Invert;    //25 usable chars

        //PFL Lines
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public DED_PFL_LineOfText[] PFLLines;  //25 usable chars
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public DED_PFL_LineOfText[] PFLInvert; //25 usable chars

        //TacanChannel
        public int UFCTChan;
        public int AUXTChan;

        //RWR
        public int RwrObjectCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public int[] RWRsymbol;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public float[] bearing;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public uint[] missileActivity;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public uint[] missileLaunch;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public uint[] selected;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public float[] lethality;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public uint[] newDetection;

        //fuel values
        public float fwd;
        public float aft;
        public float total;

        public int VersionNum;    // Version of FlightData mem area

        // New values added here for header file compatibility but not implemented
        // in this version of the code at present.
        public float headX;        // Head X offset from design eye (feet)
        public float headY;        // Head Y offset from design eye (feet)
        public float headZ;        // Head Z offset from design eye (feet)
        public int MainPower; // Main Power switch state, 0=down, 1=middle, 2=up
    }
}
