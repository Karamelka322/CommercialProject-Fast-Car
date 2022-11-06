using CodeBase.Logic.World;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class Car : MonoBehaviour, IPauseHandler, IReplayHandler
    {
        [SerializeField] 
        private Rigidbody _rigidbody;

        [Space, SerializeField]
        private Point _centerOfMass;

        [SerializeField] 
        private CarCrash _carCrash;

        [Space, SerializeField] 
        private WheelWrapper _frontLeftWheel;

        [SerializeField] 
        private WheelWrapper _frontRightWheel;

        [SerializeField] 
        private WheelWrapper _rearLeftWheel;

        [SerializeField] 
        private WheelWrapper _rearRightWheel;

        [Space] 
        public CarProperty Property;
        
        [Space] 
        public CarInfo Info;

        private IUpdateService _updateService;

        private Motor _motor;
        private SteeringGear _steeringGear;
        private Drift _drift;
        private Stabilization _stabilization;
        private CarBackup _backup;

        [Inject]
        private void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }
        
        private void Awake()
        {
            Info = new CarInfo(_rigidbody, _rearLeftWheel, _rearRightWheel, _frontRightWheel, _frontLeftWheel, Property);

            _motor = new Motor(_rearLeftWheel, _rearRightWheel, Property);
            _steeringGear = new SteeringGear(transform, _frontLeftWheel, _frontRightWheel, Property);
            _drift = new Drift(transform, _rearLeftWheel, _rearRightWheel, _frontLeftWheel, _frontRightWheel, Property);
            _stabilization = new Stabilization(Property, transform);
            
            _backup = new CarBackup(Vector3.zero, Vector3.zero);

            _rigidbody.centerOfMass = _centerOfMass.LocalPosition;
        }

        private void Start()
        {
            _updateService.OnUpdate += OnUpdate;
            _updateService.OnFixedUpdate += OnFixedUpdate;
        }

        private void OnDisable()
        {
            _updateService.OnUpdate -= OnUpdate;
            _updateService.OnFixedUpdate -= OnFixedUpdate;
        }

        private void OnUpdate()
        {
            if ((Info.Speed * Property.Axis.x < 0 && Property.NowMotorTorque > 0) || (Info.Speed * Property.Axis.x > 0 && _carCrash.Crash))
            {
                BlockWheels();
            }
            else
            {
                UnlockWheels();
            }

            _motor.Update();
            _steeringGear.Update();
            
            if(Info.IsGroundedStrict == false)
                _stabilization.Stabilize();
            
            if(_rigidbody.velocity.magnitude > Property.MaxSpeed)
                SpeedLimit();
            
            if(Property.UseDrift && Info.IsGroundedSoft)
                _drift.Update();
        }

        private void OnFixedUpdate()
        {
            if(Property.UseDrift && Info.IsGroundedSoft)
                _drift.FixedUpdate();
        }

        public void EnableDrift() => 
            _drift.Enable();

        public void DisableDrift() => 
            _drift.Disable();

        public void OnReplay()
        {
            Property.NowMotorTorque = 0;
            Property.NowSteeringAngle = 0;
            
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnEnabledPause()
        {
            _backup.Velocity = _rigidbody.velocity;
            _backup.AngularVelocity = _rigidbody.angularVelocity;
            
            _rigidbody.isKinematic = true;
        }

        public void OnDisabledPause()
        {
            _rigidbody.isKinematic = false;

            _rigidbody.velocity = _backup.Velocity;
            _rigidbody.angularVelocity = _backup.AngularVelocity;
        }

        private void SpeedLimit() => 
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, Property.MaxSpeed);

        private void BlockWheels()
        {
            _frontLeftWheel.Collider.brakeTorque = 100000;
            _frontRightWheel.Collider.brakeTorque = 100000;
            _rearLeftWheel.Collider.brakeTorque = 100000;
            _rearRightWheel.Collider.brakeTorque = 100000;
        }
        
        private void UnlockWheels()
        {
            _frontLeftWheel.Collider.brakeTorque = 0;
            _frontRightWheel.Collider.brakeTorque = 0;
            _rearLeftWheel.Collider.brakeTorque = 0;
            _rearRightWheel.Collider.brakeTorque = 0;
        }

        private class CarBackup
        {
            public Vector3 Velocity;
            public Vector3 AngularVelocity;

            public CarBackup(Vector3 velocity, Vector3 angularVelocity)
            {
                Velocity = velocity;
                AngularVelocity = angularVelocity;
            }
        }
    }
}