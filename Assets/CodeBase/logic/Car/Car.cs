using UnityEngine;

namespace CodeBase.logic.Car
{
    [RequireComponent(typeof(Motor), typeof(SteeringGear), typeof(Rigidbody))]
    public class Car : MonoBehaviour
    {
        [SerializeField] 
        private Motor _motor;

        [SerializeField] 
        private SteeringGear _steeringGear;

        [SerializeField]
        private Point _centerOfMass;

        [SerializeField] 
        private Rigidbody _rigidbody;

        private void Awake() => 
            _rigidbody.centerOfMass = _centerOfMass.WorldPosition;
        
        public void Movement(float normalizedValue) => 
            _motor.Torque(ConvertNormalizedValueToTorque(normalizedValue));

        public void Rotation(float normalizedValue) => 
            _steeringGear.Angle(ConvertNormalizedValueToAngle(normalizedValue));

        private float ConvertNormalizedValueToTorque(float normalizedValue) => 
            Mathf.Lerp(0, _motor.Power, normalizedValue);

        private float ConvertNormalizedValueToAngle(float normalizedValue) => 
            normalizedValue > 0 ? Mathf.Lerp(0, _steeringGear.SteerAngle, normalizedValue) : Mathf.Lerp(0, -_steeringGear.SteerAngle, -normalizedValue);
    }
}