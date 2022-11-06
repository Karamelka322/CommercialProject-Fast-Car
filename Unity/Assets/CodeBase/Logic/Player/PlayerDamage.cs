using CodeBase.Logic.Level.Obstacle;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerDamage : MonoBehaviour, IObstacle
    {
        [SerializeField]
        private float _damage;
        
        public float Damage => _damage;
    }
}