using System;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [Serializable]
    public class CarProperty
    {
        public int MaxSpeed;
        
        [Space]
        public int PowerForward;
        public int PowerBackwards;
        public int SpeedAcceleration;

        [Space]
        public int SteerAngle;
        public int SpeedRotation;
    }
}