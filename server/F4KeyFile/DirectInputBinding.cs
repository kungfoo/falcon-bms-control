using System;
using System.Runtime.InteropServices;
using System.Text;

namespace F4KeyFile
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Serializable]
    public sealed class DirectInputBinding : IBinding
    {
        public DirectInputBinding() { }

        public DirectInputBinding(string callback, int buttonIndex, CallbackInvocationBehavior callbackInvocationBehavior,TriggeringEvent triggeringEvent, int soundId, string description =null)
        {
            Callback = callback != null ? callback.Trim() : null;
            ButtonIndex = buttonIndex;
            CallbackInvocationBehavior = callbackInvocationBehavior;
            BindingType = DirectInputBindingType.Button;
            PovDirection = PovDirections.None;
            TriggeringEvent = triggeringEvent;
            SoundId = soundId;
            Description = description;
        }
        public DirectInputBinding(string callback, int povHatNumber, CallbackInvocationBehavior callbackInvocationBehavior,
                PovDirections povDirection, int soundId, string description = null)
        {
            Callback = callback != null ? callback.Trim() : null;
            POVHatNumber = povHatNumber;
            BindingType = DirectInputBindingType.POVDirection;
            CallbackInvocationBehavior = callbackInvocationBehavior;
            PovDirection = povDirection;
            SoundId = soundId;
            Description = description;
        }

        public int LineNum { get; set; }
        public string Callback { get; set; }
        public int ButtonIndex { get; set; }
        public int POVHatNumber { get; set; }
        public CallbackInvocationBehavior CallbackInvocationBehavior { get; set; }
        public DirectInputBindingType BindingType { get; set; }
        public PovDirections PovDirection { get; set; }
        public TriggeringEvent TriggeringEvent { get; set; }
        public int SoundId { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Callback ?? "SimDoNothing");
            sb.Append(" ");
            sb.Append(BindingType == DirectInputBindingType.POVDirection ? POVHatNumber : ButtonIndex);
            sb.Append(" ");
            sb.Append((int)CallbackInvocationBehavior);
            sb.Append(" ");
            sb.Append((int) BindingType);
            sb.Append(" ");
            if (BindingType == DirectInputBindingType.Button)
            {
                if (TriggeringEvent != 0)
                {
                    sb.Append("0x");
                    sb.Append(TriggeringEvent.ToString("x").TrimStart('0'));
                }
                else
                {
                    sb.Append("0");
                }
            }
            else
            {
                sb.Append((int) PovDirection);
            }
            sb.Append(" ");
            sb.Append("0x0");
            sb.Append(" ");
            sb.Append(SoundId);
            if (Description == null) return sb.ToString();
            sb.Append(" ");
            sb.Append(Description);
            return sb.ToString();
        }

        public static bool TryParse(string input, out DirectInputBinding directInputBinding)
        {
            directInputBinding = null;
            if (input ==null) return false;

            var tokenList = Util.Tokenize(input);
            if (tokenList.Count < 7) return false;

            var callback = tokenList[0];
            if (string.IsNullOrWhiteSpace(callback)) return false;

            int buttonIndexOrPovHatNumber;
            var parsed = Int32.TryParse(tokenList[1], out buttonIndexOrPovHatNumber);
            if (!parsed) return false;

            int callbackInvocationBehavior;
            parsed = Int32.TryParse(tokenList[2], out callbackInvocationBehavior);
            if (!parsed) return false;

            int bindingType;
            parsed = Int32.TryParse(tokenList[3], out bindingType);
            if (!parsed) return false;

            int soundId;
            parsed = Int32.TryParse(tokenList[6], out soundId);
            if (!parsed) return false;

            string description = null;
            if (tokenList.Count >= 8)
            {
                var firstQuote = input.IndexOf("\"", StringComparison.OrdinalIgnoreCase);
                if (firstQuote > 0)
                {
                    description = input.Substring(firstQuote);
                }
                else
                {
                    return false;
                }
            }

            switch ((DirectInputBindingType) bindingType)
            {
                case DirectInputBindingType.Button:
                    uint triggeringEvent;
                    parsed = UInt32.TryParse(tokenList[4], out triggeringEvent);
                    if (!parsed) return false;
                    directInputBinding = new DirectInputBinding(callback, buttonIndexOrPovHatNumber, (CallbackInvocationBehavior)callbackInvocationBehavior, (TriggeringEvent)triggeringEvent, soundId, description);
                    return true;
                case DirectInputBindingType.POVDirection:
                    uint povDirection;
                    parsed = UInt32.TryParse(tokenList[4], out povDirection);
                    if (!parsed) return false;
                    directInputBinding = new DirectInputBinding(callback, buttonIndexOrPovHatNumber, (CallbackInvocationBehavior)callbackInvocationBehavior, (PovDirections)povDirection, soundId, description);
                    return true;
            }
            return false;
        }
    }
}