using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Spawner;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoopLevelState : IUpdateableState
    {
        private readonly DiContainer _diContainer;
        
        private const int DelayEnterTime = 3;
        private float _stopwatch;

        public LoopLevelState(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Enter()
        {
            _diContainer.Resolve<IUpdateService>().OnUpdate += OnUpdate;

            InitTimer();
            DelayEnter();
        }

        private void DelayEnter() => 
            _diContainer.Resolve<ITweenService>().SingleTimer<LoopLevelState>(DelayEnterTime, OnEnter);

        private void OnEnter()
        {
            SetPause(false);
        }

        public void OnUpdate()
        {
            UpdateStopwatch();
            _diContainer.Resolve<ISpawnerService>().SpawnOnUpdate();
        }

        public void Exit()
        {
            _diContainer.Resolve<IUpdateService>().OnUpdate -= OnUpdate;
            _diContainer.Resolve<IPersistentDataService>().PlayerData.SessionData.LevelData.StopwatchTime = _stopwatch;
            _stopwatch = 0;
        }

        private void UpdateStopwatch()
        {
            _stopwatch += Time.deltaTime;
            _diContainer.Resolve<ILevelMediator>().ShowStopwatch(_stopwatch);
        }

        private void SetPause(bool isPause) => 
            _diContainer.Resolve<IPauseService>().SetPause(isPause);

        private void InitTimer() => 
            Object.Destroy(_diContainer.Resolve<IUIFactory>().LoadTimer(), DelayEnterTime);
    }
}