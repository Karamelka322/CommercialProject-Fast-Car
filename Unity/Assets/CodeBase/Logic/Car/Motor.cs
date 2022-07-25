using System.ComponentModel;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Motor : MonoBehaviour, IPauseHandler, IReplayHandler
    {
        [SerializeField] 
        private Wheel _rearLeftWheel;
        
        [SerializeField] 
        private Wheel _rearRightWheel;
        
        [Space, SerializeField, Min(0)] 
        private int _powerForward;
        
        [SerializeField, Min(0)] 
        private int _powerBackwards;
        
        [SerializeField, Min(0)]
        private int _speedAcceleration;

        public Wheel RearLeftWheel => _rearLeftWheel;
        public Wheel RearRightWheel => _rearRightWheel;

        public int PowerForward
        {
            get => _powerForward;
            
            [Editor]
            set => _powerForward = value;
        }
        
        public int PowerBackwards
        {
            get => _powerBackwards;
            
            [Editor]
            set => _powerBackwards = value;
        }

        public int Acceleration
        {
            get => _speedAcceleration;
            
            [Editor]
            set => _speedAcceleration = value;
        }

        private float _nowTorque;
        private bool _isStopping;
        private bool _isStopped;

        public void Move(in float torque, in float nowSpeed)
        {
            if(torque < 0)
                MoveBackwards(torque, nowSpeed);
            else
                MoveForward(torque, nowSpeed);
        }

        private void MoveForward(float torque, float nowSpeed)
        {
            _nowTorque = Mathf.Lerp(_nowTorque, torque, Time.deltaTime * _speedAcceleration);
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