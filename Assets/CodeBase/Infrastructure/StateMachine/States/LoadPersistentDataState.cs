using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;
using CodeBase.Data.Static;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Scene;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class LoadPersistentDataState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadDataService _saveLoadDataService;
        private readonly IStaticDataService _staticDataService;

        public LoadPersistentDataState(IGameStateMachine stateMachine, IPersistentDataService progressService, ISaveLoadDataService saveLoadDataService, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadDataService = saveLoadDataService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadPlayerPersistenDataOrInitNew();

#if UNITY_EDITOR
            LoadDeveloperPersistentDataOrInitNew();
            
            if (_progressService.DeveloperData.FirstScene == SceneNameConstant.Level)
            {
                EnterLoadLevelState();
                return;
            }
#endif
            
            EnterLoadMenuState();
        }

        public void Exit() { }

        private void LoadPlayerPersistenDataOrInitNew() => 
            _progressService.PlayerData = _saveLoadDataService.LoadPlayerData() ?? NewPlayerPersistentData();

        private PlayerPersistentData NewPlayerPersistentData()
        {
            return new PlayerPersistentData
            {
                InputData =
                {
                    Type = InputTypeId.Areas
                },
                
                LevelData =
                {
                    Type = LevelTypeId.Level_1,
                },
                
                SessionData =
                {
                    PlayerSessionData =
                    {
                        Health = _staticDataService.ForPlayer(PlayerTypeId.Default).Health
                    }
                }
            };
        }

#if UNITY_EDITOR
        private void LoadDeveloperPersistentDataOrInitNew() => 
            _progressService.DeveloperData = _saveLoadDataService.LoadDeveloperData() ?? NewDeveloperPersistentData();

        private static DeveloperPersistentData NewDeveloperPersistentData()
        {
            return new DeveloperPersistentData()
            {
                FirstScene = ""
            };
        }

#endif
        
        private void EnterLoadMenuState() => 
            _stateMachine.Enter<LoadMenuState>();

        private void EnterLoadLevelState() => 
            _stateMachine.Enter<LoadLevelState>();
    }
}