using UnityEngine;

namespace CodeBase.logic.Car
{
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour
    {
        [SerializeField] 
        private Transform _mesh;
        
        [SerializeField] 
        private WheelCollider _collider;

        private Vector3 _position;
        private Quaternion _rotation;
        
        private void Update() => 
            SetPositionRelativeToCollider();

        public void Torque(float torque) => 
            _collider.motorTorque = torque;

        public void SteerAngle(float angle) => 
            _collider.steerAngle = angle;

        private void SetPositionRelativeToCollider()
        {
            _collider.GetWorldPose(out _position, out _rotation);
            
            _mesh.position = _position;
            _mesh.rotation = _rotation;
        }
    }
}