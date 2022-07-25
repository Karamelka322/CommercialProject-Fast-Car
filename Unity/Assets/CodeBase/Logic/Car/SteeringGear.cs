using System.ComponentModel;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class SteeringGear : MonoBehaviour, IReplayHandler
    {
        [SerializeField] 
        private Wheel _frontLeftWheel;

        [SerializeField] 
        private Wheel _frontRightWheel;

        [Space, SerializeField, Min(0)] 
        private int _steerAngle;

        [SerializeField, Min(0)] 
        private int _speedRotation;

        public int SteerAngle
        {
            get => _steerAngle;
            
            [Editor]
            set => _steerAngle = value;
        }

        public int SpeedRotation
        {
            get => _speedRotation;
            
            [Editor]
            set => _speedRotation = value;
        }

        public Wheel FrontLeftWheel => _frontLeftWheel;
        public Wheel FrontRightWheel => _frontRightWheel;

        private float _nowAngle;
        
        public void Angle(float angle)
        {
            _nowAngle = GetAngle(angle);
            SetSteerAngleInWheels(_nowAngle);
        }

        private float GetAngle(float angle) => 
            Mathf.Lerp(_nowAngle, angle, Time.deltaTime * _speedRotation);

        private void SetSteerAngleInWheels(float angle)
        {
            FrontLeftWheel.SteerAngle(angle);
            FrontRightWheel.SteerAngle(angle);
        }

        public void OnReplay()
        {
            _nowAngle = 0;
            
            FrontLeftWheel.ResetSteerAngle();
            FrontRightWheel.ResetSteerAngle();
        }
    }
}