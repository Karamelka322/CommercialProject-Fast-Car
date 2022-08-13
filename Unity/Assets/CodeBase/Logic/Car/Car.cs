using CodeBase.Logic.World;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class Car : MonoBehaviour, IPauseHandler, IReplayHandler
    {
        [SerializeField] 
        private Rigidbody _rigidbody;

        [SerializeField]
        private Point _centerOfMass;

        [Space, SerializeField] 
        private Wheel _frontLeftWheel;

        [SerializeField] 
        private Wheel _frontRightWheel;

        [SerializeField] 
        private Wheel _rearLeftWheel;

        [SerializeField] 
        private Wheel _rearRightWheel;

        [Space]
        public CarProperty Property;

        [Space] 
        public CarInfo Info;

        private CarController _controller;

        private Vector3 _velocity;
        private Vector3 _angularVelocity;

        private void Awake()
        {
            _controller = new CarController(_rigidbody, _rearLeftWheel, _rearRightWheel, _frontRightWheel, _frontLeftWheel, Property);
            Info = new CarInfo(_rigidbody, _rearLeftWheel, _rearRightWheel, _frontRightWheel, _frontLeftWheel);
            
            _rigidbody.centerOfMass = _centerOfMass.LocalPosition;   
        }

        public void Movement(float axis) => 
            _controller.Movement(axis);

        public void Rotation(float axis) => 
            _controller.Rotation(axis);
        
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