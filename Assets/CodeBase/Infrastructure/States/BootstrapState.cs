using CodeBase.Services.Input;
using CodeBase.Services.Input.AssetProvider;
using CodeBase.Services.Input.Factories.UI;
using CodeBase.Services.Input.LoadScene;
using CodeBase.UI;
using UnityEngine;

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
            ShowCurtain();
            LoadMenuState();
        }

        public void Exit() { }

        private void ShowCurtain()
        {
            LoadingCurtain curtain = _services.Single<IUIFactory>().LoadLoadingMenuCurtain();
            curtain.Show();
        }

        private void LoadMenuState() => 
            _stateMachine.Enter<LoadMenuState>();

        private void RegisterServices()
        {
            RegisterInputService();
            RegisterSceneLoaderService();
            RegisterAssetProviderService();
            
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProviderService>()));
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

        private void RegisterAssetProviderService() => 
            _services.RegisterSingle<IAssetProviderService>(new AssetProviderService());
    }
}