using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [Serializable]
    public class CarInfo
    {
        private readonly Rigidbody _rigidbody;
        
        private readonly Wheel _frontRightWheel;
        private readonly Wheel _rearRightWheel;
        private readonly Wheel _frontLeftWheel;
        private readonly Wheel _rearLeftWheel;

#if UNITY_EDITOR
        
        [ShowInInspector]
        public float Speed => Application.isPlaying ? _rigidbody.velocity.magnitude : 0;

        [ShowInInspector]
        public float FrontLeftWheelTorque => Application.isPlaying ? _frontLeftWheel.Collider.motorTorque : 0;
        
        [ShowInInspector]
        public float FrontRightWheelTorque => Application.isPlaying ? _frontRightWheel.Collider.motorTorque : 0;
        
        [ShowInInspector]
        public float RearLeftWheelTorque => Application.isPlaying ? _rearLeftWheel.Collider.motorTorque : 0;
        
        [ShowInInspector]
        public float RearRightWheelTorque => Application.isPlaying ? _rearRightWheel.Collider.motorTorque : 0;
        
        [ShowInInspector]
        public bool IsGrounded => Application.isPlaying && (_frontRightWheel.Collider.isGrounded && _frontLeftWheel.Collider.isGrounded && 
                                                            _rearRightWheel.Collider.isGrounded && _rearLeftWheel.Collider.isGrounded);
#else
        
        public float Speed => _rigidbody.velocity.magnitude;
        public float FrontLeftWheelTorque => _frontLeftWheel.Collider.motorTorque;
        public float FrontRightWheelTorque => _frontRightWheel.Collider.motorTorque;
        public float RearLeftWheelTorque => _rearLeftWheel.Collider.motorTorque;
        public float RearRightWheelTorque => _rearRightWheel.Collider.motorTorque;
        public bool IsGrounded => _frontRightWheel.Collider.isGrounded && _frontLeftWheel.Collider.isGrounded && _rearRightWheel.Collider.isGrounded && _rearLeftWheel.Collider.isGrounded;

#endif

        public CarInfo(Rigidbody rigidbody, Wheel frontRightWheel, Wheel rearRightWheel, Wheel frontLeftWheel, Wheel rearLeftWheel)
        {
            _rigidbody = rigidbody;
            _frontRightWheel = frontRightWheel;
            _rearRightWheel = rearRightWheel;
            _frontLeftWheel = frontLeftWheel;
            _rearLeftWheel = rearLeftWheel;
        }
    }
}