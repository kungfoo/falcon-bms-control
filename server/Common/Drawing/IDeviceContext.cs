using System;
using System.Security.Permissions;

namespace Common.Drawing
{
    /// <summary>Defines methods for obtaining and releasing an existing handle to a Windows device context.</summary>
    public interface IDeviceContext : IDisposable
    {
        /// <summary>Returns the handle to a Windows device context.</summary>
        /// <returns>An <see cref="T:System.IntPtr" /> representing the handle of a device context.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        IntPtr GetHdc();

        /// <summary>Releases the handle of a Windows device context.</summary>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        void ReleaseHdc();
    }
}