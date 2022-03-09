using System;
using CodeBase.Extension;
using CodeBase.Scene;
using CodeBase.Scene.Menu;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.LoadScene;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.States
{
    public class LoadMenuState : IState
    {
        private const float SpeedShowCurtain = 1f;
        private const float DelayShowCurtain = 0f;
        private const float SpeedHideCurtain = 1f;
        private const float DelayHideCurtain = 0.5f;

        private readonly ISceneLoaderService _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IPauseService _pauseService;
        private readonly IReplayService _replayService;

        private bool _isFirstLoad = true;
        private LoadingCurtain _curtain;
        
        public LoadMenuState(ISceneLoaderService sceneLoader, IUIFactory uiFactory, IPauseService pauseService, IReplayService replayService)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _pauseService = pauseService;
            _replayService = replayService;
        }

        public void Enter()
        {
            LoadCurtain();
            ShowCurtain(LoadScene);
        }

        public void Exit()
        {
            _pauseService.Clenup();
            _replayService.Clenup();
        }

        private void LoadScene() => 
            _sceneLoader.Load(SceneNameConstant.Menu, LoadSceneMode.Single, OnLoaded);

        private void OnLoaded()
        {
            _uiFactory.LoadUIRoot();
            
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            scene.FindComponentInRootGameObjects<MenuUIViewer>()?.Construct(_uiFactory);
            
            if(_isFirstLoad)
            {
                _isFirstLoad = false;
                scene.FindComponentInRootGameObjects<MenuAnimator>()?.PlayOpenMenu();
            }
            else
            {
                scene.FindComponentInRootGameObjects<MenuAnimator>()?.PlayIdleMenu();
            }
            
            HideCurtain();
        }

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