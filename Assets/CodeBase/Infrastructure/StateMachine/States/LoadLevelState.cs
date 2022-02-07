using System;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Extension;
using CodeBase.Logic.Camera;
using CodeBase.Logic.Level.Generator;
using CodeBase.Scene;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStaticDataService _staticDataService;
        private readonly ITweenService _tweenService;
        private readonly IReadWriteDataService _readWriteDataService;

        private LevelStaticData _levelStaticData;

        public LoadLevelState(
            ISceneLoaderService sceneLoaderService,
            IUIFactory uiFactory,
            IPlayerFactory playerFactory,
            IInputService inputService,
            IPersistentDataService persistentDataService,
            IStaticDataService staticDataService,
            ITweenService tweenService,
            IReadWriteDataService readWriteDataService)
        {
            _sceneLoaderService = sceneLoaderService;
            _uiFactory = uiFactory;
            _playerFactory = playerFactory;
            _inputService = inputService;
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _tweenService = tweenService;
            _readWriteDataService = readWriteDataService;
        }

        public void Enter()
        {
            _levelStaticData = _staticDataService.ForLevel(_persistentDataService.PlayerData.LevelData.Type);
            
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
            _sceneLoaderService.Load(_levelStaticData.Geometry.SceneName, LoadSceneMode.Additive, OnLoaded);

        private void OnLoaded()
        {
            _uiFactory.LoadUIRoot();
            _uiFactory.LoadHUD();
            
            GameObject player = InitPlayer();
            
            UnityEngine.SceneManagement.Scene activeScene = SceneManager.GetSceneByName(SceneNameConstant.Level);
            activeScene.FindComponentInRootGameObjects<CameraFollow>().Target = player.transform;

            UnityEngine.SceneManagement.Scene additiveScene = SceneManager.GetSceneByName(_levelStaticData.Geometry.SceneName);
            additiveScene.FindComponentInRootGameObjects<GeneratorHook>().Construct(_tweenService);
            
            GeneratorPower generatorPower = additiveScene.FindComponentInRootGameObjects<GeneratorPower>();
            generatorPower.Construct(_levelStaticData);
            
            activeScene.FindComponentInRootGameObjects<GeneratorPowerBar>().Construct(generatorPower);

            InformReaders();
            
            HideCurtain(DestroyCurtain);
        }

        private void InformReaders() => 
            _readWriteDataService.InformReaders(_persistentDataService.PlayerData);

        private GameObject InitPlayer() => 
            _playerFactory.CreatePlayer(PlayerTypeId.Default, RandomPlayerSpawnPoint() + Vector3.up);

        private Vector3 RandomPlayerSpawnPoint() => 
            _levelStaticData.Geometry.PlayerSpawnPoints[Random.Range(0, _levelStaticData.Geometry.PlayerSpawnPoints.Count)];

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