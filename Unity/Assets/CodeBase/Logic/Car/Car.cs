using CodeBase.Logic.World;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Motor), typeof(SteeringGear), typeof(Rigidbody))]
    public class Car : MonoBehaviour, IPauseHandler, IReplayHandler
    {
        [SerializeField] 
        private Motor _motor;

        [SerializeField] 
        private SteeringGear _steeringGear;

        [SerializeField]
        private Point _centerOfMass;

        [Space]
        [SerializeField] 
        private Rigidbody _rigidbody;
        
        [Space]
        [SerializeField] 
        private WheelCollider _frontLeftWheel;
        
        [SerializeField] 
        private WheelCollider _frontRightWheel;

        [SerializeField] 
        private WheelCollider _rearLeftWheel;

        [SerializeField] 
        private WheelCollider _rearRightWheel;

        public float Speed => _rigidbody.velocity.magnitude;
        public bool IsGrounded => _frontLeftWheel.isGrounded || _frontRightWheel.isGrounded || _rearLeftWheel.isGrounded || _rearRightWheel.isGrounded;

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

        public void OnReplay()
        {
            _velocity = Vector3.zero;
            _angularVelocity = Vector3.zero;
        }
    }
}