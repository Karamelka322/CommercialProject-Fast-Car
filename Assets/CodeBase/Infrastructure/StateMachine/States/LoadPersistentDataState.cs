using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;
using CodeBase.Data.Static;
using CodeBase.Data.Static.Level;
using CodeBase.Scene;
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
            LoadPlayerPersistenDataOrInitNew();

#if UNITY_EDITOR
            LoadDeveloperPersistentDataOrInitNew();
            
            if (_progressService.DeveloperData.FirstScene == SceneNameConstant.Level)
            {
                _stateMachine.Enter<LoadLevelState>();
                return;
            }
#endif
            
            EnterLoadMenuState();
        }

        public void Exit() { }

        private void LoadPlayerPersistenDataOrInitNew() => 
            _progressService.PlayerData = _saveLoadService.LoadPlayerData() ?? NewPlayerPersistentData();

        private static PlayerPersistentData NewPlayerPersistentData()
        {
            return new PlayerPersistentData
            {
                InputData =
                {
                    Type = InputTypeId.Buttons
                },
                
                LevelData =
                {
                    Type = LevelTypeId.Level_1,
                },
            };
        }

#if UNITY_EDITOR
        private void LoadDeveloperPersistentDataOrInitNew() => 
            _progressService.DeveloperData = _saveLoadService.LoadDeveloperData() ?? DeveloperPersistentData();

        private static DeveloperPersistentData DeveloperPersistentData()
        {
            return new DeveloperPersistentData()
            {
                FirstScene = ""
            };
        }
#endif


        private void EnterLoadMenuState() => 
            _stateMachine.Enter<LoadMenuState>();
    }
}