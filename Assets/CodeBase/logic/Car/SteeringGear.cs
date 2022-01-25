using UnityEngine;

namespace CodeBase.logic.Car
{
    public class SteeringGear : MonoBehaviour
    {
        [SerializeField] 
        private Wheel _frontLeftWheel;
        
        [SerializeField] 
        private Wheel _frontRightWheel;

        [Space, SerializeField] 
        private int _steerAngle;

        [SerializeField] 
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