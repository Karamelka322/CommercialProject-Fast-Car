using System;
using CodeBase.Extension;
using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Logic.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentWrapper : MonoBehaviour
    {
        [SerializeField] 
        private Transform _parent;
        
        [SerializeField] 
        private NavMeshAgent _navMeshAgent;

        private Transform _target;
        private IUpdatable _updatable;

        public void Construct(IUpdatable updatable, Transform target)
        {
            _updatable = updatable;
            _target = target;
        }
        
        private void Start() => 
            _updatable.OnUpdate += OnUpdate;

        private void OnDisable() => 
            _updatable.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            ResetPosition();
            ResetRotation();

            UpdateDestination();
        }

        public float GetNormalizeAngle() => 
            Mathf.Clamp((float)Math.Round(CosAngle() / 0.01f, 3), -1, 1);

        public float GetNormalizeSpeed() => 
            Mathf.Clamp(NextPosition().magnitude, -1, 1);

        private float CosAngle() => 
            _parent.position.CosAngle(NextPosition(), _parent.position + _parent.forward);

        private Vector3 NextPosition() => 
            _navMeshAgent.hasPath ? _navMeshAgent.path.corners[1] : Vector3.zero;

        private void ResetPosition() => 
            transform.localPosition = Vector3.zero;

        private void ResetRotation() => 
            transform.localRotation = Quaternion.identity;

        private void UpdateDestination() => 
            _navMeshAgent.destination = _target.position;
    }
}