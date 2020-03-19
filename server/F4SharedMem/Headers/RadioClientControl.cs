using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RadioClientControl
    {
        [MarshalAs(UnmanagedType.I4)]
        public int PortNumber;                        // socket number to use in contacting the server

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.RCC_STRING_LENGTH)]
        public byte[] Address;                       // string representation of server IPv4 dotted number address

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.RCC_STRING_LENGTH)]
        public byte[] Password;                    // plain text of password for voice server access

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.RCC_STRING_LENGTH)]
        public byte[] Nickname;                      // player nickname 

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.NUMBER_OF_RADIOS)]
        public RadioChannel[] Radios;

        [MarshalAs(UnmanagedType.U1)]
        public bool SignalConnect;                     // tell the client we are ready to try a connection with the current settings

        [MarshalAs(UnmanagedType.U1)]
        public bool TerminateClient;                   // indicate to external client that it should shut down now

        [MarshalAs(UnmanagedType.U1)]
        public bool FlightMode;						 // true when in 3D world, false for UI state

        [MarshalAs(UnmanagedType.U1)]
        public bool UseAGC;							 // true when external voice client should use AGC features

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.NUMBER_OF_DEVICES)]
        public RadioDevice[] Devices;

        [MarshalAs(UnmanagedType.I4)]
        public int PlayerCount;                        // number of players for whom we have data in the telemetry map

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.PLAYER_MAP_SIZE)]
        public Telemetry[] PlayerMap;    // array of player telemetry data relative to ownship (held in entry zero)
    }

    [Serializable]
    public enum Radios : byte
    {
        UHF = 0,
        VHF,
        GUARD,
        NUMBER_OF_RADIOS,
    };

    [Serializable]
    public enum Devices : byte
    {
        MAIN = 0,
        NUMBER_OF_DEVICES,
    };

    [Serializable]
    public struct Constants
    {
        public const int MAX_VOLUME = 0;
        public const int MIN_VOLUME = 10000;
        public const int Zero_dB_Raw_Volume_Default = 1304; // on a scale from +6dB ro -40dB
        public const int NAME_LEN = 20;
        public const int PLAYER_MAP_SIZE = 96;
        public const int NUMBER_OF_RADIOS = 3;
        public const int NUMBER_OF_DEVICES = 1;
        public const int RCC_STRING_LENGTH = 64;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Telemetry
    {
        // Data
        [MarshalAs(UnmanagedType.R4)]
        public float Agl;  // height above terrain in feet

        [MarshalAs(UnmanagedType.R4)]
        public float Range;  // range of remote player to ownship in nautical miles

        [MarshalAs(UnmanagedType.U4)]
        public uint Flags;  // status information

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.NAME_LEN + 1)]
        public byte[] LogbookName;  // copy of player logbook name

        [MarshalAs(UnmanagedType.U1)]
        public byte padding1;

        [MarshalAs(UnmanagedType.U1)]
        public byte padding2;

        [MarshalAs(UnmanagedType.U1)]
        public byte padding3;
    }

    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct RadioDevice
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.I4)]
        public int IcVolume; // INTERCOM volume
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RadioChannel
    {
        [MarshalAs(UnmanagedType.I4)]
        public int Frequency;     // 6 digit MHz frequency x1000 (i.e. no decimal places)

        [MarshalAs(UnmanagedType.I4)]
        public int RxVolume;      // 0-15000 range, high to low respectively

        [MarshalAs(UnmanagedType.U1)]
        public bool PttDepressed;  // true for transmit switch activated

        [MarshalAs(UnmanagedType.U1)]
        public bool IsOn;          // true if this channel is associated with a radio that is on

        [MarshalAs(UnmanagedType.U1)]
        public byte padding1;

        [MarshalAs(UnmanagedType.U1)]
        public byte padding2;
    }
    [Flags]
    [Serializable]
    public enum TelemetryFlags : byte
    {
        NoFlags = 0x00,
        HasPlayerLoS = 0x01,
        IsAircraft = 0x02,
    };
}
