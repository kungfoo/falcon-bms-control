using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [Serializable]
    [ComVisible(true)]
    public enum CallbackInvocationBehavior
    {
        KeyUp=-4,
        KeyDown=-2,
        KeyUpAndDown=-1,
        DirectXButtonAssignedInUI=8
    }
}
