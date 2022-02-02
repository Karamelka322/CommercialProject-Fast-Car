using CodeBase.Scene;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly AllServices _services;
        
        private readonly ICorutineRunner _corutineRunner;

        public BootstrapState(IGameStateMachine stateMachine, AllServices services, ICorutineRunner corutineRunner)
        {
            _stateMachine = stateMachine;
            _services = services;
            _corutineRunner = corutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            LoadAndShowCurtain();
            _services.Single<ISceneLoaderService>().Load(SceneNameConstant.Initial, LoadSceneMode.Single, EnterLoadPersistentDataState);
        }

        public void Exit() { }

        private void RegisterServices()
        {
            RegisterStateMachine();
            RegisterSceneLoaderService();
            RegisterAssetProviderService();
            RegisterPresistentProgressService();
            RegisterInputService();

            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentDataService>()));
            _services.RegisterSingle<IStaticDataService>(new StaticDataService(_services.Single<IAssetProviderService>()));
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IGameStateMachine>(), _services.Single<IAssetProviderService>(), _services.Single<IPersistentDataService>(), _services.Single<IStaticDataService>(), _services.Single<IInputService>()));
            _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IStaticDataService>(), _services.Single<IInputService>()));
        }

        private void RegisterInputService() => 
            _services.RegisterSingle<IInputService>(new InputService());

        private void RegisterSceneLoaderService() => 
            _services.RegisterSingle<ISceneLoaderService>(new SceneLoaderService(_corutineRunner));

        private void RegisterStateMachine() => 
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);

        private void RegisterAssetProviderService() => 
            _services.RegisterSingle<IAssetProviderService>(new AssetProviderService());

        private void LoadAndShowCurtain() => 
            _services.Single<IUIFactory>().LoadLoadingMenuCurtain().Show();

        private void RegisterPresistentProgressService() => 
            _services.RegisterSingle<IPersistentDataService>(new PersistentDataService());

        private void EnterLoadPersistentDataState() => 
            _stateMachine.Enter<LoadPersistentDataState>();
    }
}