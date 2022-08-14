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

        public void Move(in float torque, in float nowSpeed)
        {
            if(torque < 0)
                MoveBackwards(torque, nowSpeed);
            else
                MoveForward(torque, nowSpeed);
        }

        private void MoveForward(float torque, float nowSpeed)
        {
            _property.NowMotorTorque = Mathf.Lerp(_property.NowMotorTorque, torque, Time.deltaTime * _property.Acceleration);
            _property.IsStopped = nowSpeed < 1;

            if (_property.IsStopped && !_property.IsStopping)
            {
                _property.IsStopping = true;

                BlockWheels();
            }
            else if(_property.IsStopping)
            {
                UnlockWheels();
            }

            if (!_property.IsStopped)
                _property.IsStopping = false;

            SetTorqueInWheels(_property.NowMotorTorque);
        }

        private void MoveBackwards(float torque, float nowSpeed)
        {
            _property.NowMotorTorque = torque;
            _property.IsStopping = nowSpeed > 1f;

            if (_property.IsStopping && !_property.IsStopped)
            {
                _property.IsStopped = true;

                BlockWheels();
            }
            else
            {
                UnlockWheels();
            }
            
            if(torque > 0) 
                _property.IsStopped = false;
            
            SetTorqueInWheels(_property.NowMotorTorque);
        }

        private void SetTorqueInWheels(float torque)
        {
            _rearLeftWheel.Torque(torque);
            _rearRightWheel.Torque(torque);
        }

        private void BlockWheels()
        {
            _rearLeftWheel.Block();
            _rearRightWheel.Block();
        }

        private void UnlockWheels()
        {
            _rearLeftWheel.Unlock();
            _rearRightWheel.Unlock();
        }
    }
}