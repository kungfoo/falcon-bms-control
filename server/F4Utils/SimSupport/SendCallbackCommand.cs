using Common.SimSupport;
using F4Utils.Process;

namespace F4Utils.SimSupport
{
    public class SendCallbackCommand : SimCommand
    {
        public string Callback { get; set; }
        public override void Execute() { KeyFileUtils.SendCallbackToFalcon(Callback); }
    }
}