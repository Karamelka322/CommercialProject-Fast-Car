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
        public float DriftAngle;
        public float SpeedDrift;
        
        [Space]
        public float SpeedStabilization;
        public int MaxRotationX;
        public int MaxRotationZ;

        [HideInInspector] public float NowSteeringAngle;
        [HideInInspector] public float NowMotorTorque;
        [HideInInspector] public bool UseDrift;
        [HideInInspector] public float Slip;
        [HideInInspector] public Vector3 DirectionDrift;
        [HideInInspector] public Vector2 Axis;
    }
}