using System;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [Serializable]
    public class CarController
    {
        private CarProperty _property;
        private CarInfo _info;

        private readonly Motor _motor;
        private readonly SteeringGear _steeringGear;
        private readonly Drift _drift;
        private readonly Stabilization _stabilization;

        private readonly Rigidbody _rigidbody;

        public CarController(Rigidbody rigidbody, Wheel rearLeftWheel, Wheel rearRightWheel, Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property, CarInfo info)
        {
            _rigidbody = rigidbody;
            _property = property;
            _info = info;

            _motor = new Motor(rearLeftWheel, rearRightWheel, property);
            _steeringGear = new SteeringGear(frontLeftWheel, frontRightWheel, property, info);
            _drift = new Drift(rigidbody, rearLeftWheel, rearRightWheel, frontLeftWheel, frontRightWheel, property, info);
            _stabilization = new Stabilization(rigidbody.transform, property);
        }
        
        public void Movement(float axis)
        {
            if(_info.IsGrounded == false)
                _stabilization.Stabilize();
            
            if(_rigidbody.velocity.magnitude > _property.MaxSpeed)
                SpeedLimit();
            
            _drift.UpdateSlip();
            _motor.Move(ConvertAxisToTorque(axis), _rigidbody.velocity.magnitude);
        }

        public void Rotation(float axis) => 
            _steeringGear.Angle(ConvertAxisToAngle(axis));
        
        private void SpeedLimit() => 
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _property.MaxSpeed);
        
        private float ConvertAxisToTorque(float axis) =>
            axis > 0 
                ? Mathf.Lerp(0, axis > 0 ? _property.TorqueForward : _property.TorqueBack, axis) 
                : Mathf.Lerp(0, axis > 0 ? -_property.TorqueForward : -_property.TorqueBack, -axis);

        private float ConvertAxisToAngle(float axis) => 
            axis > 0 
                ? Mathf.Lerp(0, _property.SteeringAngle, axis) 
                : Mathf.Lerp(0, -_property.SteeringAngle, -axis);
    }
}