using CodeBase.Logic.Level.Obstacle;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyDamage : MonoBehaviour, IObstacle
    {
        [SerializeField] 
        private float _damage;

        public float Damage => _damage;
    }
}