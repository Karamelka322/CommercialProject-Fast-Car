using CodeBase.Logic.Item;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Spawner;
using CodeBase.Services.Tween;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoopLevelState : IState
    {
        private const int DelayEnterTime = 3;
        
        private readonly IPersistentDataService _persistentDataService;
        private readonly IReadWriteDataService _readWriteDataService;
        private readonly ISpawnerService _spawnerService;
        private readonly IInputService _inputService;
        private readonly ITweenService _tweenService;
        private readonly IPauseService _pauseService;
        private readonly IUIFactory _uiFactory;

        private Capsule _capsule;
        private GameObject _timer;

        public LoopLevelState(
            IUIFactory uiFactory,
            IPersistentDataService persistentDataService,
            IReadWriteDataService readWriteDataService,
            IInputService inputService,
            ITweenService tweenService,
            IPauseService pauseService,
            ISpawnerService spawnerService)
        {
            _persistentDataService = persistentDataService;
            _readWriteDataService = readWriteDataService;
            _inputService = inputService;
            _tweenService = tweenService;
            _pauseService = pauseService;
            _spawnerService = spawnerService;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            InitUITimer();
            SetPause(true);
            DelayEnter();
        }

        private void DelayEnter() => 
            _tweenService.Timer<LoopLevelState>(DelayEnterTime, OnEnter);

        private void OnEnter()
        {
            DestroyUITimer();
            SetPause(false);
            SetSpawner(true);
        }

        public void Exit()
        {
            ResetSessionData();
            SetSpawner(false);
                
            _spawnerService.Clenup();
            _readWriteDataService.Clenup();
            _inputService.Clenup();
        }

        private void ResetSessionData() => 
            _persistentDataService.PlayerData.SessionData.Reset();

        private void SetPause(bool isPause) => 
            _pauseService.SetPause(isPause);

        private void InitUITimer() => 
            _timer = _uiFactory.LoadTimer();

        private void DestroyUITimer() => 
            Object.Destroy(_timer);

        private void SetSpawner(bool isActive)
        {
            if(isActive)
                _spawnerService.Enable();
            else
                _spawnerService.Disable();
        }
    }
}