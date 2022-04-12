using CodeBase.Infrastructure.States;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.Window;

namespace CodeBase.Infrastructure
{
    public class UnloadMenuState : IUnloadState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWindowService _windowService;
        private readonly IReadWriteDataService _readWriteDataService;
        private readonly ISaveLoadDataService _saveLoadDataService;

        public UnloadMenuState(
            IGameStateMachine gameStateMachine,
            IWindowService windowService,
            IReadWriteDataService readWriteDataService,
            ISaveLoadDataService saveLoadDataService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _readWriteDataService = readWriteDataService;
            _saveLoadDataService = saveLoadDataService;
        }

        public void Unload<TNextState>() where TNextState : class, IState
        {
            SavePlayerData();
            ClenupServices();

            EnterNextState<TNextState>();
        }

        public void Exit() { }

        private void SavePlayerData() => 
            _saveLoadDataService.SavePlayerData();

        private void ClenupServices()
        {
            _readWriteDataService.CleanUp();
            _windowService.CleanUp();
        }

        private void EnterNextState<TNextState>() where TNextState : class, IState => 
            _gameStateMachine.Enter<TNextState>();
    }
}