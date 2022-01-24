using UnityEngine;

namespace CodeBase.logic.Player
{
    public class SteeringGear : MonoBehaviour
    {
        [SerializeField] 
        private Wheel _frontLeftWheel;
        
        [SerializeField] 
        private Wheel _frontRightWheel;

        [Space, SerializeField] 
        private MaxMinValue _steerAngle;

        [SerializeField] 
        private float _speed;

        private float _nowAngle;
        
        public void Angle(float normalizedValue)
        {
            float angle = ConvertNormalizedValueToAngle(normalizedValue);
            _nowAngle = Mathf.Lerp(_nowAngle, angle, Time.deltaTime * _speed);
            
            _frontLeftWheel.SteerAngle(_nowAngle);
            _frontRightWheel.SteerAngle(_nowAngle);
        }

        private float ConvertNormalizedValueToAngle(float normalizedValue) => 
            normalizedValue > 0 ? Mathf.Lerp(0, _steerAngle.Max, normalizedValue) : Mathf.Lerp(0, _steerAngle.Min, -normalizedValue);
    }
}