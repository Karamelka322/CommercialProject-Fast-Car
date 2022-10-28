using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraFollow : MonoBehaviour
    {
        [Space, SerializeField] 
        private Vector3 _movementOffset;

        [SerializeField] 
        private Vector3 _rotationOffset;

        [Space, SerializeField]
        private float _movementSpeed;

        [SerializeField]
        private float _rotationSpeed;

        private Vector3 _dynamicRotationOffset;
        private IUpdateService _updateService;

        public Transform Target { get; set; }

        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Start() => 
            _updateService.OnFixedUpdate += OnFixedUpdate;

        private void OnDestroy() => 
            _updateService.OnFixedUpdate -= OnFixedUpdate;

        private void OnFixedUpdate()
        {
            if(Target != null)
                Follow(Target);
        }

        public void MoveToTarget()
        {
            if (Target != null)
            {
                Movement(Target, 1f);
                Rotation(Target, 1f);
            }
        }

        private void Follow(in Transform target)
        {
            Movement(target, Time.deltaTime * _movementSpeed);
            Rotation(target, Time.deltaTime * _rotationSpeed);
        }

        private void Movement(Transform target, float speed)
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, target.position + _movementOffset, speed);
            transform.position = nextPosition;
        }

        private void Rotation(Transform target, float speed)
        {
            Vector3 targetDirection = target.parent.position - target.position;

            if (targetDirection.x > 0 && targetDirection.z > 0)
                _dynamicRotationOffset = Vector3.Lerp(Vector3.zero, _rotationOffset, Time.deltaTime * _rotationSpeed);
            else
                _dynamicRotationOffset = Vector3.zero;

            Vector3 cameraDirection = (target.position + _dynamicRotationOffset) - transform.position;
            Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(cameraDirection), speed);
            transform.rotation = nextRotation;
        }
    }
}