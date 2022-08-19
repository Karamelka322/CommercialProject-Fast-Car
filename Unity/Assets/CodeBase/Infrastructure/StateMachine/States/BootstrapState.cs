using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.Reward;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.Spawner;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using CodeBase.Services.Window;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IUpdatable _updatable;
        
        private readonly DiContainer _diContainer;

        public BootstrapState(
            DiContainer diContainer,
            IGameStateMachine stateMachine,
            ICoroutineRunner coroutineRunner,
            IUpdatable updatable)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _updatable = updatable;
            _diContainer = diContainer;

            RegisterServices();
        }

        public void Enter() => 
            EnterLoadPersistentDataState();

        public void Exit() => 
            _diContainer.Resolve<IAssetManagementService>().InitializeAsync();

        private void RegisterServices()
        {
            _diContainer.Bind<IUpdatable>().FromInstance(_updatable).AsSingle();
            _diContainer.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
            _diContainer.Bind<IGameStateMachine>().FromInstance(_stateMachine).AsSingle();
            
            _diContainer.Bind<IWindowService>().To<WindowService>().AsSingle();
            _diContainer.Bind<IVictoryService>().To<VictoryService>().AsSingle();
            _diContainer.Bind<IDefeatService>().To<DefeatService>().AsSingle();
            _diContainer.Bind<IReplayService>().To<ReplayService>().AsSingle();
            _diContainer.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            _diContainer.Bind<ITweenService>().To<TweenService>().AsSingle();
            _diContainer.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            _diContainer.Bind<IAssetManagementService>().To<AssetManagementService>().AsSingle();
            _diContainer.Bind<IInputService>().To<InputService>().AsSingle();
            _diContainer.Bind<IPersistentDataService>().To<PersistentDataService>().AsSingle();
            _diContainer.Bind<IReadWriteDataService>().To<ReadWriteDataService>().AsSingle();
            _diContainer.Bind<ISaveLoadDataService>().To<SaveLoadDataService>().AsSingle();
            _diContainer.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            _diContainer.Bind<IRewardService>().To<RewardService>().AsSingle();
            _diContainer.Bind<IPauseService>().To<PauseService>().AsSingle();
            _diContainer.Bind<IRandomService>().To<RandomService>().AsSingle();
            
            _diContainer.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            _diContainer.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
            _diContainer.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            _diContainer.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
            _diContainer.Bind<ISpawnerService>().To<SpawnerService>().AsSingle();
        }

        private void EnterLoadPersistentDataState() => 
            _stateMachine.Enter<LoadPersistentDataState>();
    }
}