using System;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [Serializable]
    public class SteeringGear : IReplayHandler
    {
        private readonly CarProperty _property;
        
        private readonly Wheel _frontLeftWheel;
        private readonly Wheel _frontRightWheel;
        
        private float _nowAngle;

        public SteeringGear(Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property)
        {
            _property = property;
            
            _frontLeftWheel = frontLeftWheel;
            _frontRightWheel = frontRightWheel;
        }

        public string name { get; }


        public void Angle(float angle)
        {
            _nowAngle = GetAngle(angle);
            SetSteerAngleInWheels(_nowAngle);
        }

        private float GetAngle(float angle) => 
            Mathf.Lerp(_nowAngle, angle, Time.deltaTime * _property.SpeedRotation);

        private void SetSteerAngleInWheels(float angle)
        {
            _frontLeftWheel.SteerAngle(angle);
            _frontRightWheel.SteerAngle(angle);
        }

        public void OnReplay()
        {
            _nowAngle = 0;
            
            _frontLeftWheel.ResetSteerAngle();
            _frontRightWheel.ResetSteerAngle();
        }
    }
}