using CodeBase.Logic.World;
using CodeBase.Services.Pause;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Motor), typeof(SteeringGear), typeof(Rigidbody))]
    public class Car : MonoBehaviour, IPauseHandler
    {
        [SerializeField] 
        private Motor _motor;

        [SerializeField] 
        private SteeringGear _steeringGear;

        [SerializeField]
        private Point _centerOfMass;

        [SerializeField] 
        private Rigidbody _rigidbody;

        public float Speed => _rigidbody.velocity.magnitude;

        private Vector3 _velocity;
        private Vector3 _angularVelocity;
        
        private void Awake() => 
            _rigidbody.centerOfMass = _centerOfMass.LocalPosition;
        
        public void Movement(float normalizedValue) => 
            _motor.Torque(ConvertNormalizedValueToTorque(normalizedValue));

        public void Rotation(float normalizedValue) => 
            _steeringGear.Angle(ConvertNormalizedValueToAngle(normalizedValue));

        private float ConvertNormalizedValueToTorque(float normalizedValue) => 
            normalizedValue > 0 ? Mathf.Lerp(0, _motor.Power, normalizedValue) : Mathf.Lerp(0, -_motor.Power, -normalizedValue);

        private float ConvertNormalizedValueToAngle(float normalizedValue) => 
            normalizedValue > 0 ? Mathf.Lerp(0, _steeringGear.SteerAngle, normalizedValue) : Mathf.Lerp(0, -_steeringGear.SteerAngle, -normalizedValue);

        public void OnEnabledPause()
        {
            _velocity = _rigidbody.velocity;
            _angularVelocity = _rigidbody.angularVelocity;

            _rigidbody.isKinematic = true;
        }

        public void OnDisabledPause()
        {
            _rigidbody.isKinematic = false;

            _rigidbody.velocity = _velocity;
            _rigidbody.angularVelocity = _angularVelocity;
        }
    }
}