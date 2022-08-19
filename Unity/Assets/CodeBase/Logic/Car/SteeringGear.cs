using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class SteeringGear
    {
        private readonly CarProperty _property;

        private readonly Wheel _frontLeftWheel;
        private readonly Wheel _frontRightWheel;

        public SteeringGear(Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property)
        {
            _property = property;

            _frontLeftWheel = frontLeftWheel;
            _frontRightWheel = frontRightWheel;
        }
        
        public void SetAngle(in float axis)
        {
            _property.NowSteeringAngle = Mathf.Lerp(_property.NowSteeringAngle, ConvertAxisToAngle(axis), Time.deltaTime * _property.SpeedSteering);

            _frontLeftWheel.SteerAngle(_property.NowSteeringAngle);
            _frontRightWheel.SteerAngle(_property.NowSteeringAngle);
        }
        
        private float ConvertAxisToAngle(in float axis) => 
            axis > 0 
                ? Mathf.Lerp(0, _property.SteeringAngle, axis) 
                : Mathf.Lerp(0, -_property.SteeringAngle, -axis);
    }
}