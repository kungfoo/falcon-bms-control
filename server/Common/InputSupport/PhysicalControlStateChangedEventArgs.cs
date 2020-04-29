using System;

namespace Common.InputSupport
{
    /// <summary>
    ///     Event arguments class for the PhysicalControlStateChanged Event.  Extends EventArgs.
    /// </summary>
    public sealed class PhysicalControlStateChangedEventArgs : EventArgs
    {
        /// <summary>
        ///     a PhysicalControlInfo object representing the physical control whose state change is being signalled
        /// </summary>
        private readonly PhysicalControlInfo _control;

        /// <summary>
        ///     an integer value indicating the current state of the physical control whose state change is being signalled
        /// </summary>
        private readonly int _currentState;

        /// <summary>
        ///     an integer value indicating the previous state of the physical control whose state changed is being signalled
        /// </summary>
        private readonly int _previousState;

        /// <summary>
        ///     Creates a new PhysicalControlStateChangedEventArgs event argument object and sets its required properties in a
        ///     single call.
        /// </summary>
        /// <param name="control">
        ///     a PhysicalControlInfo object representing the physical control (axis, button, Pov, etc) whose
        ///     state change is being signalled by raising the event
        /// </param>
        /// <param name="currentstate">
        ///     an integer value indicating the current state of the physical control whose state change is
        ///     being signalled
        /// </param>
        /// <param name="previousState">
        ///     an integer value indicating the previous state of the physical control whose state change
        ///     is being signalled
        /// </param>
        public PhysicalControlStateChangedEventArgs(PhysicalControlInfo control, int currentstate, int previousState)
        {
            _control = control;
            _currentState = currentstate;
            _previousState = previousState;
        }

        /// <summary>
        ///     Gets the PhysicalControlInfo object representing the physical control (axis, button, Pov, etc) whose state change
        ///     is being signalled
        /// </summary>
        public PhysicalControlInfo Control => _control;

        /// <summary>
        ///     Gets an integer value indicating the current state of the physical control whose state change is being signalled.
        ///     For axes, this will
        ///     be a value in the range specified by the DIDeviceMonitor's axisRangeMin and axisRangeMax values.  For Povs, it will
        ///     be a value in the same range as the axis, but can also be set to -1, indicating centered.  For Povs, the
        ///     translation from
        ///     degrees to linear values in the corresponding axis range will already have been performed.  For buttons, this value
        ///     is either zero or one,
        ///     where 0=unpressed and 1=pressed.  Any negative value &lt;1 indicates an error during polling.
        /// </summary>
        public int CurrentState => _currentState;

        /// <summary>
        ///     Gets an integer value indicating the previous state of the physical control whose state change is being signalled.
        ///     For axes, this will
        ///     be a value in the range specified by the DIDeviceMonitor's axisRangeMin and axisRangeMax values.  For Povs, it will
        ///     be a value in the same range as the axis, but can also be set to -1, indicating centered.  For Povs, the
        ///     translation from
        ///     degrees to linear values in the corresponding axis range will already have been performed.  For buttons, this value
        ///     is either zero or one,
        ///     where 0=unpressed and 1=pressed.  Any negative value &lt;1 indicates an error during polling.
        /// </summary>
        public int PreviousState => _previousState;
    }
}