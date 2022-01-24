using UnityEngine;

namespace CodeBase.logic.Player
{
    public class Motor : MonoBehaviour
    {
        [SerializeField] 
        private Wheel _frontLeftWheel;
        
        [SerializeField] 
        private Wheel _frontRightWheel;

        [SerializeField] 
        private Wheel _rearLeftWheel;

        [SerializeField] 
        private Wheel _rearRightWheel;

        [Space, SerializeField]
        private float _acceleration;

        [SerializeField] 
        private float _power;
        
        private float _nowTorque;

        public void Torque(float normalizedValue)
        {
            float torque = ConvertNormalizedValueToTorque(normalizedValue);
            _nowTorque = Mathf.Lerp(_nowTorque, torque, Time.deltaTime * _acceleration);
            
            _frontLeftWheel.Torque(_nowTorque);
            _frontRightWheel.Torque(_nowTorque);
            _rearLeftWheel.Torque(_nowTorque);
            _rearRightWheel.Torque(_nowTorque);
        }

        private float ConvertNormalizedValueToTorque(float normalizedValue) => 
            Mathf.Lerp(0, _power, normalizedValue);
    }
}