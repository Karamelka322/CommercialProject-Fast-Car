using System;
using CodeBase.Services.Pause;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerDefeat : MonoBehaviour, IAffectPlayerDefeat
    {
        [SerializeField] private PlayerHealth _playerHealth;

        private IPauseService _pauseService;
        public event Action OnDefeat;

        [Inject]
        private void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }
        
        private void Start() => 
            _playerHealth.OnUpdateHealth += OnUpdateHealth;
        
        private void OnDestroy() => 
            _playerHealth.OnUpdateHealth -= OnUpdateHealth;

        private void OnUpdateHealth(float health)
        {
            if(health > 0)
                return;
            
            _pauseService.SetPause(true);
            OnDefeat?.Invoke();
        }
    }
}