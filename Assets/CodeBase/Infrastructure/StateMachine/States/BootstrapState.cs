using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly AllServices _services;

        private readonly IGameStateMachine _stateMachine;
        private readonly ICorutineRunner _corutineRunner;
        private readonly IUpdatable _updatable;

        public BootstrapState(IGameStateMachine stateMachine, AllServices services, ICorutineRunner corutineRunner, IUpdatable updatable)
        {
            _stateMachine = stateMachine;
            _services = services;
            _corutineRunner = corutineRunner;
            _updatable = updatable;

            RegisterServices();
        }

        public void Enter()
        {
            LoadAndShowCurtain();
            EnterLoadPersistentDataState();
        }

        public void Exit() => 
            _services.Single<IUpdateService>().Enable();

        private void RegisterServices()
        {
            _services.RegisterSingle<IUpdateService>(new UpdateService(_updatable));
            _services.RegisterSingle<ITweenService>(new TweenService(_corutineRunner));
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<ISceneLoaderService>(new SceneLoaderService(_corutineRunner));
            _services.RegisterSingle<IAssetProviderService>(new AssetProviderService());
            _services.RegisterSingle<IInputService>(new InputService());

            _services.RegisterSingle<IReadWriteDataService>(new ReadWriteDataService());
            _services.RegisterSingle<IPersistentDataService>(new PersistentDataService());
            _services.RegisterSingle<ISaveLoadDataService>(new SaveLoadDataService(_services.Single<IPersistentDataService>()));
            _services.RegisterSingle<IStaticDataService>(new StaticDataService(_services.Single<IAssetProviderService>()));

            _services.RegisterSingle<IPauseService>(new PauseService(_services.Single<IUpdateService>()));
            _services.RegisterSingle<IRandomService>(new RandomService(_services.Single<IPersistentDataService>(), _services.Single<IStaticDataService>()));
            
            _services.RegisterSingle<ILevelFactory>(new LevelFactory(
                _services.Single<IAssetProviderService>(),
                _services.Single<ITweenService>(),
                _services.Single<IPersistentDataService>()));
            
            _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(
                _services.Single<IStaticDataService>(),
                _services.Single<IInputService>(),
                _services.Single<ITweenService>(),
                _services.Single<IUpdateService>(),
                _services.Single<IPersistentDataService>(),
                _services.Single<IPauseService>()));
            
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IGameStateMachine>(),
                _services.Single<IAssetProviderService>(),
                _services.Single<IPersistentDataService>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IInputService>(),
                _services.Single<ITweenService>(),
                _services.Single<IPauseService>()));
            
            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(
                _services.Single<IAssetProviderService>(),
                _services.Single<IUpdateService>(),
                _services.Single<IPauseService>()));
        }

        private void LoadAndShowCurtain() => 
            _services.Single<IUIFactory>().LoadMenuCurtain().Show();
        
        private void EnterLoadPersistentDataState() => 
            _stateMachine.Enter<LoadPersistentDataState>();
    }
}