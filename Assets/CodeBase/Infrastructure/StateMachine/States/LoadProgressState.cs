using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IGameStateMachine stateMachine, IPersistentDataService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            EnterLoadMenuState();
        }

        public void Exit() { }

        private void LoadProgressOrInitNew() => 
            _progressService.PlayerData = _saveLoadService.LoadPlayerData() ?? NewProgress();

        private static PlayerData NewProgress()
        {
            PlayerData progress = new PlayerData();
            return progress;
        }

        private void EnterLoadMenuState() => 
            _stateMachine.Enter<LoadMenuState>();
    }
}