using System;
using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerHealth : MonoBehaviour, IReadData, IWriteData, IReplayHandler
    {
        private float _startHealth;
        private float _startMaxHealth;

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
            _startHealth = persistentData.SessionData.PlayerData.Health;
            _startMaxHealth = persistentData.SessionData.PlayerData.MaxHealth;

            _health = _startHealth;
            _maxHealth = _startMaxHealth;
        }

        public void WriteData(PlayerPersistentData persistentData)
        {
            persistentData.SessionData.PlayerData.Health = _health;
            persistentData.SessionData.PlayerData.MaxHealth = _maxHealth;
        }

        public void OnReplay()
        {
            _health = _startHealth;
            _maxHealth = _startMaxHealth;
        }
    }
}