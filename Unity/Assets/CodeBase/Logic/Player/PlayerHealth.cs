using System;
using CodeBase.Data.Perseistent;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Replay;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerHealth : MonoBehaviour, IStreamingWriteData, IReplayHandler, IAffectPlayerDefeat
    {
        [SerializeField]
        private float _health;
        
        private float _maxHealth;
        
        private ILevelMediator _mediator;
        public event Action OnDefeat;

#if UNITY_EDITOR
        public float Health
        {
            get => _health;
            set => _health = value;
        }
#endif

        [Inject]
        private void Construct(ILevelMediator mediator)
        {
            _mediator = mediator;
        }
        
        private void Awake() => 
            _maxHealth = _health;
        
        public void ReduceHealth(float value)
        {
            _health = Mathf.Clamp(_health - value, 0, _maxHealth);

            _mediator.UpdateHealthBar(_health / _maxHealth);
            
            if(_health == 0)
                OnDefeat?.Invoke();
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