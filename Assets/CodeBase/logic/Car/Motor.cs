using UnityEngine;

namespace CodeBase.logic.Car
{
    public class Motor : MonoBehaviour
    {
        [SerializeField] 
        private Wheel _frontLeftWheel;
        
        [SerializeField] 
        private Wheel _frontRightWheel;

        [Space, SerializeField] 
        private int _power;

        [SerializeField]
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