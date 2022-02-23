using System;
using CodeBase.Extension;
using CodeBase.Scene;
using CodeBase.Scene.Menu;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.LoadScene;
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
        
        public LoadMenuState(ISceneLoaderService sceneLoader, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            if(_uiFactory.LoadingCurtain == null)
            {
                LoadCurtain();
                ShowCurtain(LoadScene);
            }
            else
            {
                LoadScene();
            }
        }

        public void Exit() { }

        private void LoadScene() => 
            _sceneLoader.Load(SceneNameConstant.Menu, LoadSceneMode.Single, OnLoaded);

        private void OnLoaded()
        {
            _uiFactory.LoadUIRoot();
            
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            scene.FindComponentInRootGameObjects<MenuUIViewer>()?.Construct(_uiFactory);
            scene.FindComponentInRootGameObjects<MenuAnimator>()?.PlayOpenMenu();
            
            HideCurtain(DestroyCurtain);
        }

        private void LoadCurtain() => 
            _uiFactory.LoadMenuCurtain();

        private void ShowCurtain(Action callBack) => 
            _uiFactory.LoadingCurtain.Show(SpeedShowCurtain, DelayShowCurtain, callBack);

        private void HideCurtain(Action callBack) => 
            _uiFactory?.LoadingCurtain.Hide(SpeedHideCurtain, DelayHideCurtain, callBack);

        private void DestroyCurtain() => 
            Object.Destroy(_uiFactory.LoadingCurtain.gameObject);
    }
}