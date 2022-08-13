using System;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [Serializable]
    public class CarController : IPauseHandler, IReplayHandler
    {
        private CarProperty _property;

        private readonly Motor _motor;
        private readonly SteeringGear _steeringGear;
        private readonly Drift _drift;
        
        private readonly Rigidbody _rigidbody;
        
        public string name { get; }

        public CarController(Rigidbody rigidbody, Wheel rearLeftWheel, Wheel rearRightWheel, Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property)
        {
            _rigidbody = rigidbody;
            _property = property;
            
            _motor = new Motor(rearLeftWheel, rearRightWheel, property);
            _steeringGear = new SteeringGear(frontLeftWheel, frontRightWheel, property);
            _drift = new Drift(property);
        }
        
        public void Movement(float axis) => 
            _motor.Move(ConvertAxisToTorque(axis), _rigidbody.velocity.magnitude);

        public void Rotation(float axis) => 
            _steeringGear.Angle(ConvertAxisToAngle(axis));
        
        private void SpeedLimit() => 
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _property.MaxSpeed);
        
        private float ConvertAxisToTorque(float axis) =>
            axis > 0 
                ? Mathf.Lerp(0, axis > 0 ? _property.PowerForward : _property.PowerBackwards, axis) 
                : Mathf.Lerp(0, axis > 0 ? -_property.PowerForward : -_property.PowerBackwards, -axis);

        private float ConvertAxisToAngle(float axis) => 
            axis > 0 ? Mathf.Lerp(0, _property.SteerAngle, axis) : Mathf.Lerp(0, -_property.SteerAngle, -axis);

        public void OnReplay()
        {
            _motor.OnReplay();
            _steeringGear.OnReplay();
        }

        public void OnEnabledPause() => 
            _motor.OnEnabledPause();

        public void OnDisabledPause() => 
            _motor.OnDisabledPause();
    }
}