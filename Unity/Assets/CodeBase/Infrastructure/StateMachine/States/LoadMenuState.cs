using System;
using CodeBase.Data.Static.Player;
using CodeBase.Extension;
using CodeBase.Logic.Menu;
using CodeBase.Mediator;
using CodeBase.Scene.Menu;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.LoadScene;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.States
{
    public class LoadMenuState : IState
    {
        private const string MenuSceneName = "Menu";

        private const float SpeedShowCurtain = 1f;
        private const float DelayShowCurtain = 0f;
        private const float SpeedHideCurtain = 1f;
        private const float DelayHideCurtain = 0.5f;

        private readonly IPersistentDataService _persistentDataService;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly IUIFactory _uiFactory;

        private PlayerTypeId CurrentPlayer => _persistentDataService.PlayerData.ProgressData.CurrentPlayer;
        
        private bool _isFirstLoad = true;
        private LoadingCurtain _curtain;

        public LoadMenuState(
            ISceneLoaderService sceneLoader,
            IUIFactory uiFactory,
            IPlayerFactory playerFactory,
            IPersistentDataService persistentDataService)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _playerFactory = playerFactory;
            _persistentDataService = persistentDataService;
        }

        public void Enter()
        {
            LoadCurtain();
            ShowCurtain(OnShowCurtain);
        }

        public void Exit() => 
            _isFirstLoad = false;

        private void OnShowCurtain() => 
            _sceneLoader.Load(MenuSceneName, LoadSceneMode.Single, OnLoaded);

        private void OnLoaded()
        {
            _uiFactory.LoadUIRoot();

            IMenuMediator mediator = SceneManager.GetActiveScene().FindComponentInRootGameObjects<IMenuMediator>();

            InitPreviewPlayer(mediator.MenuAnimator);

            if(_isFirstLoad)
            {
                mediator.RebindAnimator();
                mediator.ChangeMenuState(MenuState.Intro);
            }
            else
            {
                mediator.RebindAnimator();
                mediator.SkipIntro();
                mediator.ChangeMenuState(MenuState.MainMenu);
            }
            
            HideCurtain();
        }

        private void InitPreviewPlayer(in MenuAnimator menuAnimator) => 
            _playerFactory.CreatePreviewPlayer(CurrentPlayer, menuAnimator.transform);

        private void LoadCurtain() => 
            _curtain = _uiFactory.LoadMenuCurtain();

        private void ShowCurtain(Action onShow) => 
            _curtain.Show(SpeedShowCurtain, DelayShowCurtain, onShow);

        private void HideCurtain() => 
            _curtain.Hide(SpeedHideCurtain, DelayHideCurtain, DestroyCurtain);

        private void DestroyCurtain() => 
            Object.Destroy(_curtain.gameObject);
    }
}