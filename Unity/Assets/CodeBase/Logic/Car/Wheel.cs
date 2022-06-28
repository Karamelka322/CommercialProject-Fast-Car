using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

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

        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

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

        public void Block() => 
            _collider.brakeTorque = 100000;

        public void Unlock() => 
            _collider.brakeTorque = 0;

        public void ResetSteerAngle()
        {
            _collider.steerAngle = 0;
            _mesh.rotation = Quaternion.identity;
        }

        private void SetPositionRelativeToCollider()
        {
            _collider.GetWorldPose(out _position, out _rotation);

            _mesh.position = _position;
            _mesh.rotation = _rotation;
        }
    }
}