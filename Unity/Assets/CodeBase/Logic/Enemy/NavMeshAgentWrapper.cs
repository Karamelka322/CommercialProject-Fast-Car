using System;
using CodeBase.Extension;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Logic.Car;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Update;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Logic.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentWrapper : MonoBehaviour, IWrapper
    {
        [SerializeField] 
        private Transform _parent;
        
        [SerializeField] 
        private NavMeshAgent _navMeshAgent;

        private IUpdateService _updateService;

        public Vector2 Axis;

        public Transform Target { get; set; }
        public bool Enabled
        {
            get => _navMeshAgent.enabled;
            set
            {
                _navMeshAgent.enabled = value;
                enabled = value;
            }
        }

        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }
        
        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            ResetPosition();
            ResetRotation();

            if(_navMeshAgent.enabled)
                UpdateDestination();

            Axis.x = Mathf.Clamp(NextPosition().magnitude, -1, 1);
            Axis.y = Mathf.Clamp((float) Math.Round(CosAngle() / 0.01f, 3), -1, 1);
        }

        private float CosAngle() => 
            _parent.position.CosAngle(NextPosition(), _parent.position + _parent.forward);

        private Vector3 NextPosition() => 
            _navMeshAgent.hasPath ? _navMeshAgent.path.corners[1] : Vector3.zero;

        private void ResetPosition() => 
            transform.localPosition = Vector3.zero;

        private void ResetRotation() => 
            transform.localRotation = Quaternion.identity;

        private void UpdateDestination() => 
            _navMeshAgent.destination = Target.position;
    }
}