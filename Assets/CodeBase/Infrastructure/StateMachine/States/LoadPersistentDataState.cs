using CodeBase.Data;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadPersistentDataState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadPersistentDataState(IGameStateMachine stateMachine, IPersistentDataService progressService, ISaveLoadService saveLoadService)
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

        private static PlayerPersistentData NewProgress()
        {
            return new PlayerPersistentData
            {
                InputData =
                {
                    Type = InputTypeId.Buttons
                }
            };
        }

        private void EnterLoadMenuState() => 
            _stateMachine.Enter<LoadMenuState>();
    }
}