using System;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
    public class ReplayLevelState : IState
    {
        private const float SpeedShowCurtain = 2f;
        private const float DelayShowCurtain = 0f;
        private const float SpeedHideCurtain = 2f;
        private const float DelayHideCurtain = 0f;

        private readonly IGameStateMachine _gameStateMachine;
        private readonly IReplayService _replayService;
        private readonly IPauseService _pauseService;
        private readonly IUIFactory _uiFactory;

        public ReplayLevelState(IGameStateMachine gameStateMachine, IReplayService replayService, IUIFactory uiFactory, IPauseService pauseService)
        {
            _gameStateMachine = gameStateMachine;
            _replayService = replayService;
            _uiFactory = uiFactory;
            _pauseService = pauseService;
        }

        public void Enter()
        {
            if(_uiFactory.LoadingCurtain == null)
            {
                LoadCurtain();
                ShowCurtain(ReplayLevel);
            }
            else
            {
                ReplayLevel();
            }
        }

        private void ReplayLevel()
        {
            _replayService.InformHandlers();
            _pauseService.SetPause(false);
            
            ClearUIRoot();
            EnterLoopLevelState();
        }

        private void ClearUIRoot()
        {
            for (int i = 0; i < _uiFactory.UIRoot.childCount; i++) 
                Object.Destroy(_uiFactory.UIRoot.GetChild(0).gameObject);
        }

        private void EnterLoopLevelState() => 
            _gameStateMachine.Enter<LoopLevelState>();

        public void Exit() => 
            HideCurtain();

        private void LoadCurtain() => 
            _uiFactory.LoadMenuCurtain();

        private void ShowCurtain(Action callBack) => 
            _uiFactory.LoadingCurtain.Show(SpeedShowCurtain, DelayShowCurtain, callBack);

        private void HideCurtain() => 
            _uiFactory?.LoadingCurtain.Hide(SpeedHideCurtain, DelayHideCurtain, DestroyCurtain);

        private void DestroyCurtain() => 
            Object.Destroy(_uiFactory.LoadingCurtain.gameObject);
    }
}