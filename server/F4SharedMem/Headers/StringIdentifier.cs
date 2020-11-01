using System;

namespace F4SharedMem.Headers
{
    [Serializable]
    public enum StringIdentifier : uint
    {
        // VERSION 1
        BmsExe = 0,             // BMS exe name, full path
        KeyFile,                // Key file name in use, full path

        BmsBasedir,             // BmsBasedir to BmsPictureDirectory:
        BmsBinDirectory,        // - BMS directories in use
        BmsDataDirectory,
        BmsUIArtDirectory,
        BmsUserDirectory,
        BmsAcmiDirectory,
        BmsBriefingsDirectory,
        BmsConfigDirectory,
        BmsLogsDirectory,
        BmsPatchDirectory,
        BmsPictureDirectory,

        ThrName,                 // Current theater name
        ThrCampaigndir,          // ThrCampaigndir to ThrTacrefpicsdir:
        ThrTerraindir,           // - Current theater directories in use
        ThrArtdir,
        ThrMoviedir,
        ThrUisounddir,
        ThrObjectdir,
        Thr3ddatadir,
        ThrMisctexdir,
        ThrSounddir,
        ThrTacrefdir,
        ThrSplashdir,
        ThrCockpitdir,
        ThrSimdatadir,
        ThrSubtitlesdir,
        ThrTacrefpicsdir,

        AcName,                  // Current AC name
        AcNCTR,                  // Current AC NCTR

        // VERSION 2
        ButtonsFile,             // Current 3dbuttons.dat file full path
        CockpitFile,             // Current 3dckpit.dat file full path

        // VERSION 3
        NavPoint,                // Multiple entries, one for each NavPoint. Format for each entry is (NP, O1, O2, PT can be concatenated):
                                 // (NavPoint, mandatory) NP:<index>,<type>,<x>,<y>,<z>,<grnd_elev>;
                                 //     <index>        int            NavPoint number, 1-99
                                 //     <type>         two chars      GM (GMPOINT), PO (POSPOINT), WP (WAYPOINT), MK (MARKPOINT), DL (DATALINK)
                                 //                                   CB (CAMPBULLSEYE), L1 (LINE1), L2 (LINE2), L3 (LINE3), L4 (LINE4), PT (PREPLANNEDTHREAD)
                                 //     <x>,<y>        float          position in sim coordinates
                                 //     <z>            float          altitude in 10s of feet
                                 //     <grnd_elev>    float          ground elevation in 10s of feet
                                 // (OA1/OA2, optional) O1:<bearing>,<range>,<alt>; (and/or) O2:<bearing>,<range>,<alt>;
                                 //     <bearing>      float
                                 //     <range>        unsigned int
                                 //     <alt>          unsigned int
                                 // (PPT, optional) PT:<str_id>,<range>,<declutter>;
                                 //     <str_id>       "string"
                                 //     <range>        float
                                 //     <declutter>    int            0 = false, 1 = true

        // FIXED LAST ENTRY

        StringIdentifier_DIM     // (number of identifiers; add new IDs only *above* this one)
    };
}
