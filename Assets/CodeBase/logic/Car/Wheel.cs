using CodeBase.Infrastructure;
using CodeBase.Services.Pause;
using CodeBase.Services.Update;
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

        private IUpdateService _updateService;

        private Vector3 _position;
        private Quaternion _rotation;

        public void Construct(IUpdateService updateService) => 
            _updateService = updateService;

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

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