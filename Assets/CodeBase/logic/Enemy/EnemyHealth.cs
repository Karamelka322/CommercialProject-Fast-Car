using System.ComponentModel;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] 
        private int _health;
        
        public int Health
        {
            get => _health;
            
            [Editor]
            set => _health = value;
        }

        private int _maxHealth;
        
        private void Awake() => 
            _maxHealth = _health;

        public void AddHealth(float value) => 
            _health = (int)Mathf.Clamp(_health + value, 0, _maxHealth);

        public void ReduceHealth(float value) => 
            _health = (int)Mathf.Clamp(_health - value, 0, _maxHealth);
    }
}