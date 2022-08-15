using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Drift
    {
        private readonly Rigidbody _rigidbody;
        private readonly CarProperty _property;
        private readonly CarInfo _info;

        private readonly Wheel _frontLeftWheel;
        private readonly Wheel _frontRightWheel;
        private readonly Wheel _rearLeftWheel;
        private readonly Wheel _rearRightWheel;
        
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
            
            _rearLeftWheel = rearLeftWheel;
            _rearRightWheel = rearRightWheel;
            _frontLeftWheel = frontLeftWheel;
            _frontRightWheel = frontRightWheel;

            SetFriction(_frontLeftWheel, out _frontLeftFriction, out _frontLeftStiffness);
            SetFriction(_frontRightWheel, out _frontRightFriction, out _frontRightStiffness);
            SetFriction(_rearLeftWheel, out _rearLeftFriction, out _rearLeftStiffness);
            SetFriction(_rearRightWheel, out _rearRightFriction, out _rearRightStiffness);
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
                
                _frontLeftWheel.Trail.StartDrawing();
                _frontRightWheel.Trail.StartDrawing();
                _rearLeftWheel.Trail.StartDrawing();
                _rearRightWheel.Trail.StartDrawing();
            }
            else
            {
                _property.Slip = 1;
                
                _frontLeftWheel.Trail.StopDrawing();
                _frontRightWheel.Trail.StopDrawing();
                _rearLeftWheel.Trail.StopDrawing();
                _rearRightWheel.Trail.StopDrawing();
            }
            
            UpdateFriction(_frontLeftWheel, ref _frontLeftFriction, _frontLeftStiffness, _property);
            UpdateFriction(_frontRightWheel, ref _frontRightFriction, _frontRightStiffness, _property);
            UpdateFriction(_rearLeftWheel, ref _rearLeftFriction, _rearLeftStiffness, _property);
            UpdateFriction(_rearRightWheel, ref _rearRightFriction, _rearRightStiffness, _property);
        }
        
        private static void UpdateFriction(Wheel wheel, ref WheelFrictionCurve sidewaysFriction,
            in float startSidewaysStiffness, in CarProperty property)
        {
            sidewaysFriction.stiffness = Mathf.Clamp(startSidewaysStiffness * property.Slip, 0.1f, 1);
            wheel.Collider.sidewaysFriction = sidewaysFriction;
        }
        
        private static void SetFriction(in Wheel wheel, out WheelFrictionCurve sidewaysFriction, out float sidewaysStiffness)
        {
            sidewaysFriction = wheel.Collider.sidewaysFriction;
            sidewaysStiffness = wheel.Collider.sidewaysFriction.stiffness;
        }
    }
}