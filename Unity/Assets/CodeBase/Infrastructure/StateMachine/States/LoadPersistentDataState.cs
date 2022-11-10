using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadPersistentDataState : IState
    {
        private const string LevelSceneName = "Level";
        
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadDataService _saveLoadDataService;

        public LoadPersistentDataState(IGameStateMachine stateMachine, IPersistentDataService progressService, ISaveLoadDataService saveLoadDataService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadDataService = saveLoadDataService;
        }

        public void Enter()
        {
            LoadPlayerPersistentDataOrInitNew();
            SetRenderSettings();

#if UNITY_EDITOR
            LoadDeveloperPersistentDataOrInitNew();
            
            if (IsInitialScene())
            {
                EnterLoadLevelState();
                return;
            }
#endif
            
            EnterLoadMenuState();
        }

        public void Exit() { }

        private void LoadPlayerPersistentDataOrInitNew()
        {
            PlayerPersistentData persistentData = _saveLoadDataService.LoadPlayerData() ?? NewPlayerPersistentData();
            _progressService.PlayerData = persistentData;
        }

        private void SetRenderSettings() => 
            Application.targetFrameRate = _progressService.PlayerData.SettingsData.RenderSettingsData.MaxFrameRate;

        private static PlayerPersistentData NewPlayerPersistentData() => 
            new PlayerPersistentData();

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

        private bool IsInitialScene() => 
            _progressService.DeveloperData.FirstScene == LevelSceneName;

        private void EnterLoadLevelState() => 
            _stateMachine.Enter<LoadLevelState>();
#endif
        
        private void EnterLoadMenuState() => 
            _stateMachine.Enter<LoadMenuState>();
    }
}