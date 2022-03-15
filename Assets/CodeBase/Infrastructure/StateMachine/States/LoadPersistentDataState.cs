using System;
using CodeBase.Data;
using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;
using CodeBase.Data.Static;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Scene;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadPersistentDataState : IState
    {
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
            LoadPlayerPersistenDataOrInitNew();

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

        private void LoadPlayerPersistenDataOrInitNew() => 
            _progressService.PlayerData = _saveLoadDataService.LoadPlayerData() ?? NewPlayerPersistentData();

        private static PlayerPersistentData NewPlayerPersistentData()
        {
            return new PlayerPersistentData
            {
                SettingsData =
                {
                    InputType = InputTypeId.Buttons
                },
                
                ProgressData =
                {
                    Players = GetPlayers(),
                    Levels = GetLevels(),
                    
                    CurrentPlayer = PlayerTypeId.Demon,
                    CurrentLevel = LevelTypeId.Level_1,
                },
                
                SessionData =
                {
                    StopwatchTime = 0,
                    
                    PlayerData =
                    {
                        Health = 0,
                        MaxHealth = 0,
                    },
                    
                    LevelData =
                    {
                        CurrentLevelConfig = null,
                        
                        GeneratorData =
                        {
                            Power = 0,
                        }
                    },
                }
            };
        }
        
        private static KeyValue<PlayerTypeId, bool>[] GetPlayers()
        {
            string[] names = Enum.GetNames(typeof(PlayerTypeId));
            
            KeyValue<PlayerTypeId, bool>[] players = new KeyValue<PlayerTypeId, bool>[names.Length];

            for (int i = 0; i < players.Length; i++)
            {
                players[i].Key = (PlayerTypeId)Enum.Parse(typeof(PlayerTypeId), names[i]);
                players[i].Value = i == 0;
            }
            
            return players;
        }
        
        private static KeyValue<LevelTypeId, bool>[] GetLevels()
        {
            string[] names = Enum.GetNames(typeof(LevelTypeId));
            
            KeyValue<LevelTypeId,bool>[] levels = new KeyValue<LevelTypeId, bool>[names.Length];

            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].Key = (LevelTypeId)Enum.Parse(typeof(LevelTypeId), names[i]);
                levels[i].Value = i == 0;
            }
            
            return levels;
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

        private bool IsInitialScene() => 
            _progressService.DeveloperData.FirstScene == SceneNameConstant.Level;

        private void EnterLoadLevelState() => 
            _stateMachine.Enter<LoadLevelState>();
#endif
        
        private void EnterLoadMenuState() => 
            _stateMachine.Enter<LoadMenuState>();
    }
}