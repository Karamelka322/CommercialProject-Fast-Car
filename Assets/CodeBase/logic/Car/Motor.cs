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
        private int _power;
        
        [SerializeField, Min(0)]
        private int _speedAcceleration;

        public int Power => _power;
        private float _nowTorque;

        public void Torque(float torque)
        {
            _nowTorque = GetTorque(torque);
            SetTorqueInWheels(_nowTorque);
        }

        private float GetTorque(float torque) => 
            torque != 0? Mathf.Lerp(_nowTorque, torque, Time.deltaTime * _speedAcceleration) : 0;

        private void SetTorqueInWheels(float torque)
        {
            _rearLeftWheel.Torque(torque);
            _rearRightWheel.Torque(torque);
        }

        public void OnReplay()
        {
            _nowTorque = 0;
            SetTorqueInWheels(_nowTorque);
            
            _rearLeftWheel.Block();
            _rearRightWheel.Block();
        }

        public void OnEnabledPause() { }

        public void OnDisabledPause()
        {
            _rearLeftWheel.Unlock();
            _rearRightWheel.Unlock();
        }
    }
}