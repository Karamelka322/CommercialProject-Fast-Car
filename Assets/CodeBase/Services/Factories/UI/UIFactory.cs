using System;
using CodeBase.Infrastructure;
using CodeBase.Scene.Menu;
using CodeBase.Services.AssetProvider;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using UnityEngine;
using Object = UnityEngine.Object;

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

        public GameObject LoadMainButtonInMenu(MenuAnimator menuAnimator)
        {
            GameObject mainButtonInMenu = Object.Instantiate(_assetProvider.LoadMainButtonInMenu(), _uiRoot.transform);
            
            mainButtonInMenu.GetComponentInChildren<PlayButton>().Construct(_stateMachine, menuAnimator);
            mainButtonInMenu.GetComponentInChildren<SettingsButton>().Construct(menuAnimator);
            mainButtonInMenu.GetComponentInChildren<GarageButton>().Construct(menuAnimator);
            
            return mainButtonInMenu;
        }

        public GameObject LoadSettingsInMenu(Action backEvent)
        {
            GameObject settings = Object.Instantiate(_assetProvider.LoadSettingsInMenu(), _uiRoot.transform);
            
            settings.GetComponentInChildren<BackButton>().Construct(backEvent);
            
            return settings;
        }

        public GameObject LoadGarageInMenu(Action backEvent)
        {
            GameObject garage = Object.Instantiate(_assetProvider.LoadGarageInMenu(), _uiRoot.transform);
            
            garage.GetComponentInChildren<BackButton>().Construct(backEvent);
            
            return garage;
        }

        
        public GameObject LoadSkipButton(MenuAnimator menuAnimator)
        {
            SkipButton skipButton = Object.Instantiate(_assetProvider.LoadSkipButton(), _uiRoot.transform);
            
            skipButton.Construct(menuAnimator);
            
            return skipButton.gameObject;
        }

        public GameObject LoadUIRoot() => 
            _uiRoot = Object.Instantiate(_assetProvider.LoadUIRoot());
    }
}