using System;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Logic.Camera;
using CodeBase.Scene;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.StaticData;
using CodeBase.Services.Update;
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

        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IUIFactory _uiFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly IInputService _inputService;
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelFactory _levelFactory;
        private readonly IRandomService _randomService;
        private readonly IUpdateService _updateService;

        private LevelStaticData _levelStaticData;

        public LoadLevelState(
            IGameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoaderService,
            IUIFactory uiFactory,
            IPlayerFactory playerFactory,
            IInputService inputService,
            IPersistentDataService persistentDataService,
            IStaticDataService staticDataService,
            ILevelFactory levelFactory,
            IRandomService randomService,
            IUpdateService updateService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoaderService = sceneLoaderService;
            _uiFactory = uiFactory;
            _playerFactory = playerFactory;
            _inputService = inputService;
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _levelFactory = levelFactory;
            _randomService = randomService;
            _updateService = updateService;
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

        public void Exit()
        {
            _inputService.Clenup();

            HideCurtain(DestroyCurtain);
        }

        private void LoadLevelScene()
        {
            _sceneLoaderService.Load(SceneNameConstant.Level, LoadSceneMode.Single, LoadGeometry);
        }

        private void LoadGeometry()
        {
            Camera.main.GetComponent<CameraFollow>().Construct(_updateService);
            _sceneLoaderService.Load(_levelStaticData.Geometry.SceneName, LoadSceneMode.Additive, OnLoaded);
        }

        private void OnLoaded()
        {
            InitHUD();
            InitUIRoot();
            InitGenerator();

            GameObject player = InitPlayer();
            CameraFollow(player);
            
            EnterLoopLevelState();
        }

        private void EnterLoopLevelState() => 
            _gameStateMachine.Enter<LoopLevelState>();

        private void InitHUD() => 
            _uiFactory.LoadHUD();

        private void InitUIRoot() => 
            _uiFactory.LoadUIRoot();

        private void InitGenerator() => 
            _levelFactory.LoadGenerator(_randomService.GeneratorSpawnPoint());

        private GameObject InitPlayer() => 
            _playerFactory.CreatePlayer(PlayerTypeId.Default, _randomService.PlayerSpawnPoint());
        
        private void CameraFollow(GameObject player)
        {
            if (Camera.main.TryGetComponent(out CameraFollow cameraFollow))
            {
                cameraFollow.Target = player.transform;
            }
        }

        private void LoadCurtain() => 
            _uiFactory.LoadLoadingLevelCurtain();

        private void ShowCurtain(Action callBack) => 
            _uiFactory.LoadingCurtain.Show(SpeedShowCurtain, DelayShowCurtain, callBack);

        private void HideCurtain(Action callBack) => 
            _uiFactory.LoadingCurtain.Hide(SpeedHideCurtain, DelayHideCurtain, callBack);

        private void DestroyCurtain() => 
            Object.Destroy(_uiFactory.LoadingCurtain.gameObject);
    }
}