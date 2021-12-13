using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FalconBmsUniversalServer.Messages;

namespace FalconBmsUniversalServer
{
    internal class ApPanelSwitchHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("ApPanelSwitchHandler");

        private static readonly Dictionary<string, string> CallbacksForButtons = new Dictionary<string, string>
        {
            {"roll-ap-up", "SimLeftAPUp"},
            {"roll-ap-mid", "SimLeftAPMid"},
            {"roll-ap-down", "SimLeftAPDown"},

            {"pitch-ap-up", "SimRightAPUp"},
            {"pitch-ap-mid", "SimRightAPMid"},
            {"pitch-ap-down", "SimRightAPDown"},

            {"master-arm-arm", "SimArmMasterArm"},
            {"master-arm-safe", "SimSafeMasterArm"},
            {"master-arm-sim", "SimSimMasterArm"},

            {"laser-arm-on", "SimLaserArmOn"},
            {"laser-arm-off", "SimLaserArmOff"}
        };

        private readonly CallbackSender _callbackSender;

        public ApPanelSwitchHandler(CallbackSender callbackSender)
        {
            _callbackSender = callbackSender;
        }

        public Task Handle(CockpitSwitchMessage message)
        {
            if(CallbacksForButtons.TryGetValue(message.switch, out var callback)) {
                 Logger.Debug("cockpit-switch: {0}:{1}", message.type, message.switch);
                _callbackSender.SendCallback(callback);
            }
        }
    }
}