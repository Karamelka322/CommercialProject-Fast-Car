using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Drift
    {
        private readonly Rigidbody _rigidbody;
        private readonly CarProperty _property;
        private readonly CarInfo _info;

        private WheelCollider _frontLeftWheel;
        private WheelCollider _frontRightWheel;
        private WheelCollider _rearLeftWheel;
        private WheelCollider _rearRightWheel;
        
        private WheelFrictionCurve _frontLeftFriction;
        private WheelFrictionCurve _frontRightFriction;
        private WheelFrictionCurve _rearLeftFriction;
        private WheelFrictionCurve _rearRightFriction;

        private readonly float _frontLeftStiffness;
        private readonly float _frontRightStiffness;
        private readonly float _rearLeftStiffness;
        private readonly float _rearRightStiffness;

        private Vector3 _steeringAngle;

        public Drift(Rigidbody rigidbody, Wheel rearLeftWheel, Wheel rearRightWheel, Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property, CarInfo info)
        {
            _rigidbody = rigidbody;
            _property = property;
            _info = info;
            
            SetFriction(frontLeftWheel, out _frontLeftWheel, out _frontLeftFriction, out _frontLeftStiffness);
            SetFriction(frontRightWheel, out _frontRightWheel, out _frontRightFriction, out _frontRightStiffness);
            SetFriction(rearLeftWheel, out _rearLeftWheel, out _rearLeftFriction, out _rearLeftStiffness);
            SetFriction(rearRightWheel, out _rearRightWheel, out _rearRightFriction, out _rearRightStiffness);
        }

        public void UpdateSlip()
        {
            float steeringAngle = _property.NowSteeringAngle < 0 ? -_property.NowSteeringAngle : _property.NowSteeringAngle;
            
            if(_info.Speed > _property.SpeedForDrift && steeringAngle > _property.SteeringForDrift)
            {
                _property.Slip = Mathf.Clamp01(((steeringAngle / _property.SteeringAngle) - 1) * -1);

                _steeringAngle.y = _property.NowSteeringAngle;
                _property.DirectionDrift = Quaternion.Euler(_steeringAngle) * _rigidbody.transform.forward;
                
                //_rigidbody.AddRelativeForce((_property.DirectionDrift + _rigidbody.transform.forward) * _property.ForceDrift, ForceMode.Force);
            }
            else
            {
                _property.Slip = 1;
            }
            
            UpdateFriction(ref _frontLeftWheel, ref _frontLeftFriction, _frontLeftStiffness, _property);
            UpdateFriction(ref _frontRightWheel, ref _frontRightFriction, _frontRightStiffness, _property);
            UpdateFriction(ref _rearLeftWheel, ref _rearLeftFriction, _rearLeftStiffness, _property);
            UpdateFriction(ref _rearRightWheel, ref _rearRightFriction, _rearRightStiffness, _property);
        }
        
        private static void UpdateFriction(ref WheelCollider collider, ref WheelFrictionCurve sidewaysFriction,
            in float startSidewaysStiffness, in CarProperty property)
        {
            sidewaysFriction.stiffness = Mathf.Clamp(startSidewaysStiffness * property.Slip, 0.1f, 1);
            collider.sidewaysFriction = sidewaysFriction;
        }
        
        private static void SetFriction(in Wheel wheel, out WheelCollider collider,
            out WheelFrictionCurve sidewaysFriction, out float sidewaysStiffness)
        {
            collider = wheel.Collider;

            sidewaysFriction = wheel.Collider.sidewaysFriction;
            sidewaysStiffness = wheel.Collider.sidewaysFriction.stiffness;
        }
    }
}