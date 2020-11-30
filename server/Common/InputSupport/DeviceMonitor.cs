using System;

namespace Common.InputSupport
{
    public abstract class DeviceMonitor
    {
        /// <summary>
        ///     Flag to indicate that this object instance has already been disposed
        /// </summary>
        protected volatile bool _isDisposed;

        /// <summary>
        ///     Flag indicating whether this object instance is in the Prepared state (meaning, input has been acquired and
        ///     instance variables have been initialized
        /// </summary>
        protected volatile bool _prepared;

        /// <summary>
        ///     Flag to signal that this object is currently running the Prepare() task, so subsequent calls to Prepare() should
        ///     just wait for the signal to drop
        /// </summary>
        protected volatile bool _preparing;

        protected bool IsDisposed
        {
            get => _isDisposed;
            set => _isDisposed = value;
        }

        protected bool Prepared
        {
            get => _prepared;
            set => _prepared = value;
        }

        protected bool Preparing
        {
            get => _preparing;
            set => _preparing = value;
        }

        /// <summary>
        ///     Standard finalizer, which will call Dispose() if this object is not
        ///     manually disposed.  Ordinarily called only by the garbage collector.
        /// </summary>
        ~DeviceMonitor()
        {
            Dispose();
        }

        /// <summary>
        ///     Public implementation of IDisposable.Dispose().  Cleans up managed
        ///     and unmanaged resources used by this object before allowing garbage collection
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Compares this object to another one to determine if they are equal.  Equality for this type of object simply means
        ///     that the other object must be of the same type and must be monitoring the same DirectInput device.
        /// </summary>
        /// <param name="obj">An object to compare this object to</param>
        /// <returns>
        ///     a boolean, set to true, if the this object is equal to the specified object, and set to false, if they are not
        ///     equal.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;

            return true;
        }

        /// <summary>
        ///     Gets an integer "hash" representation of this object, for use in hashtables.
        /// </summary>
        /// <returns>
        ///     an integer containing a numeric hash of this object's variables.  When two objects are Equal, their hashes
        ///     should be equal as well.
        /// </returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        ///     Gets a string representation of this object.
        /// </summary>
        /// <returns>a String containing a textual representation of this object.</returns>
        public override string ToString()
        {
            return GetType().Name;
        }

        /// <summary>
        ///     Initializes this object's state and sets up objects
        ///     to monitor the device instance that this object is responsible for.
        ///     During preparation, the _preparing flag is raised.  Subsequent concurrent calls to
        ///     Prepare() will simply wait until the _preparing flag is lowered.
        /// </summary>
        protected virtual void Prepare()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Private implementation of Dispose()
        /// </summary>
        /// <param name="disposing">
        ///     flag to indicate if we should actually perform disposal.  Distinguishes the private method
        ///     signature from the public signature.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    //dispose of managed resources here
                }
            }
            // Code to dispose the un-managed resources of the class
            _isDisposed = true;
        }
    }
}