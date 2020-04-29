using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Generic;
using Common.MacroProgramming;
using Common.SimSupport;
using Common.Strings;
using F4KeyFile;
using F4SharedMem;
using F4SharedMem.Headers;
using Constants = Common.Math.Constants;
using Util = Common.Util;

namespace F4Utils.SimSupport
{
    public class Falcon4SimSupportModule : SimSupportModule
    {
        public const float DEGREES_PER_RADIAN = 57.2957795f;
        public const float FEET_PER_SECOND_PER_KNOT = 1.68780986f;
        private const float GLIDESLOPE_DEVIATION_LIMIT_DEGREES = 1.0F;
        private const float LOCALIZER_DEVIATION_LIMIT_DEGREES = 5.0F;
        private const float SIDESLIP_ANGLE_LIMIT_DEGREES = 5;
        private const float HPA_TO_HG = 1.0f / 33.8638866667f;
        private const double BASIC_SMOOTHING_TIME_CONSTANT = 60;
        private readonly Dictionary<string, ISimOutput> _simOutputs = new Dictionary<string, ISimOutput>();
        private readonly Dictionary<string, SimCommand> _simCommands = new Dictionary<string, SimCommand>();
        private FlightData _lastFlightData;
        private Reader _smReader;
        private readonly IIndicatedRateOfTurnCalculator _rateOfTurnCalculator = new IndicatedRateOfTurnCalculator();
        private readonly MorseCode _morseCodeGenerator = new MorseCode();
        private bool _disposed;
        private bool _markerBeaconMorseCodeState;

        public Falcon4SimSupportModule()
        {
            _morseCodeGenerator.CharactersPerMinute = 53;
            _morseCodeGenerator.UnitTimeTick += _morseCodeGenerator_UnitTimeTick;
            EnsureSharedmemReaderIsCreated();
            CreateSimOutputsList();
            CreateSimCommandsList();
            UpdateSimOutputs();
        }

        public override string FriendlyName => "Falcon BMS";

        private DateTime _lastFalconIsRunningCheckTime = DateTime.UtcNow;
        private bool _lastFalconIsRunningCheckResult;


        public override bool IsSimRunning
        {
            get
            {
                if (DateTime.UtcNow.Subtract(_lastFalconIsRunningCheckTime).TotalMilliseconds < 500) return _lastFalconIsRunningCheckResult;

                _lastFalconIsRunningCheckTime = DateTime.UtcNow;
                EnsureSharedmemReaderIsCreated();
                _lastFalconIsRunningCheckResult = _smReader.IsFalconRunning;
                return _lastFalconIsRunningCheckResult;
            }
        }


        public override Dictionary<string, ISimOutput> SimOutputs => _simOutputs;
        public override Dictionary<string, SimCommand> SimCommands => _simCommands;

        private void EnsureSharedmemReaderIsCreated()
        {
            if (_smReader?.IsFalconRunning ?? false) return;

            Util.DisposeObject(_smReader);
            _smReader = new Reader();
        }

        private void GetNextFlightDataFromSharedMem() { _lastFlightData = IsSimRunning ? _smReader?.GetCurrentData() : new FlightData(); }

        private void UpdateSimOutputs()
        {
            if (_simOutputs == null) return;

            GetNextFlightDataFromSharedMem();

            DetermineWhetherToShowILSCommandBarsAndToFromFlags(_lastFlightData, out var showToFromFlag, out var showCommandBars);
            UpdateHsiData(_lastFlightData, out float courseDeviationDegrees, out float deviationLimitDegrees);

            if (_lastFlightData == null) return;

            var subscribedSimOutputs = _simOutputs.Values.Where(x => x.HasListeners || x is TextSignal);
            Parallel.ForEach(subscribedSimOutputs, simOutput => UpdateSimOutput(showToFromFlag, showCommandBars, courseDeviationDegrees, deviationLimitDegrees, simOutput));
        }

        private void UpdateSimOutput(bool showToFromFlag, bool showCommandBars, float courseDeviationDegrees, float deviationLimitDegrees, ISimOutput output)
        {
            if (output == null) return;

            var simOutput = (F4SimOutputs) ((Signal) output).SubSource;

            switch (simOutput)
            {
                case F4SimOutputs.MAP__OWNSHIP_POSITION__FEET_NORTH_OF_MAP_ORIGIN:
                    SetOutput((AnalogSignal) output, _lastFlightData.x);
                    break;

                case F4SimOutputs.MAP__OWNSHIP_POSITION__FEET_EAST_OF_MAP_ORIGIN:
                    SetOutput((AnalogSignal) output, _lastFlightData.y);
                    break;

                case F4SimOutputs.MAP__OWNSHIP_POSITION__LATITUDE:
                    SetOutput((AnalogSignal)output, _lastFlightData.latitude);
                    break;
                case F4SimOutputs.MAP__OWNSHIP_POSITION__LONGITUDE:
                    SetOutput((AnalogSignal)output, _lastFlightData.longitude);
                    break;

                case F4SimOutputs.MAP__BULLSEYE_POSITION__FEET_NORTH_OF_MAP_ORIGIN:
                    SetOutput((AnalogSignal)output, _lastFlightData.bullseyeX);
                    break;

                case F4SimOutputs.MAP__BULLSEYE_POSITION__FEET_EAST_OF_MAP_ORIGIN:
                    SetOutput((AnalogSignal)output, _lastFlightData.bullseyeY);
                    break;

                case F4SimOutputs.MAP__GROUND_SPEED_VECTOR__NORTH_COMPONENT_FPS:
                    SetOutput((AnalogSignal) output, _lastFlightData.xDot);
                    break;

                case F4SimOutputs.MAP__GROUND_SPEED_VECTOR__EAST_COMPONENT_FPS:
                    SetOutput((AnalogSignal) output, _lastFlightData.yDot);
                    break;

                case F4SimOutputs.MAP__GROUND_SPEED_KNOTS:
                    ((AnalogSignal) output).State = Math.Sqrt(_lastFlightData.xDot * _lastFlightData.xDot + _lastFlightData.yDot * _lastFlightData.yDot) / FEET_PER_SECOND_PER_KNOT;
                    break;

                case F4SimOutputs.ALTIMETER__INDICATED_ALTITUDE__MSL:
                    SetOutput((AnalogSignal) output, -_lastFlightData.aauz);
                    break;

                case F4SimOutputs.ALTIMETER__BAROMETRIC_PRESSURE_INCHES_HG:
                    if (((AltBits) _lastFlightData.altBits & AltBits.CalType) == AltBits.CalType)
                    {
                        var hg = (_lastFlightData.AltCalReading != 0 ? _lastFlightData.AltCalReading : 2992) / 100.0f;
                        SetOutput((AnalogSignal) output, hg);
                    }
                    else
                    {
                        var hPa = _lastFlightData.AltCalReading != 0 ? _lastFlightData.AltCalReading : 1013.2075;
                        var hg = hPa * HPA_TO_HG;
                        SetOutput((AnalogSignal) output, hg);
                    }
                    break;

                case F4SimOutputs.TRUE_ALTITUDE__MSL:
                    SetOutput((AnalogSignal) output, -_lastFlightData.z);
                    break;

                case F4SimOutputs.RADAR_ALTIMETER__ALTITUDE__AGL:
                    SetOutput((AnalogSignal)output, -_lastFlightData.RALT);
                    break;

                case F4SimOutputs.RADAR_ALTIMETER__RALT_VALID:
                    ((DigitalSignal)output).State = ((MiscBits)_lastFlightData.miscBits & MiscBits.RALT_Valid) == MiscBits.RALT_Valid;
                    break;

                case F4SimOutputs.VVI__VERTICAL_VELOCITY_FPM:
                    SetOutput((AnalogSignal) output, -_lastFlightData.zDot * 60);
                    break;

                case F4SimOutputs.FLIGHT_DYNAMICS__SIDESLIP_ANGLE_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.beta);
                    break;

                case F4SimOutputs.FLIGHT_DYNAMICS__CLIMBDIVE_ANGLE_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.gamma * DEGREES_PER_RADIAN);
                    break;

                case F4SimOutputs.FLIGHT_DYNAMICS__OWNSHIP_NORMAL_GS:
                    SetOutput((AnalogSignal) output, _lastFlightData.gs);
                    break;

                case F4SimOutputs.AIRSPEED_MACH_INDICATOR__MACH_NUMBER:
                    SetOutput((AnalogSignal) output, _lastFlightData.mach);
                    break;

                case F4SimOutputs.AIRSPEED_MACH_INDICATOR__INDICATED_AIRSPEED_KNOTS:
                    SetOutput((AnalogSignal) output, _lastFlightData.kias);
                    break;

                case F4SimOutputs.AIRSPEED_MACH_INDICATOR__TRUE_AIRSPEED_KNOTS:
                    SetOutput((AnalogSignal) output, _lastFlightData.vt / FEET_PER_SECOND_PER_KNOT);
                    break;

                case F4SimOutputs.HUD__WIND_DELTA_TO_FLIGHT_PATH_MARKER_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.windOffset * DEGREES_PER_RADIAN);
                    break;

                case F4SimOutputs.NOZ_POS1__NOZZLE_PERCENT_OPEN:
                    SetOutput((AnalogSignal) output, _lastFlightData.nozzlePos * 100);
                    break;

                case F4SimOutputs.NOZ_POS2__NOZZLE_PERCENT_OPEN:
                    SetOutput((AnalogSignal) output, _lastFlightData.nozzlePos2 * 100);
                    break;

                case F4SimOutputs.HYD_PRESSURE_A__PSI:
                    SetOutput((AnalogSignal) output, _lastFlightData.hydPressureA);
                    break;

                case F4SimOutputs.HYD_PRESSURE_B__PSI:
                    SetOutput((AnalogSignal) output, _lastFlightData.hydPressureB);
                    break;

                case F4SimOutputs.FUEL_QTY__INTERNAL_FUEL_POUNDS:
                    SetOutput((AnalogSignal) output, _lastFlightData.internalFuel);
                    break;

                case F4SimOutputs.FUEL_QTY__EXTERNAL_FUEL_POUNDS:
                    SetOutput((AnalogSignal) output, _lastFlightData.externalFuel);
                    break;

                case F4SimOutputs.FUEL_FLOW1__FUEL_FLOW_POUNDS_PER_HOUR:
                    SetOutput((AnalogSignal) output, _lastFlightData.fuelFlow);
                    break;

                case F4SimOutputs.FUEL_FLOW2__FUEL_FLOW_POUNDS_PER_HOUR:
                    SetOutput((AnalogSignal) output, _lastFlightData.fuelFlow2);
                    break;

                case F4SimOutputs.RPM1__RPM_PERCENT:
                    SetOutput((AnalogSignal) output, _lastFlightData.rpm);
                    break;

                case F4SimOutputs.RPM2__RPM_PERCENT:
                    SetOutput((AnalogSignal) output, _lastFlightData.rpm2);
                    break;

                case F4SimOutputs.FTIT1__FTIT_TEMP_DEG_CELCIUS:
                    SetOutput((AnalogSignal) output, _lastFlightData.ftit * 100);
                    break;

                case F4SimOutputs.FTIT2__FTIT_TEMP_DEG_CELCIUS:
                    SetOutput((AnalogSignal) output, _lastFlightData.ftit2 * 100);
                    break;

                case F4SimOutputs.SPEED_BRAKE__POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.speedBrake);
                    break;

                case F4SimOutputs.SPEED_BRAKE__NOT_STOWED_FLAG:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.SpeedBrake) == LightBits3.SpeedBrake;
                    break;

                case F4SimOutputs.EPU_FUEL__EPU_FUEL_PERCENT:
                    SetOutput((AnalogSignal) output, _lastFlightData.epuFuel);
                    break;

                case F4SimOutputs.OIL_PRESS1__OIL_PRESS_PERCENT:
                    SetOutput((AnalogSignal) output, _lastFlightData.oilPressure);
                    break;

                case F4SimOutputs.OIL_PRESS2__OIL_PRESS_PERCENT:
                    SetOutput((AnalogSignal) output, _lastFlightData.oilPressure2);
                    break;

                case F4SimOutputs.CABIN_PRESS__CABIN_PRESS_FEET_MSL:
                    SetOutput((AnalogSignal) output, _lastFlightData.cabinAlt);
                    break;

                case F4SimOutputs.COMPASS__MAGNETIC_HEADING_DEGREES:
                    SetOutput((AnalogSignal) output, (360 + _lastFlightData.yaw / Constants.RADIANS_PER_DEGREE) % 360);
                    break;

                case F4SimOutputs.GEAR_PANEL__GEAR_POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.gearPos);
                    break;

                case F4SimOutputs.GEAR_PANEL__NOSE_GEAR_DOWN_LIGHT:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.NoseGearDown) == LightBits3.NoseGearDown;
                    break;

                case F4SimOutputs.GEAR_PANEL__LEFT_GEAR_DOWN_LIGHT:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.LeftGearDown) == LightBits3.LeftGearDown;
                    break;

                case F4SimOutputs.GEAR_PANEL__RIGHT_GEAR_DOWN_LIGHT:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.RightGearDown) == LightBits3.RightGearDown;
                    break;

                case F4SimOutputs.GEAR_PANEL__NOSE_GEAR_POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.NoseGearPos);
                    break;

                case F4SimOutputs.GEAR_PANEL__LEFT_GEAR_POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.LeftGearPos);
                    break;

                case F4SimOutputs.GEAR_PANEL__RIGHT_GEAR_POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.RightGearPos);
                    break;

                case F4SimOutputs.GEAR_PANEL__GEAR_HANDLE_LIGHT:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.GEARHANDLE) == LightBits2.GEARHANDLE;
                    break;

                case F4SimOutputs.GEAR_PANEL__PARKING_BRAKE_ENGAGED_FLAG:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.ParkBrakeOn) == LightBits3.ParkBrakeOn;
                    break;

                case F4SimOutputs.ADI__PITCH_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.pitch * DEGREES_PER_RADIAN);
                    break;

                case F4SimOutputs.ADI__ROLL_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.roll * DEGREES_PER_RADIAN);
                    break;


                case F4SimOutputs.ADI__ILS_SHOW_COMMAND_BARS:
                    ((DigitalSignal) output).State = showCommandBars;
                    break;

                case F4SimOutputs.ADI__ILS_HORIZONTAL_BAR_POSITION:
                    SetOutput((AnalogSignal) output, IsSimRunning ? _lastFlightData.AdiIlsVerPos * DEGREES_PER_RADIAN / GLIDESLOPE_DEVIATION_LIMIT_DEGREES : 0.0f);
                    break;

                case F4SimOutputs.ADI__ILS_VERTICAL_BAR_POSITION:
                    SetOutput((AnalogSignal) output, IsSimRunning ? _lastFlightData.AdiIlsHorPos * DEGREES_PER_RADIAN / LOCALIZER_DEVIATION_LIMIT_DEGREES : 0.0f);
                    break;

                case F4SimOutputs.ADI__RATE_OF_TURN_INDICATOR_POSITION:
                    var rateOfTurn = _rateOfTurnCalculator.DetermineIndicatedRateOfTurn(_lastFlightData.yaw * DEGREES_PER_RADIAN);
                    var percentDeflection = rateOfTurn / (IndicatedRateOfTurnCalculator.MAX_INDICATED_RATE_OF_TURN_DECIMAL_DEGREES_PER_SECOND + 1.5f);
                    if (percentDeflection > 1.0f) percentDeflection = 1.0f;
                    if (percentDeflection < -1.0f) percentDeflection = -1.0f;
                    if (!IsSimRunning) percentDeflection = 0.0f;
                    SetOutput((AnalogSignal) output, percentDeflection);
                    break;

                case F4SimOutputs.ADI__INCLINOMETER_POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.beta / SIDESLIP_ANGLE_LIMIT_DEGREES);
                    break;

                case F4SimOutputs.ADI__OFF_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.ADI_OFF) == HsiBits.ADI_OFF || !IsSimRunning;
                    break;

                case F4SimOutputs.ADI__AUX_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.ADI_AUX) == HsiBits.ADI_AUX || !IsSimRunning;
                    break;

                case F4SimOutputs.ADI__GS_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.ADI_GS) == HsiBits.ADI_GS || !IsSimRunning;
                    break;

                case F4SimOutputs.ADI__LOC_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.ADI_LOC) == HsiBits.ADI_LOC || !IsSimRunning;
                    break;

                case F4SimOutputs.STBY_ADI__PITCH_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.pitch * DEGREES_PER_RADIAN);
                    break;

                case F4SimOutputs.STBY_ADI__ROLL_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.roll * DEGREES_PER_RADIAN);
                    break;

                case F4SimOutputs.STBY_ADI__OFF_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.BUP_ADI_OFF) == HsiBits.BUP_ADI_OFF || !IsSimRunning;
                    break;

                case F4SimOutputs.VVI__OFF_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.VVI) == HsiBits.VVI || !IsSimRunning;
                    break;

                case F4SimOutputs.AOA_INDICATOR__AOA_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.alpha);
                    break;

                case F4SimOutputs.AOA_INDICATOR__OFF_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.AOA) == HsiBits.AOA || !IsSimRunning;
                    break;

                case F4SimOutputs.HSI__COURSE_DEVIATION_INVALID_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.IlsWarning) == HsiBits.IlsWarning;
                    break;

                case F4SimOutputs.HSI__DISTANCE_INVALID_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.CourseWarning) == HsiBits.CourseWarning;
                    break;

                case F4SimOutputs.HSI__DESIRED_COURSE_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.desiredCourse);
                    break;

                case F4SimOutputs.HSI__COURSE_DEVIATION_DEGREES:
                    SetOutput((AnalogSignal) output, courseDeviationDegrees);
                    break;

                case F4SimOutputs.HSI__COURSE_DEVIATION_LIMIT_DEGREES:
                    SetOutput((AnalogSignal) output, deviationLimitDegrees);
                    break;

                case F4SimOutputs.HSI__DISTANCE_TO_BEACON_NAUTICAL_MILES:
                    SetOutput((AnalogSignal) output, _lastFlightData.distanceToBeacon);
                    break;

                case F4SimOutputs.HSI__BEARING_TO_BEACON_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.bearingToBeacon);
                    break;

                case F4SimOutputs.HSI__CURRENT_HEADING_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.currentHeading);
                    break;

                case F4SimOutputs.HSI__DESIRED_HEADING_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.desiredHeading);
                    break;

                case F4SimOutputs.HSI__LOCALIZER_COURSE_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.localizerCourse);
                    break;

                case F4SimOutputs.MAP__AIRBASE_FEET_NORTH_OF_MAP_ORIGIN:
                    SetOutput((AnalogSignal) output, _lastFlightData.airbaseX);
                    break;

                case F4SimOutputs.MAP__AIRBASE_FEET_EAST_OF_MAP_ORIGIN:
                    SetOutput((AnalogSignal) output, _lastFlightData.airbaseY);
                    break;

                case F4SimOutputs.HSI__TO_FLAG:
                    ((DigitalSignal) output).State = Math.Abs(Common.Math.Util.AngleDelta(_lastFlightData.desiredCourse, _lastFlightData.bearingToBeacon)) <= 90 && showToFromFlag;
                    break;

                case F4SimOutputs.HSI__FROM_FLAG:
                    ((DigitalSignal) output).State = Math.Abs(Common.Math.Util.AngleDelta(_lastFlightData.desiredCourse, _lastFlightData.bearingToBeacon)) > 90 && showToFromFlag;
                    break;

                case F4SimOutputs.HSI__OFF_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.HSI_OFF) == HsiBits.HSI_OFF || !IsSimRunning;
                    break;

                case F4SimOutputs.HSI__HSI_MODE:
                    SetOutput((AnalogSignal) output, _lastFlightData.navMode);
                    break;

                case F4SimOutputs.TRIM__PITCH_TRIM:
                    SetOutput((AnalogSignal) output, _lastFlightData.TrimPitch * 2);
                    break;

                case F4SimOutputs.TRIM__ROLL_TRIM:
                    SetOutput((AnalogSignal) output, _lastFlightData.TrimRoll * 2);
                    break;

                case F4SimOutputs.TRIM__YAW_TRIM:
                    SetOutput((AnalogSignal) output, _lastFlightData.TrimYaw * 2);
                    break;

                case F4SimOutputs.DED__LINES:
                    ((TextSignal) output).State = _lastFlightData.DEDLines?[((Signal) output).Index ?? 0];
                    break;

                case F4SimOutputs.DED__INVERT_LINES:
                    ((TextSignal) output).State = _lastFlightData.Invert?[((Signal) output).Index ?? 0];
                    break;

                case F4SimOutputs.PFL__LINES:
                    ((TextSignal) output).State = _lastFlightData.PFLLines?[((Signal) output).Index ?? 0];
                    break;

                case F4SimOutputs.PFL__INVERT_LINES:
                    ((TextSignal) output).State = _lastFlightData.PFLInvert?[((Signal) output).Index ?? 0];
                    break;

                case F4SimOutputs.UFC__TACAN_CHANNEL:
                    SetOutput((AnalogSignal) output, _lastFlightData.UFCTChan);
                    break;

                case F4SimOutputs.UFC__TACAN_BAND_IS_X:
                    ((DigitalSignal) output).State = _lastFlightData.UfcTacanIsX;
                    break;

                case F4SimOutputs.UFC__TACAN_MODE_IS_AA:
                    ((DigitalSignal) output).State = _lastFlightData.UfcTacanIsAA;
                    break;

                case F4SimOutputs.AUX_COMM__TACAN_CHANNEL:
                    SetOutput((AnalogSignal) output, _lastFlightData.AUXTChan);
                    break;

                case F4SimOutputs.AUX_COMM__TACAN_BAND_IS_X:
                    ((DigitalSignal) output).State = _lastFlightData.AuxTacanIsX;
                    break;

                case F4SimOutputs.AUX_COMM__TACAN_MODE_IS_AA:
                    ((DigitalSignal) output).State = _lastFlightData.AuxTacanIsAA;
                    break;

                case F4SimOutputs.AUX_COMM__UHF_PRESET:
                    SetOutput((AnalogSignal) output, _lastFlightData.BupUhfPreset);
                    break;

                case F4SimOutputs.AUX_COMM__UHF_FREQUENCY:
                    SetOutput((AnalogSignal) output, _lastFlightData.BupUhfFreq);
                    break;

                case F4SimOutputs.IFF__BACKUP_MODE_1_DIGIT_1:
                    SetOutput((AnalogSignal)output, _lastFlightData.iffBackupMode1Digit1);
                    break;
                case F4SimOutputs.IFF__BACKUP_MODE_1_DIGIT_2:
                    SetOutput((AnalogSignal)output, _lastFlightData.iffBackupMode1Digit2);
                    break;
                case F4SimOutputs.IFF__BACKUP_MODE_3A_DIGIT_1:
                    SetOutput((AnalogSignal)output, _lastFlightData.iffBackupMode3ADigit1);
                    break;
                case F4SimOutputs.IFF__BACKUP_MODE_3A_DIGIT_2:
                    SetOutput((AnalogSignal)output, _lastFlightData.iffBackupMode3ADigit2);
                    break;

                case F4SimOutputs.FUEL_QTY__FOREWARD_QTY_LBS:
                    SetOutput((AnalogSignal) output, _lastFlightData.fwd / 10.0f);
                    break;

                case F4SimOutputs.FUEL_QTY__AFT_QTY_LBS:
                    SetOutput((AnalogSignal) output, _lastFlightData.aft / 10.0f);
                    break;

                case F4SimOutputs.FUEL_QTY__TOTAL_FUEL_LBS:
                    SetOutput((AnalogSignal) output, _lastFlightData.total);
                    break;

                case F4SimOutputs.LMFD__OSB_LABEL_LINES1:
                    ((TextSignal) output).State = _lastFlightData.leftMFD?[((Signal) output).Index ?? 0].Line1;
                    break;

                case F4SimOutputs.LMFD__OSB_LABEL_LINES2:
                    ((TextSignal) output).State = _lastFlightData.leftMFD?[((Signal) output).Index ?? 0].Line2;
                    break;

                case F4SimOutputs.LMFD__OSB_INVERTED_FLAGS:
                    ((DigitalSignal) output).State = _lastFlightData.leftMFD?[((Signal) output).Index ?? 0].Inverted ?? false;
                    break;

                case F4SimOutputs.RMFD__OSB_LABEL_LINES1:
                    ((TextSignal) output).State = _lastFlightData.rightMFD?[((Signal) output).Index ?? 0].Line1;
                    break;

                case F4SimOutputs.RMFD__OSB_LABEL_LINES2:
                    ((TextSignal) output).State = _lastFlightData.rightMFD?[((Signal) output).Index ?? 0].Line2;
                    break;

                case F4SimOutputs.RMFD__OSB_INVERTED_FLAGS:
                    ((DigitalSignal) output).State = _lastFlightData.rightMFD?[((Signal) output).Index ?? 0].Inverted ?? false;
                    break;

                case F4SimOutputs.MASTER_CAUTION_LIGHT:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.MasterCaution) == LightBits.MasterCaution;
                    break;

                case F4SimOutputs.MASTER_CAUTION_ANNOUNCED:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.MCAnnounced) == LightBits3.MCAnnounced;
                    break;

                case F4SimOutputs.LEFT_EYEBROW_LIGHTS__TFFAIL:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.TF) == LightBits.TF;
                    break;

                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__ENGFIRE:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.ENG_FIRE) == LightBits.ENG_FIRE;
                    break;

                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__ENGINE:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.ENGINE) == LightBits2.ENGINE;
                    break;

                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__HYDOIL:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.HYD) == LightBits.HYD;
                    break;

                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__FLCS:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.FLCS) == LightBits.FLCS;
                    break;

                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__CANOPY:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.CAN) == LightBits.CAN;
                    break;

                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__TO_LDG_CONFIG:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.T_L_CFG) == LightBits.T_L_CFG;
                    break;

                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__OXY_LOW:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.OXY_BROW) == LightBits.OXY_BROW
                                                     || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.OXY_BROW) == BlinkBits.OXY_BROW && DateTime.UtcNow.Millisecond % 500 < 250;

                    break;

                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__DBU_ON:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.DbuWarn) == LightBits3.DbuWarn;
                    break;

                case F4SimOutputs.CAUTION_PANEL__FLCS_FAULT:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.FltControlSys) == LightBits.FltControlSys;
                    break;

                case F4SimOutputs.CAUTION_PANEL__LE_FLAPS:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.LEFlaps) == LightBits.LEFlaps
                                                     || ((LightBits3) _lastFlightData.lightBits3 & LightBits3.Lef_Fault) == LightBits3.Lef_Fault;
                    break;

                case F4SimOutputs.CAUTION_PANEL__ELEC_SYS:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.Elec_Fault) == LightBits3.Elec_Fault
                                                     || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.Elec_Fault) == BlinkBits.Elec_Fault && DateTime.UtcNow.Millisecond % 250 < 125;
                    break;

                case F4SimOutputs.CAUTION_PANEL__ENGINE_FAULT:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.EngineFault) == LightBits.EngineFault;
                    break;

                case F4SimOutputs.CAUTION_PANEL__SEC:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.SEC) == LightBits2.SEC;
                    break;

                case F4SimOutputs.CAUTION_PANEL__FWD_FUEL_LOW:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.FwdFuelLow) == LightBits2.FwdFuelLow;
                    break;

                case F4SimOutputs.CAUTION_PANEL__AFT_FUEL_LOW:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.AftFuelLow) == LightBits2.AftFuelLow;
                    //TODO: what about standalone Fuel Low bit?
                    break;

                case F4SimOutputs.CAUTION_PANEL__OVERHEAT:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.Overheat) == LightBits.Overheat;
                    break;

                case F4SimOutputs.CAUTION_PANEL__BUC:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.BUC) == LightBits2.BUC;
                    break;

                case F4SimOutputs.CAUTION_PANEL__FUEL_OIL_HOT:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.FUEL_OIL_HOT) == LightBits2.FUEL_OIL_HOT;
                    break;

                case F4SimOutputs.CAUTION_PANEL__SEAT_NOT_ARMED:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.SEAT_ARM) == LightBits2.SEAT_ARM;
                    break;

                case F4SimOutputs.CAUTION_PANEL__AVIONICS_FAULT:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.Avionics) == LightBits.Avionics;
                    break;

                case F4SimOutputs.CAUTION_PANEL__RADAR_ALT:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.RadarAlt) == LightBits.RadarAlt;
                    break;

                case F4SimOutputs.CAUTION_PANEL__EQUIP_HOT:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.EQUIP_HOT) == LightBits.EQUIP_HOT;
                    break;

                case F4SimOutputs.CAUTION_PANEL__ECM:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.ECM) == LightBits.ECM;
                    break;

                case F4SimOutputs.CAUTION_PANEL__STORES_CONFIG:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.CONFIG) == LightBits.CONFIG;
                    break;

                case F4SimOutputs.CAUTION_PANEL__ANTI_SKID:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.ANTI_SKID) == LightBits2.ANTI_SKID;
                    break;

                case F4SimOutputs.CAUTION_PANEL__HOOK:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.Hook) == LightBits.Hook;
                    break;

                case F4SimOutputs.CAUTION_PANEL__NWS_FAIL:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.NWSFail) == LightBits.NWSFail;
                    break;

                case F4SimOutputs.CAUTION_PANEL__CABIN_PRESS:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.CabinPress) == LightBits.CabinPress;
                    break;

                case F4SimOutputs.CAUTION_PANEL__OXY_LOW:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.OXY_LOW) == LightBits2.OXY_LOW;
                    break;

                case F4SimOutputs.CAUTION_PANEL__PROBE_HEAT:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.PROBEHEAT) == LightBits2.PROBEHEAT
                                                     || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.PROBEHEAT) == BlinkBits.PROBEHEAT && DateTime.UtcNow.Millisecond % 250 < 125;
                    break;

                case F4SimOutputs.CAUTION_PANEL__FUEL_LOW:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.FuelLow) == LightBits.FuelLow;
                    break;

                case F4SimOutputs.CAUTION_PANEL__IFF:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.IFF) == LightBits.IFF;
                    break;

                case F4SimOutputs.CAUTION_PANEL__C_ADC:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.cadc) == LightBits3.cadc;
                    break;

                case F4SimOutputs.CAUTION_PANEL__ATF_NOT_ENGAGED:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.ATF_Not_Engaged) == LightBits3.ATF_Not_Engaged;
                    break;

                case F4SimOutputs.AOA_INDEXER__AOA_TOO_HIGH:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.AOAAbove) == LightBits.AOAAbove;
                    break;

                case F4SimOutputs.AOA_INDEXER__AOA_IDEAL:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.AOAOn) == LightBits.AOAOn;
                    break;

                case F4SimOutputs.AOA_INDEXER__AOA_TOO_LOW:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.AOABelow) == LightBits.AOABelow;
                    break;

                case F4SimOutputs.NWS_INDEXER__RDY:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.RefuelRDY) == LightBits.RefuelRDY;
                    break;

                case F4SimOutputs.NWS_INDEXER__AR_NWS:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.RefuelAR) == LightBits.RefuelAR;
                    break;

                case F4SimOutputs.NWS_INDEXER__DISC:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.RefuelDSC) == LightBits.RefuelDSC;
                    break;

                case F4SimOutputs.TWP__HANDOFF:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.HandOff) == LightBits2.HandOff;
                    break;

                case F4SimOutputs.TWP__MISSILE_LAUNCH:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.Launch) == LightBits2.Launch
                                                     || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.Launch) == BlinkBits.Launch && DateTime.UtcNow.Millisecond % 250 < 125;
                    break;

                case F4SimOutputs.TWP__PRIORITY_MODE:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.PriMode) == LightBits2.PriMode
                                                     || ((LightBits2) _lastFlightData.lightBits2 & LightBits2.PriMode) == LightBits2.PriMode
                                                     && ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.PriMode) == BlinkBits.PriMode
                                                     && DateTime.UtcNow.Millisecond % 250 < 125;
                    break;

                case F4SimOutputs.TWP__UNKNOWN:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.Unk) == LightBits2.Unk
                                                     || ((LightBits2) _lastFlightData.lightBits2 & LightBits2.Unk) == LightBits2.Unk
                                                     && ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.Unk) == BlinkBits.Unk
                                                     && DateTime.UtcNow.Millisecond % 250 < 125;
                    break;

                case F4SimOutputs.TWP__NAVAL:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.Naval) == LightBits2.Naval;
                    break;

                case F4SimOutputs.TWP__TARGET_SEP:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.TgtSep) == LightBits2.TgtSep;
                    break;

                case F4SimOutputs.TWP__SYS_TEST:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.SysTest) == LightBits3.SysTest;
                    break;

                case F4SimOutputs.TWA__SEARCH:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.AuxSrch) == LightBits2.AuxSrch
                                                     || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.AuxSrch) == BlinkBits.AuxSrch && DateTime.UtcNow.Millisecond % 250 < 125;
                    break;

                case F4SimOutputs.TWA__ACTIVITY_POWER:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.AuxAct) == LightBits2.AuxAct;
                    break;

                case F4SimOutputs.TWA__LOW_ALTITUDE:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.AuxLow) == LightBits2.AuxLow;
                    break;

                case F4SimOutputs.TWA__SYSTEM_POWER:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.AuxPwr) == LightBits2.AuxPwr;
                    break;

                case F4SimOutputs.ECM__POWER:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.EcmPwr) == LightBits2.EcmPwr;
                    break;

                case F4SimOutputs.ECM__FAIL:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.EcmFail) == LightBits2.EcmFail;
                    break;

                case F4SimOutputs.MISC__ADV_MODE_ACTIVE:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.TFR_ENGAGED) == LightBits2.TFR_ENGAGED;
                    break;

                case F4SimOutputs.MISC__ADV_MODE_STBY:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.TFR_STBY) == LightBits.TFR_STBY;
                    break;

                case F4SimOutputs.MISC__AUTOPILOT_ENGAGED:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.AutoPilotOn) == LightBits.AutoPilotOn;
                    break;

                case F4SimOutputs.CMDS__CHAFF_COUNT:
                    SetOutput((AnalogSignal) output, _lastFlightData.ChaffCount);
                    break;

                case F4SimOutputs.CMDS__FLARE_COUNT:
                    SetOutput((AnalogSignal) output, _lastFlightData.FlareCount);
                    break;

                case F4SimOutputs.CMDS__GO:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.Go) == LightBits2.Go;
                    break;

                case F4SimOutputs.CMDS__NOGO:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.NoGo) == LightBits2.NoGo;
                    break;

                case F4SimOutputs.CMDS__AUTO_DEGR:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.Degr) == LightBits2.Degr;
                    break;

                case F4SimOutputs.CMDS__DISPENSE_RDY:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.Rdy) == LightBits2.Rdy;
                    break;

                case F4SimOutputs.CMDS__CHAFF_LO:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.ChaffLo) == LightBits2.ChaffLo;
                    break;

                case F4SimOutputs.CMDS__FLARE_LO:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.FlareLo) == LightBits2.FlareLo;
                    break;

                case F4SimOutputs.CMDS__MODE:
                    SetOutput((AnalogSignal) output, _lastFlightData.cmdsMode);
                    break;

                case F4SimOutputs.ELEC__FLCS_PMG:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.FlcsPmg) == LightBits3.FlcsPmg;
                    break;

                case F4SimOutputs.ELEC__MAIN_GEN:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.MainGen) == LightBits3.MainGen;
                    break;

                case F4SimOutputs.ELEC__STBY_GEN:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.StbyGen) == LightBits3.StbyGen;
                    break;

                case F4SimOutputs.ELEC__EPU_GEN:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.EpuGen) == LightBits3.EpuGen;
                    break;

                case F4SimOutputs.ELEC__EPU_PMG:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.EpuPmg) == LightBits3.EpuPmg;
                    break;

                case F4SimOutputs.ELEC__TO_FLCS:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.ToFlcs) == LightBits3.ToFlcs;
                    break;

                case F4SimOutputs.ELEC__FLCS_RLY:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.FlcsRly) == LightBits3.FlcsRly;
                    break;

                case F4SimOutputs.ELEC__BATT_FAIL:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.BatFail) == LightBits3.BatFail;
                    break;

                case F4SimOutputs.TEST__ABCD:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.Flcs_ABCD) == LightBits.Flcs_ABCD;
                    break;

                case F4SimOutputs.EPU__HYDRAZN:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.Hydrazine) == LightBits3.Hydrazine;
                    break;

                case F4SimOutputs.EPU__AIR:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.Air) == LightBits3.Air;
                    break;

                case F4SimOutputs.EPU__RUN:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.EPUOn) == LightBits2.EPUOn
                                                     || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.EPUOn) == BlinkBits.EPUOn && DateTime.UtcNow.Millisecond % 250 < 125;
                    break;

                case F4SimOutputs.AVTR__AVTR:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.AVTR) == HsiBits.AVTR;
                    break;

                case F4SimOutputs.JFS__RUN:
                    ((DigitalSignal) output).State = ((LightBits2) _lastFlightData.lightBits2 & LightBits2.JFSOn) == LightBits2.JFSOn
                                                     || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.JFSOn_Slow) == BlinkBits.JFSOn_Slow && DateTime.UtcNow.Millisecond % 1000 < 500
                                                     || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.JFSOn_Fast) == BlinkBits.JFSOn_Fast && DateTime.UtcNow.Millisecond % 250 < 125;
                    break;

                case F4SimOutputs.MARKER_BEACON__MRK_BCN_LIGHT:
                    if (((HsiBits) _lastFlightData.hsiBits & HsiBits.OuterMarker) == HsiBits.OuterMarker) { _morseCodeGenerator.PlainText = "T"; }
                    else if (((HsiBits) _lastFlightData.hsiBits & HsiBits.MiddleMarker) == HsiBits.MiddleMarker) _morseCodeGenerator.PlainText = "A";

                    ((DigitalSignal) output).State =
                        (((BlinkBits) _lastFlightData.blinkBits & BlinkBits.OuterMarker) == BlinkBits.OuterMarker
                         || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.MiddleMarker) == BlinkBits.MiddleMarker)
                        && _markerBeaconMorseCodeState
                        || ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.OuterMarker) != BlinkBits.OuterMarker
                        && ((BlinkBits) _lastFlightData.blinkBits & BlinkBits.MiddleMarker) != BlinkBits.MiddleMarker
                        && (((HsiBits) _lastFlightData.hsiBits & HsiBits.OuterMarker) == HsiBits.OuterMarker || ((HsiBits) _lastFlightData.hsiBits & HsiBits.MiddleMarker) == HsiBits.MiddleMarker);
                    break;

                case F4SimOutputs.MARKER_BEACON__OUTER_MARKER_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.OuterMarker) == HsiBits.OuterMarker;

                    break;

                case F4SimOutputs.MARKER_BEACON__MIDDLE_MARKER_FLAG:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.MiddleMarker) == HsiBits.MiddleMarker;
                    break;

                case F4SimOutputs.AIRCRAFT__ONGROUND:
                    ((DigitalSignal) output).State = ((LightBits) _lastFlightData.lightBits & LightBits.ONGROUND) == LightBits.ONGROUND
                                                     || ((LightBits3) _lastFlightData.lightBits3 & LightBits3.OnGround) == LightBits3.OnGround;
                    break;

                case F4SimOutputs.AIRCRAFT__MAIN_LANDING_GEAR__WEIGHT_ON_WHEELS:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.MLGWOW) == LightBits3.MLGWOW;
                    break;

                case F4SimOutputs.AIRCRAFT__NOSE_LANDING_GEAR__WEIGHT_ON_WHEELS:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.NLGWOW) == LightBits3.NLGWOW;
                    break;

                case F4SimOutputs.AIRCRAFT__LEADING_EDGE_FLAPS_POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.lefPos);
                    break;

                case F4SimOutputs.AIRCRAFT__TRAILING_EDGE_FLAPS_POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.tefPos);
                    break;

                case F4SimOutputs.AIRCRAFT__VTOL_POSITION:
                    SetOutput((AnalogSignal) output, _lastFlightData.vtolPos);
                    break;

                case F4SimOutputs.POWER__ELEC_POWER_OFF:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.Power_Off) == LightBits3.Power_Off || !IsSimRunning;
                    break;

                case F4SimOutputs.POWER__MAIN_POWER:
                    SetOutput((AnalogSignal) output, _lastFlightData.MainPower);
                    break;

                case F4SimOutputs.POWER__BUS_POWER_BATTERY:
                    ((DigitalSignal) output).State = ((PowerBits) _lastFlightData.powerBits & PowerBits.BusPowerBattery) == PowerBits.BusPowerBattery;
                    break;

                case F4SimOutputs.POWER__BUS_POWER_EMERGENCY:
                    ((DigitalSignal) output).State = ((PowerBits) _lastFlightData.powerBits & PowerBits.BusPowerEmergency) == PowerBits.BusPowerEmergency;
                    break;

                case F4SimOutputs.POWER__BUS_POWER_ESSENTIAL:
                    ((DigitalSignal) output).State = ((PowerBits) _lastFlightData.powerBits & PowerBits.BusPowerEssential) == PowerBits.BusPowerEssential;
                    break;

                case F4SimOutputs.POWER__BUS_POWER_NON_ESSENTIAL:
                    ((DigitalSignal) output).State = ((PowerBits) _lastFlightData.powerBits & PowerBits.BusPowerNonEssential) == PowerBits.BusPowerNonEssential;
                    break;

                case F4SimOutputs.POWER__MAIN_GENERATOR:
                    ((DigitalSignal) output).State = ((PowerBits) _lastFlightData.powerBits & PowerBits.MainGenerator) == PowerBits.MainGenerator;
                    break;

                case F4SimOutputs.POWER__STANDBY_GENERATOR:
                    ((DigitalSignal) output).State = ((PowerBits) _lastFlightData.powerBits & PowerBits.StandbyGenerator) == PowerBits.StandbyGenerator;
                    break;

                case F4SimOutputs.POWER__JET_FUEL_STARTER:
                    ((DigitalSignal) output).State = ((PowerBits) _lastFlightData.powerBits & PowerBits.JetFuelStarter) == PowerBits.JetFuelStarter;
                    break;



                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__ALL_WORDS:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Allwords) == BettyBits.Betty_Allwords;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__PULL_UP:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Pullup) == BettyBits.Betty_Pullup;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__ALTITUDE:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Altitude) == BettyBits.Betty_Altitude;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__WARNING:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Warning) == BettyBits.Betty_Warning;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__JAMMER:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Jammer) == BettyBits.Betty_Jammer;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__COUNTER:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Counter) == BettyBits.Betty_Counter;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__CHAFFFLARE:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_ChaffFlare) == BettyBits.Betty_ChaffFlare;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__CHAFFFLARE_LOW:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_ChaffFlare_Low) == BettyBits.Betty_ChaffFlare_Low;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__CHAFFFLARE_OUT:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_ChaffFlare_Out) == BettyBits.Betty_ChaffFlare_Out;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__LOCK:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Lock) == BettyBits.Betty_Lock;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__CAUTION:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Caution) == BettyBits.Betty_Caution;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__DATA:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Data) == BettyBits.Betty_Data;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__BINGO:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Bingo) == BettyBits.Betty_Bingo;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__IFF:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_IFF) == BettyBits.Betty_IFF;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__LOWSPEED:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Lowspeed) == BettyBits.Betty_Lowspeed;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__BEEPS:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_Beeps) == BettyBits.Betty_Beeps;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__AOA:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_AOA) == BettyBits.Betty_AOA;
                    break;

                case F4SimOutputs.VOICE_MESSAGE_SYSTEM__MAXG:
                    ((DigitalSignal)output).State = ((BettyBits)_lastFlightData.bettyBits & BettyBits.Betty_MaxG) == BettyBits.Betty_MaxG;
                    break;

                case F4SimOutputs.ICP__BINGO_FUEL_LEVEL:
                    SetOutput((AnalogSignal)output, _lastFlightData.bingoFuel);
                    break;

                case F4SimOutputs.ICP__CARA_ALOW:
                    SetOutput((AnalogSignal)output, _lastFlightData.caraAlow);
                    break;


                case F4SimOutputs.SIM__BMS_PLAYER_IS_FLYING:
                    ((DigitalSignal) output).State = ((HsiBits) _lastFlightData.hsiBits & HsiBits.Flying) == HsiBits.Flying;
                    break;

                case F4SimOutputs.SIM__FLIGHTDATA_VERSION_NUM:
                    SetOutput((AnalogSignal) output, _lastFlightData.VersionNum);
                    break;

                case F4SimOutputs.SIM__FLIGHTDATA2_VERSION_NUM:
                    SetOutput((AnalogSignal) output, _lastFlightData.VersionNum2);
                    break;

                case F4SimOutputs.SIM__CURRENT_TIME:
                    SetOutput((AnalogSignal) output, _lastFlightData.currentTime);
                    break;

                case F4SimOutputs.AIRCRAFT__VEHICLE_ACD:
                    SetOutput((AnalogSignal) output, _lastFlightData.vehicleACD);
                    break;

                case F4SimOutputs.SIM__PILOT_CALLSIGN:
                    ((TextSignal) output).State = (((Signal) output).Index ?? 0) <= _lastFlightData.pilotsOnline
                        ? (_lastFlightData.pilotsCallsign?[((Signal) output).Index ?? 0] ?? string.Empty).TrimAtNull()
                        : string.Empty;
                    break;

                case F4SimOutputs.SIM__PILOT_STATUS:
                {
                    var status = (((Signal) output).Index ?? 0) <= _lastFlightData.pilotsOnline ? _lastFlightData.pilotsStatus?[((Signal) output).Index ?? 0] : 0;
                    SetOutput((AnalogSignal) output, status ?? 0);
                }
                    break;

                case F4SimOutputs.SIM__AA_MISSILE_FIRED:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.AAMissileFired);
                    break;

                case F4SimOutputs.SIM__AG_MISSILE_FIRED:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.AGMissileFired);
                    break;

                case F4SimOutputs.SIM__BOMB_DROPPED:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.BombDropped);
                    break;

                case F4SimOutputs.SIM__FLARE_DROPPED:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.FlareDropped);
                    break;

                case F4SimOutputs.SIM__CHAFF_DROPPED:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.ChaffDropped);
                    break;

                case F4SimOutputs.SIM__BULLETS_FIRED:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.BulletsFired);
                    break;

                case F4SimOutputs.SIM__COLLISION_COUNTER:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.CollisionCounter);
                    break;

                case F4SimOutputs.SIM__GFORCE:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.Gforce);
                    break;

                case F4SimOutputs.SIM__LAST_DAMAGE:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.lastdamage);
                    break;

                case F4SimOutputs.SIM__DAMAGE_FORCE:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.damageforce);
                    break;

                case F4SimOutputs.SIM__WHEN_DAMAGE:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.whendamage);
                    break;

                case F4SimOutputs.SIM__EYE_X:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.eyex);
                    break;

                case F4SimOutputs.SIM__EYE_Y:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.eyey);
                    break;

                case F4SimOutputs.SIM__EYE_Z:
                    SetOutput((AnalogSignal) output, _lastFlightData.IntellivibeData.eyez);
                    break;

                case F4SimOutputs.SIM__IS_FIRING_GUN:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.IsFiringGun;
                    break;

                case F4SimOutputs.SIM__IS_END_FLIGHT:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.IsEndFlight;
                    break;

                case F4SimOutputs.SIM__IS_EJECTING:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.IsEjecting;
                    break;

                case F4SimOutputs.SIM__IN_3D:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.In3D;
                    break;

                case F4SimOutputs.SIM__IS_PAUSED:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.IsPaused;
                    break;

                case F4SimOutputs.SIM__IS_FROZEN:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.IsFrozen;
                    break;

                case F4SimOutputs.SIM__IS_OVER_G:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.IsOverG;
                    break;

                case F4SimOutputs.SIM__IS_ON_GROUND:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.IsOnGround;
                    break;

                case F4SimOutputs.SIM__IS_EXIT_GAME:
                    ((DigitalSignal) output).State = _lastFlightData.IntellivibeData.IsExitGame;
                    break;

                case F4SimOutputs.SIM__BUMP_INTENSITY:
                    SetOutput((AnalogSignal)output, _lastFlightData.bumpIntensity);
                    break;

                case F4SimOutputs.INSTR_LIGHTING_INTENSITY:
                    SetOutput((AnalogSignal)output, _lastFlightData.instrLight);
                    break;


                case F4SimOutputs.RADIO_CLIENT_STATUS__CLIENT_ACTIVE_FLAG:
                    ((DigitalSignal) output).State = ((ClientFlags) _lastFlightData.RadioClientStatus.ClientFlags & ClientFlags.ClientActive) == ClientFlags.ClientActive;
                    break;

                case F4SimOutputs.RADIO_CLIENT_STATUS__CONNECTION_FAIL_FLAG:
                    ((DigitalSignal) output).State = ((ClientFlags) _lastFlightData.RadioClientStatus.ClientFlags & ClientFlags.ConnectionFail) == ClientFlags.ConnectionFail;
                    break;

                case F4SimOutputs.RADIO_CLIENT_STATUS__BAD_PASSWORD_FLAG:
                    ((DigitalSignal) output).State = ((ClientFlags) _lastFlightData.RadioClientStatus.ClientFlags & ClientFlags.BadPassword) == ClientFlags.BadPassword;
                    break;

                case F4SimOutputs.RADIO_CLIENT_STATUS__NO_SPEAKERS_FLAG:
                    ((DigitalSignal) output).State = ((ClientFlags) _lastFlightData.RadioClientStatus.ClientFlags & ClientFlags.NoSpeakers) == ClientFlags.NoSpeakers;
                    break;

                case F4SimOutputs.RADIO_CLIENT_STATUS__CONNECTED_FLAG:
                    ((DigitalSignal) output).State = ((ClientFlags) _lastFlightData.RadioClientStatus.ClientFlags & ClientFlags.Connected) == ClientFlags.Connected;
                    break;

                case F4SimOutputs.RADIO_CLIENT_STATUS__HOST_UNKNOWN_FLAG:
                    ((DigitalSignal) output).State = ((ClientFlags) _lastFlightData.RadioClientStatus.ClientFlags & ClientFlags.HostUnknown) == ClientFlags.HostUnknown;
                    break;

                case F4SimOutputs.RADIO_CLIENT_STATUS__NO_MICROPHONE_FLAG:
                    ((DigitalSignal) output).State = ((ClientFlags) _lastFlightData.RadioClientStatus.ClientFlags & ClientFlags.NoMicrophone) == ClientFlags.NoMicrophone;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__UHF_RADIO__FREQUENCY:
                    SetOutput((AnalogSignal) output, _lastFlightData.RadioClientControlData.Radios?[(int) Radios.UHF].Frequency ?? double.NaN);
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__UHF_RADIO__RX_VOLUME:
                    SetOutput((AnalogSignal) output, _lastFlightData.RadioClientControlData.Radios?[(int) Radios.UHF].RxVolume ?? double.NaN);
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__UHF_RADIO__IS_ON_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.Radios != null && _lastFlightData.RadioClientControlData.Radios[(int) Radios.UHF].IsOn;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__UHF_RADIO__PTT_DEPRESSED_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.Radios?[(int) Radios.UHF].PttDepressed ?? false;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__VHF_RADIO__FREQUENCY:
                    SetOutput((AnalogSignal) output, _lastFlightData.RadioClientControlData.Radios?[(int) Radios.VHF].Frequency ?? double.NaN);
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__VHF_RADIO__RX_VOLUME: break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__VHF_RADIO__IS_ON_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.Radios?[(int) Radios.VHF].IsOn ?? false;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__VHF_RADIO__PTT_DEPRESSED_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.Radios?[(int) Radios.VHF].PttDepressed ?? false;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__GUARD_RADIO__FREQUENCY:
                    SetOutput((AnalogSignal) output, _lastFlightData.RadioClientControlData.Radios?[(int) Radios.GUARD].Frequency ?? double.NaN);
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__GUARD_RADIO__RX_VOLUME:
                    SetOutput((AnalogSignal) output, _lastFlightData.RadioClientControlData.Radios?[(int) Radios.GUARD].RxVolume ?? double.NaN);
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__GUARD_RADIO__IS_ON_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.Radios?[(int) Radios.GUARD].IsOn ?? false;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__GUARD_RADIO__PTT_DEPRESSED_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.Radios?[(int) Radios.GUARD].PttDepressed ?? false;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__NICKNAME:
                    ((TextSignal) output).State = _lastFlightData.RadioClientControlData.Nickname != null
                        ? Encoding.Default.GetString(_lastFlightData.RadioClientControlData.Nickname).TrimAtNull()
                        : string.Empty;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__ADDRESS:
                    ((TextSignal) output).State = _lastFlightData.RadioClientControlData.Address != null
                        ? Encoding.Default.GetString(_lastFlightData.RadioClientControlData.Address).TrimAtNull()
                        : string.Empty;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__PORT_NUMBER:
                    SetOutput((AnalogSignal) output, _lastFlightData.RadioClientControlData.PortNumber);
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__PASSWORD:
                    ((TextSignal) output).State = _lastFlightData.RadioClientControlData.Nickname != null
                        ? Encoding.Default.GetString(_lastFlightData.RadioClientControlData.Password).TrimAtNull()
                        : string.Empty;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__PLAYER_COUNT:
                    SetOutput((AnalogSignal) output, _lastFlightData.RadioClientControlData.PlayerCount);
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__SIGNAL_CONNECT_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.SignalConnect;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__TERMINATE_CLIENT_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.TerminateClient;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__FLIGHT_MODE_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.FlightMode;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__USE_AGC_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.RadioClientControlData.UseAGC;
                    break;

                case F4SimOutputs.RADIO_CLIENT_CONTROL__MAIN_DEVICE__IC_VOLUME:
                    SetOutput((AnalogSignal) output, _lastFlightData.RadioClientControlData.Devices?[(int) Devices.MAIN].IcVolume ?? double.NaN);
                    break;

                case F4SimOutputs.PILOT__HEADX_OFFSET:
                    SetOutput((AnalogSignal) output, _lastFlightData.headX);
                    break;

                case F4SimOutputs.PILOT__HEADY_OFFSET:
                    SetOutput((AnalogSignal) output, _lastFlightData.headY);
                    break;

                case F4SimOutputs.PILOT__HEADZ_OFFSET:
                    SetOutput((AnalogSignal) output, _lastFlightData.headZ);
                    break;

                case F4SimOutputs.PILOT__HEAD_PITCH_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.headPitch);
                    break;

                case F4SimOutputs.PILOT__HEAD_ROLL_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.headRoll);
                    break;

                case F4SimOutputs.PILOT__HEAD_YAW_DEGREES:
                    SetOutput((AnalogSignal) output, _lastFlightData.headYaw);
                    break;

                case F4SimOutputs.FLIGHT_CONTROL__RUN:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.FlcsBitRun) == LightBits3.FlcsBitRun;
                    break;

                case F4SimOutputs.FLIGHT_CONTROL__FAIL:
                    ((DigitalSignal) output).State = ((LightBits3) _lastFlightData.lightBits3 & LightBits3.FlcsBitFail) == LightBits3.FlcsBitFail;
                    break;

                case F4SimOutputs.RWR__OBJECT_COUNT:
                    SetOutput((AnalogSignal) output, _lastFlightData.RwrObjectCount);
                    break;

                case F4SimOutputs.RWR__SYMBOL_ID:
                    SetOutput((AnalogSignal) output, _lastFlightData.RWRsymbol?.Length > ((Signal) output).Index ? _lastFlightData.RWRsymbol[((Signal) output).Index ?? 0] : -1);
                    break;

                case F4SimOutputs.RWR__BEARING_DEGREES:
                    SetOutput(
                        (AnalogSignal) output, _lastFlightData.bearing?.Length > ((Signal) output).Index ? (double) _lastFlightData.bearing?[((Signal) output).Index ?? 0] * DEGREES_PER_RADIAN : 0);
                    break;

                case F4SimOutputs.RWR__MISSILE_ACTIVITY_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.missileActivity?.Length > ((Signal) output).Index && _lastFlightData.missileActivity?[((Signal) output).Index ?? 0] !=0;
                    break;

                case F4SimOutputs.RWR__MISSILE_LAUNCH_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.missileLaunch?.Length > ((Signal) output).Index && _lastFlightData.missileLaunch?[((Signal) output).Index ?? 0] !=0;
                    break;

                case F4SimOutputs.RWR__SELECTED_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.selected?.Length > ((Signal) output).Index && _lastFlightData.selected?[((Signal) output).Index ?? 0] !=0;
                    break;

                case F4SimOutputs.RWR__LETHALITY:
                    SetOutput((AnalogSignal) output, _lastFlightData.lethality?.Length > ((Signal) output).Index ? (float) _lastFlightData.lethality?[((Signal) output).Index ?? 0] : -1);
                    break;

                case F4SimOutputs.RWR__NEWDETECTION_FLAG:
                    ((DigitalSignal) output).State = _lastFlightData.newDetection?.Length > ((Signal) output).Index && _lastFlightData.newDetection?[((Signal) output).Index ?? 0] !=0;
                    break;

                case F4SimOutputs.RWR__ADDITIONAL_INFO:
                    ((TextSignal) output).State = _lastFlightData.RwrInfo != null ? Encoding.Default.GetString(_lastFlightData.RwrInfo) : null;
                    break;
                case F4SimOutputs.LEFT_EYEBROW_LIGHTS__ALTLOW: break;
                case F4SimOutputs.LEFT_EYEBROW_LIGHTS__OBSWRN: break;
                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__DUALFC: break;
                case F4SimOutputs.POWER__BUS_POWER_MAIN_GENERATOR: break;
                case F4SimOutputs.RIGHT_EYEBROW_LIGHTS__ENGINE_2_FIRE: break;

                case F4SimOutputs.SIM__BMS_EXE:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x=>x.strId == (uint)StringIdentifier.BmsExe).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_BASE_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsBasedir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_BIN_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsBinDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_UI_ART_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsUIArtDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_USER_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsUserDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_ACMI_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsAcmiDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_BRIEFINGS_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsBriefingsDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_LOGS_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsLogsDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_CONFIG_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsConfigDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_PATCH_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsPatchDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_PICTURE_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.BmsPictureDirectory).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_3DBUTTONS_FILE:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ButtonsFile).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.SIM__BMS_3DCOCKPIT_FILE:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.CockpitFile).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__THEATER_NAME:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrName).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__CAMPAIGN_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrCampaigndir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__ART_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrArtdir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__MOVIE_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrMoviedir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__UI_SOUND_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrUisounddir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__OBJECT_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrObjectdir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__3D_DATA_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.Thr3ddatadir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__MISC_TEX_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrMisctexdir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__SOUND_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrSounddir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__TAC_REF_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrTacrefdir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__SPLASH_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrSplashdir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__COCKPIT_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrCockpitdir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__SIM_DATA_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrSimdatadir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__SUBTITLES_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrSubtitlesdir).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.THEATER__TAC_REF_PICS_DIRECTORY:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.ThrTacrefpicsdir).FirstOrDefault().value ?? string.Empty;
                    break;

                case F4SimOutputs.SIM_KEYFILE_PATH:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.KeyFile).FirstOrDefault().value ?? string.Empty;
                    break;

                case F4SimOutputs.AIRCRAFT_NAME:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.AcName).FirstOrDefault().value ?? string.Empty;
                    break;
                case F4SimOutputs.AIRCRAFT_NCTR_DESIGNATION:
                    ((TextSignal)output).State = _lastFlightData.StringData?.data?.Where(x => x.strId == (uint)StringIdentifier.AcNCTR).FirstOrDefault().value ?? string.Empty;
                    break;

                case F4SimOutputs.SIM__BMS_VERSION:
                    ((TextSignal)output).State = $"{_lastFlightData.BMSVersionMajor}.{_lastFlightData.BMSVersionMinor}.{_lastFlightData.BMSVersionMicro}.{_lastFlightData.BMSBuildNumber}";
                        break;

                case F4SimOutputs.SIM__STRING_AREA_SIZE:
                    SetOutput((AnalogSignal)output, _lastFlightData.StringAreaSize);
                    break;

                case F4SimOutputs.SIM__STRING_DATA_VERSION_NUM:
                    SetOutput((AnalogSignal)output, _lastFlightData.StringData?.VersionNum ?? float.NaN);
                    break;

                case F4SimOutputs.SIM__DRAWING_AREA_SIZE:
                    SetOutput((AnalogSignal)output, _lastFlightData.DrawingAreaSize);
                    break;

                case F4SimOutputs.SIM__DRAWING_DATA_VERSION_NUM:
                    SetOutput((AnalogSignal)output, _lastFlightData.DrawingData?.VersionNum ?? float.NaN);
                    break;


                case F4SimOutputs.HUD__DRAWING_COMMANDS_LENGTH:
                    SetOutput((AnalogSignal)output, _lastFlightData.DrawingData?.HUD_length ?? float.NaN);
                    break;

                case F4SimOutputs.HUD__DRAWING_COMMANDS:
                    ((TextSignal)output).State = _lastFlightData.DrawingData?.HUD_commands ?? string.Empty;
                    break;

                case F4SimOutputs.RWR__DRAWING_COMMANDS_LENGTH:
                    SetOutput((AnalogSignal)output, _lastFlightData.DrawingData?.RWR_length ?? float.NaN);
                    break;

                case F4SimOutputs.RWR__DRAWING_COMMANDS:
                    ((TextSignal)output).State = _lastFlightData.DrawingData?.RWR_commands ?? string.Empty;
                    break;

                case F4SimOutputs.HMS__DRAWING_COMMANDS_LENGTH:
                    SetOutput((AnalogSignal)output, _lastFlightData.DrawingData?.HMS_length ?? float.NaN);
                    break;

                case F4SimOutputs.HMS__DRAWING_COMMANDS:
                    ((TextSignal)output).State = _lastFlightData.DrawingData?.HMS_commands ?? string.Empty;
                    break;

            }
        }

        private SimCommand CreateNewF4SimCommand(string collectionName, string subcollectionName, string signalFriendlyName, Callbacks callback)
        {
            var simCommand = new SendCallbackCommand();
            var falconInput = simCommand.In;
            falconInput.Category = "Sim Inputs (Callbacks)";
            falconInput.CollectionName = collectionName;
            falconInput.SubcollectionName = subcollectionName;
            falconInput.FriendlyName = signalFriendlyName;
            falconInput.Id = "F4_CALLBACK__" + Enum.GetName(typeof(Callbacks), callback)?.ToUpperInvariant();
            falconInput.PublisherObject = this;
            falconInput.Source = this;
            falconInput.SourceFriendlyName = "Falcon BMS";
            falconInput.SubSource = callback;
            simCommand.Id = "F4_SEND_CALLBACK" + simCommand.In.Id;
            simCommand.FriendlyName = simCommand.In.FriendlyName;
            return simCommand;
        }

        private ISimOutput CreateNewF4SimOutput(
            string collectionName, string subcollectionName, string signalFriendlyName, F4SimOutputs simOutput, int? index, Type dataType, double minVal = 0.0000001, double maxVal = 0.0000001,
            bool isAngle = false, bool isPercentage = false, double? timeConstant = null)
        {
            var indexString = index >= 0 ? "[" + index + "]" : string.Empty;

            if (dataType.IsAssignableFrom(typeof(string)))
            {
                return new TextSimOutput
                {
                    Category = "Sim Outputs",
                    CollectionName = collectionName,
                    SubcollectionName = subcollectionName,
                    FriendlyName = signalFriendlyName,
                    Id = "F4_" + Enum.GetName(typeof(F4SimOutputs), simOutput) + indexString,
                    Index = index,
                    PublisherObject = this,
                    Source = this,
                    SourceFriendlyName = "Falcon BMS",
                    SubSource = simOutput
                };
            }
            if (dataType.IsAssignableFrom(typeof(bool)))
            {
                return new DigitalSimOutput
                {
                    Category = "Sim Outputs",
                    CollectionName = collectionName,
                    SubcollectionName = subcollectionName,
                    FriendlyName = signalFriendlyName,
                    Id = "F4_" + Enum.GetName(typeof(F4SimOutputs), simOutput) + indexString,
                    Index = index,
                    PublisherObject = this,
                    Source = this,
                    SourceFriendlyName = "Falcon BMS",
                    SubSource = simOutput
                };
            }

            return new AnalogSimOutput
            {
                Category = "Sim Outputs",
                CollectionName = collectionName,
                SubcollectionName = subcollectionName,
                FriendlyName = signalFriendlyName,
                Id = "F4_" + Enum.GetName(typeof(F4SimOutputs), simOutput) + indexString,
                Index = index,
                PublisherObject = this,
                Source = this,
                SourceFriendlyName = "Falcon BMS",
                SubSource = simOutput,
                IsAngle = isAngle,
                IsPercentage = isPercentage,
                MinValue = minVal,
                MaxValue = maxVal,
                TimeConstant = timeConstant,
                Precision = dataType == typeof(long) || dataType == typeof(int) || dataType == typeof(short) || dataType == typeof(byte) ? 0 : -1
            };
        }

        private static void SetOutput(AnalogSignal signal, double newVal)
        {
            var currentState = signal.TimestampedState;
            var delta = signal.IsAngle && signal.MinValue == -180 && signal.MaxValue == 180
                ? Common.Math.Util.AngleDelta((float) (currentState.Value + 180), (float) (newVal + 180))
                : newVal - currentState.Value;

            if (double.IsNaN(currentState.Value) || double.IsInfinity(currentState.Value)) currentState.Value = 0;

            var originalDelta = delta;
            if (signal.TimeConstant.HasValue)
            {
                var time = DateTime.UtcNow.Subtract(currentState.Timestamp).TotalMilliseconds;
                delta= (1.0d - 1.0d / Math.Pow(Math.E, time / signal.TimeConstant.Value)) * originalDelta;
            }

            double outputVal;
            double correlatedOutputVal;
            if (signal.IsAngle && signal.MinValue == -180 && signal.MaxValue == 180)
            {
                outputVal = currentState.Value + delta;
                if (outputVal < -180) { outputVal += 360; }
                else if (outputVal > 180) outputVal -= 360;

                correlatedOutputVal = currentState.Value + originalDelta;
                if (correlatedOutputVal < -180) { correlatedOutputVal += 360; }
                else if (correlatedOutputVal > 180) correlatedOutputVal -= 360;
            }
            else
            {
                outputVal = currentState.Value + delta;
                correlatedOutputVal = currentState.Value + originalDelta;
            }
            signal.CorrelatedState = correlatedOutputVal;
            signal.State = outputVal;
        }

        private ISimOutput CreateNewF4SimOutput(
            string collectionName, string signalFriendlyName, F4SimOutputs simOutputEnumVal, Type dataType, double minVal = 0.0000001, double maxVal = 0.0000001, bool isAngle = false,
            bool isPercentage = false, double? timeConstant = null)
        {
            return CreateNewF4SimOutput(collectionName, null, signalFriendlyName, simOutputEnumVal, null, dataType, minVal, maxVal, isAngle, isPercentage, timeConstant);
        }

        private void AddF4SimOutput(ISimOutput output) { _simOutputs.Add(output.Id, output); }

        private void AddF4SimCommand(SimCommand simCommand) { _simCommands.Add(simCommand.Id, simCommand); }

        private void CreateSimCommandsList()
        {
            _simCommands.Clear();
            foreach (Callbacks callback in Enum.GetValues(typeof(Callbacks)))
            {
                var category = EnumAttributeReader.GetAttribute<CategoryAttribute>(callback).Category;
                if (string.Compare(category, "NOOP", StringComparison.OrdinalIgnoreCase) > -1) continue;

                var subCategory = EnumAttributeReader.GetAttribute<SubCategoryAttribute>(callback).SubCategory;
                var description = EnumAttributeReader.GetAttribute<DescriptionAttribute>(callback).Description;
                var simCommand = CreateNewF4SimCommand(category, subCategory, description, callback);
                AddF4SimCommand(simCommand);
            }
        }

        private void CreateSimOutputsList()
        {
            _simOutputs.Clear();

            AddF4SimOutput(CreateNewF4SimOutput("Map", "Ownship Position", "Feet North of Map Origin", F4SimOutputs.MAP__OWNSHIP_POSITION__FEET_NORTH_OF_MAP_ORIGIN, null, typeof(float), float.MinValue, float.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Map", "Ownship Position", "Feet East of Map Origin", F4SimOutputs.MAP__OWNSHIP_POSITION__FEET_EAST_OF_MAP_ORIGIN, null, typeof(float), float.MinValue, float.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Map", "Ownship Position", "Latitude", F4SimOutputs.MAP__OWNSHIP_POSITION__LATITUDE, null, typeof(float), -90, 90));
            AddF4SimOutput(CreateNewF4SimOutput("Map", "Ownship Position", "Longitude", F4SimOutputs.MAP__OWNSHIP_POSITION__LONGITUDE, null, typeof(float), -180, 180));
            AddF4SimOutput(CreateNewF4SimOutput("Map", "Ground Speed", "Speed Vector - North Component (feet/sec)", F4SimOutputs.MAP__GROUND_SPEED_VECTOR__NORTH_COMPONENT_FPS, null, typeof(float), -1450, 1450));
            AddF4SimOutput(CreateNewF4SimOutput("Map", "Ground Speed", "Speed Vector - East Component (feet/sec)", F4SimOutputs.MAP__GROUND_SPEED_VECTOR__EAST_COMPONENT_FPS, null, typeof(float), -1450, 1450));
            AddF4SimOutput(CreateNewF4SimOutput("Map", "Ground Speed", "Ground Speed (knots)", F4SimOutputs.MAP__GROUND_SPEED_KNOTS, null, typeof(float), 0, 1000));

            AddF4SimOutput(CreateNewF4SimOutput("Map", "Airbase Position", "Feet North of Map Origin", F4SimOutputs.MAP__AIRBASE_FEET_NORTH_OF_MAP_ORIGIN, null, typeof(float), float.MinValue, float.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Map", "Airbase Position", "Feet East of Map Origin", F4SimOutputs.MAP__AIRBASE_FEET_EAST_OF_MAP_ORIGIN, null, typeof(float), float.MinValue, float.MaxValue));

            AddF4SimOutput(CreateNewF4SimOutput("Map", "Bullseye Position", "Feet North of Map Origin", F4SimOutputs.MAP__BULLSEYE_POSITION__FEET_NORTH_OF_MAP_ORIGIN, null, typeof(float), float.MinValue, float.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Map", "Bullseye Position", "Feet East of Map Origin", F4SimOutputs.MAP__BULLSEYE_POSITION__FEET_EAST_OF_MAP_ORIGIN, null, typeof(float), float.MinValue, float.MaxValue));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Altimeter", "Indicated Altitude (feet MSL)", F4SimOutputs.ALTIMETER__INDICATED_ALTITUDE__MSL, null, typeof(float), -1000, 80000, false, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Altimeter", "Barometric Pressure (inches Hg)", F4SimOutputs.ALTIMETER__BAROMETRIC_PRESSURE_INCHES_HG, null, typeof(float), 28.10, 31.00));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Radar Altimeter", "Radar Altitude (feet AGL)", F4SimOutputs.RADAR_ALTIMETER__ALTITUDE__AGL, null, typeof(float), -1000, 100000, false, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Radar Altimeter", "RALT Valid Flag", F4SimOutputs.RADAR_ALTIMETER__RALT_VALID, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Vertical Velocity Indicator (VVI)", "Vertical Velocity (feet/min)", F4SimOutputs.VVI__VERTICAL_VELOCITY_FPM, null, typeof(float), -6000, 6000, false, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Vertical Velocity Indicator (VVI)", "OFF flag", F4SimOutputs.VVI__OFF_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Flight dynamics", "Sideslip angle [beta] (degrees)", F4SimOutputs.FLIGHT_DYNAMICS__SIDESLIP_ANGLE_DEGREES, typeof(float), -180, 180, true));
            AddF4SimOutput(CreateNewF4SimOutput("Flight dynamics", "Climb/Dive Angle [gamma] (degrees)", F4SimOutputs.FLIGHT_DYNAMICS__CLIMBDIVE_ANGLE_DEGREES, typeof(float), -90, 90, true));
            AddF4SimOutput(CreateNewF4SimOutput("Flight dynamics", "Ownship Normal Gs", F4SimOutputs.FLIGHT_DYNAMICS__OWNSHIP_NORMAL_GS, typeof(float), -10, 10));
            AddF4SimOutput(CreateNewF4SimOutput("Flight dynamics", "True Airspeed (knots)", F4SimOutputs.AIRSPEED_MACH_INDICATOR__TRUE_AIRSPEED_KNOTS, typeof(float), 0, 1700));
            AddF4SimOutput(CreateNewF4SimOutput("Flight dynamics", "True Altitude (feet MSL)", F4SimOutputs.TRUE_ALTITUDE__MSL, typeof(float), -1000, 80000));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Airspeed indicator/Machmeter", "Mach number", F4SimOutputs.AIRSPEED_MACH_INDICATOR__MACH_NUMBER, null, typeof(float), 0, 2.5, false, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Airspeed indicator/Machmeter", "Indicated Airspeed (knots)", F4SimOutputs.AIRSPEED_MACH_INDICATOR__INDICATED_AIRSPEED_KNOTS, null, typeof(float), 0, 850));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HUD", "Wind delta to flight path marker (degrees)", F4SimOutputs.HUD__WIND_DELTA_TO_FLIGHT_PATH_MARKER_DEGREES, null, typeof(float), -90, 90, true, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HUD", "Drawing Commands length", F4SimOutputs.HUD__DRAWING_COMMANDS_LENGTH, null, typeof(uint), 0, uint.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HUD", "Drawing Commands", F4SimOutputs.HUD__DRAWING_COMMANDS, null, typeof(string)));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HMS", "Drawing Commands length", F4SimOutputs.HMS__DRAWING_COMMANDS_LENGTH, null, typeof(uint), 0, uint.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HMS", "Drawing Commands", F4SimOutputs.HMS__DRAWING_COMMANDS, null, typeof(string)));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Nozzle Position Indicator", "Engine #1 Nozzle Position (Percent Open 0-100)", F4SimOutputs.NOZ_POS1__NOZZLE_PERCENT_OPEN, null, typeof(float), 0, 100, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Nozzle Position Indicator", "Engine #2 Nozzle Position (Percent Open 0-100)", F4SimOutputs.NOZ_POS2__NOZZLE_PERCENT_OPEN, null, typeof(float), 0, 100, false, true, BASIC_SMOOTHING_TIME_CONSTANT));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Hydraulic Pressure Indicators", "HYD A (Pounds per Square Inch)", F4SimOutputs.HYD_PRESSURE_A__PSI, null, typeof(float), 0, 4000, false, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Hydraulic Pressure Indicators", "HYD B (Pounds per Square Inch)", F4SimOutputs.HYD_PRESSURE_B__PSI, null, typeof(float), 0, 4000, false, false, BASIC_SMOOTHING_TIME_CONSTANT));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Fuel Flow Indicator", "Engine #1 Fuel flow (pounds/hour)", F4SimOutputs.FUEL_FLOW1__FUEL_FLOW_POUNDS_PER_HOUR, null, typeof(float), 0, 99900.00));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Fuel Flow Indicator", "Engine #2 Fuel flow (pounds/hour)", F4SimOutputs.FUEL_FLOW2__FUEL_FLOW_POUNDS_PER_HOUR, null, typeof(float), 0, 99900.00));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RPM", "Engine #1 RPM (Percent 0-103)", F4SimOutputs.RPM1__RPM_PERCENT, null, typeof(float), 0, 103, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RPM", "Engine #2 RPM(Percent 0-103)", F4SimOutputs.RPM2__RPM_PERCENT, null, typeof(float), 0, 103, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "FTIT", "Engine #1 Forward Turbine Inlet Temp (Degrees C)", F4SimOutputs.FTIT1__FTIT_TEMP_DEG_CELCIUS, null, typeof(float), 0, 1200, false, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "FTIT", "Engine #2 Forward Turbine Inlet Temp (Degrees C)", F4SimOutputs.FTIT2__FTIT_TEMP_DEG_CELCIUS, null, typeof(float), 0, 1200, false, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Speedbrake", "Speedbrake position (0 = closed, 1 = 60 Degrees open)", F4SimOutputs.SPEED_BRAKE__POSITION, null, typeof(float), 0, 1, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Speedbrake", "Speedbrake Not Stowed Flag", F4SimOutputs.SPEED_BRAKE__NOT_STOWED_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "EPU Fuel", "EPU fuel (Percent 0-100)", F4SimOutputs.EPU_FUEL__EPU_FUEL_PERCENT, null, typeof(float), 0, 100, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "OIL Pressure", "Engine #1 Oil Pressure (Percent 0-100)", F4SimOutputs.OIL_PRESS1__OIL_PRESS_PERCENT, null, typeof(float), 0, 100, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "OIL Pressure", "Engine #2 Oil Pressure (Percent 0-100)", F4SimOutputs.OIL_PRESS2__OIL_PRESS_PERCENT, null, typeof(float), 0, 100, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Cabin Pressure Altimeter", "Cabin Pressure Altitude (in Feet MSL)", F4SimOutputs.CABIN_PRESS__CABIN_PRESS_FEET_MSL, null, typeof(float), 0, 50000, false, false, BASIC_SMOOTHING_TIME_CONSTANT));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Compass", "Magnetic Heading (degrees)", F4SimOutputs.COMPASS__MAGNETIC_HEADING_DEGREES, null, typeof(float), 0, 360, true, false, BASIC_SMOOTHING_TIME_CONSTANT));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Gear position (0 = up, 1 = down)", F4SimOutputs.GEAR_PANEL__GEAR_POSITION, null, typeof(float), 0, 1, false, true));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Nose Gear Down Light", F4SimOutputs.GEAR_PANEL__NOSE_GEAR_DOWN_LIGHT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Left Gear Down Light", F4SimOutputs.GEAR_PANEL__LEFT_GEAR_DOWN_LIGHT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Right Gear Down Light", F4SimOutputs.GEAR_PANEL__RIGHT_GEAR_DOWN_LIGHT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Nose Gear Position (0 = up, 1 = down)", F4SimOutputs.GEAR_PANEL__NOSE_GEAR_POSITION, null, typeof(float), 0, 1, false, true));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Left Gear Position (0 = up, 1 = down)", F4SimOutputs.GEAR_PANEL__LEFT_GEAR_POSITION, null, typeof(float), 0, 1, false, true));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Right Gear Position (0 = up, 1 = down)", F4SimOutputs.GEAR_PANEL__RIGHT_GEAR_POSITION, null, typeof(float), 0, 1, false, true));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Gear handle 'Lollipop' light", F4SimOutputs.GEAR_PANEL__GEAR_HANDLE_LIGHT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "GEAR", "Parking brake engaged flag", F4SimOutputs.GEAR_PANEL__PARKING_BRAKE_ENGAGED_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "Pitch (degrees)", F4SimOutputs.ADI__PITCH_DEGREES, null, typeof(float), -90, 90, true, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "Roll (degrees)", F4SimOutputs.ADI__ROLL_DEGREES, null, typeof(float), -180, 180, true, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "ILS command bars enabled flag", F4SimOutputs.ADI__ILS_SHOW_COMMAND_BARS, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "ILS glideslope bar Position", F4SimOutputs.ADI__ILS_HORIZONTAL_BAR_POSITION, null, typeof(float), -1, 1, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "ILS localizer bar Position", F4SimOutputs.ADI__ILS_VERTICAL_BAR_POSITION, null, typeof(float), -1, 1, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "Rate of Turn Indicator Position", F4SimOutputs.ADI__RATE_OF_TURN_INDICATOR_POSITION, null, typeof(float), -1, 1, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "Inclinometer Position", F4SimOutputs.ADI__INCLINOMETER_POSITION, null, typeof(float), -1, 1, false, true, BASIC_SMOOTHING_TIME_CONSTANT));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "OFF flag", F4SimOutputs.ADI__OFF_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "AUX flag", F4SimOutputs.ADI__AUX_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "GS flag", F4SimOutputs.ADI__GS_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "ADI", "LOC flag", F4SimOutputs.ADI__LOC_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Standby ADI", "OFF flag", F4SimOutputs.STBY_ADI__OFF_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Standby ADI", "Pitch (degrees)", F4SimOutputs.STBY_ADI__PITCH_DEGREES, null, typeof(float), -90, 90, true, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Standby ADI", "Roll (degrees)", F4SimOutputs.STBY_ADI__ROLL_DEGREES, null, typeof(float), -180, 180, true, false, BASIC_SMOOTHING_TIME_CONSTANT));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "AOA Indicator", "Angle of Attack [alpha] (degrees)", F4SimOutputs.AOA_INDICATOR__AOA_DEGREES, null, typeof(float), -5, 40, true, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "AOA Indicator", "OFF flag", F4SimOutputs.AOA_INDICATOR__OFF_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Course deviation invalid flag", F4SimOutputs.HSI__COURSE_DEVIATION_INVALID_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Distance invalid flag", F4SimOutputs.HSI__DISTANCE_INVALID_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Desired course (degrees)", F4SimOutputs.HSI__DESIRED_COURSE_DEGREES, null, typeof(float), 0, 360, true, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Course deviation (degrees)", F4SimOutputs.HSI__COURSE_DEVIATION_DEGREES, null, typeof(float), -10, 10, true, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Course deviation limit (degrees)", F4SimOutputs.HSI__COURSE_DEVIATION_LIMIT_DEGREES, null, typeof(float), -10, 10, true));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Distance to beacon (nautical miles)", F4SimOutputs.HSI__DISTANCE_TO_BEACON_NAUTICAL_MILES, null, typeof(float), 0, 1000));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Bearing to beacon (degrees)", F4SimOutputs.HSI__BEARING_TO_BEACON_DEGREES, null, typeof(float), 0, 360, true, false, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Current heading (degrees)", F4SimOutputs.HSI__CURRENT_HEADING_DEGREES, null, typeof(float), 0, 360, true));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Desired heading (degrees)", F4SimOutputs.HSI__DESIRED_HEADING_DEGREES, null, typeof(float), 0, 360, true));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "Localizer course (degrees)", F4SimOutputs.HSI__LOCALIZER_COURSE_DEGREES, null, typeof(float), 0, 360, true));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "TO Flag", F4SimOutputs.HSI__TO_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "FROM Flag", F4SimOutputs.HSI__FROM_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "OFF Flag", F4SimOutputs.HSI__OFF_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "HSI", "HSI mode (0=ILS/TCN, 1=TACAN, 2=NAV, 3=ILS/NAV)", F4SimOutputs.HSI__HSI_MODE, null, typeof(int), 0, 3));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "LIGHTING", "PRIMARY - INST PNL Lighting Intensity (0=OFF, 1=DIM, 2=BRT)", F4SimOutputs.INSTR_LIGHTING_INTENSITY,null, typeof(int), 0, 3));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "TRIM", "Pitch trim (-1 to +1)", F4SimOutputs.TRIM__PITCH_TRIM, null, typeof(float), -1, 1, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "TRIM", "Roll trim (-1 to +1)", F4SimOutputs.TRIM__ROLL_TRIM, null, typeof(float), -1, 1, false, true, BASIC_SMOOTHING_TIME_CONSTANT));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "TRIM", "Yaw trim (-1 to +1)", F4SimOutputs.TRIM__YAW_TRIM, null, typeof(float), -1, 1, false, true, BASIC_SMOOTHING_TIME_CONSTANT));

            for (var i = 0; i < 5; i++)
            {
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "DED", $"DED Line {i + 1} Text", F4SimOutputs.DED__LINES, i, typeof(string)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "DED", $"DED Line {i + 1} Inverse Characters", F4SimOutputs.DED__INVERT_LINES, i, typeof(string)));

                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "PFL", $"PFL Line {i + 1} Text", F4SimOutputs.PFL__LINES, i, typeof(string)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "PFL", $"PFL Line {i + 1} Inverse Characters", F4SimOutputs.PFL__INVERT_LINES, i, typeof(string)));
            }

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Up-Front Controls", "TACAN Channel", F4SimOutputs.UFC__TACAN_CHANNEL, null, typeof(int), 0, 199));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Up-Front Controls", "Tacan Band is X", F4SimOutputs.UFC__TACAN_BAND_IS_X, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Up-Front Controls", "Tacan Mode is AA", F4SimOutputs.UFC__TACAN_MODE_IS_AA, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "AUX COMM", "TACAN Channel", F4SimOutputs.AUX_COMM__TACAN_CHANNEL, null, typeof(int),0, 199));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "AUX COMM", "Tacan Band is X", F4SimOutputs.AUX_COMM__TACAN_BAND_IS_X, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "AUX COMM", "Tacan Mode is AA", F4SimOutputs.AUX_COMM__TACAN_MODE_IS_AA, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "UHF", "UHF Preset", F4SimOutputs.AUX_COMM__UHF_PRESET, null, typeof(int), 1, 19));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "UHF", "UHF Frequency", F4SimOutputs.AUX_COMM__UHF_FREQUENCY, null, typeof(int), 0, 399975));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "IFF", "IFF Backup Mode 1 Digit 1", F4SimOutputs.IFF__BACKUP_MODE_1_DIGIT_1, null, typeof(int), 0, 9));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "IFF", "IFF Backup Mode 1 Digit 2", F4SimOutputs.IFF__BACKUP_MODE_1_DIGIT_2, null, typeof(int), 0, 9));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "IFF", "IFF Backup Mode 3A Digit 1", F4SimOutputs.IFF__BACKUP_MODE_3A_DIGIT_1, null, typeof(int), 0, 9));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "IFF", "IFF Backup Mode 3A Digit 2", F4SimOutputs.IFF__BACKUP_MODE_3A_DIGIT_2, null, typeof(int), 0, 9));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Fuel QTY", "Internal fuel (pounds)", F4SimOutputs.FUEL_QTY__INTERNAL_FUEL_POUNDS, null, typeof(float), 0, 42000));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Fuel QTY", "External fuel (pounds)", F4SimOutputs.FUEL_QTY__EXTERNAL_FUEL_POUNDS, null, typeof(float), 0, 42000));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Fuel QTY", "Foreward fuel quantity (lbs)", F4SimOutputs.FUEL_QTY__FOREWARD_QTY_LBS, null, typeof(float), 0, 42000));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Fuel QTY", "Aft fuel quantity (lbs) ", F4SimOutputs.FUEL_QTY__AFT_QTY_LBS, null, typeof(float), 0, 42000));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Fuel QTY", "Total fuel (lbs)", F4SimOutputs.FUEL_QTY__TOTAL_FUEL_LBS, null, typeof(float), 0, 20100));

            for (var i = 0; i < 20; i++)
            {
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Left MFD", $"LMFD OSB #{(i + 1).ToString().PadLeft(2, '0')} Label - Line 1", F4SimOutputs.LMFD__OSB_LABEL_LINES1, i, typeof(string)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Left MFD", $"LMFD OSB #{(i + 1).ToString().PadLeft(2, '0')} Label - Line 2", F4SimOutputs.LMFD__OSB_LABEL_LINES2, i, typeof(string)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Left MFD", $"LMFD OSB #{(i + 1).ToString().PadLeft(2, '0')} Label - Inverted Flag", F4SimOutputs.LMFD__OSB_INVERTED_FLAGS, i, typeof(bool)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Right MFD", $"RMFD OSB #{(i + 1).ToString().PadLeft(2, '0')} Label - Line 1", F4SimOutputs.RMFD__OSB_LABEL_LINES1, i, typeof(string)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Right MFD", $"RMFD OSB #{(i + 1).ToString().PadLeft(2, '0')} Label - Line 2", F4SimOutputs.RMFD__OSB_LABEL_LINES2, i, typeof(string)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "Right MFD", $"RMFD OSB #{(i + 1).ToString().PadLeft(2, '0')} Label - Inverted Flag", F4SimOutputs.RMFD__OSB_INVERTED_FLAGS, i, typeof(bool)));
            }

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Left Eyebrow Lights", "Master Caution Light", F4SimOutputs.MASTER_CAUTION_LIGHT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Left Eyebrow Lights", "Master Caution Announced", F4SimOutputs.MASTER_CAUTION_ANNOUNCED, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Left Eyebrow Lights", "TF-FAIL Light", F4SimOutputs.LEFT_EYEBROW_LIGHTS__TFFAIL, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Left Eyebrow Lights", "ALT LOW Light", F4SimOutputs.LEFT_EYEBROW_LIGHTS__ALTLOW, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Left Eyebrow Lights", "OBS WRN Light", F4SimOutputs.LEFT_EYEBROW_LIGHTS__OBSWRN, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "ENG FIRE Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__ENGFIRE, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "Engine 2 Fire flag", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__ENGINE_2_FIRE, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "ENGINE Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__ENGINE, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "HYD/OIL PRESS Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__HYDOIL, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "DUAL FC Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__DUALFC, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "FLCS Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__FLCS, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "CANOPY Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__CANOPY, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "TO/LDG CONFIG Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__TO_LDG_CONFIG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "OXY LOW Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__OXY_LOW, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Right Eyebrow Lights", "DBU ON Light", F4SimOutputs.RIGHT_EYEBROW_LIGHTS__DBU_ON, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "FLCS FAULT Light", F4SimOutputs.CAUTION_PANEL__FLCS_FAULT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "LE FLAPS Light", F4SimOutputs.CAUTION_PANEL__LE_FLAPS, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "ELEC SYS Light", F4SimOutputs.CAUTION_PANEL__ELEC_SYS, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "ENGINE FAULT Light", F4SimOutputs.CAUTION_PANEL__ENGINE_FAULT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "SEC Light", F4SimOutputs.CAUTION_PANEL__SEC, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "FWD FUEL LOW Light", F4SimOutputs.CAUTION_PANEL__FWD_FUEL_LOW, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "AFT FUEL LOW Light", F4SimOutputs.CAUTION_PANEL__AFT_FUEL_LOW, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "OVERHEAT Light", F4SimOutputs.CAUTION_PANEL__OVERHEAT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "BUC Light", F4SimOutputs.CAUTION_PANEL__BUC, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "FUEL OIL HOT Light", F4SimOutputs.CAUTION_PANEL__FUEL_OIL_HOT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "SEAT NOT ARMED Light", F4SimOutputs.CAUTION_PANEL__SEAT_NOT_ARMED, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "AVIONICS FAULT Light", F4SimOutputs.CAUTION_PANEL__AVIONICS_FAULT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "RADAR ALT Light", F4SimOutputs.CAUTION_PANEL__RADAR_ALT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "EQUIP HOT Light", F4SimOutputs.CAUTION_PANEL__EQUIP_HOT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "ECM Light", F4SimOutputs.CAUTION_PANEL__ECM, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "STORES CONFIG Light", F4SimOutputs.CAUTION_PANEL__STORES_CONFIG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "ANTI SKID Light", F4SimOutputs.CAUTION_PANEL__ANTI_SKID, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "HOOK Light", F4SimOutputs.CAUTION_PANEL__HOOK, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "NWS FAIL Light", F4SimOutputs.CAUTION_PANEL__NWS_FAIL, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "CABIN PRESS Light", F4SimOutputs.CAUTION_PANEL__CABIN_PRESS, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "OXY LOW Light", F4SimOutputs.CAUTION_PANEL__OXY_LOW, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "PROBE HEAT Light", F4SimOutputs.CAUTION_PANEL__PROBE_HEAT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "FUEL LOW Light", F4SimOutputs.CAUTION_PANEL__FUEL_LOW, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "IFF Light", F4SimOutputs.CAUTION_PANEL__IFF, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "C ADC Light", F4SimOutputs.CAUTION_PANEL__C_ADC, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Caution", "ATF NOT ENGAGED Light", F4SimOutputs.CAUTION_PANEL__ATF_NOT_ENGAGED, null, typeof(bool)));


            /*
             * MISSING CAUTION PANEL FLAGS
            ADC
            INLET ICING
            EEC
            NUCLEAR
             */

            /*
            // Caution panel
        Lef_Fault = 0x800,  //TODO: WHAT IS THIS?? LE FLAPS IS ALREADY A FLAG??
             * 
            */

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "AOA Indexer", "AOA Too-High Light", F4SimOutputs.AOA_INDEXER__AOA_TOO_HIGH, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "AOA Indexer", "AOA Ideal Light", F4SimOutputs.AOA_INDEXER__AOA_IDEAL, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "AOA Indexer", "AOA Too-Low Light", F4SimOutputs.AOA_INDEXER__AOA_TOO_LOW, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Refuel/NWS Indexer", "RDY Light", F4SimOutputs.NWS_INDEXER__RDY, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Refuel/NWS Indexer", "AR NWS Light", F4SimOutputs.NWS_INDEXER__AR_NWS, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Refuel/NWS Indexer", "DISC Light", F4SimOutputs.NWS_INDEXER__DISC, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Prime", "HANDOFF Indicator Light", F4SimOutputs.TWP__HANDOFF, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Prime", "MISSILE LAUNCH Indicator Light", F4SimOutputs.TWP__MISSILE_LAUNCH, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Prime", "PRIORITY MODE OPEN Indicator Light", F4SimOutputs.TWP__PRIORITY_MODE, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Prime", "UNKNOWN Indicator Light", F4SimOutputs.TWP__UNKNOWN, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Prime", "NAVAL Indicator Light", F4SimOutputs.TWP__NAVAL, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Prime", "TGT SEP Indicator Light", F4SimOutputs.TWP__TARGET_SEP, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Prime", "SYS TEST Indicator Light", F4SimOutputs.TWP__SYS_TEST, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Aux", "SEARCH Indicator Light", F4SimOutputs.TWA__SEARCH, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Aux", "ACTIVITY/POWER Indicator Light", F4SimOutputs.TWA__ACTIVITY_POWER, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Aux", "LOW ALTITUDE Indicator Light", F4SimOutputs.TWA__LOW_ALTITUDE, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Threat Warning Aux", "SYSTEM POWER Indicator Light", F4SimOutputs.TWA__SYSTEM_POWER, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ECM", "POWER Flag", F4SimOutputs.ECM__POWER, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ECM", "FAIL Flag", F4SimOutputs.ECM__FAIL, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "MISC", "ADV MODE ACTIVE Light", F4SimOutputs.MISC__ADV_MODE_ACTIVE, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "MISC", "ADV MODE STBY Light", F4SimOutputs.MISC__ADV_MODE_STBY, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "MISC", "Autopilot engaged flag", F4SimOutputs.MISC__AUTOPILOT_ENGAGED, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "Chaff Count (# remaining)", F4SimOutputs.CMDS__CHAFF_COUNT, null, typeof(float), 0, 99));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "Flare Count (# remaining)", F4SimOutputs.CMDS__FLARE_COUNT, null, typeof(float), 0, 99));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "GO Flag", F4SimOutputs.CMDS__GO, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "NO GO Flag", F4SimOutputs.CMDS__NOGO, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "AUTO DEGR Flag", F4SimOutputs.CMDS__AUTO_DEGR, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "DISPENSE RDY Flag", F4SimOutputs.CMDS__DISPENSE_RDY, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "CHAFF LO Flag", F4SimOutputs.CMDS__CHAFF_LO, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "FLARE LO Flag", F4SimOutputs.CMDS__FLARE_LO, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "CMDS", "MODE", F4SimOutputs.CMDS__MODE, null, typeof(int), 0, 5));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ELEC", "FLCS PMG Indicator Light", F4SimOutputs.ELEC__FLCS_PMG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ELEC", "MAIN GEN Indicator Light", F4SimOutputs.ELEC__MAIN_GEN, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ELEC", "STBY GEN Indicator Light", F4SimOutputs.ELEC__STBY_GEN, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ELEC", "EPU GEN Indicator Light", F4SimOutputs.ELEC__EPU_GEN, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ELEC", "EPU PMG Indicator Light", F4SimOutputs.ELEC__EPU_PMG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ELEC", "TO FLCS Indicator Light", F4SimOutputs.ELEC__TO_FLCS, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ELEC", "FLCS RLY Indicator Light", F4SimOutputs.ELEC__FLCS_RLY, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "ELEC", "BATT FAIL Indicator Light", F4SimOutputs.ELEC__BATT_FAIL, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "TEST", "FLCS ABCD Indicator Light", F4SimOutputs.TEST__ABCD, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "EPU", "HYDRAZN Indicator Light", F4SimOutputs.EPU__HYDRAZN, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "EPU", "AIR Indicator Light", F4SimOutputs.EPU__AIR, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "EPU", "RUN Indicator Light", F4SimOutputs.EPU__RUN, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "AVTR", "AVTR Indicator Light", F4SimOutputs.AVTR__AVTR, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "JFS", "RUN Indicator Light", F4SimOutputs.JFS__RUN, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Marker Beacon", "MRK BCN Light", F4SimOutputs.MARKER_BEACON__MRK_BCN_LIGHT, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Marker Beacon", "Outer marker flag", F4SimOutputs.MARKER_BEACON__OUTER_MARKER_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "Marker Beacon", "Middle marker flag", F4SimOutputs.MARKER_BEACON__MIDDLE_MARKER_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "Aircraft Name", F4SimOutputs.AIRCRAFT_NAME, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "Aircraft NCTR Designation", F4SimOutputs.AIRCRAFT_NCTR_DESIGNATION, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "Vehicle ACD", F4SimOutputs.AIRCRAFT__VEHICLE_ACD, typeof(short)));
            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "On Ground", F4SimOutputs.AIRCRAFT__ONGROUND, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "Main Landing Gear - Weight on Wheels", F4SimOutputs.AIRCRAFT__MAIN_LANDING_GEAR__WEIGHT_ON_WHEELS, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "Nose Landing Gear - Weight on Wheels", F4SimOutputs.AIRCRAFT__NOSE_LANDING_GEAR__WEIGHT_ON_WHEELS, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "Leading edge flaps position", F4SimOutputs.AIRCRAFT__LEADING_EDGE_FLAPS_POSITION, typeof(float), 0, 1, false, true));
            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "Trailing edge flaps position", F4SimOutputs.AIRCRAFT__TRAILING_EDGE_FLAPS_POSITION, typeof(float), 0, 1, false, true));
            AddF4SimOutput(CreateNewF4SimOutput("Aircraft", "VTOL position", F4SimOutputs.AIRCRAFT__VTOL_POSITION, typeof(float), 0, 1, false, true));


            AddF4SimOutput(CreateNewF4SimOutput("Power", "Electrical Power OFF flag", F4SimOutputs.POWER__ELEC_POWER_OFF, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Power", "Main Power Switch (Down = 0, Middle = 1, Up = 2)", F4SimOutputs.POWER__MAIN_POWER, typeof(int), 0, 2));
            AddF4SimOutput(CreateNewF4SimOutput("Power", "Bus Power - Battery Power flag", F4SimOutputs.POWER__BUS_POWER_BATTERY, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Power", "Bus Power - Emergency Power flag", F4SimOutputs.POWER__BUS_POWER_EMERGENCY, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Power", "Bus Power - Essential Power flag", F4SimOutputs.POWER__BUS_POWER_ESSENTIAL, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Power", "Bus Power - Non-Essential Power flag", F4SimOutputs.POWER__BUS_POWER_NON_ESSENTIAL, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Power", "Main Generator flag", F4SimOutputs.POWER__MAIN_GENERATOR, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Power", "Standby Generator flag", F4SimOutputs.POWER__STANDBY_GENERATOR, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Power", "Jet Fuel Starter flag", F4SimOutputs.POWER__JET_FUEL_STARTER, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "Test (All Words) Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__ALL_WORDS, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"PULL UP\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__PULL_UP, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"ALTITUDE\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__ALTITUDE, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"WARNING\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__WARNING, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"JAMMER\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__JAMMER, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"COUNTER\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__COUNTER, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"CHAFF/FLARE\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__CHAFFFLARE, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"CHAFF/FLARE LOW\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__CHAFFFLARE_LOW, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"CHAFF/FLARE OUT\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__CHAFFFLARE_OUT, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"LOCK\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__LOCK, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"CAUTION\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__CAUTION, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"BINGO\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__BINGO, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"DATA\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__DATA, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"IFF\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__IFF, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"LOW SPEED\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__LOWSPEED, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"BEEPS\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__BEEPS, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"AOA\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__AOA, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Voice Message System (VMS)", "\"MAX G\" Message Is Playing flag", F4SimOutputs.VOICE_MESSAGE_SYSTEM__MAXG, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Integrated Control Panel (ICP)", "BINGO Fuel Level", F4SimOutputs.ICP__BINGO_FUEL_LEVEL, typeof(float), 0, 100000));
            AddF4SimOutput(CreateNewF4SimOutput("Integrated Control Panel (ICP)", "CARA ALOW", F4SimOutputs.ICP__CARA_ALOW, typeof(float), 0, 100000));

            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Shared Memory Area Descriptors", "Shared Memory Area \"FlightData\" Version Number", F4SimOutputs.SIM__FLIGHTDATA_VERSION_NUM, null, typeof(int), 0, 999));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Shared Memory Area Descriptors", "Shared Memory Area \"FlightData2\" Version Number", F4SimOutputs.SIM__FLIGHTDATA2_VERSION_NUM, null, typeof(int), 0, 999));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Shared Memory Area Descriptors", "Shared Memory Area \"StringData\" Version Number", F4SimOutputs.SIM__STRING_DATA_VERSION_NUM, null, typeof(int), 0, 999));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Shared Memory Area Descriptors", "Shared Memory Area \"StringData\" Size (bytes)", F4SimOutputs.SIM__STRING_AREA_SIZE, null, typeof(int), 0, 999));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Shared Memory Area Descriptors", "Shared Memory Area \"DrawingData\" Version Number", F4SimOutputs.SIM__DRAWING_DATA_VERSION_NUM, null, typeof(int), 0, 999));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Shared Memory Area Descriptors", "Shared Memory Area \"DrawingData\" Size (bytes)", F4SimOutputs.SIM__DRAWING_AREA_SIZE, null, typeof(int), 0, 999));

            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "BMS Version Number (Major.Minor.Micro.Build)", F4SimOutputs.SIM__BMS_VERSION, typeof(string)));

            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Current Time", F4SimOutputs.SIM__CURRENT_TIME, typeof(int), int.MinValue, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "'Pilot Is Flying' flag", F4SimOutputs.SIM__BMS_PLAYER_IS_FLYING, typeof(bool)));
            for (var i = 0; i < 32; i++)
            {
                AddF4SimOutput(CreateNewF4SimOutput("Pilots", $"Pilot #{(i + 1).ToString().PadLeft(2, '0')}", "Callsign", F4SimOutputs.SIM__PILOT_CALLSIGN, i, typeof(string)));
                AddF4SimOutput(CreateNewF4SimOutput("Pilots", $"Pilot #{(i + 1).ToString().PadLeft(2, '0')}", "Status (0=In UI, 1=Loading, 2=Waiting, 3=Flying, 4=Dead, 5=Unknown)", F4SimOutputs.SIM__PILOT_STATUS, i, typeof(int),0, 5));
                /*
                 IN_UI = 0, // UI      - in the UI
                LOADING = 1, // UI>3D   - loading the sim data
                WAITING = 2, // UI>3D   - waiting for other players
                FLYING = 3, // 3D      - flying
                DEAD = 4, // 3D>Dead - dead, waiting to respawn
                UNKNOWN = 5, // ???
                 */
            }

            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "AA Missile Fired", F4SimOutputs.SIM__AA_MISSILE_FIRED,0, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Bomb Dropped", F4SimOutputs.SIM__BOMB_DROPPED, 0, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Flare Dropped", F4SimOutputs.SIM__FLARE_DROPPED, 0,typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Chaff Dropped", F4SimOutputs.SIM__CHAFF_DROPPED,0, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Bullets Fired", F4SimOutputs.SIM__BULLETS_FIRED, 0, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Collision Counter", F4SimOutputs.SIM__COLLISION_COUNTER, 0, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "g Force", F4SimOutputs.SIM__GFORCE, 0, typeof(float), 0, 10));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Last Damage", F4SimOutputs.SIM__LAST_DAMAGE, 0, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Damage Force", F4SimOutputs.SIM__DAMAGE_FORCE, 0, typeof(float), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "When Damage", F4SimOutputs.SIM__WHEN_DAMAGE, 0, typeof(int), int.MinValue, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Eye X", F4SimOutputs.SIM__EYE_X, 0, typeof(float), float.MinValue, float.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Eye Y", F4SimOutputs.SIM__EYE_Y, 0, typeof(float), float.MinValue, float.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Eye Z", F4SimOutputs.SIM__EYE_Z, 0, typeof(float), float.MinValue, float.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Is Firing Gun flag", F4SimOutputs.SIM__IS_FIRING_GUN, 0, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Is End Flight flag", F4SimOutputs.SIM__IS_END_FLIGHT, 0, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Is Ejecting flag", F4SimOutputs.SIM__IS_EJECTING, 0, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "In 3D flag", F4SimOutputs.SIM__IN_3D, 0, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Is Paused flag", F4SimOutputs.SIM__IS_PAUSED, 0, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Is Frozen flag", F4SimOutputs.SIM__IS_FROZEN, 0, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Is Over G flag", F4SimOutputs.SIM__IS_OVER_G, 0, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Is On Ground flag", F4SimOutputs.SIM__IS_ON_GROUND, 0, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "IntelliVibe Data", "Is Exit Game flag", F4SimOutputs.SIM__IS_EXIT_GAME, 0, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Bump Intensity (Intensity of a \"bump\" while taxiing/rolling [0 to 1])", F4SimOutputs.SIM__BUMP_INTENSITY, typeof(float), 0, 1));

            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Status", "Client Active flag", F4SimOutputs.RADIO_CLIENT_STATUS__CLIENT_ACTIVE_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Status", "Connection Fail flag", F4SimOutputs.RADIO_CLIENT_STATUS__CONNECTION_FAIL_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Status", "Bad Password flag", F4SimOutputs.RADIO_CLIENT_STATUS__BAD_PASSWORD_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Status", "No Speakers flag", F4SimOutputs.RADIO_CLIENT_STATUS__NO_SPEAKERS_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Status", "Connected flag", F4SimOutputs.RADIO_CLIENT_STATUS__CONNECTED_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Status", "Host Unknown flag", F4SimOutputs.RADIO_CLIENT_STATUS__HOST_UNKNOWN_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Status", "No Microphone flag", F4SimOutputs.RADIO_CLIENT_STATUS__NO_MICROPHONE_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "UHF Radio", "Frequency", F4SimOutputs.RADIO_CLIENT_CONTROL__UHF_RADIO__FREQUENCY, null, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "UHF Radio", "Rx Volume", F4SimOutputs.RADIO_CLIENT_CONTROL__UHF_RADIO__RX_VOLUME, null, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "UHF Radio", "On Flag", F4SimOutputs.RADIO_CLIENT_CONTROL__UHF_RADIO__IS_ON_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "UHF Radio", "PTT Depressed Flag", F4SimOutputs.RADIO_CLIENT_CONTROL__UHF_RADIO__PTT_DEPRESSED_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "VHF Radio", "Frequency", F4SimOutputs.RADIO_CLIENT_CONTROL__VHF_RADIO__FREQUENCY, null, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "VHF Radio", "Rx Volume", F4SimOutputs.RADIO_CLIENT_CONTROL__VHF_RADIO__RX_VOLUME, null, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "VHF Radio", "On Flag", F4SimOutputs.RADIO_CLIENT_CONTROL__VHF_RADIO__IS_ON_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "VHF Radio", "PTT Depressed Flag", F4SimOutputs.RADIO_CLIENT_CONTROL__VHF_RADIO__PTT_DEPRESSED_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Guard Radio", "Frequency", F4SimOutputs.RADIO_CLIENT_CONTROL__GUARD_RADIO__FREQUENCY, null, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Guard Radio", "Rx Volume", F4SimOutputs.RADIO_CLIENT_CONTROL__GUARD_RADIO__RX_VOLUME, null, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Guard Radio", "On Flag", F4SimOutputs.RADIO_CLIENT_CONTROL__GUARD_RADIO__IS_ON_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Guard Radio", "PTT Depressed Flag", F4SimOutputs.RADIO_CLIENT_CONTROL__GUARD_RADIO__PTT_DEPRESSED_FLAG, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Nickname", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__NICKNAME, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Address", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__ADDRESS, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Port Number", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__PORT_NUMBER, null, typeof(int), 0, int.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Password", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__PASSWORD, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Player Count", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__PLAYER_COUNT, null, typeof(int), 0, 32));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Signal Connect flag", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__SIGNAL_CONNECT_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Terminate Client flag", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__TERMINATE_CLIENT_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Flight Mode flag", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__FLIGHT_MODE_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "Connection", "Use AGC flag", F4SimOutputs.RADIO_CLIENT_CONTROL__CONNECTION__USE_AGC_FLAG, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Radio Client", "MAIN Device", "IcVolume", F4SimOutputs.RADIO_CLIENT_CONTROL__MAIN_DEVICE__IC_VOLUME, null, typeof(int), 0, int.MaxValue));

            /*
            AddF4SimOutput(CreateNewF4SimOutput("Pilot", "Head X offset from design eye (feet)",F4SimOutputs.PILOT__HEADX_OFFSET, null, typeof(float)));
            AddF4SimOutput(CreateNewF4SimOutput("Pilot", "Head Y offset from design eye (feet)", F4SimOutputs.PILOT__HEADY_OFFSET, null, typeof(float)));
            AddF4SimOutput(CreateNewF4SimOutput("Pilot", "Head Z offset from design eye (feet)", F4SimOutputs.PILOT__HEADZ_OFFSET, null,  typeof(float)));
            AddF4SimOutput(CreateNewF4SimOutput("Pilot", "Head pitch offset from design eye (degrees)", F4SimOutputs.PILOT__HEAD_PITCH_DEGREES, null, typeof(float)));
            AddF4SimOutput(CreateNewF4SimOutput("Pilot", "Head roll offset from design eye (degrees)", F4SimOutputs.PILOT__HEAD_ROLL_DEGREES, null, typeof(float)));
            AddF4SimOutput(CreateNewF4SimOutput("Pilot", "Head yaw offset from design eye (degrees)", F4SimOutputs.PILOT__HEAD_YAW_DEGREES, null, typeof(float)));
            */

            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "FLIGHT CONTROL", "RUN Indicator Light", F4SimOutputs.FLIGHT_CONTROL__RUN, null, typeof(bool)));
            AddF4SimOutput(CreateNewF4SimOutput("Panels and Lights", "FLIGHT CONTROL", "FAIL Indicator Light", F4SimOutputs.FLIGHT_CONTROL__FAIL, null, typeof(bool)));

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", "Object Count", F4SimOutputs.RWR__OBJECT_COUNT, null, typeof(int)));
            for (var i = 0; i < 64; i++)
            {
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", $"Threat #{(i + 1).ToString().PadLeft(2, '0')} Symbol ID", F4SimOutputs.RWR__SYMBOL_ID, i, typeof(int), Int32.MinValue, Int32.MaxValue));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", $"Threat #{(i + 1).ToString().PadLeft(2, '0')} Bearing (Degrees)", F4SimOutputs.RWR__BEARING_DEGREES, i, typeof(float), 0, 360, true));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", $"Threat #{(i + 1).ToString().PadLeft(2, '0')} Missile Activity Flag", F4SimOutputs.RWR__MISSILE_ACTIVITY_FLAG, i, typeof(bool)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", $"Threat #{(i + 1).ToString().PadLeft(2, '0')} Missile Launch Flag", F4SimOutputs.RWR__MISSILE_LAUNCH_FLAG, i, typeof(bool)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", $"Threat #{(i + 1).ToString().PadLeft(2, '0')} Selected Flag", F4SimOutputs.RWR__SELECTED_FLAG, i, typeof(bool)));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", $"Threat #{(i + 1).ToString().PadLeft(2, '0')} Lethality", F4SimOutputs.RWR__LETHALITY, i, typeof(float), -1, 3));
                AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", $"Threat #{(i + 1).ToString().PadLeft(2, '0')} New Detection Flag", F4SimOutputs.RWR__NEWDETECTION_FLAG, i, typeof(bool)));
            }

            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", "Additional Info", F4SimOutputs.RWR__ADDITIONAL_INFO, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", "Drawing Commands length", F4SimOutputs.RWR__DRAWING_COMMANDS_LENGTH, null, typeof(uint), 0, uint.MaxValue));
            AddF4SimOutput(CreateNewF4SimOutput("Instruments, Displays and Gauges", "RWR", "Drawing Commands", F4SimOutputs.RWR__DRAWING_COMMANDS, null, typeof(string)));


            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "EXE Path", F4SimOutputs.SIM__BMS_EXE, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Keyfile Path", F4SimOutputs.SIM_KEYFILE_PATH, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Base Directory", F4SimOutputs.SIM__BMS_BASE_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Bin Directory", F4SimOutputs.SIM__BMS_BIN_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "UI Art Directory", F4SimOutputs.SIM__BMS_UI_ART_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "User directory", F4SimOutputs.SIM__BMS_USER_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "ACMI directory", F4SimOutputs.SIM__BMS_ACMI_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Briefings directory", F4SimOutputs.SIM__BMS_BRIEFINGS_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Logs directory", F4SimOutputs.SIM__BMS_LOGS_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Config directory", F4SimOutputs.SIM__BMS_CONFIG_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Patch directory", F4SimOutputs.SIM__BMS_PATCH_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Picture directory", F4SimOutputs.SIM__BMS_PICTURE_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "3DButtons.dat file location", F4SimOutputs.SIM__BMS_3DBUTTONS_FILE, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "3DCockpit.dat file location", F4SimOutputs.SIM__BMS_3DCOCKPIT_FILE, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Theater", "Theater name", F4SimOutputs.THEATER__THEATER_NAME, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Campaign directory", F4SimOutputs.THEATER__CAMPAIGN_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Art directory", F4SimOutputs.THEATER__ART_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater UI Sound directory", F4SimOutputs.THEATER__UI_SOUND_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Object directory", F4SimOutputs.THEATER__OBJECT_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater 3D data directory", F4SimOutputs.THEATER__3D_DATA_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Miscellaneous textures directory", F4SimOutputs.THEATER__MISC_TEX_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Sound directory", F4SimOutputs.THEATER__SOUND_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater TacRef directory", F4SimOutputs.THEATER__TAC_REF_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater TacRef Pictures directory", F4SimOutputs.THEATER__TAC_REF_PICS_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Splash directory", F4SimOutputs.THEATER__SPLASH_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Cockpit directory", F4SimOutputs.THEATER__COCKPIT_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Sim Data directory", F4SimOutputs.THEATER__SIM_DATA_DIRECTORY, null, typeof(string)));
            AddF4SimOutput(CreateNewF4SimOutput("Simulation", "Paths", "Theater Subtitles directory", F4SimOutputs.THEATER__SUBTITLES_DIRECTORY, null, typeof(string)));

        }

        public override void Update() { UpdateSimOutputs(); }

        private static void UpdateHsiData(FlightData flightData, out float courseDeviationDecimalDegrees, out float deviationLimitDecimalDegrees)
        {
            deviationLimitDecimalDegrees = flightData.deviationLimit % 180;
            courseDeviationDecimalDegrees = flightData.courseDeviation;

            courseDeviationDecimalDegrees = courseDeviationDecimalDegrees < -90
                ? Common.Math.Util.AngleDelta(Math.Abs(courseDeviationDecimalDegrees), 180) % 180
                : (courseDeviationDecimalDegrees > 90 ? -Common.Math.Util.AngleDelta(courseDeviationDecimalDegrees, 180) % 180 : -courseDeviationDecimalDegrees);

            if (!(Math.Abs(courseDeviationDecimalDegrees) > deviationLimitDecimalDegrees)) return;

            courseDeviationDecimalDegrees = Math.Sign(courseDeviationDecimalDegrees) * deviationLimitDecimalDegrees;
        }

        private static void DetermineWhetherToShowILSCommandBarsAndToFromFlags(FlightData flightData, out bool showToFromFlag, out bool showCommandBars)
        {
            showToFromFlag = true;
            showCommandBars = Math.Abs(flightData.AdiIlsVerPos / Constants.RADIANS_PER_DEGREE) <= flightData.deviationLimit / 5.0f
                              && Math.Abs(flightData.AdiIlsHorPos / Constants.RADIANS_PER_DEGREE) <= flightData.deviationLimit
                              && ((HsiBits) flightData.hsiBits & HsiBits.ADI_GS) != HsiBits.ADI_GS
                              && ((HsiBits) flightData.hsiBits & HsiBits.ADI_LOC) != HsiBits.ADI_LOC
                              && ((HsiBits) flightData.hsiBits & HsiBits.ADI_OFF) != HsiBits.ADI_OFF;

            switch (flightData.navMode)
            {
                case 0: //NavModes.PlsTcn:
                    showToFromFlag = false;
                    break;
                case 1: //NavModes.Tcn:
                    showToFromFlag = true;
                    showCommandBars = false;
                    break;
                case 2: //NavModes.Nav:
                    showToFromFlag = false;
                    showCommandBars = false;
                    break;
                case 3: //NavModes.PlsNav:
                    showToFromFlag = false;
                    break;
            }

            showToFromFlag &= !showCommandBars;
        }

        private void _morseCodeGenerator_UnitTimeTick(object sender, UnitTimeTickEventArgs e) { _markerBeaconMorseCodeState = e.CurrentSignalLineState; }

        public override void Dispose() { Dispose(true); }

        private void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;

            if (_morseCodeGenerator.Sending) _morseCodeGenerator.StopSending();
            _morseCodeGenerator.UnitTimeTick -= _morseCodeGenerator_UnitTimeTick;
            Util.DisposeObject(_morseCodeGenerator);
            _disposed = true;
        }

        ~Falcon4SimSupportModule() { Dispose(false); }
    }
}