using CodeBase.Logic.Item;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Update;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoopLevelState : IUpdateableState
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IUpdateService _updateService;
        private readonly IPauseService _pauseService;
        private readonly IRandomService _randomService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ILevelFactory _levelFactory;
        private readonly IUIFactory _uiFactory;

        private Capsule _capsule;

        public LoopLevelState(
            ILevelFactory levelFactory,
            IEnemyFactory enemyFactory,
            IUIFactory uiFactory,
            IRandomService randomService,
            IPersistentDataService persistentDataService,
            IUpdateService updateService,
            IPauseService pauseService)
        {
            _persistentDataService = persistentDataService;
            _updateService = updateService;
            _pauseService = pauseService;
            _randomService = randomService;
            _levelFactory = levelFactory;
            _enemyFactory = enemyFactory;
            _uiFactory = uiFactory;
        }

        public void Enter() => 
            _updateService.OnUpdate += OnUpdate;

        public void Exit()
        {
            _updateService.OnUpdate -= OnUpdate;
            
            _persistentDataService.PlayerData.SessionData.PlayerData.ResetHealth();
            _persistentDataService.PlayerData.SessionData.GeneratorData.ResetPower();
            
            _pauseService.Clenup();
        }

        public void OnUpdate()
        {
            if (_capsule == null)
            {
                _capsule = _levelFactory.LoadCapsule(_randomService.CapsuleSpawnPoint());
                _uiFactory.HUD.GetComponentInChildren<Waypoints>().Target = _capsule.transform;
            }
        }

        private void InitEnemy(Transform player) => 
            _enemyFactory.CreateEnemy(player, _randomService.EnemySpawnPoint());
    }
}