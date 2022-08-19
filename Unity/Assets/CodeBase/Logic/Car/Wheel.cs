using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour, IReplayHandler, IPauseHandler
    {
        [SerializeField] 
        private Transform _mesh;
        
        [SerializeField] 
        private WheelCollider _collider;

        [SerializeField] 
        private WheelTrail _trail;

        public WheelCollider Collider => _collider;
        public WheelTrail Trail => _trail;
        
        private IUpdateService _updateService;

        private Vector3 _position;
        private Quaternion _rotation;
        
        private Vector3 _startRotation;

        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
            _startRotation = TransformUtils.GetInspectorRotation(_mesh);
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
        
        public void OnReplay()
        {
            _collider.motorTorque = 0;
            
            _collider.steerAngle = 0;
            _mesh.rotation = Quaternion.identity;
            
            _collider.brakeTorque = 100000;
        }

        private void SetPositionRelativeToCollider()
        {
            _collider.GetWorldPose(out _position, out _rotation);

            _mesh.position = _position;
            _mesh.rotation = _rotation;

            if (_startRotation != Vector3.zero)
                FlipMesh();
        }

        public void OnEnabledPause() { }

        public void OnDisabledPause() => 
            _collider.brakeTorque = 0;

        private void FlipMesh() => 
            TransformUtils.SetInspectorRotation(_mesh, TransformUtils.GetInspectorRotation(_mesh) + _startRotation);
    }
}