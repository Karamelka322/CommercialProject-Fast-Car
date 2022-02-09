using System;
using UnityEngine;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class PlayerSessionData
    {
        public float MaxHealth;
        public float Health;

        public Action<float> ChangeHealth;
        
        public void AddHealth(float value)
        {
            Health = Mathf.Clamp(Health + value, 0, MaxHealth);
            ChangeHealth?.Invoke(Health);
        }

        public void ReduceHealth(float value)
        {
            Health = Mathf.Clamp(Health - value, 0, MaxHealth);
            ChangeHealth?.Invoke(Health);
        }
    }
}