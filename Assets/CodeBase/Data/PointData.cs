using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Services.Random
{
    [Serializable]
    public struct PointData
    {
        [BoxGroup("Point")]
        public Vector3 Position;
        
        [BoxGroup("Point")]
        public Quaternion Rotation;

        public PointData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public static bool operator ==(PointData point_1, PointData point_2) => 
            point_1.Position == point_2.Position && point_1.Rotation == point_2.Rotation;

        public static bool operator !=(PointData point_1, PointData point_2) => 
            point_1.Position != point_2.Position || point_1.Rotation != point_2.Rotation;

        public static PointData operator +(PointData point, Vector3 vector)
        {
            point.Position += vector;
            return point;
        }
        
        public static PointData operator -(PointData point, Vector3 vector)
        {
            point.Position -= vector;
            return point;
        }
    }
}