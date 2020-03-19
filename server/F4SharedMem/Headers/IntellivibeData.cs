using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct IntellivibeData
    {
        [MarshalAs(UnmanagedType.U1)]
        public byte AAMissileFired; // how many AA missiles fired.

        [MarshalAs(UnmanagedType.U1)]
        public byte AGMissileFired; // how many maveric/rockets fired

        [MarshalAs(UnmanagedType.U1)]
        public byte BombDropped; // how many bombs dropped

        [MarshalAs(UnmanagedType.U1)]
        public byte FlareDropped; // how many flares dropped

        [MarshalAs(UnmanagedType.U1)]
        public byte ChaffDropped; // how many chaff dropped

        [MarshalAs(UnmanagedType.U1)]
        public byte BulletsFired; // how many bullets shot

        [MarshalAs(UnmanagedType.I4)]
        public int CollisionCounter; // Collisions

        [MarshalAs(UnmanagedType.U1)]
        public bool IsFiringGun; // gun is firing

        [MarshalAs(UnmanagedType.U1)]
        public bool IsEndFlight; // Ending the flight from 3d

        [MarshalAs(UnmanagedType.U1)]
        public bool IsEjecting; // we've ejected

        [MarshalAs(UnmanagedType.U1)]
        public bool In3D; // In 3D?

        [MarshalAs(UnmanagedType.U1)]
        public bool IsPaused; // sim paused?

        [MarshalAs(UnmanagedType.U1)]
        public bool IsFrozen; // sim frozen?

        [MarshalAs(UnmanagedType.U1)]
        public bool IsOverG; // are G limits being exceeded?

        [MarshalAs(UnmanagedType.U1)]
        public bool IsOnGround; // are we on the ground

        [MarshalAs(UnmanagedType.U1)]
        public bool IsExitGame; // Did we exit Falcon?

        [MarshalAs(UnmanagedType.R4)]
        public float Gforce; // what gforce we are feeling

        [MarshalAs(UnmanagedType.R4)]
        public float eyex; // where the eye is in relationship to the plane

        [MarshalAs(UnmanagedType.R4)]
        public float eyey; // where the eye is in relationship to the plane

        [MarshalAs(UnmanagedType.R4)]
        public float eyez; // where the eye is in relationship to the plane

        [MarshalAs(UnmanagedType.I4)]
        public int lastdamage; // 1 to 8 depending on quadrant. 

        [MarshalAs(UnmanagedType.R4)]
        public float damageforce; // how big the hit was.

        [MarshalAs(UnmanagedType.I4)]
        public int whendamage;
    }
}
