using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Drift
    {
        private readonly Transform _transform;
        private readonly CarProperty _property;

        private readonly WheelWrapper _frontLeftWheel;
        private readonly WheelWrapper _frontRightWheel;
        private readonly WheelWrapper _rearLeftWheel;
        private readonly WheelWrapper _rearRightWheel;

        private WheelFrictionCurve _frontLeftFriction;
        private WheelFrictionCurve _frontRightFriction;
        private WheelFrictionCurve _rearLeftFriction;
        private WheelFrictionCurve _rearRightFriction;

        private readonly float _frontLeftStiffness;
        private readonly float _frontRightStiffness;
        private readonly float _rearLeftStiffness;
        private readonly float _rearRightStiffness;

        private readonly AnimationCurve _curve;
        
        private Vector3 _steeringAngle;

        private float _minAngle;
        private float _maxAngle;

        public Drift(Transform transform, WheelWrapper rearLeftWheel, WheelWrapper rearRightWheel,
            WheelWrapper frontLeftWheel, WheelWrapper frontRightWheel, CarProperty property)
        {
            _transform = transform;
            _property = property;

            _rearLeftWheel = rearLeftWheel;
            _rearRightWheel = rearRightWheel;
            _frontLeftWheel = frontLeftWheel;
            _frontRightWheel = frontRightWheel;
            
            _curve = AnimationCurve.Linear(0, 0, 1, 1);

            SetFriction(_frontLeftWheel, out _frontLeftFriction, out _frontLeftStiffness);
            SetFriction(_frontRightWheel, out _frontRightFriction, out _frontRightStiffness);
            SetFriction(_rearLeftWheel, out _rearLeftFriction, out _rearLeftStiffness);
            SetFriction(_rearRightWheel, out _rearRightFriction, out _rearRightStiffness);
        }

        public void Enable()
        {
            _property.Slip = 0;
            _property.UseDrift = true;
            _property.DirectionDrift = _transform.forward;

            UpdateFrictions();
        }

        public void Disable()
        {
            _property.Slip = 1;
            _property.UseDrift = false;
            _property.DirectionDrift = Vector3.zero;

            UpdateFrictions();
        }

        public void Update() => 
            UpdateDirectionDrift();

        public void FixedUpdate()
        {
            Vector3 eulerAngles = _transform.localEulerAngles;

            _minAngle = eulerAngles.y - _property.DriftAngle;
            _maxAngle = eulerAngles.y + _property.DriftAngle;
            
            Quaternion nextRotation = Quaternion.AngleAxis(_property.Axis.y >= 0 ? _maxAngle : _minAngle, _transform.up);
            _transform.localRotation = Quaternion.Lerp(_transform.localRotation, nextRotation, _curve.Evaluate(Time.deltaTime * _property.SpeedDrift));
        }

        private void UpdateDirectionDrift()
        {
            _property.DirectionDrift = Vector3.Lerp(_property.DirectionDrift, _transform.forward,
                Time.deltaTime * _property.SpeedDrift);
        }

        private void UpdateFrictions()
        {
            UpdateFriction(_frontLeftWheel, ref _frontLeftFriction, _frontLeftStiffness, _property.Slip);
            UpdateFriction(_frontRightWheel, ref _frontRightFriction, _frontRightStiffness, _property.Slip);
            UpdateFriction(_rearLeftWheel, ref _rearLeftFriction, _rearLeftStiffness, _property.Slip);
            UpdateFriction(_rearRightWheel, ref _rearRightFriction, _rearRightStiffness, _property.Slip);
        }

        private static void UpdateFriction(WheelWrapper wheel, ref WheelFrictionCurve sidewaysFriction,
            in float startSidewaysStiffness, in float slip)
        {
            float newStiffness = startSidewaysStiffness * slip;
            
            sidewaysFriction.stiffness = newStiffness;
            wheel.Collider.sidewaysFriction = sidewaysFriction;
        }

        private static void SetFriction(in WheelWrapper wheel, out WheelFrictionCurve sidewaysFriction, out float sidewaysStiffness)
        {
            sidewaysFriction = wheel.Collider.sidewaysFriction;
            sidewaysStiffness = wheel.Collider.sidewaysFriction.stiffness;
        }
    }
}