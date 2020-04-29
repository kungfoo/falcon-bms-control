using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4KeyFile
{
    public enum Callbacks
    {
        [Category("NOOP")]
        [SubCategory("NOOP")]
        [ShortDescription("Do nothing")]
        [Description("Do Nothing")]
        SimDoNothing,


        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("Fire&Oheat Detect")]
        [Description("FIRE & OHEAT DETECT Button - Hold")]
        SimOverHeat,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("Oxy Qty Switch")]
        [Description("OXY QTY Switch - Hold")]
        SimOBOGSBit,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("MalIndLts Hold")]
        [Description("MAL & IND LTS Button - Hold")]
        SimMalIndLights,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("MalIndLts Rel")]
        [Description("MAL & IND LTS Button - Release")]
        SimMalIndLightsOFF,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("Probe Heat Up")]
        [Description("PROBE HEAT Switch - Step Up")]
        SimProbeHeatMoveUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("Probe Heat Dn")]
        [Description("PROBE HEAT Switch - Step Down")]
        SimProbeHeatMoveDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("Probe Heat On")]
        [Description("PROBE HEAT Switch - ON")]
        SimProbeHeatOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("Probe Heat Off")]
        [Description("PROBE HEAT Switch - OFF")]
        SimProbeHeatOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("Probe Heat Test")]
        [Description("PROBE HEAT Switch - TEST")]
        SimProbeHeatTest,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("EPU GEN Switch")]
        [Description("EPU/GEN Switch - Hold")]
        SimEpuGenTest,

        [Category("LEFT CONSOLE")]
        [SubCategory("TEST PANEL")]
        [ShortDescription("FLCS PWR Test")]
        [Description("FLCS PWR TEST Switch - Hold")]
        SimFlcsPowerTest,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Digital Sw Toggle")]
        [Description("DIGITAL Switch - Toggle")]
        SimDigitalBUP,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Digital Sw Backup")]
        [Description("DIGITAL Switch - BACKUP")]
        SimDigitalBUPBackup,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Digital Switch Off")]
        [Description("DIGITAL Switch - OFF")]
        SimDigitalBUPOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Alt Flaps Tog")]
        [Description("ALT FLAPS Switch - Toggle")]
        SimAltFlaps,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Alt Flaps Extend")]
        [Description("ALT FLAPS Switch - EXTEND")]
        SimAltFlapsExtend,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Alt Flaps Norm")]
        [Description("ALT FLAPS Switch - NORM")]
        SimAltFlapsNorm,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Man TF Flyup Tog")]
        [Description("MANUAL TF FLYUP Switch - Toggle")]
        SimManualFlyup,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Man TF Flyup Dis")]
        [Description("MANUAL TF FLYUP Switch - DISABLE")]
        SimManualFlyupDisable,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Man TF Flyup En")]
        [Description("MANUAL TF FLYUP Switch - ENABLE")]
        SimManualFlyupEnable,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("LE Flaps Tog")]
        [Description("LE FLAPS Switch - Toggle")]
        SimLEFLockSwitch,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("LE Flaps Lock")]
        [Description("LE FLAPS Switch - LOCK")]
        SimLEFLock,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("LE Flaps Auto")]
        [Description("LE FLAPS Switch - AUTO")]
        SimLEFAuto,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("FLCS Switch")]
        [Description("FLCS Switch - Hold")]
        SimFLCSReset,

        [Category("LEFT CONSOLE")]
        [SubCategory("FLT CONTROL PANEL")]
        [ShortDescription("Bit Switch")]
        [Description("BIT Switch - Push")]
        SimFLTBIT,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim L Wing Dn")]
        [Description("ROLL TRIM Wheel - L WING DN")]
        SimTrimRollLeft,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim R Wing Dn")]
        [Description("ROLL TRIM Wheel - R WING DN")]
        SimTrimRollRight,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim/AP Tog")]
        [Description("TRIM/AP DISC Switch - Toggle")]
        SimTrimAPToggle,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim/AP Disc")]
        [Description("TRIM/AP DISC Switch - DISC")]
        SimTrimAPDISC,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim/AP Norm")]
        [Description("TRIM/AP DISC Switch - NORM")]
        SimTrimAPNORM,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim Yaw Left")]
        [Description("YAW TRIM Knob - L")]
        SimTrimYawLeft,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim Yaw Right")]
        [Description("YAW TRIM Knob - R")]
        SimTrimYawRight,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim Nose Up")]
        [Description("PITCH TRIM Wheel - NOSE UP")]
        SimTrimNoseUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("MANUAL TRIM PANEL")]
        [ShortDescription("Trim Nose Dn")]
        [Description("PITCH TRIM Wheel - NOSE DN")]
        SimTrimNoseDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Fuel Master Tog")]
        [Description("MASTER Switch - Toggle")]
        SimToggleMasterFuel,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Fuel Master On")]
        [Description("MASTER Switch - ON")]
        SimMasterFuelOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Fuel Master Off")]
        [Description("MASTER Switch - OFF")]
        SimMasterFuelOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Eng Feed Up")]
        [Description("ENG FEED Knob - Step Up")]
        SimIncFuelPump,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Eng Feed Dn")]
        [Description("ENG FEED Knob - Step Down")]
        SimDecFuelPump,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Eng Feed Off")]
        [Description("ENG FEED Knob - OFF")]
        SimFuelPumpOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Eng Feed Norm")]
        [Description("ENG FEED Knob - NORM")]
        SimFuelPumpNorm,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Eng Feed Aft")]
        [Description("ENG FEED Knob - AFT")]
        SimFuelPumpAft,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Eng Feed Fwd")]
        [Description("ENG FEED Knob - FWD")]
        SimFuelPumpFwd,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Air Refuel Tog")]
        [Description("AIR REFUEL Switch - Toggle")]
        SimFuelDoorToggle,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Air Refuel Open")]
        [Description("AIR REFUEL Switch - OPEN")]
        SimFuelDoorOpen,

        [Category("LEFT CONSOLE")]
        [SubCategory("FUEL PANEL")]
        [ShortDescription("Air Refuel Close")]
        [Description("AIR REFUEL Switch - CLOSE")]
        SimFuelDoorClose,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("CNI Knob Tog")]
        [Description("CNI Knob Switch - Toggle")]
        SimToggleAuxComMaster,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("CNI Knob Bckup")]
        [Description("CNI Knob Switch - BACKUP")]
        SimAuxComBackup,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("CNI Knob UFC")]
        [Description("CNI Knob Switch - UFC")]
        SimAuxComUFC,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Left Channel Up")]
        [Description("CHANNEL - Cycle Up Left Digit")]
        SimCycleLeftAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Left Channel Dn ")]
        [Description("CHANNEL - Cycle Down Left Digit")]
        SimDecLeftAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Centr Channel Up")]
        [Description("CHANNEL - Cycle Up Center Digit")]
        SimCycleCenterAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Centr Channel Dn")]
        [Description("CHANNEL - Cycle Down Center Dig.")]
        SimDecCenterAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Right Channel Up")]
        [Description("CHANNEL - Cycle Up Right Digit")]
        SimCycleRightAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Right Channel Dn")]
        [Description("CHANNEL - Cycle Down Right Digit")]
        SimDecRightAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Channel Tog X/Y")]
        [Description("CHANNEL - Toggle Band X/Y")]
        SimCycleBandAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Channel X")]
        [Description("CHANNEL - Toggle Band X")]
        SimXBandAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Channel Y")]
        [Description("CHANNEL - Toggle Band Y")]
        SimYBandAuxComDigit,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Station Sel Tog")]
        [Description("STATION SELECTOR Switch - Toggle")]
        SimToggleAuxComAATR,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Station Sel T/R")]
        [Description("STATION SELECTOR Switch - T/R")]
        SimTACANTR,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUX COMM PANEL")]
        [ShortDescription("Station Sel A/A TR")]
        [Description("STATION SELECTOR Switch - A/A TR")]
        SimTACANAATR,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Anti Coll Lts Tog")]
        [Description("ANTI COLLISION Switch - Toggle")]
        SimExtlAntiColl,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Anti Coll Lts On")]
        [Description("ANTI COLLISION Switch - ON")]
        SimAntiCollOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Anti Coll Lts Off")]
        [Description("ANTI COLLISION Switch - OFF")]
        SimAntiCollOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Position Lts Tog")]
        [Description("POSITION Switch - Toggle")]
        SimExtlSteady,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Position Lts Flash")]
        [Description("POSITION Switch - FLASH")]
        SimLightsFlash,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Position Lts Stdy")]
        [Description("POSITION Switch - STEADY")]
        SimLightsSteady,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Wing/Fus Lts Tog")]
        [Description("WING/TAIL Switch - Toggle")]
        SimExtlWing,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Wing/Fus Lts Brt")]
        [Description("WING/TAIL Switch - BRT")]
        SimWingLightBrt,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Wing/Fus Lts Off")]
        [Description("WING/TAIL Switch - OFF")]
        SimWingLightOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Master Lts Tog")]
        [Description("MASTER Switch - Toggle")]
        SimExtlPower,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Master Lts Norm")]
        [Description("MASTER Switch - NORM")]
        SimExtlMasterNorm,

        [Category("LEFT CONSOLE")]
        [SubCategory("EXT LIGHTING PANEL")]
        [ShortDescription("Master Lts Off")]
        [Description("MASTER Switch - OFF")]
        SimExtlMasterOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("EPU PANEL")]
        [ShortDescription("EPU Sw Cycl")]
        [Description("EPU Switch - Cycle")]
        SimEpuToggle,

        [Category("LEFT CONSOLE")]
        [SubCategory("EPU PANEL")]
        [ShortDescription("EPU Sw Step Up")]
        [Description("EPU Switch - Step Up")]
        SimEpuUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("EPU PANEL")]
        [ShortDescription("EPU Sw Step Dn")]
        [Description("EPU Switch - Step Down")]
        SimEpuDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("EPU PANEL")]
        [ShortDescription("EPU Sw On")]
        [Description("EPU Switch - ON")]
        SimEpuOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("EPU PANEL")]
        [ShortDescription("EPU Sw Norm")]
        [Description("EPU Switch - NORM")]
        SimEpuAuto,

        [Category("LEFT CONSOLE")]
        [SubCategory("EPU PANEL")]
        [ShortDescription("EPU Sw Off")]
        [Description("EPU Switch - OFF")]
        SimEpuOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("ELEC PANEL")]
        [ShortDescription("Main Pwr Step Up")]
        [Description("MAIN PWR Switch - Step Up")]
        SimMainPowerInc,

        [Category("LEFT CONSOLE")]
        [SubCategory("ELEC PANEL")]
        [ShortDescription("Main Pwr Step Dn")]
        [Description("MAIN PWR Switch - Step Down")]
        SimMainPowerDec,

        [Category("LEFT CONSOLE")]
        [SubCategory("ELEC PANEL")]
        [ShortDescription("Main Pwr Main")]
        [Description("MAIN PWR Switch - MAIN")]
        SimMainPowerMain,

        [Category("LEFT CONSOLE")]
        [SubCategory("ELEC PANEL")]
        [ShortDescription("Main Pwr Batt")]
        [Description("MAIN PWR Switch - BATT")]
        SimMainPowerBatt,

        [Category("LEFT CONSOLE")]
        [SubCategory("ELEC PANEL")]
        [ShortDescription("Main Pwr Off")]
        [Description("MAIN PWR Switch - OFF")]
        SimMainPowerOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("ELEC PANEL")]
        [ShortDescription("Caution Reset")]
        [Description("CAUTION RESET Button - Push")]
        SimElecReset,

        [Category("LEFT CONSOLE")]
        [SubCategory("AVTR PANEL")]
        [ShortDescription("AVTR Sw Tog ")]
        [Description("AVTR Switch - Toggle ON / OFF")]
        SimAVTRToggle,

        [Category("LEFT CONSOLE")]
        [SubCategory("AVTR PANEL")]
        [ShortDescription("AVTR Sw Cycle")]
        [Description("AVTR Switch - Cycle")]
        SimAVTRSwitch,

        [Category("LEFT CONSOLE")]
        [SubCategory("AVTR PANEL")]
        [ShortDescription("AVTR Sw Up")]
        [Description("AVTR Switch - Step Up")]
        SimAVTRSwitchUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("AVTR PANEL")]
        [ShortDescription("AVTR Sw Dn")]
        [Description("AVTR Switch - Step Down")]
        SimAVTRSwitchDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("AVTR PANEL")]
        [ShortDescription("AVTR Sw On")]
        [Description("AVTR Switch - ON")]
        SimAVTRSwitchOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("AVTR PANEL")]
        [ShortDescription("AVTR Sw Auto")]
        [Description("AVTR Switch - AUTO")]
        SimAVTRSwitchAuto,

        [Category("LEFT CONSOLE")]
        [SubCategory("AVTR PANEL")]
        [ShortDescription("AVTR Sw Off")]
        [Description("AVTR Switch - OFF")]
        SimAVTRSwitchOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("ECM PANEL")]
        [ShortDescription("ECM Opr Tog")]
        [Description("OPR Switch - Toggle")]
        SimEcmPower,

        [Category("LEFT CONSOLE")]
        [SubCategory("ECM PANEL")]
        [ShortDescription("ECM Opr On")]
        [Description("OPR Switch - OPR")]
        SimEcmPowerOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("ECM PANEL")]
        [ShortDescription("ECM Opr Off")]
        [Description("OPR Switch - OFF")]
        SimEcmPowerOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("ENG & JET START PANEL")]
        [ShortDescription("JFS Tog Start")]
        [Description("JFS Switch - Toggle! START 2 / OFF")]
        SimJfsStart,

        [Category("LEFT CONSOLE")]
        [SubCategory("ENG & JET START PANEL")]
        [ShortDescription("JFS Off")]
        [Description("JFS Switch - OFF")]
        SimJfsStart_Off,

        [Category("LEFT CONSOLE")]
        [SubCategory("ENG & JET START PANEL")]
        [ShortDescription("JFS Start2")]
        [Description("JFS Switch - START 2")]
        SimJfsStart_Start2,

        [Category("LEFT CONSOLE")]
        [SubCategory("ENG & JET START PANEL")]
        [ShortDescription("Eng Cont Tog")]
        [Description("ENG CONT Switch - Toggle")]
        SimEngCont,

        [Category("LEFT CONSOLE")]
        [SubCategory("ENG & JET START PANEL")]
        [ShortDescription("Eng Cont Pri")]
        [Description("ENG CONT Switch - PRI")]
        SimEngContPri,

        [Category("LEFT CONSOLE")]
        [SubCategory("ENG & JET START PANEL")]
        [ShortDescription("Eng Cont Sec")]
        [Description("ENG CONT Switch - SEC")]
        SimEngContSec,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 2 PANEL")]
        [ShortDescription("Intercom Vol Inc")]
        [Description("INTERCOM Knob - Volume Incr.")]
        SimStepIntercomVolumeUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 2 PANEL")]
        [ShortDescription("Intercom Vol Dec")]
        [Description("INTERCOM Knob - Volume Decr.")]
        SimStepIntercomVolumeDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 2 PANEL")]
        [ShortDescription("ILS Knob Tog")]
        [Description("ILS Knob - Toggle")]
        SimILS,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 2 PANEL")]
        [ShortDescription("ILS Knob On")]
        [Description("ILS Knob - ON")]
        SimILSOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 2 PANEL")]
        [ShortDescription("ILS Knob Off")]
        [Description("ILS Knob - OFF")]
        SimILSOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm1 Vol Inc")]
        [Description("COMM 1 Knob - Volume Incr.")]
        SimStepComm1VolumeUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm1 Vol Dec")]
        [Description("COMM 1 Knob - Volume Decr.")]
        SimStepComm1VolumeDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm1 Pwr On")]
        [Description("COMM 1 Knob - Power On")]
        SimComm1PowerOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm1 Pwr Off")]
        [Description("COMM 1 Knob - Power Off")]
        SimComm1PowerOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm2 Vol Inc")]
        [Description("COMM 2 Knob - Volume Incr.")]
        SimStepComm2VolumeUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm2 Vol Dec")]
        [Description("COMM 2 Knob - Volume Decr.")]
        SimStepComm2VolumeDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm2 Pwr On")]
        [Description("COMM 2 Knob - Power On")]
        SimComm2PowerOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm2 Pwr Off")]
        [Description("COMM 2 Knob - Power Off")]
        SimComm2PowerOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("MSL Vol Inc")]
        [Description("MSL Knob - Volume Incr.")]
        SimStepMissileVolumeUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("MSL Vol Dec")]
        [Description("MSL Knob - Volume Decr.")]
        SimStepMissileVolumeDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Com1 Mode Tog")]
        [Description("COMM 1 Mode Knob - Toggle")]
        SimAud1Com1,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm1 Mode Sql")]
        [Description("COMM 1 Mode Knob - SQL")]
        SimAud1Com1Sql,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm1 Mode Gd")]
        [Description("COMM 1 Mode Knob - GD")]
        SimAud1Com1Gd,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Com2 Mode Tog")]
        [Description("COMM 2 Mode Knob - Toggle")]
        SimAud1Com2,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm2 Mode Sql")]
        [Description("COMM 2 Mode Knob - SQL")]
        SimAud1Com2Sql,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Comm2 Mode Gd")]
        [Description("COMM 2 Mode Knob - GD")]
        SimAud1Com2Gd,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Threat Vol Inc")]
        [Description("THREAT Knob - Volume Incr.")]
        SimStepThreatVolumeUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("AUDIO 1 PANEL")]
        [ShortDescription("Threat Vol Dec")]
        [Description("THREAT Knob - Volume Decr.")]
        SimStepThreatVolumeDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("MPO PANEL")]
        [ShortDescription("MPO Tog")]
        [Description("MANUAL PITCH Switch - Toggle")]
        SimMPOToggle,

        [Category("LEFT CONSOLE")]
        [SubCategory("MPO PANEL")]
        [ShortDescription("MPO Hold")]
        [Description("MANUAL PITCH Switch - Hold")]
        SimMPO,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Channel Cycle Up")]
        [Description("PRESET CHANNEL Knob - Cycle Up")]
        SimCycleRadioChannel,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Channel Cycle Dn")]
        [Description("PRESET CHANNEL Knob - Cycle Down")]
        SimDecRadioChannel,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF X__.___ Up")]
        [Description("A-3-2-T Rotary X__.___ - Step Up")]
        SimBupUhfFreq1Inc,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF X__.___ Dn")]
        [Description("A-3-2-T Rotary X__.___ - Step Down")]
        SimBupUhfFreq1Dec,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF 2__.___")]
        [Description("A-3-2-T Rotary 2__.___")]
        SimBupUhfFreq1_2,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF 3__.___")]
        [Description("A-3-2-T Rotary 3__.___")]
        SimBupUhfFreq1_3,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _X_.___ Up")]
        [Description("Manual Frequency _X_.___ - Cycle Up")]
        SimBupUhfFreq2Inc,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _X_.___ Dn")]
        [Description("Manual Frequency _X_.___ - Cycle Down")]
        SimBupUhfFreq2Dec,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _0_.___")]
        [Description("Manual Frequency _0_.___")]
        SimBupUhfFreq2_0,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _1_.___")]
        [Description("Manual Frequency _1_.___")]
        SimBupUhfFreq2_1,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _2_.___")]
        [Description("Manual Frequency _2_.___")]
        SimBupUhfFreq2_2,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _3_.___")]
        [Description("Manual Frequency _3_.___")]
        SimBupUhfFreq2_3,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _4_.___")]
        [Description("Manual Frequency _4_.___")]
        SimBupUhfFreq2_4,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _5_.___")]
        [Description("Manual Frequency _5_.___")]
        SimBupUhfFreq2_5,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _6_.___")]
        [Description("Manual Frequency _6_.___")]
        SimBupUhfFreq2_6,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _7_.___")]
        [Description("Manual Frequency _7_.___")]
        SimBupUhfFreq2_7,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _8_.___")]
        [Description("Manual Frequency _8_.___")]
        SimBupUhfFreq2_8,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF _9_.___")]
        [Description("Manual Frequency _9_.___")]
        SimBupUhfFreq2_9,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __X.___ Up")]
        [Description("Manual Frequency __X.___ - Cycle Up")]
        SimBupUhfFreq3Inc,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __X.___ Dn")]
        [Description("Manual Frequency __X.___ - Cycle Down")]
        SimBupUhfFreq3Dec,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __0.___")]
        [Description("Manual Frequency __0.___")]
        SimBupUhfFreq3_0,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __1.___")]
        [Description("Manual Frequency __1.___")]
        SimBupUhfFreq3_1,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __2.___")]
        [Description("Manual Frequency __2.___")]
        SimBupUhfFreq3_2,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __3.___")]
        [Description("Manual Frequency __3.___")]
        SimBupUhfFreq3_3,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __4.___")]
        [Description("Manual Frequency __4.___")]
        SimBupUhfFreq3_4,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __5.___")]
        [Description("Manual Frequency __5.___")]
        SimBupUhfFreq3_5,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __6.___")]
        [Description("Manual Frequency __6.___")]
        SimBupUhfFreq3_6,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __7.___")]
        [Description("Manual Frequency __7.___")]
        SimBupUhfFreq3_7,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __8.___")]
        [Description("Manual Frequency __8.___")]
        SimBupUhfFreq3_8,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF __9.___")]
        [Description("Manual Frequency __9.___")]
        SimBupUhfFreq3_9,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.X__ Up")]
        [Description("Manual Frequency ___.X__ - Cycle Up")]
        SimBupUhfFreq4Inc,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.X__ Dn")]
        [Description("Manual Frequency ___.X__ - Cycle Down")]
        SimBupUhfFreq4Dec,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.0__")]
        [Description("Manual Frequency ___.0__")]
        SimBupUhfFreq4_0,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.1__")]
        [Description("Manual Frequency ___.1__")]
        SimBupUhfFreq4_1,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.2__")]
        [Description("Manual Frequency ___.2__")]
        SimBupUhfFreq4_2,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.3__")]
        [Description("Manual Frequency ___.3__")]
        SimBupUhfFreq4_3,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.4__")]
        [Description("Manual Frequency ___.4__")]
        SimBupUhfFreq4_4,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.5__")]
        [Description("Manual Frequency ___.5__")]
        SimBupUhfFreq4_5,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.6__")]
        [Description("Manual Frequency ___.6__")]
        SimBupUhfFreq4_6,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.7__")]
        [Description("Manual Frequency ___.7__")]
        SimBupUhfFreq4_7,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.8__")]
        [Description("Manual Frequency ___.8__")]
        SimBupUhfFreq4_8,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___.9__")]
        [Description("Manual Frequency ___.9__")]
        SimBupUhfFreq4_9,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___._XX Up")]
        [Description("Manual Frequency ___._XX - Cycle Up")]
        SimBupUhfFreq5Inc,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___._XX Dn")]
        [Description("Manual Frequency ___._XX - Cycle Down")]
        SimBupUhfFreq5Dec,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___._00")]
        [Description("Manual Frequency ___._00")]
        SimBupUhfFreq5_00,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___._25")]
        [Description("Manual Frequency ___._25")]
        SimBupUhfFreq5_25,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___._50")]
        [Description("Manual Frequency ___._50")]
        SimBupUhfFreq5_50,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("UHF ___._75")]
        [Description("Manual Frequency ___._75")]
        SimBupUhfFreq5_75,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Function Step Up")]
        [Description("FUNCTION Knob - Step Up")]
        SimBupUhfFuncInc,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Function Step Dn")]
        [Description("FUNCTION Knob - Step Down")]
        SimBupUhfFuncDec,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Function Off")]
        [Description("FUNCTION Knob - OFF")]
        SimBupUhfOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Function Main")]
        [Description("FUNCTION Knob - MAIN")]
        SimBupUhfMain,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Function Both")]
        [Description("FUNCTION Knob - BOTH")]
        SimBupUhfBoth,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Vol Knob Incr")]
        [Description("VOL Knob - AI vs IVC Volume Incr")]
        OTWBalanceIVCvsAIUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Vol Knob Decr")]
        [Description("VOL Knob - AI vs IVC Volume Decr")]
        OTWBalanceIVCvsAIDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Mode Step Up")]
        [Description("MODE Knob - Step Up")]
        SimBupUhfModeInc,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Mode Step Dn")]
        [Description("MODE Knob - Step Down")]
        SimBupUhfModeDec,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Mode Manual")]
        [Description("MODE Knob - MNL")]
        SimBupUhfManual,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Mode Preset")]
        [Description("MODE Knob - PRESET")]
        SimBupUhfPreset,

        [Category("LEFT CONSOLE")]
        [SubCategory("UHF PANEL")]
        [ShortDescription("Mode Guard")]
        [Description("MODE Knob - GRD")]
        SimBupUhfGuard,

        [Category("LEFT CONSOLE")]
        [SubCategory("LEFT SIDE WALL")]
        [ShortDescription("Slap Switch")]
        [Description("SLAP Switch (ECM-PGRM # 5)")]
        SimSlapSwitch,

        [Category("LEFT CONSOLE")]
        [SubCategory("LEFT SIDE WALL")]
        [ShortDescription("Canopy Toggle")]
        [Description("CANOPY - Toggle Open/Close")]
        AFCanopyToggle,

        [Category("LEFT CONSOLE")]
        [SubCategory("LEFT SIDE WALL")]
        [ShortDescription("Canopy Open")]
        [Description("CANOPY - Open")]
        AFCanopyOpen,

        [Category("LEFT CONSOLE")]
        [SubCategory("LEFT SIDE WALL")]
        [ShortDescription("Canopy Close")]
        [Description("CANOPY - Close")]
        AFCanopyClose,

        [Category("LEFT CONSOLE")]
        [SubCategory("SEAT")]
        [ShortDescription("Safety Lever Tog")]
        [Description("Safety Lever - Toggle")]
        SimSeatArm,

        [Category("LEFT CONSOLE")]
        [SubCategory("SEAT")]
        [ShortDescription("Safety Lever Arm")]
        [Description("Safety Lever - Armed")]
        SimSeatOn,

        [Category("LEFT CONSOLE")]
        [SubCategory("SEAT")]
        [ShortDescription("Safety Lever Lock")]
        [Description("Safety Lever - Locked")]
        SimSeatOff,

        [Category("LEFT CONSOLE")]
        [SubCategory("SEAT")]
        [ShortDescription("EJECT")]
        [Description("EJECT Handle - Hold For Eject")]
        SimEject,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Comms Sw Up")]
        [Description("COMMS Switch Up - UHF")]
        SimTransmitCom1,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Comms Sw Dn")]
        [Description("COMMS Switch Down - VHF")]
        SimTransmitCom2,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Comms IFF Out")]
        [Description("COMMS Switch Left - IFF OUT")]
        SimCommsSwitchLeft,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Comms Sw IFF In")]
        [Description("COMMS Switch Right - IFF IN")]
        SimCommsSwitchRight,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Man Range Up")]
        [Description("MAN RANGE Knob - Up")]
        SimRangeKnobUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Man Range Dn")]
        [Description("MAN RANGE Knob - Down")]
        SimRangeKnobDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("ManRng Uncage")]
        [Description("MAN RANGE Knob - UNCAGE")]
        SimToggleMissileCage,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("DF Override")]
        [Description("DOGFIGHT Switch - DF Override")]
        SimSelectSRMOverride,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("MRM Override")]
        [Description("DOGFIGHT Switch - MRM Override")]
        SimSelectMRMOverride,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("MRM/DF Cancel")]
        [Description("DOGFIGHT Switch - MRM/DF Cancel")]
        SimDeselectOverride,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("SpdBrk Tog")]
        [Description("SPD BREAK Switch - Toggle")]
        AFBrakesToggle,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("SpdBrk Open")]
        [Description("SPD BREAK Switch - Open")]
        AFBrakesOut,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("SpdBrk Close")]
        [Description("SPD BREAK Switch - Close")]
        AFBrakesIn,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Rdr Cursor Up")]
        [Description("RDR CURSOR - Up")]
        SimCursorUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Rdr Cursor Dn")]
        [Description("RDR CURSOR - Down")]
        SimCursorDown,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Rdr Cursor Left")]
        [Description("RDR CURSOR - Left")]
        SimCursorLeft,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Rdr Cursor Right")]
        [Description("RDR CURSOR - Right")]
        SimCursorRight,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Rdr Cursor Enabl")]
        [Description("RDR CURSOR - Cursor Enable")]
        SimCursorEnable,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Rdr Cursor Zero")]
        [Description("RDR CURSOR - Cursor Zero")]
        SimRadarCursorZero,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("Idle Detent")]
        [Description("CUTOFF RELEASE - Idle Detent")]
        SimThrottleIdleDetent,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("AntElev Up")]
        [Description("ANT ELEV Knob - Tilt Up")]
        SimRadarElevationUp,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("AntElev Center")]
        [Description("ANT ELEV Knob - Center")]
        SimRadarElevationCenter,

        [Category("LEFT CONSOLE")]
        [SubCategory("THROTTLE QUADRANT SYSTEM")]
        [ShortDescription("AntElev Dn")]
        [Description("ANT ELEV Knob - Tilt Down")]
        SimRadarElevationDown,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("ALT GEAR CONTROL")]
        [ShortDescription("Alt Gear Extend")]
        [Description("Extend Gear Handle - Push")]
        AFAlternateGear,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("ALT GEAR CONTROL")]
        [ShortDescription("Alt Gear Reset")]
        [Description("Reset Button - Push")]
        AFAlternateGearReset,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("TWA PANEL")]
        [ShortDescription("TWA Low Tog.")]
        [Description("LOW Button - Toggle")]
        SimRWRSetGroundPriority,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("TWA PANEL")]
        [ShortDescription("TWA Search Tog.")]
        [Description("SEARCH Button - Toggle")]
        SimRWRSetSearch,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("TWA PANEL")]
        [ShortDescription("TWA Power Tog.")]
        [Description("POWER Button - Toggle")]
        SimRwrPower,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("TWA PANEL")]
        [ShortDescription("TWA Power On")]
        [Description("POWER Button - On")]
        SimRwrPowerOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("TWA PANEL")]
        [ShortDescription("TWA Power Off")]
        [Description("POWER Button - Off")]
        SimRwrPowerOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("HMCS PANEL")]
        [ShortDescription("HMCS Bright Inc")]
        [Description("HMSC Knob - Brightness Incr.")]
        SimHmsSymWheelUp,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("HMCS PANEL")]
        [ShortDescription("HMCS Bright Dec")]
        [Description("HMSC Knob - Brightness Decr.")]
        SimHmsSymWheelDn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("HMCS PANEL")]
        [ShortDescription("HMCS Knob On")]
        [Description("HMSC Knob - ON")]
        SimHmsOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("HMCS PANEL")]
        [ShortDescription("HMCS Knob Off")]
        [Description("HMSC Knob - OFF")]
        SimHmsOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("RWR Pwr Tog")]
        [Description("RWR Switch - Toggle Power")]
        SimEWSRWRPower,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("RWR Pwr On")]
        [Description("RWR Switch - Power ON")]
        SimEWSRWROn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("RWR Pwr Off")]
        [Description("RWR Switch - Power OFF")]
        SimEWSRWROff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("JMR Pwr Tog")]
        [Description("JMR Switch - Toggle Power")]
        SimEWSJammerPower,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("JMR Pwr On")]
        [Description("JMR Switch - Power ON")]
        SimEWSJammerOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("JMR Pwr Off")]
        [Description("JMR Switch - Power OFF")]
        SimEWSJammerOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("MWS Pwr Tog")]
        [Description("MWS Switch - Toggle Power")]
        SimEWSMwsPower,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("MWS Pwr On")]
        [Description("MWS Switch - Power ON")]
        SimEWSMwsOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("MWS Pwr Off")]
        [Description("MWS Switch - Power OFF")]
        SimEWSMwsOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("O1 Pwr Tog")]
        [Description("O1 Switch - Toggle Power")]
        SimEWSO1Power,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("O1 Pwr On")]
        [Description("O1 Switch - Power ON")]
        SimEWSO1On,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("O1 Pwr Off")]
        [Description("O1 Switch - Power OFF")]
        SimEWSO1Off,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("O2 Pwr Tog")]
        [Description("O2 Switch - Toggle Power")]
        SimEWSO2Power,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("O2 Pwr On")]
        [Description("O2 Switch - Power ON")]
        SimEWSO2On,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("O2 Pwr Off")]
        [Description("O2 Switch - Power OFF")]
        SimEWSO2Off,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CH Pwr Tog")]
        [Description("CH Switch - Toggle Power")]
        SimEWSChaffPower,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CH Pwr On")]
        [Description("CH Switch - Power ON")]
        SimEWSChaffOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CH Pwr Off")]
        [Description("CH Switch - Power OFF")]
        SimEWSChaffOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("FL Pwr Tog")]
        [Description("FL Switch - Toggle Power")]
        SimEWSFlarePower,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("FL Pwr On")]
        [Description("FL Switch - Power ON")]
        SimEWSFlareOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("FL Pwr Off")]
        [Description("FL Switch - Power OFF")]
        SimEWSFlareOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("Jett Sw Toggle")]
        [Description("JETT Switch - Toggle")]
        SimEwsJett,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("Jett Sw On")]
        [Description("JETT Switch - ON")]
        SimEwsJettOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("Jett Sw Off")]
        [Description("JETT Switch - OFF")]
        SimEwsJettOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("PRGM Knob Up")]
        [Description("PRGM Knob - Step Up")]
        SimEWSProgInc,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("PRGM Knob Dn")]
        [Description("PRGM Knob - Step Down")]
        SimEWSProgDec,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("PRGM Knob 1")]
        [Description("PRGM Knob - 1")]
        SimEWSProgOne,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("PRGM Knob 2")]
        [Description("PRGM Knob - 2")]
        SimEWSProgTwo,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("PRGM Knob 3")]
        [Description("PRGM Knob - 3")]
        SimEWSProgThree,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("PRGM Knob 4")]
        [Description("PRGM Knob - 4")]
        SimEWSProgFour,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CMDS Mode Up")]
        [Description("MODE Knob - Step Up")]
        SimEWSPGMInc,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CMDS Mode Dn")]
        [Description("MODE Knob - Step Down")]
        SimEWSPGMDec,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CMDS Mode Off")]
        [Description("MODE Knob - OFF")]
        SimEWSModeOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CMDS Mode Stby")]
        [Description("MODE Knob - STBY")]
        SimEWSModeStby,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CMDS Mode Man")]
        [Description("MODE Knob - MAN")]
        SimEWSModeMan,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CMDS Mode Semi")]
        [Description("MODE Knob - SEMI")]
        SimEWSModeSemi,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CMDS Mode Auto")]
        [Description("MODE Knob - AUTO")]
        SimEWSModeAuto,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("CMDS PANEL")]
        [ShortDescription("CMDS Mode Byp")]
        [Description("MODE Knob - BYP")]
        SimEWSModeByp,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Emergency Jett")]
        [Description("EMER STORES JETTISON Button - Hold")]
        SimEmergencyJettison,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Hook Tog")]
        [Description("HOOK Switch - Toggle")]
        SimHookToggle,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Hook Up")]
        [Description("HOOK Switch - UP")]
        SimHookUp,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Hook Dn")]
        [Description("HOOK Switch - DN")]
        SimHookDown,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("GND Jett Tog")]
        [Description("GND JETT Switch - Toggle")]
        SimGndJettEnable,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("GND Jett Enable")]
        [Description("GND JETT Switch - ENABLE")]
        SimGndJettOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("GND Jett Off")]
        [Description("GND JETT Switch - OFF")]
        SimGndJettOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Parking Brk Tog")]
        [Description("PARKING BREAK Switch - Toggle")]
        SimParkingBrakeToggle,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Parking Brk On")]
        [Description("PARKING BREAK Switch - ON")]
        SimParkingBrakeOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Parking Brk Off")]
        [Description("PARKING BREAK Switch - OFF")]
        SimParkingBrakeOff,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Gear Toggle")]
        [Description("LG Handle - Toggle")]
        AFGearToggle,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Gear Up")]
        [Description("LG Handle - UP")]
        AFGearUp,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Gear Down")]
        [Description("LG Handle - DN")]
        AFGearDown,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("CAT Sw Tog")]
        [Description("STORES CONFIG Switch - Toggle")]
        SimCATSwitch,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("CAT Sw I")]
        [Description("STORES CONFIG Switch - CAT I")]
        SimCATI,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("CAT Sw III")]
        [Description("STORES CONFIG Switch - CAT III")]
        SimCATIII,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Horn Silencer")]
        [Description("HORN SILENCER Button - Push")]
        SimSilenceHorn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Lndg Lights Tog")]
        [Description("LIGHTS Switch - Toggle")]
        SimLandingLightToggle,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Light Sw Land")]
        [Description("LIGHTS Switch - LANDING")]
        SimLandingLightOn,

        [Category("LEFT AUX CONSOLE")]
        [SubCategory("GEAR PANEL")]
        [ShortDescription("Light Sw Off")]
        [Description("LIGHTS Switch - OFF")]
        SimLandingLightOff,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("RF Sw Cycle")]
        [Description("RF Switch - Cycle")]
        SimRFSwitch,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("RF Sw Up")]
        [Description("RF Switch - Step Up")]
        SimRFSwitchUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("RF Sw Dn")]
        [Description("RF Switch - Step Down")]
        SimRFSwitchDown,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("RF Sw Norm")]
        [Description("RF Switch - NORM")]
        SimRFNorm,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("RF Sw Quiet")]
        [Description("RF Switch - QUIET")]
        SimRFQuiet,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("RF Sw Silent")]
        [Description("RF Switch - SILENT")]
        SimRFSilent,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Laser Sw Tog")]
        [Description("LASER Switch - Toggle")]
        SimLaserArmToggle,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Laser Sw Arm")]
        [Description("LASER Switch - ARM")]
        SimLaserArmOn,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Laser Sw Off")]
        [Description("LASER Switch - OFF")]
        SimLaserArmOff,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Master Arm Cyc")]
        [Description("MASTER ARM Switch - Cycle")]
        SimStepMasterArm,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Master Arm Up")]
        [Description("MASTER ARM Switch - Step Up")]
        SimMasterArmUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Master Arm Dn")]
        [Description("MASTER ARM Switch - Step Down")]
        SimMasterArmDown,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Master Arm On")]
        [Description("MASTER ARM Switch - ON")]
        SimArmMasterArm,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Master Arm Off")]
        [Description("MASTER ARM Switch - OFF")]
        SimSafeMasterArm,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("Master Arm Sim")]
        [Description("MASTER ARM Switch - SIM")]
        SimSimMasterArm,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("TFR Toggle")]
        [Description("ADV MODE - Toggle TFR On / Off")]
        SimToggleTFR,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Roll Cyc")]
        [Description("ROLL Switch - Cycle")]
        SimLeftAPSwitch,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Roll Step Up")]
        [Description("ROLL Switch - Step Up")]
        SimLeftAPInc,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Roll Step Dn")]
        [Description("ROLL Switch - Step Down")]
        SimLeftAPDec,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Roll Hdg Sel")]
        [Description("ROLL Switch - HDG SEL")]
        SimLeftAPUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Roll Att Hold")]
        [Description("ROLL Switch - ATT HOLD")]
        SimLeftAPMid,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Roll Strg Sel")]
        [Description("ROLL Switch -  STRG SEL")]
        SimLeftAPDown,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Pitch Cyc")]
        [Description("PITCH Switch - Cycle (also Combat AP)")]
        SimRightAPSwitch,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Pitch Step Up")]
        [Description("PITCH Switch - Step Up")]
        SimRightAPInc,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Pitch Step Dn")]
        [Description("PITCH Switch - Step Down")]
        SimRightAPDec,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Pitch Alt Hold")]
        [Description("PITCH Switch - ALT HOLD")]
        SimRightAPUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Pitch A/P Off")]
        [Description("PITCH Switch - A/P OFF")]
        SimRightAPMid,

        [Category("CENTER CONSOLE")]
        [SubCategory("MISC PANEL")]
        [ShortDescription("AP Pitch Att Hold")]
        [Description("PITCH Switch - ATT HOLD")]
        SimRightAPDown,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT EYEBROW")]
        [ShortDescription("Master Caution")]
        [Description("MASTER CAUTION Button - Push")]
        ExtinguishMasterCaution,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT EYEBROW")]
        [ShortDescription("F ACK")]
        [Description("F ACK Button - Push")]
        SimICPFAck,

        [Category("CENTER CONSOLE")]
        [SubCategory("TWP")]
        [ShortDescription("TWP Handoff")]
        [Description("HANDOFF - Push")]
        SimRWRHandoff,

        [Category("CENTER CONSOLE")]
        [SubCategory("TWP")]
        [ShortDescription("TWP Msl Launch")]
        [Description("MISSILE LAUNCH - Push")]
        SimRWRLaunch,

        [Category("CENTER CONSOLE")]
        [SubCategory("TWP")]
        [ShortDescription("TWP Priority")]
        [Description("PRIORITY MODE - Toggle")]
        SimRWRSetPriority,

        [Category("CENTER CONSOLE")]
        [SubCategory("TWP")]
        [ShortDescription("TWP Unkown")]
        [Description("UNKNOWN - Toggle")]
        SimRWRSetUnknowns,

        [Category("CENTER CONSOLE")]
        [SubCategory("TWP")]
        [ShortDescription("TWP Sys Test")]
        [Description("SYS TEST - Push")]
        SimRWRSysTest,

        [Category("CENTER CONSOLE")]
        [SubCategory("TWP")]
        [ShortDescription("TWP Tgt Sep")]
        [Description("TGT SEP - Push")]
        SimRWRSetTargetSep,

        [Category("CENTER CONSOLE")]
        [SubCategory("RWR")]
        [ShortDescription("RWR Brightn Inc")]
        [Description("Brightness Knob - Increase")]
        SimRWRBrightnessUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("RWR")]
        [ShortDescription("RWR Brightn Dec")]
        [Description("Brightness Knob - Decrease")]
        SimRWRBrightnessDown,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 1")]
        [Description("OSB-1 Button - Push")]
        SimCBEOSB_1L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 2")]
        [Description("OSB-2 Button - Push")]
        SimCBEOSB_2L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 3")]
        [Description("OSB-3 Button - Push")]
        SimCBEOSB_3L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 4")]
        [Description("OSB-4 Button - Push")]
        SimCBEOSB_4L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 5")]
        [Description("OSB-5 Button - Push")]
        SimCBEOSB_5L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 6")]
        [Description("OSB-6 Button - Push")]
        SimCBEOSB_6L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 7")]
        [Description("OSB-7 Button - Push")]
        SimCBEOSB_7L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 8")]
        [Description("OSB-8 Button - Push")]
        SimCBEOSB_8L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 9")]
        [Description("OSB-9 Button - Push")]
        SimCBEOSB_9L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 10")]
        [Description("OSB-10 Button - Push")]
        SimCBEOSB_10L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 11")]
        [Description("OSB-11 Button - Push")]
        SimCBEOSB_11L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 12")]
        [Description("OSB-12 Button - Push")]
        SimCBEOSB_12L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 13")]
        [Description("OSB-13 Button - Push")]
        SimCBEOSB_13L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 14")]
        [Description("OSB-14 Button - Push")]
        SimCBEOSB_14L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 15")]
        [Description("OSB-15 Button - Push")]
        SimCBEOSB_15L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 16")]
        [Description("OSB-16 Button - Push")]
        SimCBEOSB_16L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 17")]
        [Description("OSB-17 Button - Push")]
        SimCBEOSB_17L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 18")]
        [Description("OSB-18 Button - Push")]
        SimCBEOSB_18L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 19")]
        [Description("OSB-19 Button - Push")]
        SimCBEOSB_19L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD OSB 20")]
        [Description("OSB-20 Button - Push")]
        SimCBEOSB_20L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD Brt Inc")]
        [Description("BRT Button - Increase Brightness")]
        SimCBEOSB_BRTUP_L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("LMFD Brt Dec")]
        [Description("BRT Button - Decrease Brightness")]
        SimCBEOSB_BRTDOWN_L,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("Radar Gain Inc")]
        [Description("GAIN Button - Increase Sensor Gain")]
        SimRadarGainUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("LEFT MFD")]
        [ShortDescription("Radar Gain Dec")]
        [Description("GAIN Button - Decrease Sensor Gain")]
        SimRadarGainDown,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP COM1")]
        [Description("COM1 Button - Push")]
        SimICPCom1,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP COM2")]
        [Description("COM2 Button - Push")]
        SimICPCom2,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP IFF")]
        [Description("IFF Button - Push")]
        SimICPIFF,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP LIST")]
        [Description("LIST Button - Push")]
        SimICPLIST,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP A-A")]
        [Description("A-A Button - Push")]
        SimICPAA,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP A-G")]
        [Description("A-G Button - Push")]
        SimICPAG,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP NAV Mode")]
        [Description("NAV Mode (no such button In Pit)")]
        SimICPNav,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 1-ILS")]
        [Description("1-ILS Button - Push")]
        SimICPTILS,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 2-ALOW")]
        [Description("2-ALOW Button - Push")]
        SimICPALOW,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 3")]
        [Description("3 Button - Push")]
        SimICPTHREE,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 4-STPT")]
        [Description("4-STPT Button - Push")]
        SimICPStpt,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 5-CRUS")]
        [Description("5-CRUS Button - Push")]
        SimICPCrus,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 6-TIME")]
        [Description("6-TIME Button - Push")]
        SimICPSIX,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 7-MARK")]
        [Description("7-MARK Button - Push")]
        SimICPMark,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 8-FIX")]
        [Description("8-FIX Button - Push")]
        SimICPEIGHT,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 9-A-CAL")]
        [Description("9-A-CAL Button - Push")]
        SimICPNINE,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP 0-M-SEL")]
        [Description("0-M-SEL Button - Push")]
        SimICPZERO,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP RCL")]
        [Description("RCL Button - Push")]
        SimICPCLEAR,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP ENTR")]
        [Description("ENTER Button - Push")]
        SimICPEnter,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Next")]
        [Description("▲ Button (NEXT) - Push")]
        SimICPNext,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Previous")]
        [Description("▼ Button (PREVIOUS) - Push")]
        SimICPPrevious,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP DCS Up")]
        [Description("DCS UP - Push")]
        SimICPDEDUP,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP DCS Dn")]
        [Description("DCS DOWN - Push")]
        SimICPDEDDOWN,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP DCS SEQ")]
        [Description("DCS SEQ (Right) - Push")]
        SimICPDEDSEQ,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP DCS RTN")]
        [Description("DCS RTN (Left) - Push")]
        SimICPResetDED,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Drift c/o Tog")]
        [Description("DRIFT C/O Switch - Tog. ON/NORM!")]
        SimDriftCO,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Drift c/o On")]
        [Description("DRIFT C/O Switch - ON")]
        SimDriftCOOn,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Drift c/o Norm")]
        [Description("DRIFT C/O Switch - NORM")]
        SimDriftCOOff,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Drift c/o Warn")]
        [Description("DRIFT C/O Switch - WARN RESET")]
        SimWarnReset,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP FLIR WX")]
        [Description("FLIR - WX Mode")]
        SimSetWX,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP FLIR Lvl Up")]
        [Description("FLIR Rocker ▲ - Level Up")]
        SimFlirLevelUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP FLIR Lvl Dn")]
        [Description("FLIR Rocker ▼ - Level Down")]
        SimFlirLevelDown,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP HudBrght Inc")]
        [Description("SYM Wheel - Increase HUD Brightness")]
        SimSymWheelUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP HudBrghtDec")]
        [Description("SYM Wheel - Decrease HUD Brightness")]
        SimSymWheelDn,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Hud Pwr Tog")]
        [Description("SYM Wheel - HUD Power - Toggle")]
        SimHUDPower,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Hud Pwr On")]
        [Description("SYM Wheel - HUD Power - On")]
        SimHUDOn,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Hud Pwr Off")]
        [Description("SYM Wheel - HUD Power - OFF")]
        SimHUDOff,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP FLIR BRT Up")]
        [Description("BRT Wheel - Increase FLIR Intensity")]
        SimBrtWheelUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP FLIR BRT Dn")]
        [Description("BRT Wheel - Decrease FLIR Intensity")]
        SimBrtWheelDn,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Depr Ret Up")]
        [Description("DEPR RET Wheel - Step Up")]
        SimRetUp,

        [Category("CENTER CONSOLE")]
        [SubCategory("ICP")]
        [ShortDescription("ICP Depr Ret Dn")]
        [Description("DEPR RET Wheel - Step Down")]
        SimRetDn,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("HSI Hdg Inc 5°")]
        [Description("HSI HDG Knob - Increase (5°)")]
        SimHsiHeadingInc,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("HSI Hdg Dec 5°")]
        [Description("HSI HDG Knob - Decrease (5°)")]
        SimHsiHeadingDec,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("HSI Hdg Inc 1°")]
        [Description("HSI HDG Knob - Increase (1°)")]
        SimHsiHdgIncBy1,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("HSI Hdg Dec 1°")]
        [Description("HSI HDG Knob - Decrease (1°)")]
        SimHsiHdgDecBy1,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("HSI Crs Inc 5°")]
        [Description("HSI CRS Knob - Increase (5°)")]
        SimHsiCourseInc,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("HSI Crs Dec 5°")]
        [Description("HSI CRS Knob - Decrease (5°)")]
        SimHsiCourseDec,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("HSI Crs Inc 1°")]
        [Description("HSI CRS Knob - Increase (1°)")]
        SimHsiCrsIncBy1,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("HSI Crs Dec 1°")]
        [Description("HSI CRS Knob - Decrease (1°)")]
        SimHsiCrsDecBy1,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("Alt Press + 5")]
        [Description("Altimeter Pressure Knob - Incr. (5°)")]
        SimAltPressInc,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("Alt Press - 5")]
        [Description("Altimeter Pressure Knob - Decr. (5°)")]
        SimAltPressDec,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("Alt Press + 1")]
        [Description("Altimeter Pressure Knob - Incr. (1°)")]
        SimAltPressIncBy1,

        [Category("CENTER CONSOLE")]
        [SubCategory("MAIN INSTRUMENT")]
        [ShortDescription("Alt Press - 1")]
        [Description("Altimeter Pressure Knob - Decr. (1°)")]
        SimAltPressDecBy1,

        [Category("CENTER CONSOLE")]
        [SubCategory("INSTR MODE PANEL")]
        [ShortDescription("Instr Mode Cyc")]
        [Description("MODE Knob - Cycle")]
        SimStepHSIMode,

        [Category("CENTER CONSOLE")]
        [SubCategory("INSTR MODE PANEL")]
        [ShortDescription("Instr Mode Up")]
        [Description("MODE Knob - Step Up")]
        SimHSIModeInc,

        [Category("CENTER CONSOLE")]
        [SubCategory("INSTR MODE PANEL")]
        [ShortDescription("Instr Mode Dn")]
        [Description("MODE Knob - Step Down")]
        SimHSIModeDec,

        [Category("CENTER CONSOLE")]
        [SubCategory("INSTR MODE PANEL")]
        [ShortDescription("Instr Mode Ils/Tcn")]
        [Description("MODE Knob - ILS/TCN")]
        SimHSIIlsTcn,

        [Category("CENTER CONSOLE")]
        [SubCategory("INSTR MODE PANEL")]
        [ShortDescription("Instr Mode Tcn")]
        [Description("MODE Knob - TCN")]
        SimHSITcn,

        [Category("CENTER CONSOLE")]
        [SubCategory("INSTR MODE PANEL")]
        [ShortDescription("Instr Mode Nav")]
        [Description("MODE Knob - NAV")]
        SimHSINav,

        [Category("CENTER CONSOLE")]
        [SubCategory("INSTR MODE PANEL")]
        [ShortDescription("Instr Mode Ils/Nav")]
        [Description("MODE Knob - ILS/NAV")]
        SimHSIIlsNav,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Qty Up")]
        [Description("FUEL QTY SEL Knob - Step Up")]
        SimIncFuelSwitch,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Qty Dn")]
        [Description("FUEL QTY SEL Knob - Step Down")]
        SimDecFuelSwitch,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Test")]
        [Description("FUEL QTY SEL Knob - TEST")]
        SimFuelSwitchTest,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Norm")]
        [Description("FUEL QTY SEL Knob - NORM")]
        SimFuelSwitchNorm,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Rsvr")]
        [Description("FUEL QTY SEL Knob - RSVR")]
        SimFuelSwitchResv,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Int Wg")]
        [Description("FUEL QTY SEL Knob - INT WING")]
        SimFuelSwitchWingInt,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Ext Wg")]
        [Description("FUEL QTY SEL Knob - EXT WING")]
        SimFuelSwitchWingExt,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Ext Ctr")]
        [Description("FUEL QTY SEL Knob - EXT CTR")]
        SimFuelSwitchCenterExt,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Tog")]
        [Description("EXT FUEL TRANS Switch - Toggle")]
        SimExtFuelTrans,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Norm")]
        [Description("EXT FUEL TRANS Switch - NORM")]
        SimFuelTransNorm,

        [Category("CENTER CONSOLE")]
        [SubCategory("FUEL QTY PANEL")]
        [ShortDescription("Fuel Trans Wing")]
        [Description("EXT FUEL TRANS Switch - WING FIRST")]
        SimFuelTransWing,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 1")]
        [Description("OSB-1 Button - Push")]
        SimCBEOSB_1R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 2")]
        [Description("OSB-2 Button - Push")]
        SimCBEOSB_2R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 3")]
        [Description("OSB-3 Button - Push")]
        SimCBEOSB_3R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 4")]
        [Description("OSB-4 Button - Push")]
        SimCBEOSB_4R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 5")]
        [Description("OSB-5 Button - Push")]
        SimCBEOSB_5R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 6")]
        [Description("OSB-6 Button - Push")]
        SimCBEOSB_6R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 7")]
        [Description("OSB-7 Button - Push")]
        SimCBEOSB_7R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 8")]
        [Description("OSB-8 Button - Push")]
        SimCBEOSB_8R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 9")]
        [Description("OSB-9 Button - Push")]
        SimCBEOSB_9R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 10")]
        [Description("OSB-10 Button - Push")]
        SimCBEOSB_10R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 11")]
        [Description("OSB-11 Button - Push")]
        SimCBEOSB_11R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 12")]
        [Description("OSB-12 Button - Push")]
        SimCBEOSB_12R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 13")]
        [Description("OSB-13 Button - Push")]
        SimCBEOSB_13R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 14")]
        [Description("OSB-14 Button - Push")]
        SimCBEOSB_14R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 15")]
        [Description("OSB-15 Button - Push")]
        SimCBEOSB_15R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 16")]
        [Description("OSB-16 Button - Push")]
        SimCBEOSB_16R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 17")]
        [Description("OSB-17 Button - Push")]
        SimCBEOSB_17R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 18")]
        [Description("OSB-18 Button - Push")]
        SimCBEOSB_18R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 19")]
        [Description("OSB-19 Button - Push")]
        SimCBEOSB_19R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD OSB 20")]
        [Description("OSB-20 Button - Push")]
        SimCBEOSB_20R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD Brt Inc")]
        [Description("BRT Button - Increase Brightness")]
        SimCBEOSB_BRTUP_R,

        [Category("CENTER CONSOLE")]
        [SubCategory("RIGHT MFD")]
        [ShortDescription("RMFD Brt Dec")]
        [Description("BRT Button - Decrease Brightness")]
        SimCBEOSB_BRTDOWN_R,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("Left Hdpt Tog")]
        [Description("LEFT HDPT Switch - Toggle")]
        SimLeftHptPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("Left Hdpt On")]
        [Description("LEFT HDPT Switch - ON")]
        SimLeftHptOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("Left Hdpt Off")]
        [Description("LEFT HDPT Switch - OFF")]
        SimLeftHptOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("Right Hdpt Tog")]
        [Description("RIGHT HDPT Switch - Toggle")]
        SimRightHptPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("Right Hdpt On")]
        [Description("RIGHT HDPT Switch - ON")]
        SimRightHptOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("Right Hdpt Off")]
        [Description("RIGHT HDPT Switch - OFF")]
        SimRightHptOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("FCR Sw Tog")]
        [Description("FCR Switch - Toggle")]
        SimFCRPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("FCR Sw On")]
        [Description("FCR Switch - ON")]
        SimFCROn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("FCR Sw Off")]
        [Description("FCR Switch - OFF")]
        SimFCROff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("RDR Alt Up")]
        [Description("RDR ALT Switch - Step Up")]
        SimRALTUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("RDR Alt Dn")]
        [Description("RDR ALT Switch - Step Down")]
        SimRALTDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("RDR Alt On")]
        [Description("RDR ALT Switch - ON")]
        SimRALTON,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("RDR Alt Stdby")]
        [Description("RDR ALT Switch - STDBY")]
        SimRALTSTDBY,

        [Category("RIGHT CONSOLE")]
        [SubCategory("SNSR PWR PANEL")]
        [ShortDescription("RDR Alt Off")]
        [Description("RDR ALT Switch - OFF")]
        SimRALTOFF,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Scales Cyc")]
        [Description("Scales Switch - Cycle")]
        SimHUDScales,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Scales Up")]
        [Description("Scales Switch - Step Up")]
        SimHUDScalesUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Scales Dn")]
        [Description("Scales Switch - Step Down")]
        SimHUDScalesDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Scls Vv/Vah")]
        [Description("Scales Switch - VV/VAH")]
        SimScalesVVVAH,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Scales Vah")]
        [Description("Scales Switch - VAH")]
        SimScalesVAH,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Scales Off")]
        [Description("Scales Switch - OFF")]
        SimScalesOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD FPM Cyc")]
        [Description("FPM Switch - Cycle")]
        SimHUDFPM,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD FPM Up")]
        [Description("FPM Switch - Step Up")]
        SimPitchLadderUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD FPM Dn")]
        [Description("FPM Switch - Step Down")]
        SimPitchLadderDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD FPM Att/Hud")]
        [Description("FPM Switch - ATT/FPM")]
        SimPitchLadderATTFPM,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD FPM Fpm")]
        [Description("FPM Switch - FPM")]
        SimPitchLadderFPM,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD FPM Off")]
        [Description("FPM Switch - OFF")]
        SimPitchLadderOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DED Cyc")]
        [Description("DED Data Switch - Cycle")]
        SimHUDDED,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DED Up")]
        [Description("DED Data Switch - Step Up")]
        SimHUDDEDUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DED Dn")]
        [Description("DED Data Switch - Step Dn")]
        SimHUDDEDDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DED Data")]
        [Description("DED Data Switch - DED")]
        SimHUDDEDDED,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DED Pfl")]
        [Description("DED Data Switch - PFL")]
        SimHUDDEDPFL,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DED Off")]
        [Description("DED Data Switch - OFF")]
        SimHUDDEDOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DeprRet Cyc")]
        [Description("DEPR RET Switch - Cycle")]
        SimReticleSwitch,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DeprRet Up")]
        [Description("DEPR RET Switch - Step Up")]
        SimReticleSwitchUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DeprRet Dn")]
        [Description("DEPR RET Switch - Step Down")]
        SimReticleSwitchDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD DeprRet Sby")]
        [Description("DEPR RET Switch - STBY")]
        SimReticleStby,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Depr Ret Pri")]
        [Description("DEPR RET Switch - PRI")]
        SimReticlePri,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Depr Ret Off")]
        [Description("DEPR RET Switch - OFF")]
        SimReticleOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Velocity Cyc")]
        [Description("Velocity Switch - Cycle")]
        SimHUDVelocity,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Velocity Up")]
        [Description("Velocity Switch - Step Up")]
        SimHUDVelocityUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Velocity Dn")]
        [Description("Velocity Switch - Step Dn")]
        SimHUDVelocityDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Velocity Cas")]
        [Description("Velocity Switch - CAS")]
        SimHUDVelocityCAS,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Velocity Tas")]
        [Description("Velocity Switch - TAS")]
        SimHUDVelocityTAS,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Velocity Gnd")]
        [Description("Velocity Switch - GND SPD")]
        SimHUDVelocityGND,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Alt Cyc")]
        [Description("Altitude Switch - Cycle")]
        SimHUDRadar,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Alt Up")]
        [Description("Altitude Switch - Step Up")]
        SimHUDAltUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Alt Dn")]
        [Description("Altitude Switch - Step Dn")]
        SimHUDAltDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Alt Radar")]
        [Description("Altitude Switch - RADAR")]
        SimHUDAltRadar,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Alt Baro")]
        [Description("Altitude Switch - BARO")]
        SimHUDAltBaro,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUDAlt Auto")]
        [Description("Altitude Switch - AUTO")]
        SimHUDAltAuto,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Brightn Cyc")]
        [Description("Brightness Switch - Cycle")]
        SimHUDBrightness,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Brightn Up")]
        [Description("Brightness Switch - Step Up")]
        SimHUDBrightnessUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Brightn Dn")]
        [Description("Brightness Switch - Step Dn")]
        SimHUDBrightnessDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Brightn Day")]
        [Description("Brightness Switch - DAY")]
        SimHUDBrtDay,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Brightn Auto")]
        [Description("Brightness Switch - AUTO BRT")]
        SimHUDBrtAuto,

        [Category("RIGHT CONSOLE")]
        [SubCategory("HUD  PANEL")]
        [ShortDescription("HUD Brightn Nig")]
        [Description("Brightness Switch - NIG")]
        SimHUDBrtNight,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Pri Inst Pnl Cyc")]
        [Description("INST PNL Knob (Primary) - Cycle")]
        SimInstrumentLight,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Pri Inst Pnl Up")]
        [Description("INST PNL Knob (Primary) - Step Up")]
        SimInstrumentLightCW,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Pri Inst Pnl Dn")]
        [Description("INST PNL Knob (Primary) - Step Down")]
        SimInstrumentLightCCW,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Pri DED Cyc")]
        [Description("DED Knob (Primary) - Cycle")]
        SimDedBrightness,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Pri DED Up")]
        [Description("DED Knob (Primary) - Step Up")]
        SimDedBrightnessCW,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Pri DED Dn")]
        [Description("DED Knob (Primary) - Step Down")]
        SimDedBrightnessCCW,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Fld Consoles Cyc")]
        [Description("CONSOLES Knob (Flood) - Cycle")]
        SimInteriorLight,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Fld Consoles Up")]
        [Description("CONSOLES Knob (Flood) - Step Up")]
        SimInteriorLightCW,

        [Category("RIGHT CONSOLE")]
        [SubCategory("LIGHTING  PANEL")]
        [ShortDescription("Fld Consoles Dn")]
        [Description("CONSOLES Knob (Flood) - Step Down")]
        SimInteriorLightCCW,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AIR COND PANEL")]
        [ShortDescription("Air Source Up")]
        [Description("AIR SOURCE Knob - Step Up")]
        SimIncAirSource,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AIR COND PANEL")]
        [ShortDescription("Air Source Dn")]
        [Description("AIR SOURCE Knob - Step Down")]
        SimDecAirSource,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AIR COND PANEL")]
        [ShortDescription("Air Source Off")]
        [Description("AIR SOURCE Knob - OFF")]
        SimAirSourceOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AIR COND PANEL")]
        [ShortDescription("Air Source Norm")]
        [Description("AIR SOURCE Knob - NORM")]
        SimAirSourceNorm,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AIR COND PANEL")]
        [ShortDescription("Air Source Dump")]
        [Description("AIR SOURCE Knob - DUMP")]
        SimAirSourceDump,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AIR COND PANEL")]
        [ShortDescription("Air Source Ram")]
        [Description("AIR SOURCE Knob - RAM")]
        SimAirSourceRam,

        [Category("RIGHT CONSOLE")]
        [SubCategory("ZEROIZE PANEL")]
        [ShortDescription("VMS Sw Tog")]
        [Description("VMS Switch - Toggle")]
        SimInhibitVMS,

        [Category("RIGHT CONSOLE")]
        [SubCategory("ZEROIZE PANEL")]
        [ShortDescription("VMS Sw On")]
        [Description("VMS Switch - ON")]
        SimVMSOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("ZEROIZE PANEL")]
        [ShortDescription("VMS Sw Inhibit")]
        [Description("VMS Switch - INHIBIT")]
        SimVMSOFF,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("INS Knob Up")]
        [Description("INS Knob - Step Up")]
        SimINSInc,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("INS Knob Dn")]
        [Description("INS Knob - Step Down")]
        SimINSDec,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("INS Knob Off")]
        [Description("INS Knob - OFF")]
        SimINSOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("INS Knob Norm")]
        [Description("INS Knob - NORM")]
        SimINSNorm,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("INS Knob Nav")]
        [Description("INS Knob - NAV")]
        SimINSNav,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("INS Knob In Flt Ali")]
        [Description("INS Knob - IN FLT ALIGN")]
        SimINSInFlt,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("FCC Sw Tog")]
        [Description("FCC Switch - Toggle")]
        SimFCCPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("FCC Sw On")]
        [Description("FCC Switch - ON")]
        SimFCCOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("FCC Sw Off")]
        [Description("FCC Switch - OFF")]
        SimFCCOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("SMS Sw Tog")]
        [Description("SMS Switch - Toggle")]
        SimSMSPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("SMS Sw On")]
        [Description("SMS Switch - ON")]
        SimSMSOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("SMS Sw Off")]
        [Description("SMS Switch - OFF")]
        SimSMSOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("MFD Sw Tog")]
        [Description("MFD Switch - Toggle")]
        SimMFDPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("MFD Sw On")]
        [Description("MFD Switch - ON")]
        SimMFDOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("MFD Sw Off")]
        [Description("MFD Switch - OFF")]
        SimMFDOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("UFC Sw Tog")]
        [Description("UFC Switch - Toggle")]
        SimUFCPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("UFC Sw On")]
        [Description("UFC Switch - ON")]
        SimUFCOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("UFC Sw Off")]
        [Description("UFC Switch - OFF")]
        SimUFCOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("GPS Sw Tog")]
        [Description("GPS Switch - Toggle")]
        SimGPSPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("GPS Sw On")]
        [Description("GPS Switch - ON")]
        SimGPSOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("GPS Sw Off")]
        [Description("GPS Switch - OFF")]
        SimGPSOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("DL Sw Tog")]
        [Description("DL Switch - Toggle")]
        SimDLPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("DL Sw On")]
        [Description("DL Switch - ON")]
        SimDLOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("DL Sw Off")]
        [Description("DL Switch - OFF")]
        SimDLOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("MAP Sw Tog")]
        [Description("MAP Switch - Toggle")]
        SimMAPPower,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("MAP Sw On")]
        [Description("MAP Switch - ON")]
        SimMAPOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("AVIONIC POWER  PANEL")]
        [ShortDescription("MAP Sw Off")]
        [Description("MAP Switch - OFF")]
        SimMAPOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("OXYGEN PANEL")]
        [ShortDescription("Oxy Set2 Cyc")]
        [Description("Setting 2 - Toggle (Pilot breathing)")]
        SimOxySupplyToggle,

        [Category("RIGHT CONSOLE")]
        [SubCategory("OXYGEN PANEL")]
        [ShortDescription("Oxy Set2 ON")]
        [Description("Setting 2 - ON (Pilot breathing)")]
        SimOxySupplyOn,

        [Category("RIGHT CONSOLE")]
        [SubCategory("OXYGEN PANEL")]
        [ShortDescription("Oxy Set2 OFF")]
        [Description("Setting 2 - OFF (Pilot breathing)")]
        SimOxySupplyOff,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("TMS Up")]
        [Description("TMS Up")]
        SimTMSUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("TMS Down")]
        [Description("TMS Down")]
        SimTMSDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("TMS Left")]
        [Description("TMS Left")]
        SimTMSLeft,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("TMS Right")]
        [Description("TMS Right")]
        SimTMSRight,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("DMS Up")]
        [Description("DMS Up")]
        SimDMSUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("DMS Down")]
        [Description("DMS Down")]
        SimDMSDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("DMS Left")]
        [Description("DMS Left")]
        SimDMSLeft,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("DMS Right")]
        [Description("DMS Right")]
        SimDMSRight,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("CMS Up")]
        [Description("CMS Up")]
        SimCMSUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("CMS Down")]
        [Description("CMS Down")]
        SimCMSDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("CMS Left")]
        [Description("CMS Left")]
        SimCMSLeft,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("CMS Right")]
        [Description("CMS Right")]
        SimCMSRight,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("Stick Trim Ns Dn")]
        [Description("TRIM Up - Nose Down")]
        AFElevatorTrimUp,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("Stick Trim Ns Up")]
        [Description("TRIM Down - Nose Up")]
        AFElevatorTrimDown,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("Stick Trim Left")]
        [Description("TRIM Left - Roll Left")]
        AFAileronTrimLeft,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("Stick Trim Right")]
        [Description("TRIM Right - Roll Right")]
        AFAileronTrimRight,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("1st Trigger Det")]
        [Description("FIRST TRIGGER DETENT")]
        SimTriggerFirstDetent,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("2nd Trigger Det")]
        [Description("SECOND TRIGGER DETENT")]
        SimTriggerSecondDetent,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("WPN Release")]
        [Description("WEAPON RELEASE (Pickle)")]
        SimPickle,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("NWS A/R MSL")]
        [Description("NWS A/R DISC MSL STEP SWITCH")]
        SimMissileStep,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("Pinky Switch")]
        [Description("PINKY SWITCH")]
        SimPinkySwitch,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("Pinky (DX Shift)")]
        [Description("PINKY SWITCH (DX SHIFT)")]
        SimHotasPinkyShift,

        [Category("RIGHT CONSOLE")]
        [SubCategory("FLIGHT STICK ")]
        [ShortDescription("Paddle Switch")]
        [Description("PADDLE SWITCH")]
        SimAPOverride,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("NVG Toggle")]
        [Description("Nightvision - Toggle")]
        ToggleNVGMode,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Smoke Toggle")]
        [Description("Smoke - Toggle")]
        ToggleSmoke,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("HUD Color Cyc")]
        [Description("HUD Color - Cycle")]
        OTWStepHudColor,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Wheel Brakes")]
        [Description("Wheel Brakes - Hold")]
        SimWheelBrakes,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Spotlight")]
        [Description("Spotlight - Toggle")]
        SimSpotLight,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Clickabl Pit Mode")]
        [Description("Clickable Pit Mode - Toggle")]
        ToggleClickablePitMode,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Mouse Btns Tog")]
        [Description("Toggle Mouse Btns in 3D")]
        OTWMouseButtonsIn3dToggle,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Mouse Btns en")]
        [Description("Enable Mouse Btns in 3D")]
        OTWMouseButtonsIn3dEnable,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Mouse Btns dis")]
        [Description("Disable Mouse Btns in 3D")]
        OTWMouseButtonsIn3dDisable,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Drag Chute Depl")]
        [Description("Drag Chute Deploy")]
        AFDragChute,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Trim Reset")]
        [Description("Trim-Reset (Change here)")]
        AFResetTrim,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Fuel Dump")]
        [Description("Dump Fuel")]
        SimFuelDump,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Drop Chaff")]
        [Description("Drop Chaff (non EWS AC)")]
        SimDropChaff,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Drop Flare")]
        [Description("Drop Flare (non EWS AC)")]
        SimDropFlare,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Release Catapult")]
        [Description("NAVOPS - Release Catapult Trigger")]
        AFTriggerCatapult,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Flaps Full")]
        [Description("FLAPS - Set To Full")]
        AFFullFlap,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Flaps Null")]
        [Description("FLAPS - Set To Null")]
        AFNoFlap,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Flaps Inc")]
        [Description("FLAPS - Increase")]
        AFIncFlap,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Flaps Dec")]
        [Description("FLAPS - Decrease")]
        AFDecFlap,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Lefs Full")]
        [Description("LEFS - Set To Full")]
        AFFullLEF,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Lefs Null")]
        [Description("LEFS - Set To Null")]
        AFNoLEF,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Lefs Inc")]
        [Description("LEFS - Increase")]
        AFIncLEF,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Lefs Dec")]
        [Description("LEFS - Decrease")]
        AFDecLEF,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Wing Fold Tog")]
        [Description("Wing Fold - Toggle")]
        AFWingFoldToggle,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Wing Fold Up")]
        [Description("Wing Fold - Up")]
        AFWingFoldUp,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Wing Fold Dn")]
        [Description("Wing Fold - Down")]
        AFWingFoldDown,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Form Lights Up")]
        [Description("Formation Lights - Step Up")]
        SimStepFormationLightsUp,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Form Lights Dn")]
        [Description("Formation Lights - Step Down")]
        SimStepFormationLightsDown,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Load Ckpit Deflts")]
        [Description("Cockpit Defaults - Load")]
        LoadCockpitDefaults,

        [Category("MISCELLANEOUS")]
        [SubCategory("OTHER COCKPIT CALLBACKS")]
        [ShortDescription("Save Ckpit Deflts")]
        [Description("Cockpit Defaults - Save")]
        SaveCockpitDefaults,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("ALOW Inc.")]
        [Description("Increase ALOW")]
        IncreaseAlow,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("ALOW Dec.")]
        [Description("Decrease ALOW")]
        DecreaseAlow,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Next Waypoint")]
        [Description("Next Waypoint")]
        SimNextWaypoint,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Prev. Waypoint")]
        [Description("Previous Waypoint")]
        SimPrevWaypoint,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Next AG Weapon")]
        [Description("Next AG Weapon")]
        SimNextAGWeapon,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Toggle Jammer")]
        [Description("Toggle Jammer")]
        SimECMOn,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Step 3rd MFD")]
        [Description("Step 3rd MFD (like DMS l/r)")]
        OTWStepMFD3,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Step 4th MFD")]
        [Description("Step 4th MFD (like DMS l/r)")]
        OTWStepMFD4,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Radar Range Up")]
        [Description("Radar Range Up")]
        SimRadarRangeStepUp,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Radar Range Dn")]
        [Description("Radar Range Down")]
        SimRadarRangeStepDown,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Bomb Ripple Inc")]
        [Description("Bomb Ripple Increment")]
        BombRippleIncrement,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Bomb Ripple Dec")]
        [Description("Bomb Ripple Dencrement")]
        BombRippleDecrement,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Bomb Interval Inc")]
        [Description("Bomb Interval Increment")]
        BombIntervalIncrement,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Bomb Interval Dec")]
        [Description("Bomb Interval Decrement")]
        BombIntervalDecrement,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Bomb Pair Rel")]
        [Description("Bomb Pair Release")]
        BombPairRelease,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Bomb Single Rel")]
        [Description("Bomb Single Release")]
        BombSGLRelease,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Burst Alt Incr")]
        [Description("Bomb Burst Altitude Increase")]
        BombBurstIncrement,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Burst Alt Decr")]
        [Description("Bomb Burst Altitude Decrease")]
        BombBurstDecrement,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("HSD Range Incr")]
        [Description("HSD Range Increase")]
        SimHSDRangeStepUp,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("HSD Range Decr")]
        [Description("HSD Range Decrease")]
        SimHSDRangeStepDown,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Swap MFDs")]
        [Description("Swap MFDs")]
        OTWSwapMFDS,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Bar Scan")]
        [Description("Radar Bar Scan Change")]
        SimRadarBarScanChange,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Azimuth Scan")]
        [Description("Radar Azimuth Scan Change")]
        SimRadarAzimuthScanChange,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Radar Freeze")]
        [Description("Radar Freeze")]
        SimRadarFreeze,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Radar Snowplow")]
        [Description("Radar Snowplow")]
        SimRadarSnowplow,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("AA Mode Step")]
        [Description("Radar AA Mode Step")]
        SimRadarAAModeStep,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("AG Mode Step")]
        [Description("Radar AG Mode Step")]
        SimRadarAGModeStep,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Missile Spot/Scan")]
        [Description("Toggle Missile Spot/Scan")]
        SimToggleMissileSpotScan,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Missile Bore/Slave")]
        [Description("Toggle Missile Bore/Slave")]
        SimToggleMissileBoreSlave,

        [Category("MISCELLANEOUS")]
        [SubCategory("SHORTCUTS")]
        [ShortDescription("Missile TD/BP")]
        [Description("Toggle Missile TD/BP")]
        SimToggleMissileTDBPUncage,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Nose Up")]
        [Description("Nose Up")]
        AFElevatorUp,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Nose Down")]
        [Description("Nose Down")]
        AFElevatorDown,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Roll Left")]
        [Description("Roll Left")]
        AFAileronLeft,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Roll Right")]
        [Description("Roll Right")]
        AFAileronRight,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Rudder Left")]
        [Description("Rudder Left")]
        AFRudderLeft,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Rudder Right")]
        [Description("Rudder Right")]
        AFRudderRight,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Throttle Step Up")]
        [Description("Throttle Step Up")]
        AFCoarseThrottleUp,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Throttle Step Dn")]
        [Description("Throttle Step Down")]
        AFCoarseThrottleDown,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Throttle Fwd")]
        [Description("Throttle Forward")]
        AFThrottleUp,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Throttle Back")]
        [Description("Throttle Back")]
        AFThrottleDown,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Throttle Min AB")]
        [Description("Throttle Min. Afterburner")]
        AFABOn,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Throttle Max AB")]
        [Description("Throttle Full Afterburner")]
        AFABFull,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Throttle Idle")]
        [Description("Throttle Idle")]
        AFIdle,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Vtol Exhaust Inc")]
        [Description("VTOL-EXHAUST - Increase Angle")]
        AFIncExhaust,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Vtol Exhaust Dec")]
        [Description("VTOL-EXHAUST - Decrease Angle")]
        AFDecExhaust,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Cycle Engines")]
        [Description("ENGINE - Cycle Engines")]
        CycleEngine,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Left Engine")]
        [Description("ENGINE - Select Left Engine")]
        selectLeftEngine,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Right Engine")]
        [Description("ENGINE - Select Right Engine")]
        selectRightEngine,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Both Engines")]
        [Description("ENGINE - Select Both Engines")]
        selectBothEngines,

        [Category("MISCELLANEOUS")]
        [SubCategory("KEYBOARD FLIGHT CONTROLS")]
        [ShortDescription("Thrust Reverser")]
        [Description("ENGINE - Togg. Thrust Reverser")]
        AFTriggerReverseThrust,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 1")]
        [Description("OSB-1 Button - Push")]
        SimCBEOSB_1T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 2")]
        [Description("OSB-2 Button - Push")]
        SimCBEOSB_2T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 3")]
        [Description("OSB-3 Button - Push")]
        SimCBEOSB_3T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 4")]
        [Description("OSB-4 Button - Push")]
        SimCBEOSB_4T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 5")]
        [Description("OSB-5 Button - Push")]
        SimCBEOSB_5T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 6")]
        [Description("OSB-6 Button - Push")]
        SimCBEOSB_6T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 7")]
        [Description("OSB-7 Button - Push")]
        SimCBEOSB_7T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 8")]
        [Description("OSB-8 Button - Push")]
        SimCBEOSB_8T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 9")]
        [Description("OSB-9 Button - Push")]
        SimCBEOSB_9T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 10")]
        [Description("OSB-10 Button - Push")]
        SimCBEOSB_10T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 11")]
        [Description("OSB-11 Button - Push")]
        SimCBEOSB_11T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 12")]
        [Description("OSB-12 Button - Push")]
        SimCBEOSB_12T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 13")]
        [Description("OSB-13 Button - Push")]
        SimCBEOSB_13T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 14")]
        [Description("OSB-14 Button - Push")]
        SimCBEOSB_14T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 15")]
        [Description("OSB-15 Button - Push")]
        SimCBEOSB_15T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 16")]
        [Description("OSB-16 Button - Push")]
        SimCBEOSB_16T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 17")]
        [Description("OSB-17 Button - Push")]
        SimCBEOSB_17T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 18")]
        [Description("OSB-18 Button - Push")]
        SimCBEOSB_18T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 19")]
        [Description("OSB-19 Button - Push")]
        SimCBEOSB_19T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD OSB 20")]
        [Description("OSB-20 Button - Push")]
        SimCBEOSB_20T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD Brt Inc")]
        [Description("BRT Button - Increase Brightness")]
        SimCBEOSB_BRTUP_T,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (THIRD)")]
        [ShortDescription("TMFD Brt Dec")]
        [Description("BRT Button - Decrease Brightness")]
        SimCBEOSB_BRTDOWN_T,


        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 1")]
        [Description("OSB-1 Button - Push")]
        SimCBEOSB_1F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 2")]
        [Description("OSB-2 Button - Push")]
        SimCBEOSB_2F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 3")]
        [Description("OSB-3 Button - Push")]
        SimCBEOSB_3F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 4")]
        [Description("OSB-4 Button - Push")]
        SimCBEOSB_4F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 5")]
        [Description("OSB-5 Button - Push")]
        SimCBEOSB_5F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 6")]
        [Description("OSB-6 Button - Push")]
        SimCBEOSB_6F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 7")]
        [Description("OSB-7 Button - Push")]
        SimCBEOSB_7F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 8")]
        [Description("OSB-8 Button - Push")]
        SimCBEOSB_8F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 9")]
        [Description("OSB-9 Button - Push")]
        SimCBEOSB_9F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 10")]
        [Description("OSB-10 Button - Push")]
        SimCBEOSB_10F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 11")]
        [Description("OSB-11 Button - Push")]
        SimCBEOSB_11F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 12")]
        [Description("OSB-12 Button - Push")]
        SimCBEOSB_12F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 13")]
        [Description("OSB-13 Button - Push")]
        SimCBEOSB_13F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 14")]
        [Description("OSB-14 Button - Push")]
        SimCBEOSB_14F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 15")]
        [Description("OSB-15 Button - Push")]
        SimCBEOSB_15F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 16")]
        [Description("OSB-16 Button - Push")]
        SimCBEOSB_16F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 17")]
        [Description("OSB-17 Button - Push")]
        SimCBEOSB_17F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 18")]
        [Description("OSB-18 Button - Push")]
        SimCBEOSB_18F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 19")]
        [Description("OSB-19 Button - Push")]
        SimCBEOSB_19F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD OSB 20")]
        [Description("OSB-20 Button - Push")]
        SimCBEOSB_20F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD Brt Inc")]
        [Description("BRT Button - Increase Brightness")]
        SimCBEOSB_BRTUP_F,

        [Category("MISCELLANEOUS")]
        [SubCategory("EXTRA MFD (FOURTH)")]
        [ShortDescription("FMFD Brt Dec")]
        [Description("BRT Button - Decrease Brightness")]
        SimCBEOSB_BRTDOWN_F,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Time Accel 2x")]
        [Description("Time Acceleration - Toggle 2x")]
        TimeAccelerate,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Time Accel 4x")]
        [Description("Time Acceleration - Toggle 4x")]
        TimeAccelerateMaxToggle,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Time Accel Up")]
        [Description("Time Acceleration - Step Up")]
        TimeAccelerateInc,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Time Accel Dn")]
        [Description("Time Acceleration - Step Down")]
        TimeAccelerateDec,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Open Chat Box")]
        [Description("Chat")]
        SimOpenChatBox,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Toggle Freeze")]
        [Description("Sim-Freeze - Toggle")]
        SimMotionFreeze,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Toggle Pause")]
        [Description("Sim-Pause - Toggle")]
        SimTogglePaused,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Sim Pause")]
        [Description("Sim - Pause")]
        SimPause,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Sim Resume")]
        [Description("Sim - Resume")]
        SimResume,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Add. Screenshot ")]
        [Description("Screenshot (additional)")]
        ScreenShot,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("PrettyScreenshot")]
        [Description("Pretty Screenshot (additional)")]
        PrettyScreenShot,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Exit Sim")]
        [Description("Toggle Exit Sim Menu")]
        SimEndFlight,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Labels Near")]
        [Description("Labels Near - Toggle")]
        OTWToggleNames,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Labels Far")]
        [Description("Labels Far - Toggle")]
        OTWToggleCampNames,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Flap Display")]
        [Description("Flap Display Toggle")]
        OTWToggleFlapDisplay,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Engine Display")]
        [Description("Engine Display Toggle")]
        OTWToggleEngineDisplay,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Infobar")]
        [Description("Toggle Infobar")]
        ToggleInfoBar,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Subtitles")]
        [Description("Toggle Radio Subtitles")]
        ToggleSubTitles,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("PrettyFilming")]
        [Description("Pretty Filming (Hide Overlays)")]
        PrettyFilm,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Frame Rate")]
        [Description("Display Frame Rate - Toggle")]
        OTWToggleFrameRate,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Score Display")]
        [Description("Show Score Display - Toggle")]
        OTWToggleScoreDisplay,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Online Status")]
        [Description("Show Online Status - Toggle")]
        OTWToggleOnlinePlayersDisplay,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("HUD Rendering")]
        [Description("Toggle HUD Rendering")]
        OTWToggleHUDRendering,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Camp Quick Save")]
        [Description("Campaign-QuickSave (Host only)")]
        CampaignQuickSave,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Random Error")]
        [Description("Random Error")]
        SimRandomError,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("KeyCombo")]
        [Description("Key Combination Keys (KeyCombo)")]
        CommandsSetKeyCombo,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Recenter Joystick")]
        [Description("Joystick Recenter")]
        RecenterJoystick,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Reload TrackIR")]
        [Description("TrackIR Reload")]
        ReloadTrackIR,

        [Category("MISCELLANEOUS")]
        [SubCategory("SIMULATION & HARDWARE")]
        [ShortDescription("Recenter TrackIR")]
        [Description("TrackIR Recenter (additional)")]
        RecenterTrackIR,

        [Category("MISCELLANEOUS")]
        [SubCategory("WINAMP")]
        [ShortDescription("WinAmp Next")]
        [Description("Next Track")]
        WinAmpNextTrack,

        [Category("MISCELLANEOUS")]
        [SubCategory("WINAMP")]
        [ShortDescription("WinAmp Prev")]
        [Description("Previous Track")]
        WinAmpPreviousTrack,

        [Category("MISCELLANEOUS")]
        [SubCategory("WINAMP")]
        [ShortDescription("WinAmp Start")]
        [Description("Start Playback")]
        WinAmpStartPlayback,

        [Category("MISCELLANEOUS")]
        [SubCategory("WINAMP")]
        [ShortDescription("WinAmp Stop")]
        [Description("Stop Playback")]
        WinAmpStopPlayback,

        [Category("MISCELLANEOUS")]
        [SubCategory("WINAMP")]
        [ShortDescription("WinAmp Play")]
        [Description("Toggle Playback")]
        WinAmpTogglePlayback,

        [Category("MISCELLANEOUS")]
        [SubCategory("WINAMP")]
        [ShortDescription("WinAmp Pause")]
        [Description("Toggle Pause")]
        WinAmpTogglePause,

        [Category("MISCELLANEOUS")]
        [SubCategory("WINAMP")]
        [ShortDescription("WinAmp Vol Up")]
        [Description("Volume Down")]
        WinAmpVolumeDown,

        [Category("MISCELLANEOUS")]
        [SubCategory("WINAMP")]
        [ShortDescription("WinAmp Vol Dn")]
        [Description("Volume Up")]
        WinAmpVolumeUp,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Up")]
        [Description("Rotate View Up")]
        OTWViewUp,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Down")]
        [Description("Rotate View Down")]
        OTWViewDown,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Left")]
        [Description("Rotate View Left")]
        OTWViewLeft,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Right")]
        [Description("Rotate View Right")]
        OTWViewRight,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Up-Right")]
        [Description("Rotate View Up-Right")]
        OTWViewUpRight,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Up-Left")]
        [Description("Rotate View Up-Left")]
        OTWViewUpLeft,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Down-Right")]
        [Description("Rotate View Down-Right")]
        OTWViewDownRight,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Down-Left")]
        [Description("Rotate View Down-Left")]
        OTWViewDownLeft,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("View Reset")]
        [Description("Reset View")]
        OTWViewReset,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("Look Closer")]
        [Description("Look Closer - Toggle")]
        FOVToggle,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("Decr FOV")]
        [Description("Decrease FOV – Or Mousewheel")]
        FOVDecrease,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("Default FOV")]
        [Description("Default FOV")]
        FOVDefault,

        [Category("VIEWS")]
        [SubCategory("VIEW GENERAL CONTROL")]
        [ShortDescription("Incr FOV")]
        [Description("Increase FOV – Or Mousewheel")]
        FOVIncrease,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("SA-Bar")]
        [Description("Toggle SA bar")]
        OTWToggleSidebar,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock Next")]
        [Description("Padlock next")]
        OTWStepNextPadlock,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock Prev")]
        [Description("Padlock previous")]
        OTWStepPrevPadlock,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock Nxt AA")]
        [Description("Padlock next AA")]
        OTWStepNextPadlockAA,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock Prev AA")]
        [Description("Padlock prev AA")]
        OTWStepPrevPadlockAA,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock Nxt AG")]
        [Description("Padlock next AG")]
        OTWStepNextPadlockAG,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock Prev AG")]
        [Description("Padlock prev AG")]
        OTWStepPrevPadlockAG,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock")]
        [Description("Padlock")]
        OTWSelectF3PadlockMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock Mode AA")]
        [Description("Padlock Mode=AA")]
        OTWSelectF3PadlockModeAA,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Padlock Mode AG")]
        [Description("Padlock Mode=AG")]
        OTWSelectF3PadlockModeAG,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Extended FOV")]
        [Description("Extended FOV")]
        OTWSelectEFOVPadlockMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("EFOV Mode AA")]
        [Description("Padlock EFOV Mode=AA")]
        OTWSelectEFOVPadlockModeAA,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("EFOV Mode AG")]
        [Description("Padlock EFOV Mode=AG")]
        OTWSelectEFOVPadlockModeAG,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Hud Only")]
        [Description("HUD Only")]
        OTWSelectHUDMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Snap Pit (3D)")]
        [Description("Snap (3D) Cockpit")]
        OTWSelect2DCockpitMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Pan Pit (3D)")]
        [Description("Pan (3D) Cockpit")]
        OTWSelect3DCockpitMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Empty Ckpit Shell")]
        [Description("Toggle Empty Cockpit Shell")]
        OTWToggle3DEmptyShell,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Tog CustomView")]
        [Description("Toggle Custom 3dPit View")]
        OTWToggleCustom3dPitView,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Nxt Custom View")]
        [Description(" Next Custom 3dPit View")]
        OTWNextCustom3dPitView,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Prev Custom View")]
        [Description(" Previous Custom 3dPit View")]
        OTWPrevCustom3dPitView,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Glance Fwd")]
        [Description("Glance Forward")]
        OTWGlanceForward,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Glace Bckwd")]
        [Description("Glance Backward")]
        OTWCheckSix,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Zoom In")]
        [Description("Zoom In")]
        OTWViewZoomIn,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Zoom Out")]
        [Description("Zoom Out")]
        OTWViewZoomOut,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Tgt to Self Cam")]
        [Description("Target-To-Self Camera")]
        OTWTrackExternal,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Tgt to Wpn Cam")]
        [Description("Target-to-Weapon Camera")]
        OTWTrackTargetToWeapon,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Next Aircraft")]
        [Description("Next Aircraft")]
        OTWStepNextAC,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Prev Aircraft")]
        [Description("Previous Aircraft")]
        OTWStepPrevAC,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Orbit Cam")]
        [Description("Orbit Camera")]
        OTWSelectOrbitMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Friendly AC Cam")]
        [Description("Friendly Aircraft Camera")]
        OTWSelectAirFriendlyMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Friendly GU Cam")]
        [Description("Friendly Ground Unit Camera")]
        OTWSelectGroundFriendlyMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Enemy AC Cam")]
        [Description("Enemy Aircraft Camera")]
        OTWSelectAirEnemyMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Enemy GU Cam")]
        [Description("Enemy Ground Unit Camera")]
        OTWSelectGroundEnemyMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Target Cam")]
        [Description("Target Camera")]
        OTWSelectTargetMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Weapon Cam")]
        [Description("Weapon Camera")]
        OTWSelectWeaponMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Satellite Cam")]
        [Description("Satellite Camera")]
        OTWSelectSatelliteMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Flyby Cam")]
        [Description("Flyby Camera")]
        OTWSelectFlybyMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Incoming Cam")]
        [Description("Incoming Camera")]
        OTWSelectIncomingMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Chase Cam")]
        [Description("Chase Camera")]
        OTWSelectChaseMode,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Displcmnt Cam")]
        [Description("Toggle Displacement Camera")]
        ToggleDisplacementCam,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("TopGun Cam")]
        [Description("TopGun Camera")]
        OTWSelectTopGunView,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("TopGun Nxt")]
        [Description("Next TopGun View")]
        OTWSelectNextTopGunView,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("TopGun Prev")]
        [Description("Prev TopGun View")]
        OTWSelectPrevTopGunView,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("Action Cam")]
        [Description("Action Camera")]
        OTWToggleActionCamera,

        [Category("VIEWS")]
        [SubCategory("VIEW INTERNAL")]
        [ShortDescription("EyFly Free Cam")]
        [Description("Toggle EyeFly (Free Cam)")]
        OTWToggleEyeFly,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Awacs Menu")]
        [Description("AWACS Menu")]
        RadioAWACSCommand,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Wingman Menu")]
        [Description("Wingman Menu")]
        RadioWingCommand,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Element Menu")]
        [Description("Element Menu")]
        RadioElementCommand,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Flight Menu")]
        [Description("Flight Menu")]
        RadioFlightCommand,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("ATC Menu")]
        [Description("ATC Menu")]
        RadioTowerCommand,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Tanker Menu")]
        [Description("Tanker Menu")]
        RadioTankerCommand,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Message")]
        [Description("Send Message")]
        RadioMessageSend,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu Nav")]
        [Description("Next menu")]
        OTWRadioMenuStep,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu Nav")]
        [Description("Previous menu")]
        OTWRadioMenuStepBack,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 1")]
        [Description("Menu One")]
        RadioMenuOne,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 2")]
        [Description("Menu Two")]
        RadioMenuTwo,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 3")]
        [Description("Menu Three")]
        RadioMenuThree,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 4")]
        [Description("Menu Four")]
        RadioMenuFour,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 5")]
        [Description("Menu Five")]
        RadioMenuFive,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 6")]
        [Description("Menu Six")]
        RadioMenuSix,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 7")]
        [Description("Menu Seven")]
        RadioMenuSeven,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 8")]
        [Description("Menu Eight")]
        RadioMenuEight,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu 9")]
        [Description("Menu Nine")]
        RadioMenuNine,

        [Category("RADIO COMMS")]
        [SubCategory("GENERAL RADIO OPTIONS")]
        [ShortDescription("Radio Menu Clear")]
        [Description("Menu Clear")]
        OTWRadioMenuClear,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Picture")]
        [Description("Request Picture")]
        AWACSRequestPicture,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Declare")]
        [Description("Declare")]
        AWACSDeclare,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Req Help")]
        [Description("Request Help")]
        AWACSRequestHelp,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Wilco")]
        [Description("Wilco")]
        AWACSWilco,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Unable")]
        [Description("Unable")]
        AWACSUnable,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Req Relief")]
        [Description("Request Relief")]
        AWACSRequestRelief,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Vector Tgt")]
        [Description("Vector To Nearest Threat")]
        AWACSVectorToThreat,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Vect Tank")]
        [Description("Vector To Tanker")]
        AWACSRequestTanker,

        [Category("RADIO COMMS")]
        [SubCategory("AWACS COMMS")]
        [ShortDescription("Awacs Vect Carrie")]
        [Description("Vector To Carrier Group")]
        AWACSRequestCarrier,

        [Category("RADIO COMMS")]
        [SubCategory("ATC COMMS")]
        [ShortDescription("Atc Inbound")]
        [Description("Inbound For Landing")]
        ATCRequestClearance,

        [Category("RADIO COMMS")]
        [SubCategory("ATC COMMS")]
        [ShortDescription("Atc Emergency")]
        [Description("Declaring An Emergency")]
        ATCRequestEmergencyClearance,

        [Category("RADIO COMMS")]
        [SubCategory("ATC COMMS")]
        [ShortDescription("Atc Abort")]
        [Description("Abort Approach")]
        ATCAbortApproach,

        [Category("RADIO COMMS")]
        [SubCategory("ATC COMMS")]
        [ShortDescription("Atc Req Taxi")]
        [Description("Request Taxi")]
        ATCRequestTaxi,

        [Category("RADIO COMMS")]
        [SubCategory("ATC COMMS")]
        [ShortDescription("Atc Req TakeOff")]
        [Description("Request Takeoff")]
        ATCRequestTakeoff,

        [Category("RADIO COMMS")]
        [SubCategory("TANKER COMMS")]
        [ShortDescription("Tanker Req Fuel")]
        [Description("Request Fuel")]
        TankerRequestFuel,

        [Category("RADIO COMMS")]
        [SubCategory("TANKER COMMS")]
        [ShortDescription("Tanker Ready")]
        [Description("Ready For Gas")]
        TankerReadyForGas,

        [Category("RADIO COMMS")]
        [SubCategory("TANKER COMMS")]
        [ShortDescription("Tanker Done")]
        [Description("Done Refueling")]
        TankerDoneRefueling,

        [Category("RADIO COMMS")]
        [SubCategory("TANKER COMMS")]
        [ShortDescription("Tanker Break")]
        [Description("Breakaway")]
        TankerBreakaway,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Attck my Tgt")]
        [Description("Attack My Target")]
        WingmanDesignateTarget,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Wpns Free")]
        [Description("Weapons Free")]
        WingmanWeaponsFree,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Wpns Hold")]
        [Description("Weapons Hold")]
        WingmanWeaponsHold,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Check Six")]
        [Description("Check Your Six")]
        WingmanCheckSix,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Clear Six")]
        [Description("Clear My Six")]
        WingmanClearSix,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Attck Tgts")]
        [Description("Attack Targets")]
        WingmanDesignateGroup,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Shooter")]
        [Description("Go Shooter")]
        WingmanGoShooterMode,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Cover")]
        [Description("Go Cover")]
        WingmanGoCoverMode,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Rejoin")]
        [Description("Rejoin")]
        WingmanRejoin,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Pince")]
        [Description("Pince")]
        WingmanPince,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Posthole")]
        [Description("Posthole")]
        WingmanPosthole,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Chainsaw")]
        [Description("Chainsaw")]
        WingmanChainsaw,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Drop Stores")]
        [Description("Drop Stores")]
        WingmanDropStores,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Datalnk Gnd")]
        [Description("Datalink Ground Target")]
        WingmanSendGrdDL,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Resume ")]
        [Description("Resume Mission")]
        WingmanResumeNormal,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing RTB")]
        [Description("Return to Base")]
        WingmanRTB,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Say Posit")]
        [Description("Say Position")]
        WingmanGiveBra,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Say Damge")]
        [Description("Say Damage")]
        WingmanGiveDamageReport,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Say Status")]
        [Description("Say Status")]
        WingmanGiveStatus,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Say Fuel")]
        [Description("Say Fuel")]
        WingmanGiveFuelState,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Say Wpns")]
        [Description("Say Weapons")]
        WingmanGiveWeaponsCheck,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Close Up")]
        [Description("Close Up")]
        WingmanCloseup,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Switch Side")]
        [Description("Switch Side")]
        WingmanToggleSide,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Break Right")]
        [Description("Break Right")]
        WingmanBreakRight,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Break Left")]
        [Description("Break Left")]
        WingmanBreakLeft,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Higher")]
        [Description("Go Higher")]
        WingmanIncreaseRelAlt,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Lower")]
        [Description("Go Lower")]
        WingmanDecreaseRelAlt,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Flex")]
        [Description("Flex")]
        WingmanFlex,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Kickout")]
        [Description("Kickout")]
        WingmanKickout,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Wedge")]
        [Description("Go Wedge")]
        WingmanWedge,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Trail")]
        [Description("Go Trail")]
        WingmanTrail,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Ladder")]
        [Description("Go Ladder")]
        WingmanLadder,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Stack")]
        [Description("Go Stack")]
        WingmanStack,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Fluid")]
        [Description("Go Fluid")]
        WingmanFluid,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Spread")]
        [Description("Go Spread")]
        WingmanSpread,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Arrowh")]
        [Description("Go Arrowhead")]
        WingmanArrow,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Echel. Rt")]
        [Description("Go Echelon Right")]
        WingmanEchelonRight,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Echel. Lt")]
        [Description("Go Echelon Left")]
        WingmanEchelonLeft,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Line")]
        [Description("Go Line")]
        WingmanLine,

        [Category("RADIO COMMS")]
        [SubCategory("WINGMAN COMMANDS")]
        [ShortDescription("Wing Go Diamond")]
        [Description("Go Diamond")]
        WingmanDiamond,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Attck my Tgt")]
        [Description("Attack My Target")]
        ElementDesignateTarget,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Wpns Free")]
        [Description("Weapons Free")]
        ElementWeaponsFree,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Wpns Hold")]
        [Description("Weapons Hold")]
        ElementWeaponsHold,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Check Six")]
        [Description("Check Your Six")]
        ElementCheckSix,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Clear Six")]
        [Description("Clear My Six")]
        ElementClearSix,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Attck Tgts")]
        [Description("Attack Targets")]
        ElementDesignateGroup,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Rejoin")]
        [Description("Rejoin")]
        ElementRejoin,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Pince")]
        [Description("Pince")]
        ElementPince,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Posthole")]
        [Description("Posthole")]
        ElementPosthole,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Chainsaw")]
        [Description("Chainsaw")]
        ElementChainsaw,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Drop Stores")]
        [Description("Drop Stores")]
        ElementDropStores,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem DatalnkGnd")]
        [Description("Datalink Ground Target")]
        ElementSendGrnDL,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Resume ")]
        [Description("Resume Mission")]
        ElementResumeNormal,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem RTB")]
        [Description("Return to Base")]
        ElementRTB,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Say Posit")]
        [Description("Say Position")]
        ElementGiveBra,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Say Damge")]
        [Description("Say Damage")]
        ElementGiveDamageReport,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Say Status")]
        [Description("Say Status")]
        ElementGiveStatus,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Say Fuel")]
        [Description("Say Fuel")]
        ElementGiveFuelState,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Say Wpns")]
        [Description("Say Weapons")]
        ElementGiveWeaponsCheck,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Close Up")]
        [Description("Close Up")]
        ElementCloseup,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Switch Side")]
        [Description("Switch Side")]
        ElementToggleSide,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Break Right")]
        [Description("Break Right")]
        ElementBreakRight,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Break Left")]
        [Description("Break Left")]
        ElementBreakLeft,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Higher")]
        [Description("Go Higher")]
        ElementIncreaseRelAlt,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Lower")]
        [Description("Go Lower")]
        ElementDecreaseRelAlt,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Flex")]
        [Description("Flex")]
        ElementFlex,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Kickout")]
        [Description("Kickout")]
        ElementKickout,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Wedge")]
        [Description("Go Wedge")]
        ElementWedge,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Trail")]
        [Description("Go Trail")]
        ElementTrail,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Ladder")]
        [Description("Go Ladder")]
        ElementLadder,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Stack")]
        [Description("Go Stack")]
        ElementStack,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Fluid")]
        [Description("Go Fluid")]
        ElementFluid,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Spread")]
        [Description("Go Spread")]
        ElementSpread,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Arrowh")]
        [Description("Go Arrowhead")]
        ElementArrow,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Eche Rt")]
        [Description("Go Echelon Right")]
        ElementEchelonRight,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Eche Lt")]
        [Description("Go Echelon Left")]
        ElementEchelonLeft,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Line")]
        [Description("Go Line")]
        ElementLine,

        [Category("RADIO COMMS")]
        [SubCategory("ELEMENT COMMANDS")]
        [ShortDescription("Elem Go Diamnd")]
        [Description("Go Diamond")]
        ElementDiamond,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Attck my Tgt")]
        [Description("Attack My Target")]
        FlightDesignateTarget,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Wpns Free")]
        [Description("Weapons Free")]
        FlightWeaponsFree,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Wpns Hold")]
        [Description("Weapons Hold")]
        FlightWeaponsHold,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Check Six")]
        [Description("Check Your Six")]
        FlightCheckSix,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Clear Six")]
        [Description("Clear My Six")]
        FlightClearSix,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Attck Tgts")]
        [Description("Attack Target")]
        FlightDesignateGroup,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Rejoin")]
        [Description("Rejoin")]
        FlightRejoin,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Pince")]
        [Description("Pince")]
        FlightPince,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Posthole")]
        [Description("Posthole")]
        FlightPosthole,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Chainsaw")]
        [Description("Chainsaw")]
        FlightChainsaw,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Drop Stores")]
        [Description("Drop Store")]
        FlightDropStores,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt DatalnkGnd")]
        [Description("Datalink Ground Target")]
        FlightSendGrnDL,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Resume ")]
        [Description("Resume Mission")]
        FlightResumeNormal,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt RTB")]
        [Description("Return to Base")]
        FlightRTB,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Say Posit")]
        [Description("Say Position")]
        FlightGiveBra,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Say Damge")]
        [Description("Say Damage")]
        FlightGiveDamageReport,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Say Status")]
        [Description("Say Status")]
        FlightGiveStatus,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Say Fuel")]
        [Description("Say Fuel")]
        FlightGiveFuelState,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Say Wpns")]
        [Description("Say Weapon")]
        FlightGiveWeaponsCheck,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Close Up")]
        [Description("Close Up")]
        FlightCloseup,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Switch Side")]
        [Description("Switch Side")]
        FlightToggleSide,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Break Right")]
        [Description("Break Right")]
        FlightBreakRight,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Break Left")]
        [Description("Break Left")]
        FlightBreakLeft,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Higher")]
        [Description("Go Higher")]
        FlightIncreaseRelAlt,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Lower")]
        [Description("Go Lower")]
        FlightDecreaseRelAlt,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Flex")]
        [Description("Flex")]
        FlightFlex,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Kickout")]
        [Description("Kickout")]
        FlightKickout,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Wedge")]
        [Description("Go Wedge")]
        FlightWedge,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Trail")]
        [Description("Go Trail")]
        FlightTrail,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Ladder")]
        [Description("Go Ladder")]
        FlightLadder,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Stack")]
        [Description("Go Stack")]
        FlightStack,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Fluid")]
        [Description("Go Fluid")]
        FlightFluid,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Spread")]
        [Description("Go Spread")]
        FlightSpread,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Arrowh")]
        [Description("Go Arrowhead")]
        FlightArrow,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Box")]
        [Description("Go Box")]
        FlightBox,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Res Cell")]
        [Description("Go Res Cell")]
        FlightResCell,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Vic")]
        [Description("Go VIC")]
        FlightVic,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Line")]
        [Description("Go Line")]
        FlightLine,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Finger 4")]
        [Description("Go Finger Four")]
        FlightFinger4,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Echolon Rt")]
        [Description("Go Echolon Left")]
        FlightEchelonRight,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Echolon Lt")]
        [Description("Go Echolon Right")]
        FlightEchelonLeft,

        [Category("RADIO COMMS")]
        [SubCategory("FLIGHT COMMANDS")]
        [ShortDescription("Flt Go Diamond")]
        [Description("Go Diamond")]
        FlightDiamond,


    }
}
