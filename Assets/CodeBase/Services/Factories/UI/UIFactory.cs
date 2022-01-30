using CodeBase.Infrastructure;
using CodeBase.Services.AssetProvider;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAssetProviderService _assetProvider;

        private GameObject _uiRoot;
        
        public LoadingCurtain LoadingCurtain { get; private set; }

        public UIFactory(IGameStateMachine stateMachine,IAssetProviderService assetProvider)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
        }

        public LoadingCurtain LoadLoadingMenuCurtain() => 
            LoadingCurtain = Object.Instantiate(_assetProvider.LoadLoadingMenuCurtain());

        public LoadingCurtain LoadLoadingLevelCurtain() => 
            LoadingCurtain = Object.Instantiate(_assetProvider.LoadLoadingLevelCurtain());

        public GameObject LoadMainButtonInMenu()
        {
            GameObject mainButtonInMenu = Object.Instantiate(_assetProvider.LoadMainButtonInMenu(), _uiRoot.transform);
            
            mainButtonInMenu.GetComponentInChildren<PlayButton>().Construct(_stateMachine);
            
            return mainButtonInMenu;
        }

        public SkipButton LoadSkipButton() => 
            Object.Instantiate(_assetProvider.LoadSkipButton(), _uiRoot.transform);

        public GameObject LoadUIRoot() => 
            _uiRoot = Object.Instantiate(_assetProvider.LoadUIRoot());
    }
}