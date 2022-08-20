using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class SteeringGear
    {
        private readonly CarProperty _property;

        private readonly Transform _transform;
        
        private readonly Wheel _frontLeftWheel;
        private readonly Wheel _frontRightWheel;

        public SteeringGear(Transform transform, Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property)
        {
            _transform = transform;
            _property = property;

            _frontLeftWheel = frontLeftWheel;
            _frontRightWheel = frontRightWheel;
        }

        public void Update()
        {
            if (_property.UseDrift)
                SetAngle(_property.DirectionDrift, _property.Axis.y);
            else
                SetAngle(_property.Axis.y);
        }

        private void SetAngle(in float axis)
        {
            _property.NowSteeringAngle = Mathf.Lerp(_property.NowSteeringAngle, ConvertAxisToAngle(axis), Time.deltaTime * _property.SpeedSteering);

            _frontLeftWheel.SteerAngle(_property.NowSteeringAngle);
            _frontRightWheel.SteerAngle(_property.NowSteeringAngle);
        }

        private void SetAngle(in Vector3 direction, in float axis)
        {
            float nextAngle = ConvertDirectionToAngle(direction, axis);
            _property.NowSteeringAngle = Mathf.Lerp(_property.NowSteeringAngle, nextAngle, Time.deltaTime * _property.SpeedSteering);

            _frontLeftWheel.SteerAngle(_property.NowSteeringAngle);
            _frontRightWheel.SteerAngle(_property.NowSteeringAngle);
        }

        private float ConvertDirectionToAngle(Vector3 direction, float axis) => 
            Mathf.Clamp(Vector3.Angle(direction, _transform.forward) * (axis >= 0 ? -1 : 1), -_property.SteeringAngle, _property.SteeringAngle);

        private float ConvertAxisToAngle(in float axis) => 
            axis > 0 
                ? Mathf.Lerp(0, _property.SteeringAngle, axis) 
                : Mathf.Lerp(0, -_property.SteeringAngle, -axis);
    }
}