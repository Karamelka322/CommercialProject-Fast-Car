using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour
    {
        [SerializeField] 
        private Transform _mesh;
        
        [SerializeField] 
        private WheelCollider _collider;

        private IUpdatable _updatable;
        
        private Vector3 _position;
        private Quaternion _rotation;

        public void Construct(IUpdatable updatable) => 
            _updatable = updatable;

        private void Start() => 
            _updatable.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updatable.OnUpdate -= OnUpdate;

        private void OnUpdate() => 
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