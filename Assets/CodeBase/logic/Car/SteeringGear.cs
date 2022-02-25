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
        
        public int SteerAngle => _steerAngle;
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