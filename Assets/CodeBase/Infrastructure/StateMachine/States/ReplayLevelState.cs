using System;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Camera;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Replay;
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
        private readonly IReplayService _replayService;
        private readonly IPauseService _pauseService;
        private readonly IUIFactory _uiFactory;

        private LoadingCurtain _curtain;
        
        public ReplayLevelState(
            IGameStateMachine gameStateMachine,
            IReplayService replayService,
            IUIFactory uiFactory,
            IPauseService pauseService,
            IPersistentDataService persistentDataService)
        {
            _gameStateMachine = gameStateMachine;
            _replayService = replayService;
            _uiFactory = uiFactory;
            _pauseService = pauseService;
            _persistentDataService = persistentDataService;
        }

        public void Enter()
        {
            LoadCurtain();
            ShowCurtain(ReplayLevel);
        }

        public void Exit() => 
            HideCurtain();

        private void ReplayLevel()
        {
            _replayService.InformHandlers();
            _pauseService.SetPause(false);

            ResetCamera();
            ResetStopwatch();
            EnterLoopLevelState();
        }

        private void ResetStopwatch() => 
            _persistentDataService.PlayerData.SessionData.StopwatchTime = 0;

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