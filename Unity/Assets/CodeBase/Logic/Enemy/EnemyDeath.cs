using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] 
        private EnemyHealth _enemyHealth;

        private void Start() => 
            _enemyHealth.OnChangeHealth += OnChangeHealth;

        private void OnDestroy() => 
            _enemyHealth.OnChangeHealth -= OnChangeHealth;

        private void OnChangeHealth(float health)
        {
            if(health == 0)
                Death();
        }

        private void Death() => 
            Destroy(gameObject);
    }
}