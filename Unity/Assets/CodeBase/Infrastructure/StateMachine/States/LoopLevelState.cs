using CodeBase.Logic.Item;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Factories.UI;
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
        private readonly ITweenService _tweenService;
        private readonly IPauseService _pauseService;
        private readonly IUIFactory _uiFactory;

        private Energy _energy;
        private GameObject _timer;

        public LoopLevelState(
            IUIFactory uiFactory,
            IPersistentDataService persistentDataService,
            IReadWriteDataService readWriteDataService,
            ITweenService tweenService,
            IPauseService pauseService,
            ISpawnerService spawnerService,
            IUpdateService updateService,
            IDefeatService defeatService,
            IVictoryService victoryService)
        {
            _persistentDataService = persistentDataService;
            _readWriteDataService = readWriteDataService;
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
            _updateService.Enable();

            InitUITimer();
            SetPause(true);
            DelayEnter();
        }

        private void DelayEnter() => 
            _tweenService.SingleTimer<LoopLevelState>(DelayEnterTime, OnEnter);

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
            _spawnerService.SpawnOnUpdate();
        }

        public void Exit()
        {
            _updateService.OnUpdate -= OnUpdate;
            _updateService.Disable();

            SetStreamingData(false);
            
            _spawnerService.Reset();
            
            _defeatService.SetDefeat(false);
            _victoryService.SetVictory(false);
        }

        private void AddTimeInStopwatch(float time) => 
            _persistentDataService.PlayerData.SessionData.LevelData.StopwatchTime += time;
        
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