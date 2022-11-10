using System;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Camera;
using CodeBase.Services.Defeat;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.Spawner;
using CodeBase.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
    public class ReplayLevelState : IState
    {
        private const float SpeedShowCurtain = 2f;
        private const float DelayShowCurtain = 0f;
        private const float SpeedHideCurtain = 2f;
        private const float DelayHideCurtain = 0f;

        private readonly IPersistentDataService _persistentDataService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISaveLoadDataService _saveLoadDataService;
        private readonly ISpawnerService _spawnerService;
        private readonly IReplayService _replayService;
        private readonly IDefeatService _defeatService;
        private readonly IRandomService _randomService;
        private readonly IUIFactory _uiFactory;

        private LoadingCurtain _curtain;

        public ReplayLevelState(
            IGameStateMachine gameStateMachine,
            IReplayService replayService,
            IUIFactory uiFactory,
            IPersistentDataService persistentDataService,
            ISaveLoadDataService saveLoadDataService,
            ISpawnerService spawnerService,
            IRandomService randomService,
            IDefeatService defeatService)
        {
            _defeatService = defeatService;
            _randomService = randomService;
            _spawnerService = spawnerService;
            _saveLoadDataService = saveLoadDataService;
            _gameStateMachine = gameStateMachine;
            _replayService = replayService;
            _uiFactory = uiFactory;
            _persistentDataService = persistentDataService;
        }

        public void Enter()
        {
            ReplayLevel();
            
            //LoadCurtain();
            //ShowCurtain(ReplayLevel);
        }

        public void Exit()
        {
            //HideCurtain();
        }

        private void ReplayLevel()
        {
            TryUpdateRecordTime();
            ResetStopwatch();
            
            _replayService.InformHandlers();
            _saveLoadDataService.SavePlayerData();
            
            _spawnerService.Reset();
            _randomService.Reset();
            
            _defeatService.SetDefeat(false);

            ResetCamera();
            EnterLoopLevelState();
        }

        private void TryUpdateRecordTime()
        {
            float recordTime = _persistentDataService.PlayerData.ProgressData.RecordTime;
            float stopwatch = _persistentDataService.PlayerData.SessionData.LevelData.StopwatchTime;

            if (recordTime < stopwatch)
                _persistentDataService.PlayerData.ProgressData.RecordTime = stopwatch;
        }

        private void ResetStopwatch() => 
            _persistentDataService.PlayerData.SessionData.LevelData.StopwatchTime = 0;

        private static void ResetCamera()
        {
            if (Camera.main.TryGetComponent(out CameraFollow cameraFollow))
                cameraFollow.MoveToTarget();
        }

        private void EnterLoopLevelState() => 
            _gameStateMachine.Enter<LoopLevelState>();
        
        private void LoadCurtain() => 
            _curtain = _uiFactory.LoadMenuCurtain();

        private void ShowCurtain(Action onShow) => 
            _curtain.Show(SpeedShowCurtain, DelayShowCurtain, onShow);

        private void HideCurtain() => 
            _curtain.Hide(SpeedHideCurtain, DelayHideCurtain, DestroyCurtain);

        private void DestroyCurtain() => 
            Object.Destroy(_curtain.gameObject);
    }
}