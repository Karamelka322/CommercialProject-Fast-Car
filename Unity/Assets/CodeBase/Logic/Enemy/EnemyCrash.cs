using CodeBase.Logic.Car;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyCrash : CarCrash
    {
        [SerializeField] private Car.Car _car;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if(CheckCrash(other)) 
                Crash = true;
        }

        protected override void OnTriggerExit(Collider other)
        {
            if(CheckCrash(other)) 
                Crash = false;
        }
        
        private bool CheckCrash(Collider other) => 
            other.gameObject.layer == LayerMask.NameToLayer(Ground) || other.TryGetComponent(out EnemyPrefab enemy);
    }
}