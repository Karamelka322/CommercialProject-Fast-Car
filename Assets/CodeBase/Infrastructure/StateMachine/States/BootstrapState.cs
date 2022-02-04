using CodeBase.Scene;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
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
            _services.RegisterSingle<ITweenService>(new TweenService(_corutineRunner));
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<ISceneLoaderService>(new SceneLoaderService(_corutineRunner));
            _services.RegisterSingle<IAssetProviderService>(new AssetProviderService());
            _services.RegisterSingle<IPersistentDataService>(new PersistentDataService());
            _services.RegisterSingle<IInputService>(new InputService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentDataService>()));
            _services.RegisterSingle<IStaticDataService>(new StaticDataService(_services.Single<IAssetProviderService>()));
            
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IGameStateMachine>(), _services.Single<IAssetProviderService>(), _services.Single<IPersistentDataService>(), _services.Single<IStaticDataService>(), _services.Single<IInputService>(), _services.Single<ITweenService>()));
            _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IStaticDataService>(), _services.Single<IInputService>(), _services.Single<ITweenService>()));
        }

        private void LoadAndShowCurtain() => 
            _services.Single<IUIFactory>().LoadLoadingMenuCurtain().Show();

        private void EnterLoadPersistentDataState() => 
            _stateMachine.Enter<LoadPersistentDataState>();
    }
}