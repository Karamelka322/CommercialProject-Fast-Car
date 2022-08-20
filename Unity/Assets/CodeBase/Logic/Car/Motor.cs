using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Motor
    {
        private readonly CarProperty _property;

        private readonly Wheel _rearLeftWheel;
        private readonly Wheel _rearRightWheel;

        public Motor(Wheel rearLeftWheel, Wheel rearRightWheel, CarProperty property)
        {
            _property = property;

            _rearLeftWheel = rearLeftWheel;
            _rearRightWheel = rearRightWheel;
        }

        public void Update() => 
            SetTorque(_property.Axis.x);

        private void SetTorque(in float axis)
        {
            _property.NowMotorTorque = Mathf.Lerp(_property.NowMotorTorque, ConvertAxisToTorque(axis), Time.deltaTime * _property.Acceleration);
            
            _rearLeftWheel.Torque(_property.NowMotorTorque);
            _rearRightWheel.Torque(_property.NowMotorTorque);
        }
        
        private float ConvertAxisToTorque(float axis) =>
            axis > 0 
                ? Mathf.Lerp(0, axis > 0 ? _property.TorqueForward : _property.TorqueBack, axis) 
                : Mathf.Lerp(0, axis > 0 ? -_property.TorqueForward : -_property.TorqueBack, -axis);
    }
}