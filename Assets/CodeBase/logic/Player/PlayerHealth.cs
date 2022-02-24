using System;
using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReaderWriter;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerHealth : MonoBehaviour, IReadData, IWriteData
    {
        private float _health;
        private float _maxHealth;
        
        public event Action<float> OnChangeHealth;
        
        public void AddHealth(float value)
        {
            _health = Mathf.Clamp(_health + value, 0, _maxHealth);
            OnChangeHealth?.Invoke(_health);
        }

        public void ReduceHealth(float value)
        {
            _health = Mathf.Clamp(_health - value, 0, _maxHealth);
            OnChangeHealth?.Invoke(_health);
        }

        public void ReadData(PlayerPersistentData persistentData)
        {
            _health = persistentData.SessionData.PlayerData.Health;
            _maxHealth = persistentData.SessionData.PlayerData.MaxHealth;
        }

        public void WriteData(PlayerPersistentData persistentData)
        {
            persistentData.SessionData.PlayerData.Health = _health;
            persistentData.SessionData.PlayerData.MaxHealth = _maxHealth;
        }
    }
}