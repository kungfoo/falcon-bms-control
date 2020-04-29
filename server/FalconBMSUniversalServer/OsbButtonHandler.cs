using FalconBmsUniversalServer.Messages;

namespace FalconBmsUniversalServer
{
    internal class OsbButtonHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("OsbButtonHandler");
        public void handle(OsbButtonMessage message)
        {
            Logger.Debug("MFD OSB message received: {0}:{1}:{2}", message.type, message.mfd, message.osb);
        }
    }
}