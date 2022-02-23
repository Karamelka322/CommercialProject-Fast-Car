using System;
using CodeBase.Services.Update;
using UnityEngine;

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

        public void Construct(IUpdateService updateService) => 
            _updateService = updateService;

        private void Start() => 
            _updateService.OnFixedUpdate += OnFixedUpdate;

        private void OnDestroy() => 
            _updateService.OnFixedUpdate -= OnFixedUpdate;

        private void OnFixedUpdate()
        {
            if(Target != null)
                Follow(Target);
        }

        private void Follow(in Transform target)
        {
            Movement(target);
            Rotation(target);
        }

        private void Movement(Transform target)
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, target.position + _movementOffset, Time.deltaTime * _movementSpeed);
            transform.position = nextPosition;
        }

        private void Rotation(Transform target)
        {
            Vector3 direction = (target.position + _rotationOffset) - transform.position;
            Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);
            transform.rotation = nextRotation;
        }
    }
}