using System;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [Serializable]
    public class CarProperty
    {
        public int MaxSpeed;
        
        [Space]
        public int TorqueForward;
        public int TorqueBack;
        public int Acceleration;

        [Space]
        public int SteeringAngle;
        public int SpeedSteering;

        [Space] 
        public float SpeedForDrift;
        public float SteeringForDrift;
        public float ForceDrift;

        [Space]
        public int MaxRotationX;
        public int MaxRotationZ;

        [HideInInspector]
        public float NowSteeringAngle;

        [HideInInspector]
        public float NowMotorTorque;

        [HideInInspector] 
        public float Slip;

        [HideInInspector]
        public bool IsStopping;

        [HideInInspector]
        public bool IsStopped;

        [HideInInspector] 
        public Vector3 DirectionDrift;
    }
}