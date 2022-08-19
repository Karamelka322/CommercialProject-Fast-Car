using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Drift
    {
        private readonly CarInfo _info;
        private readonly CarProperty _property;
        private readonly Rigidbody _rigidbody;

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
        
        private float _minAngle;
        private float _maxAngle;

        public Drift(Rigidbody rigidbody, Wheel rearLeftWheel, Wheel rearRightWheel,
            Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property, CarInfo info)
        {
            _property = property;
            _rigidbody = rigidbody;
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

        public void Enable()
        {
            _property.DirectionDrift = _rigidbody.transform.forward;
            _property.UseDrift = true;

            _minAngle = _rigidbody.transform.localEulerAngles.y - _property.DriftAngle;
            _maxAngle = _rigidbody.transform.localEulerAngles.y + _property.DriftAngle;
        }

        public void Disable()
        {
            _property.UseDrift = false;
            _property.Slip = 1;

            StopDrawingTrails();

            _frontLeftFriction.stiffness = _frontLeftStiffness;
            _frontLeftWheel.Collider.sidewaysFriction = _frontLeftFriction;
                
            _frontRightFriction.stiffness = _frontRightStiffness;
            _frontRightWheel.Collider.sidewaysFriction = _frontRightFriction;
                
            _rearLeftFriction.stiffness = _rearLeftStiffness;
            _rearLeftWheel.Collider.sidewaysFriction = _rearLeftFriction;
                
            _rearRightFriction.stiffness = _rearRightStiffness;
            _rearRightWheel.Collider.sidewaysFriction = _rearRightFriction;
        }

        public void Update()
        {
            float steeringAngle = _property.NowSteeringAngle < 0 ? -_property.NowSteeringAngle : _property.NowSteeringAngle;
            _property.Slip = Mathf.Clamp01(((steeringAngle / _property.SteeringAngle) - 1) * -1);
            
            Rotation();
            DrawingTrails();
            UpdateFrictions();

            /*_property.DirectionDrift = _rigidbody.transform.forward;
                
            _minAngle = _rigidbody.transform.localEulerAngles.y - 50;
            _maxAngle = _rigidbody.transform.localEulerAngles.y + 50;*/
        }

        private void Rotation()
        {
            float nextAngle = _property.NowSteeringAngle >= 0 ? _maxAngle : _minAngle;

            Vector3 rotation = _rigidbody.transform.localEulerAngles;
            rotation.x = _rigidbody.transform.localEulerAngles.x;
            rotation.y = Mathf.Clamp(nextAngle, _minAngle, _maxAngle);
            rotation.z = _rigidbody.transform.localEulerAngles.z;
            
            Quaternion nextRotation = Quaternion.Euler(rotation);
            
            _rigidbody.transform.localRotation = Quaternion.Lerp(_rigidbody.transform.localRotation, nextRotation, Time.deltaTime * _property.SpeedDrift);
        }

        private void DrawingTrails()
        {
            if (_frontLeftWheel.Collider.isGrounded)
                _frontLeftWheel.Trail.StartDrawing();
            else
                _frontLeftWheel.Trail.StopDrawing();

            if (_frontRightWheel.Collider.isGrounded)
                _frontRightWheel.Trail.StartDrawing();
            else
                _frontRightWheel.Trail.StopDrawing();

            if (_rearLeftWheel.Collider.isGrounded)
                _rearLeftWheel.Trail.StartDrawing();
            else
                _rearLeftWheel.Trail.StopDrawing();

            if(_rearRightWheel.Collider.isGrounded)
                _rearRightWheel.Trail.StartDrawing();
            else
                _rearRightWheel.Trail.StopDrawing();
        }

        private void StopDrawingTrails()
        {
            _frontLeftWheel.Trail.StopDrawing();
            _frontRightWheel.Trail.StopDrawing();
            _rearLeftWheel.Trail.StopDrawing();
            _rearRightWheel.Trail.StopDrawing();
        }

        private void UpdateFrictions()
        {
            UpdateFriction(_frontLeftWheel, ref _frontLeftFriction, _frontLeftStiffness, _property.MinStiffnessForFrontWheel, _property);
            UpdateFriction(_frontRightWheel, ref _frontRightFriction, _frontRightStiffness, _property.MinStiffnessForFrontWheel, _property);
            UpdateFriction(_rearLeftWheel, ref _rearLeftFriction, _rearLeftStiffness, _property.MinStiffnessForRearWheel, _property);
            UpdateFriction(_rearRightWheel, ref _rearRightFriction, _rearRightStiffness, _property.MinStiffnessForRearWheel, _property);
        }

        private static void UpdateFriction(Wheel wheel, ref WheelFrictionCurve sidewaysFriction,
            in float startSidewaysStiffness, in float minStiffness, in CarProperty property)
        {
            float newStiffness = Mathf.Clamp(startSidewaysStiffness * property.Slip, minStiffness, startSidewaysStiffness);
            
            sidewaysFriction.stiffness = newStiffness;
            
            wheel.Collider.sidewaysFriction = sidewaysFriction;
        }

        private static void SetFriction(in Wheel wheel, out WheelFrictionCurve sidewaysFriction, out float sidewaysStiffness)
        {
            sidewaysFriction = wheel.Collider.sidewaysFriction;
            sidewaysStiffness = wheel.Collider.sidewaysFriction.stiffness;
        }
    }
}