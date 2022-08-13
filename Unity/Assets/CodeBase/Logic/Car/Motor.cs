using System;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [Serializable]
    public class Motor : IPauseHandler, IReplayHandler
    {
        private readonly CarProperty _property;
        
        private readonly Wheel _rearLeftWheel;
        private readonly Wheel _rearRightWheel;
        
        private float _nowTorque;
        private bool _isStopping;
        private bool _isStopped;

        public string name { get; }

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
            _nowTorque = Mathf.Lerp(_nowTorque, torque, Time.deltaTime * _property.SpeedAcceleration);
            _isStopped = nowSpeed < 1;

            if (_isStopped && !_isStopping)
            {
                _isStopping = true;

                BlockWheels();
            }
            else if(_isStopping)
            {
                UnlockWheels();
            }

            if (!_isStopped)
                _isStopping = false;

            SetTorqueInWheels(_nowTorque);
        }

        private void MoveBackwards(float torque, float nowSpeed)
        {
            _nowTorque = torque;
            _isStopping = nowSpeed > 1f;

            if (_isStopping && !_isStopped)
            {
                _isStopped = true;

                BlockWheels();
            }
            else
            {
                UnlockWheels();
            }
            
            if(torque > 0) 
                _isStopped = false;
            
            SetTorqueInWheels(_nowTorque);
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

        public void OnReplay()
        {
            _nowTorque = 0;
            SetTorqueInWheels(_nowTorque);

            BlockWheels();
        }

        public void OnEnabledPause() { }

        public void OnDisabledPause() => 
            UnlockWheels();
    }
}