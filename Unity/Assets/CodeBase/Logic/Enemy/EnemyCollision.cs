using CodeBase.Logic.Level.Obstacle;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] 
        private EnemyHealth _enemyHealth;

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.TryGetComponent(out IObstacle obstacle))
                _enemyHealth.ReduceHealth(obstacle.Damage);
        }
    }
}