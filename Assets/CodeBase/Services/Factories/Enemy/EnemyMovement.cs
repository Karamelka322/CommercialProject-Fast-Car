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
        
        public void Construct(Transform target)
        {
            _target = target;
            
            _navMeshAgent.SetDestination(target.position);
        }
    }
}