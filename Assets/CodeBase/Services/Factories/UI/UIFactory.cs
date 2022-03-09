using System;
using CodeBase.Infrastructure;
using CodeBase.Scene.Menu;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Input;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Replay;
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
        private readonly IReplayService _replayService;

        public Transform UIRoot { get; private set; }

        public UIFactory(
            IGameStateMachine stateMachine,
            IAssetProviderService assetProvider,
            IPersistentDataService persistentDataService,
            IStaticDataService staticDataService,
            IInputService inputService,
            ITweenService tweenService,
            IPauseService pauseService,
            IReadWriteDataService readWriteDataService,
            IReplayService replayService)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _tweenService = tweenService;
            _pauseService = pauseService;
            _readWriteDataService = readWriteDataService;
            _replayService = replayService;
        }

        public LoadingCurtain LoadMenuCurtain()
        {
            LoadingCurtain curtain = Object.Instantiate(_assetProvider.LoadMenuCurtain());
            curtain.Construct(_tweenService);
            return curtain;
        }

        public LoadingCurtain LoadLevelCurtain()
        {
            LoadingCurtain curtain = Object.Instantiate(_assetProvider.LoadLevelCurtain());
            curtain.Construct(_tweenService);
            return curtain;
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
            settings.GetComponentInChildren<InputSettings>().Construct(_persistentDataService.PlayerData.SettingsData);
            
            return settings;
        }

        public GameObject LoadGarageInMenu(Action backEvent)
        {
            GameObject garage = Object.Instantiate(_assetProvider.LoadGarageInMenu(), UIRoot);
            
            garage.GetComponentInChildren<BackButton>().Construct(backEvent);
            
            return garage;
        }

        public void LoadHUD()
        {
            HUD hud = InstantiateRegister(_assetProvider.LoadHUD());
            
            GameObject prefab = _staticDataService.ForInput(_persistentDataService.PlayerData.SettingsData.InputType);
            GameObject inputVariant = Object.Instantiate(prefab, hud.InputContainer);
            _inputService.RegisterInput(_persistentDataService.PlayerData.SettingsData.InputType, inputVariant);

            hud.gameObject.GetComponentInChildren<PauseButton>().Construct(this, _pauseService);
        }

        public void LoadPauseWindow()
        {
            GameObject prefab = _assetProvider.LoadPauseWindow();
            GameObject window = InstantiateRegister(prefab, UIRoot);
            
            window.GetComponentInChildren<HomeButton>().Construct(_stateMachine);
            window.GetComponentInChildren<ResumeButton>().Construct(_pauseService);
            window.GetComponentInChildren<ReplayButton>().Construct(_stateMachine);
        }

        public GameObject LoadTimer()
        {
            GameObject prefab = _assetProvider.LoadTimer();
            return Object.Instantiate(prefab, UIRoot);
        }

        public void LoadDefeatWindow()
        {
            GameObject prefab = _assetProvider.LoadDefeatWindow();
            GameObject window = InstantiateRegister(prefab, UIRoot);
            
            window.GetComponentInChildren<HomeButton>().Construct(_stateMachine);
            window.GetComponentInChildren<ReplayButton>().Construct(_stateMachine);
        }

        public void LoadVictoryWindow()
        {
            GameObject prefab = _assetProvider.LoadVictoryWindow();
            GameObject window = InstantiateRegister(prefab, UIRoot);
            
            window.GetComponentInChildren<HomeButton>().Construct(_stateMachine);
            window.GetComponentInChildren<NextLevelButton>().Construct(_stateMachine);
        }

        public void LoadSkipButton(MenuAnimator menuAnimator)
        {
            SkipButton skipButton = Object.Instantiate(_assetProvider.LoadSkipButton(), UIRoot);
            skipButton.Construct(menuAnimator);
        }

        public void LoadUIRoot() => 
            UIRoot = Object.Instantiate(_assetProvider.LoadUIRoot()).transform;

        private T InstantiateRegister<T>(T prefab) where T : MonoBehaviour
        {
            T monoBehaviour = Object.Instantiate(prefab);
            _readWriteDataService.Register(monoBehaviour.gameObject);
            _replayService.Register(monoBehaviour.gameObject);
            return monoBehaviour;
        }
        
        private GameObject InstantiateRegister(GameObject prefab, Transform parent)
        {
            GameObject obj = Object.Instantiate(prefab, parent);
            _replayService.Register(obj);
            return obj;
        }
    }
}