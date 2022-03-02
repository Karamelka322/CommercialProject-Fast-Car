using System;
using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerHealth : MonoBehaviour, ISingleReadData, IStreamingWriteData, IReplayHandler, IAffectPlayerDefeat
    {
        [SerializeField]
        private float _health;
        
        private float _maxHealth;
        public event Action OnDefeat;

#if UNITY_EDITOR
        public float Health
        {
            get => _health;
            set => _health = value;
        }
#endif

        private void Awake() => 
            _maxHealth = _health;

        public void AddHealth(float value) => 
            _health = Mathf.Clamp(_health + value, 0, _maxHealth);

        public void ReduceHealth(float value)
        {
            _health = Mathf.Clamp(_health - value, 0, _maxHealth);
            
            if(_health == 0)
                OnDefeat?.Invoke();
        }

        public void SingleReadData(PlayerPersistentData persistentData)
        {
            if(persistentData.SessionData.PlayerData.MaxHealth == 0)
                return;
            
            _health = persistentData.SessionData.PlayerData.Health;
            _maxHealth = persistentData.SessionData.PlayerData.MaxHealth;
        }

        public void StreamingWriteData(PlayerPersistentData persistentData)
        {
            persistentData.SessionData.PlayerData.Health = _health;
            persistentData.SessionData.PlayerData.MaxHealth = _maxHealth;
        }

        public void OnReplay() => 
            _health = _maxHealth;
    }
}