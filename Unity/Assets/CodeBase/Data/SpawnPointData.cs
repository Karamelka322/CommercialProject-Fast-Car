using System;

namespace CodeBase.Services.Random
{
    [Serializable]
    public struct SpawnPointData
    {
        public bool IsLocked;
        public PointData Point;

        public SpawnPointData(bool isLocked, PointData point)
        {
            IsLocked = isLocked;
            Point = point;
        }
    }
}