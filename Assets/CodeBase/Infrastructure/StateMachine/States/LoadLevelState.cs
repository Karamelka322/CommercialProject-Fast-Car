using System;
using CodeBase.Extension;
using CodeBase.logic.Camera;
using CodeBase.Scene;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.StaticData.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const float SpeedShowCurtain = 1f;
        private const float DelayShowCurtain = 0f;
        private const float SpeedHideCurtain = 1f;
        private const float DelayHideCurtain = 0.5f;

        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IUIFactory _uiFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly IInputService _inputService;

        public LoadLevelState(ISceneLoaderService sceneLoaderService, IUIFactory uiFactory, IPlayerFactory playerFactory, IInputService inputService)
        {
            _sceneLoaderService = sceneLoaderService;
            _uiFactory = uiFactory;
            _playerFactory = playerFactory;
            _inputService = inputService;
        }

        public void Enter()
        {
            if(_uiFactory.LoadingCurtain == null)
            {
                LoadCurtain();
                ShowCurtain(LoadLevelScene);
            }
            else
            {
                LoadLevelScene();
            }
        }

        public void Exit() => 
            _inputService.Clenup();

        private void LoadLevelScene() => 
            _sceneLoaderService.Load(SceneNameConstant.Level, LoadSceneMode.Single, LoadGeometry);

        private void LoadGeometry() => 
            _sceneLoaderService.Load(SceneNameConstant.City_Geometry, LoadSceneMode.Additive, LoadLevel);

        private void LoadLevel()
        {
            _uiFactory.LoadUIRoot();
            _uiFactory.LoadHUD();
            
            GameObject player = _playerFactory.CreatePlayer(PlayerTypeId.Default, Vector3.up);
            SceneManager.GetActiveScene().FindComponentInRootGameObjects<CameraFollow>().Target = player.transform;
            
            HideCurtain(DestroyCurtain);
        }

        private void LoadCurtain() => 
            _uiFactory.LoadLoadingLevelCurtain();

        private void ShowCurtain(Action callBack) => 
            _uiFactory.LoadingCurtain.Show(SpeedShowCurtain, DelayShowCurtain, callBack);

        private void HideCurtain(Action callBack) => 
            _uiFactory?.LoadingCurtain.Hide(SpeedHideCurtain, DelayHideCurtain, callBack);

        private void DestroyCurtain() => 
            Object.Destroy(_uiFactory.LoadingCurtain.gameObject);
    }
}