using System;
using CodeBase.Infrastructure;
using CodeBase.Logic.Level.Generator;
using CodeBase.Logic.Player;
using CodeBase.Scene.Menu;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Input;
using CodeBase.Services.Pause;
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
        private readonly IPauseService _pauseService;
        private readonly IReadWriteDataService _readWriteDataService;

        public Transform UIRoot { get; private set; }
        public LoadingCurtain LoadingCurtain { get; private set; }
        public HUD HUD { get; private set; }
        
        public UIFactory(
            IGameStateMachine stateMachine,
            IAssetProviderService assetProvider,
            IPersistentDataService persistentDataService,
            IStaticDataService staticDataService,
            IInputService inputService,
            ITweenService tweenService,
            IPauseService pauseService,
            IReadWriteDataService readWriteDataService)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _tweenService = tweenService;
            _pauseService = pauseService;
            _readWriteDataService = readWriteDataService;
        }

        public LoadingCurtain LoadMenuCurtain()
        {
            LoadingCurtain curtain = Object.Instantiate(_assetProvider.LoadMenuCurtain());
            curtain.Construct(_tweenService);
            return LoadingCurtain = curtain;
        }

        public LoadingCurtain LoadLevelCurtain()
        {
            LoadingCurtain curtain = Object.Instantiate(_assetProvider.LoadLevelCurtain());
            curtain.Construct(_tweenService);
            return LoadingCurtain = curtain;
        }

        public GameObject LoadMainButtonInMenu(MenuAnimator menuAnimator)
        {
            GameObject mainButtonInMenu = Object.Instantiate(_assetProvider.LoadMainButtonInMenu(), UIRoot);
            
            mainButtonInMenu.GetComponentInChildren<PlayButton>().Construct(_stateMachine, menuAnimator);
            mainButtonInMenu.GetComponentInChildren<SettingsButton>().Construct(menuAnimator);
            mainButtonInMenu.GetComponentInChildren<GarageButton>().Construct(menuAnimator);
            
            return mainButtonInMenu;
        }

        public GameObject LoadSettingsInMenu(Action backEvent)
        {
            GameObject settings = Object.Instantiate(_assetProvider.LoadSettingsInMenu(), UIRoot);
            
            settings.GetComponentInChildren<BackButton>().Construct(backEvent);
            settings.GetComponentInChildren<InputSettings>().Construct(_persistentDataService.PlayerData.InputData);
            
            return settings;
        }

        public GameObject LoadGarageInMenu(Action backEvent)
        {
            GameObject garage = Object.Instantiate(_assetProvider.LoadGarageInMenu(), UIRoot);
            
            garage.GetComponentInChildren<BackButton>().Construct(backEvent);
            
            return garage;
        }

        public void LoadHUD(GameObject generator, GameObject player)
        {
            HUD = Object.Instantiate(_assetProvider.LoadHUD());
            _readWriteDataService.Register(HUD.gameObject);
            
            GameObject prefab = _staticDataService.ForInput(_persistentDataService.PlayerData.InputData.Type);
            GameObject inputVariant = Object.Instantiate(prefab, HUD.InputContainer);
            _inputService.RegisterInput(_persistentDataService.PlayerData.InputData.Type, inputVariant);

            if (generator.TryGetComponent(out GeneratorPower generatorPower))
                HUD.GeneratorPowerBar.Construct(generatorPower);

            if (player.TryGetComponent(out PlayerHealth playerHealth))
                HUD.PlayerHealthBar.Construct(playerHealth);
            
            HUD.gameObject.GetComponentInChildren<PauseButton>().Construct(this, _pauseService);
        }

        public void LoadPauseWindow()
        {
            GameObject prefab = _assetProvider.LoadPauseWindow();
            GameObject window = Object.Instantiate(prefab, UIRoot.transform);
            
            window.GetComponentInChildren<HomeButton>().Construct(_stateMachine);
            window.GetComponentInChildren<ResumeButton>().Construct(_pauseService);
            window.GetComponentInChildren<ReplayButton>().Construct(_stateMachine);
        }

        public void LoadSkipButton(MenuAnimator menuAnimator)
        {
            SkipButton skipButton = Object.Instantiate(_assetProvider.LoadSkipButton(), UIRoot);
            skipButton.Construct(menuAnimator);
        }

        public void LoadUIRoot() => 
            UIRoot = Object.Instantiate(_assetProvider.LoadUIRoot()).transform;
    }
}