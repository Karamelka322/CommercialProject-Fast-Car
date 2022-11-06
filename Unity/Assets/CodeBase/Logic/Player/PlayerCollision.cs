using CodeBase.Logic.Level.Obstacle;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] 
        private PlayerHealth _playerHealth;

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.TryGetComponent(out IObstacle obstacle)) 
                _playerHealth.ReduceHealth(obstacle.Damage);
        }
    }
}