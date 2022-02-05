using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class SteeringGear : MonoBehaviour
    {
        [SerializeField] 
        private Wheel _frontLeftWheel;
        
        [SerializeField] 
        private Wheel _frontRightWheel;

        [Space, SerializeField, UnityEngine.Min(0)] 
        private int _steerAngle;

        [SerializeField, UnityEngine.Min(0)] 
        private int _speedRotation;

        public int SteerAngle => _steerAngle;
        
        private float _nowAngle;
        
        public void Angle(float angle)
        {
            _nowAngle = Mathf.Lerp(_nowAngle, angle, Time.deltaTime * _speedRotation);
            
            _frontLeftWheel.SteerAngle(_nowAngle);
            _frontRightWheel.SteerAngle(_nowAngle);
        }
    }
}