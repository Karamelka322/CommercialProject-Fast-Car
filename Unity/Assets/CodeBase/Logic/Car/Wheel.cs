using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour, IReplayHandler
    {
        [SerializeField] 
        private Transform _mesh;
        
        [SerializeField] 
        private WheelCollider _collider;

        [SerializeField] 
        private WheelTrail _trail;

        public WheelCollider Collider => _collider;
        
        private IUpdateService _updateService;

        private Vector3 _position;
        private Quaternion _rotation;

        private WheelBackup _backup;

        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
            _backup = new WheelBackup(_mesh, _collider);
        }

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            SetPositionRelativeToCollider();

            if (IsDrawingTrail())
                _trail.StartDrawing();
            else
                _trail.StopDrawing();
        }

        public void Torque(float torque) => 
            _collider.motorTorque = torque;

        public void SteerAngle(float angle) => 
            _collider.steerAngle = angle;

        public void OnReplay()
        {
            _collider.motorTorque = 0;
            _collider.steerAngle = 0;
            
            _mesh.localPosition = _backup.Position;
            _mesh.localEulerAngles = _backup.Rotation;
        }

        private bool IsDrawingTrail() => 
            Collider.isGrounded && Collider.sidewaysFriction.stiffness < _backup.SidewaysStiffness;

        private void SetPositionRelativeToCollider()
        {
            _collider.GetWorldPose(out _position, out _rotation);

            _mesh.position = _position;
            _mesh.rotation = _rotation;

            if (_backup.Rotation != Vector3.zero)
                _mesh.localRotation = Quaternion.Euler(_mesh.localEulerAngles + _backup.Rotation);
        }

        private class WheelBackup
        {
            public readonly Vector3 Position; 
            public readonly Vector3 Rotation;
            
            public readonly float SidewaysStiffness;
            
            public WheelBackup(Transform mesh, WheelCollider collider)
            {
                Position = mesh.localPosition;
                Rotation = mesh.localEulerAngles;
                SidewaysStiffness = collider.sidewaysFriction.stiffness;
            }
        }
    }
}