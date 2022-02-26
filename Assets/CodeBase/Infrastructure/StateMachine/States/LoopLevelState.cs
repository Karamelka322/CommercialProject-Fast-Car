using CodeBase.Logic.Item;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoopLevelState : IUpdateableState
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IUpdateService _updateService;
        private readonly IReadWriteDataService _readWriteDataService;
        private readonly IInputService _inputService;
        private readonly IPlayerFactory _playerFactory;
        private readonly ITweenService _tweenService;
        private readonly IPauseService _pauseService;
        private readonly IRandomService _randomService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ILevelFactory _levelFactory;
        private readonly IUIFactory _uiFactory;

        private Capsule _capsule;
        private GameObject _timer;

        public LoopLevelState(
            ILevelFactory levelFactory,
            IEnemyFactory enemyFactory,
            IUIFactory uiFactory,
            IRandomService randomService,
            IPersistentDataService persistentDataService,
            IUpdateService updateService,
            IReadWriteDataService readWriteDataService,
            IInputService inputService,
            IPlayerFactory playerFactory,
            ITweenService tweenService,
            IPauseService pauseService)
        {
            _persistentDataService = persistentDataService;
            _updateService = updateService;
            _readWriteDataService = readWriteDataService;
            _inputService = inputService;
            _playerFactory = playerFactory;
            _tweenService = tweenService;
            _pauseService = pauseService;
            _randomService = randomService;
            _levelFactory = levelFactory;
            _enemyFactory = enemyFactory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _timer = _uiFactory.LoadTimer();
            _pauseService.SetPause(true);
            _tweenService.Timer<LoopLevelState>(3, OnEnter);    
        }

        private void OnEnter()
        {
            Object.Destroy(_timer);
            _updateService.OnUpdate += OnUpdate;
            _pauseService.SetPause(false);
        }

        public void OnUpdate()
        {
            if (_capsule == null)
            {
                _capsule = _levelFactory.LoadCapsule(_randomService.CapsuleSpawnPoint());
                _uiFactory.HUD.GetComponentInChildren<Waymarkers>().Target = _capsule.transform;
            }
        }

        public void Exit()
        {
            _updateService.OnUpdate -= OnUpdate;
            
            _persistentDataService.PlayerData.SessionData.Reset();
            
            _readWriteDataService.Clenup();
            _inputService.Clenup();
        }

        private void InitEnemy() => 
            _enemyFactory.CreateEnemy(_playerFactory.Player.transform, _randomService.EnemySpawnPoint());
    }
}