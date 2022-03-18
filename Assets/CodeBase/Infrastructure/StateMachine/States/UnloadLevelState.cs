using CodeBase.Infrastructure.States;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Input;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.Spawner;
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
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISpawnerService _spawnerService;
        private readonly IInputService _inputService;
        private readonly IVictoryService _victoryService;
        private readonly IDefeatService _defeatService;
        private readonly IReplayService _replayService;

        public UnloadLevelState(
            IGameStateMachine gameStateMachine,
            ISpawnerService spawnerService,
            IInputService inputService,
            IVictoryService victoryService,
            IDefeatService defeatService,
            IReplayService replayService,
            IPersistentDataService persistentDataService,
            IReadWriteDataService readWriteDataService,
            IPauseService pauseService,
            IRandomService randomService,
            ISaveLoadDataService saveLoadDataService)
        {
            _gameStateMachine = gameStateMachine;
            _spawnerService = spawnerService;
            _inputService = inputService;
            _victoryService = victoryService;
            _defeatService = defeatService;
            _replayService = replayService;
            _persistentDataService = persistentDataService;
            _readWriteDataService = readWriteDataService;
            _pauseService = pauseService;
            _randomService = randomService;
            _saveLoadDataService = saveLoadDataService;
        }

        public void Unload<TNextState>() where TNextState : class, IState
        {
            _readWriteDataService.InformSingleWriters();

            ClenupPlayerSessionData();
            SavePlayerData();
            ClenupServices();

            EnterNextState<TNextState>();
        }

        private void SavePlayerData() => 
            _saveLoadDataService.SavePlayerData();

        public void Exit() { }

        private void ClenupServices()
        {
            _readWriteDataService.Clenup();
            _spawnerService.Clenup();
            _inputService.Clenup();
            _victoryService.Clenup();
            _defeatService.Clenup();
            _replayService.Clenup();
            _pauseService.Clenup();
            _randomService.Clenup();
        }

        private void ClenupPlayerSessionData() => 
            _persistentDataService.PlayerData.SessionData.Clenup();

        private void EnterNextState<TNextState>() where TNextState : class, IState => 
            _gameStateMachine.Enter<TNextState>();
    }
}