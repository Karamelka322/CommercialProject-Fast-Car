using CodeBase.Infrastructure.States;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.Spawner;
using CodeBase.Services.Tasks;
using CodeBase.Services.Victory;

namespace CodeBase.Infrastructure
{
    public class UnloadLevelState : IUnloadState
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IReadWriteDataService _readWriteDataService;
        private readonly IPauseService _pauseService;
        private readonly IRandomService _randomService;
        private readonly ISaveLoadDataService _saveLoadDataService;
        private readonly IAssetManagementService _assetManagementService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISpawnerService _spawnerService;
        private readonly IVictoryService _victoryService;
        private readonly IDefeatService _defeatService;
        private readonly IReplayService _replayService;
        private readonly ITaskService _taskService;

        public UnloadLevelState(
            IGameStateMachine gameStateMachine,
            ISpawnerService spawnerService,
            IVictoryService victoryService,
            IDefeatService defeatService,
            IReplayService replayService,
            IPersistentDataService persistentDataService,
            IReadWriteDataService readWriteDataService,
            IPauseService pauseService,
            IRandomService randomService,
            ISaveLoadDataService saveLoadDataService,
            IAssetManagementService assetManagementService,
            ITaskService taskService)
        {
            _taskService = taskService;
            _gameStateMachine = gameStateMachine;
            _spawnerService = spawnerService;
            _victoryService = victoryService;
            _defeatService = defeatService;
            _replayService = replayService;
            _persistentDataService = persistentDataService;
            _readWriteDataService = readWriteDataService;
            _pauseService = pauseService;
            _randomService = randomService;
            _saveLoadDataService = saveLoadDataService;
            _assetManagementService = assetManagementService;
        }

        public void Unload<TNextState>() where TNextState : class, IState
        {
            _readWriteDataService.InformSingleWriters();

            CleanupPlayerSessionData();
            SavePlayerData();
            CleanupServices();

            EnterNextState<TNextState>();
        }

        private void SavePlayerData() => 
            _saveLoadDataService.SavePlayerData();

        public void Exit() { }

        private void CleanupServices()
        {
            _readWriteDataService.CleanUp();
            _spawnerService.CleanUp();
            _victoryService.CleanUp();
            _defeatService.CleanUp();
            _replayService.CleanUp();
            _pauseService.CleanUp();
            _randomService.CleanUp();
            _assetManagementService.CleanUp();
            _taskService.CleanUp();
        }

        private void CleanupPlayerSessionData() => 
            _persistentDataService.PlayerData.SessionData.CleanUp();

        private void EnterNextState<TNextState>() where TNextState : class, IState => 
            _gameStateMachine.Enter<TNextState>();
    }
}