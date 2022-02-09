using CodeBase.Logic.Level.Obstacle;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private const float Divider = 0.007f;

        [SerializeField] 
        private PlayerHealth _playerHealth;

        [Space, SerializeField] 
        private float _impulseForCollision;

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.TryGetComponent(out IObstacle obstacle))
            {
                if(other.impulse.magnitude * Divider > _impulseForCollision)
                    _playerHealth.ReduceHealth(other.impulse.magnitude * Divider * obstacle.Demage);
            }
        }
    }
}