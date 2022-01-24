using UnityEngine;

namespace CodeBase.logic.Player
{
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour
    {
        [SerializeField] 
        private Transform _transform;
        
        [SerializeField] 
        private WheelCollider _collider;

        public void Torque(float speed)
        {
            _collider.motorTorque = speed;
            _transform.Rotate(speed,0,0, Space.Self);            
        }

        public void SteerAngle(float angle)
        {
            _collider.steerAngle = angle;
            _transform.eulerAngles = Vector3.up * angle;
        }
    }
}