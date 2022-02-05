using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Motor : MonoBehaviour
    {
        [SerializeField] 
        private Wheel _frontLeftWheel;
        
        [SerializeField] 
        private Wheel _frontRightWheel;

        [Space, SerializeField, UnityEngine.Min(0)] 
        private int _power;

        [SerializeField, UnityEngine.Min(0)]
        private int _speedAcceleration;

        public int Power => _power;
        
        private float _nowTorque;

        public void Torque(float torque)
        {
            _nowTorque = Mathf.Lerp(_nowTorque, torque, Time.deltaTime * _speedAcceleration);
            
            _frontLeftWheel.Torque(_nowTorque);
            _frontRightWheel.Torque(_nowTorque);
        }
    }
}