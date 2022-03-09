using CodeBase.Logic.Item;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Spawner;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoopLevelState : IUpdateableState
    {
        private const int DelayEnterTime = 3;
        
        private readonly IPersistentDataService _persistentDataService;
        private readonly IReadWriteDataService _readWriteDataService;
        private readonly ISpawnerService _spawnerService;
        private readonly IUpdateService _updateService;
        private readonly IDefeatService _defeatService;
        private readonly IVictoryService _victoryService;
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
            ISpawnerService spawnerService,
            IUpdateService updateService,
            IDefeatService defeatService,
            IVictoryService victoryService)
        {
            _persistentDataService = persistentDataService;
            _readWriteDataService = readWriteDataService;
            _inputService = inputService;
            _tweenService = tweenService;
            _pauseService = pauseService;
            _spawnerService = spawnerService;
            _updateService = updateService;
            _defeatService = defeatService;
            _victoryService = victoryService;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _updateService.OnUpdate += OnUpdate;
            
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
            SetStreamingData(true);
        }

        public void OnUpdate()
        {
            if (_defeatService.IsDefeat || _victoryService.IsVictory) 
                return;
            
            AddTimeInStopwatch(Time.deltaTime);
            _spawnerService.RealTimeSpawn();
        }

        public void Exit()
        {
            _updateService.OnUpdate -= OnUpdate;

            ResetSessionData();
            SetStreamingData(false);
            
            _defeatService.SetDefeat(false);
            _victoryService.SetVictory(false);
        }

        private void AddTimeInStopwatch(float time) => 
            _persistentDataService.PlayerData.SessionData.StopwatchTime += time;

        private void ResetSessionData() => 
            _persistentDataService.PlayerData.SessionData.Clenup();

        private void SetPause(bool isPause) => 
            _pauseService.SetPause(isPause);

        private void InitUITimer() => 
            _timer = _uiFactory.LoadTimer();

        private void DestroyUITimer() => 
            Object.Destroy(_timer);

        private void SetStreamingData(bool isActive)
        {
            if (isActive)
                _readWriteDataService.StartStreaming();
            else
                _readWriteDataService.StopStreaming();
        }
    }
}