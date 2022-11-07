using CodeBase.Logic.Car;
using CodeBase.Logic.Level.Obstacle;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] 
        private PlayerHealth _playerHealth;

        [SerializeField] 
        private CarHitAnimation _hitAnimation;

        [SerializeField] 
        private Invulnerability _invulnerability;

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.TryGetComponent(out IObstacle obstacle))
            {
                _playerHealth.ReduceHealth(obstacle.Damage);
                _hitAnimation.Play();
                _invulnerability.Enable();
            }
        }
    }
}