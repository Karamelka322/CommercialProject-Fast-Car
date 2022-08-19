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

        //public float SpeedForDrift;
        //public float SpeedStartDrift;
        //public float SpeedStopDrift;
        //public float SteeringForDrift;

        [Space]
        public float DriftAngle;
        public float SpeedDrift;
        public float MinStiffnessForRearWheel;
        public float MinStiffnessForFrontWheel;

        [Space]
        public float SpeedStabilization;
        public int MaxRotationX;
        public int MaxRotationZ;

        [HideInInspector] public float NowSteeringAngle;
        [HideInInspector] public float NowMotorTorque;
        [HideInInspector] public bool UseDrift;
        [HideInInspector] public float Slip;
        [HideInInspector] public Vector3 DirectionDrift;
    }
}