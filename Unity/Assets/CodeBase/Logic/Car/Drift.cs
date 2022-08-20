using UnityEditor;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Drift
    {
        private readonly Transform _transform;
        private readonly CarProperty _property;

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

        public Drift(Transform transform, Wheel rearLeftWheel, Wheel rearRightWheel,
            Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property)
        {
            _transform = transform;
            _property = property;

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

            StopDrawingTrails();
            UpdateFrictions();
        }

        public void Update()
        {
            UpdateDirectionDrift();
            DrawingTrails();
        }

        public void FixedUpdate()
        {
            Vector3 rotation = TransformUtils.GetInspectorRotation(_transform);
            _minAngle = Mathf.Lerp(_minAngle, rotation.y - _property.DriftAngle, Time.deltaTime * _property.SpeedTurningInDrift);
            _maxAngle = Mathf.Lerp(_maxAngle, rotation.y + _property.DriftAngle, Time.deltaTime * _property.SpeedTurningInDrift);

            Rotation();
        }

        private void UpdateDirectionDrift()
        {
            _property.DirectionDrift = Vector3.Lerp(_property.DirectionDrift, _transform.forward,
                Time.deltaTime * _property.SpeedTurningInDrift);
        }

        private void Rotation()
        {
            float nextAngle = _property.Axis.y >= 0 ? _maxAngle : _minAngle;
            Vector3 rotation = TransformUtils.GetInspectorRotation(_transform);
            rotation.y = Mathf.Lerp(rotation.y, Mathf.Clamp(nextAngle, _minAngle, _maxAngle), Time.deltaTime * _property.SpeedStartDrift);
            
            TransformUtils.SetInspectorRotation(_transform, rotation);
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
            UpdateFriction(_frontLeftWheel, ref _frontLeftFriction, _frontLeftStiffness, _property.Slip);
            UpdateFriction(_frontRightWheel, ref _frontRightFriction, _frontRightStiffness, _property.Slip);
            UpdateFriction(_rearLeftWheel, ref _rearLeftFriction, _rearLeftStiffness, _property.Slip);
            UpdateFriction(_rearRightWheel, ref _rearRightFriction, _rearRightStiffness, _property.Slip);
        }

        private static void UpdateFriction(Wheel wheel, ref WheelFrictionCurve sidewaysFriction,
            in float startSidewaysStiffness, in float slip)
        {
            float newStiffness = startSidewaysStiffness * slip;
            
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