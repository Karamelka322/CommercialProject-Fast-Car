using System;
using CodeBase.Infrastructure;
using CodeBase.Scene.Menu;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
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
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly ITweenService _tweenService;

        private GameObject _uiRoot;
        
        public LoadingCurtain LoadingCurtain { get; private set; }
        public HUD HUD { get; private set; }
        
        public UIFactory(
            IGameStateMachine stateMachine,
            IAssetProviderService assetProvider,
            IPersistentDataService persistentDataService,
            IStaticDataService staticDataService,
            IInputService inputService,
            ITweenService tweenService)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _tweenService = tweenService;
        }

        public LoadingCurtain LoadLoadingMenuCurtain()
        {
            LoadingCurtain curtain = Object.Instantiate(_assetProvider.LoadLoadingMenuCurtain());
            curtain.Construct(_tweenService);
            return LoadingCurtain = curtain;
        }

        public LoadingCurtain LoadLoadingLevelCurtain()
        {
            LoadingCurtain curtain = Object.Instantiate(_assetProvider.LoadLoadingLevelCurtain());
            curtain.Construct(_tweenService);
            return LoadingCurtain = curtain;
        }

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
            
            settings.GetComponentInChildren<BackButton>()?.Construct(backEvent);
            settings.GetComponentInChildren<InputSettings>().Construct(_persistentDataService.PlayerData.InputData);
            
            return settings;
        }

        public GameObject LoadGarageInMenu(Action backEvent)
        {
            GameObject garage = Object.Instantiate(_assetProvider.LoadGarageInMenu(), _uiRoot.transform);
            
            garage.GetComponentInChildren<BackButton>().Construct(backEvent);
            
            return garage;
        }

        public GameObject LoadHUD()
        {
            HUD = Object.Instantiate(_assetProvider.LoadHUD());

            GameObject prefab = _staticDataService.ForInput(_persistentDataService.PlayerData.InputData.Type);
            GameObject inputVariant = Object.Instantiate(prefab, HUD.ControlContainer);
            _inputService.RegisterInput(_persistentDataService.PlayerData.InputData.Type, inputVariant);
            
            HUD.gameObject.GetComponentInChildren<GeneratorPowerBar>()?.Construct(_persistentDataService.PlayerData.SessionData.GeneratorData);
            HUD.gameObject.GetComponentInChildren<PlayerHealthBar>()?.Construct(_persistentDataService.PlayerData.SessionData.PlayerData);
            HUD.gameObject.GetComponentInChildren<PauseButton>()?.Construct(this);

            return HUD.gameObject;
        }

        public void LoadPauseWindow()
        {
            GameObject prefab = _assetProvider.LoadPauseWindow();
            GameObject window = Object.Instantiate(prefab, _uiRoot.transform);
            
            window.GetComponentInChildren<HomeButton>()?.Construct(_stateMachine);
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