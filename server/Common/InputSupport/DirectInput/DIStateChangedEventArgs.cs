using System;
using SlimDX.DirectInput;

namespace Common.InputSupport.DirectInput
{
    public class DIStateChangedEventArgs : EventArgs
    {
        public DIStateChangedEventArgs()
        {
        }

        public DIStateChangedEventArgs(JoystickState newState, JoystickState previousState)
        {
            NewState = newState;
            PreviousState = previousState;
        }

        public JoystickState NewState { get; }

        public JoystickState PreviousState { get; }
    }
}