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
        
        private readonly CarProperty _property;
        

#if UNITY_EDITOR
        
        [ShowInInspector]
        public float Speed => Application.isPlaying ? _rigidbody.velocity.magnitude : 0;
        
        [PropertySpace, ShowInInspector]
        public float SteeringAngle => Application.isPlaying ? _property.NowSteeringAngle : 0;

        [ShowInInspector]
        public float MotorTorque => Application.isPlaying ? _property.NowMotorTorque : 0;

        [PropertySpace, ShowInInspector] 
        public float DriftSlip => Application.isPlaying ? _property.Slip : 0;

        [ShowInInspector] 
        public Vector3 DirectionDrift => Application.isPlaying ? _property.DirectionDrift : Vector3.zero;

        [PropertySpace, ShowInInspector]
        public bool IsGrounded => Application.isPlaying && (_frontRightWheel.Collider.isGrounded || _frontLeftWheel.Collider.isGrounded || 
                                                            _rearRightWheel.Collider.isGrounded || _rearLeftWheel.Collider.isGrounded);

#else
        
        public float Speed => _rigidbody.velocity.magnitude;

        public float SterringAngle => _property.NowSteeringAngle;

        public float MotorTorque => _property.NowMotorTorque;

        public float DriftSlip => _property.Slip;
        public Vector3 DirectionDrift => _property.DirectionDrift; 

        public bool IsGrounded => _frontRightWheel.Collider.isGrounded || _frontLeftWheel.Collider.isGrounded
                                 || _rearRightWheel.Collider.isGrounded || _rearLeftWheel.Collider.isGrounded;
        
#endif

        public CarInfo(Rigidbody rigidbody, Wheel frontRightWheel, Wheel rearRightWheel, Wheel frontLeftWheel,
            Wheel rearLeftWheel, CarProperty property)
        {
            _rigidbody = rigidbody;
            
            _frontRightWheel = frontRightWheel;
            _rearRightWheel = rearRightWheel;
            _frontLeftWheel = frontLeftWheel;
            _rearLeftWheel = rearLeftWheel;
            
            _property = property;
        }
    }
}