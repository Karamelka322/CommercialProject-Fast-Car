using System;
using CodeBase.Extension;
using CodeBase.Logic.Car;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Services.Factories.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] 
        private Car _car;

        [SerializeField] 
        private NavMeshAgent _navMeshAgent;

        private Transform _target;
        
        public void Construct(Transform target) => 
            _target = target;

        private void Update()
        {
            _navMeshAgent.transform.position = transform.position;
            _navMeshAgent.destination = _target.position;

            double angle = Math.Round(CosAngle() / 0.01f, 4);
            
            _car.Rotation(Mathf.Clamp((float)angle, -1, 1));
            _car.Movement(Mathf.Clamp(_navMeshAgent.path.corners[1].magnitude, -1, 1));
        }

        private float CosAngle() => 
            transform.position.CosAngle(_navMeshAgent.path.corners[1], transform.position + transform.forward);
    }
}