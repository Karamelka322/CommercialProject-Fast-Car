using CodeBase.Scene;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        
        private readonly ICorutineRunner _corutineRunner;

        public BootstrapState(GameStateMachine stateMachine, AllServices services, ICorutineRunner corutineRunner)
        {
            _stateMachine = stateMachine;
            _services = services;
            _corutineRunner = corutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            _services.Single<IUIFactory>().LoadLoadingMenuCurtain().Show();
            _services.Single<ISceneLoaderService>().Load(SceneNameConstant.Initial, LoadSceneMode.Single, EnterLoadMenuState);
        }

        public void Exit() { }

        private void RegisterServices()
        {
            RegisterStateMachine();
            RegisterInputService();
            RegisterSceneLoaderService();
            RegisterAssetProviderService();
            
            _services.RegisterSingle<IStaticDataService>(new StaticDataService(_services.Single<IAssetProviderService>()));
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IGameStateMachine>(),_services.Single<IAssetProviderService>()));
            _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IStaticDataService>(), _services.Single<IInputService>()));
        }

        private void RegisterInputService()
        {
            if (Application.isEditor)
            {
                _services.RegisterSingle<IInputService>(new StandaloneInputService());
            }
            else
            {
                _services.RegisterSingle<IInputService>(new MobileInputService());
            }
        }

        private void RegisterSceneLoaderService() => 
            _services.RegisterSingle<ISceneLoaderService>(new SceneLoaderService(_corutineRunner));

        private void RegisterStateMachine() => 
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);

        private void RegisterAssetProviderService() => 
            _services.RegisterSingle<IAssetProviderService>(new AssetProviderService());

        private void EnterLoadMenuState() => 
            _stateMachine.Enter<LoadMenuState>();
    }
}