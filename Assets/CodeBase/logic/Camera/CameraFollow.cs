using System;
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
            Vector3 direction = (target.position + _rotationOffset) - transform.position;
            Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), speed);
            transform.rotation = nextRotation;
        }
    }
}