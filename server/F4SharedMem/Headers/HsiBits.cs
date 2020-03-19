using System;

namespace F4SharedMem.Headers
{
    [Flags]
    [Serializable]
    public enum HsiBits : uint
    {
        ToTrue = 0x01,    // HSI_FLAG_TO_TRUE == 1, TO
        IlsWarning = 0x02,    // HSI_FLAG_ILS_WARN
        CourseWarning = 0x04,    // HSI_FLAG_CRS_WARN
        Init = 0x08,    // HSI_FLAG_INIT
        TotalFlags = 0x10,    // HSI_FLAG_TOTAL_FLAGS; never set
        ADI_OFF = 0x20,    // ADI OFF Flag
        ADI_AUX = 0x40,    // ADI AUX Flag
        ADI_GS = 0x80,    // ADI GS FLAG
        ADI_LOC = 0x100,   // ADI LOC FLAG
        HSI_OFF = 0x200,   // HSI OFF Flag
        BUP_ADI_OFF = 0x400,   // Backup ADI Off Flag
        VVI = 0x800,   // VVI OFF Flag
        AOA = 0x1000,  // AOA OFF Flag
        AVTR = 0x2000,  // AVTR Light
        OuterMarker = 0x4000,  // MARKER beacon light for outer marker
        MiddleMarker = 0x8000,  // MARKER beacon light for middle marker
        FromTrue = 0x10000, // HSI_FLAG_TO_TRUE == 2, FROM

        Flying = 0x80000000, // true if player is attached to an aircraft (i.e. not in UI state).  NOTE: Not a lamp bit

        // Used with the MAL/IND light code to light up "everything"
        // please update this is you add/change bits!
        AllLampHsiBitsOn = 0xE000
    };
}
