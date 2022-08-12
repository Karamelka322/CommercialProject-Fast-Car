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

        [Space, SerializeField] 
        private Rigidbody _rigidbody;
        
        [Space, SerializeField] 
        private int _maxSpeed;

        public float Speed => _rigidbody.velocity.magnitude;
        public bool IsGrounded => _motor.RearLeftWheel.Collider.isGrounded || _motor.RearRightWheel.Collider.isGrounded || 
                                  _steeringGear.FrontLeftWheel.Collider.isGrounded || _steeringGear.FrontRightWheel.Collider.isGrounded;

        private Vector3 _velocity;
        private Vector3 _angularVelocity;

        private void Awake() => 
            _rigidbody.centerOfMass = _centerOfMass.LocalPosition;

        public void Movement(float axis)
        {
            SpeedLimit();
            
            _motor.Move(ConvertAxisToTorque(axis), _rigidbody.velocity.magnitude);
        }

        private void SpeedLimit() => 
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);

        public void Rotation(float axis) => 
            _steeringGear.Angle(ConvertAxisToAngle(axis));
        
        private float ConvertAxisToTorque(float axis) =>
            axis > 0 
                ? Mathf.Lerp(0, axis > 0 ? _motor.PowerForward : _motor.PowerBackwards, axis) 
                : Mathf.Lerp(0, axis > 0 ? -_motor.PowerForward : -_motor.PowerBackwards, -axis);

        private float ConvertAxisToAngle(float axis) => 
            axis > 0 ? Mathf.Lerp(0, _steeringGear.SteerAngle, axis) : Mathf.Lerp(0, -_steeringGear.SteerAngle, -axis);

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