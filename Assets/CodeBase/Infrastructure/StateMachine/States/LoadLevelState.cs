using System;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Logic.Camera;
using CodeBase.Scene;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.LoadScene;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Spawner;
using CodeBase.Services.StaticData;
using CodeBase.Services.Update;
using CodeBase.UI;
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
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelFactory _levelFactory;
        private readonly IRandomService _randomService;
        private readonly IUpdateService _updateService;
        private readonly IReadWriteDataService _readWriteDataService;
        private readonly IPauseService _pauseService;
        private readonly ISpawnerService _spawnerService;

        private LevelStaticData _currentLevel;
        private LoadingCurtain _curtain;
        
        public LoadLevelState(
            IGameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoaderService,
            IUIFactory uiFactory,
            IPlayerFactory playerFactory,
            IPersistentDataService persistentDataService,
            IStaticDataService staticDataService,
            ILevelFactory levelFactory,
            IRandomService randomService,
            IUpdateService updateService,
            IReadWriteDataService readWriteDataService,
            IPauseService pauseService,
            ISpawnerService spawnerService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoaderService = sceneLoaderService;
            _uiFactory = uiFactory;
            _playerFactory = playerFactory;
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _levelFactory = levelFactory;
            _randomService = randomService;
            _updateService = updateService;
            _readWriteDataService = readWriteDataService;
            _pauseService = pauseService;
            _spawnerService = spawnerService;
        }

        public void Enter()
        {
            LoadCurtain();
            ShowCurtain(OnShowCurtain);
        }

        public void Exit() => 
            HideCurtain();

        private void OnShowCurtain()
        {
            SetCurrentLevel();
            LoadLevelScene();
        }

        private void LoadLevelScene() => 
            _sceneLoaderService.Load(SceneNameConstant.Level, LoadSceneMode.Single, LoadGeometry);

        private void LoadGeometry() => 
            _sceneLoaderService.Load(_currentLevel.SceneName, LoadSceneMode.Additive, OnLoaded);

        private void OnLoaded()
        {
            _randomService.SetConfig(_currentLevel);
            _spawnerService.SetConfig(_currentLevel);
            
            GameObject player = InitPlayer();
            CameraFollow(player);
            
            GameObject generator = InitGenerator();
            InitHUD(generator, player);
            
            InitUIRoot();
            
            _readWriteDataService.InformReaders();
            _pauseService.SetPause(false);
            
            EnterLoopLevelState();
        }

        private void SetCurrentLevel() => 
            _currentLevel = _staticDataService.ForLevel(_persistentDataService.PlayerData.ProgressData.LevelType);

        private void EnterLoopLevelState() => 
            _gameStateMachine.Enter<LoopLevelState>();

        private void InitHUD(GameObject generator, GameObject player) => 
            _uiFactory.LoadHUD(generator, player);

        private void InitUIRoot() => 
            _uiFactory.LoadUIRoot();

        private GameObject InitGenerator() => 
            _levelFactory.LoadGenerator(_randomService.GeneratorSpawnPoint());

        private GameObject InitPlayer() => 
            _playerFactory.CreatePlayer(PlayerTypeId.Default, _randomService.PlayerSpawnPoint());
        
        private void CameraFollow(GameObject player)
        {
            if (Camera.main.TryGetComponent(out CameraFollow cameraFollow))
            {
                cameraFollow.Construct(_updateService);
                cameraFollow.Target = player.transform;
                cameraFollow.MoveToTarget();
            }
        }

        private void LoadCurtain() => 
            _curtain = _uiFactory.LoadLevelCurtain();  
        private void ShowCurtain(Action onShow) => 
            _curtain.Show(SpeedShowCurtain, DelayShowCurtain, onShow);

        private void HideCurtain() => 
            _curtain.Hide(SpeedHideCurtain, DelayHideCurtain, DestroyCurtain);

        private void DestroyCurtain() => 
            Object.Destroy(_curtain.gameObject);
    }
}