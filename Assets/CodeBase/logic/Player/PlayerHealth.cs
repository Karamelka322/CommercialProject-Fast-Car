using System;
using System.ComponentModel;
using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerHealth : MonoBehaviour, IReadData, IWriteData, IReplayHandler
    {
        [SerializeField]
        private float _health;
        
        private float _maxHealth;
        
        private float _startHealth;
        private float _startMaxHealth;

        public event Action<float> OnChangeHealth;

#if UNITY_EDITOR
        public float Health
        {
            [Editor]
            set => _health = value;
        }
#endif
        
        private void Awake()
        {
            _maxHealth = _health;
            _startHealth = _health;
            _startMaxHealth = _health;
        }

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
            if(persistentData.SessionData == null)
                return;
            
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
            
            OnChangeHealth?.Invoke(_health);
        }
    }
}