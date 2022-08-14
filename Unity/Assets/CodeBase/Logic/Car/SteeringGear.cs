using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class SteeringGear
    {
        private readonly CarProperty _property;
        private readonly CarInfo _info;

        private readonly Wheel _frontLeftWheel;
        private readonly Wheel _frontRightWheel;

        public SteeringGear(Wheel frontLeftWheel, Wheel frontRightWheel, CarProperty property, CarInfo info)
        {
            _property = property;
            _info = info;

            _frontLeftWheel = frontLeftWheel;
            _frontRightWheel = frontRightWheel;
        }

        public void Angle(float angle)
        {
            _property.NowSteeringAngle = GetAngle(angle);
            SetSteerAngleInWheels(_info.SteeringAngle);
        }

        private float GetAngle(float angle) => 
            Mathf.Lerp(_info.SteeringAngle, angle, Time.deltaTime * _property.SpeedSteering);

        private void SetSteerAngleInWheels(float angle)
        {
            _frontLeftWheel.SteerAngle(angle);
            _frontRightWheel.SteerAngle(angle);
        }
    }
}