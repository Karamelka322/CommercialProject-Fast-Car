using System;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] 
        private int _health;

        private int _maxHealth;
        public event Action<float> OnChangeHealth;
        
#if UNITY_EDITOR
        public int Health
        {
            get => _health;
            set => _health = value;
        }
#endif

        private void Awake() => 
            _maxHealth = _health;

        public void AddHealth(float value)
        {
            _health = (int)Mathf.Clamp(_health + value, 0, _maxHealth);
            OnChangeHealth?.Invoke(_health);
        }

        public void ReduceHealth(float value)
        {
            _health = (int)Mathf.Clamp(_health - value, 0, _maxHealth);
            OnChangeHealth?.Invoke(_health);
        }
    }
}