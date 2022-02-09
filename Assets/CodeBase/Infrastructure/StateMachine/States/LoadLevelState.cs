using System;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Extension;
using CodeBase.Logic.Camera;
using CodeBase.Scene;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
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
        private readonly ILevelFactory _levelFactory;
        private readonly IEnemyFactory _enemyFactory;

        private LevelStaticData _levelStaticData;

        public LoadLevelState(
            ISceneLoaderService sceneLoaderService,
            IUIFactory uiFactory,
            IPlayerFactory playerFactory,
            IInputService inputService,
            IPersistentDataService persistentDataService,
            IStaticDataService staticDataService,
            ILevelFactory levelFactory,
            IEnemyFactory enemyFactory)
        {
            _sceneLoaderService = sceneLoaderService;
            _uiFactory = uiFactory;
            _playerFactory = playerFactory;
            _inputService = inputService;
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _levelFactory = levelFactory;
            _enemyFactory = enemyFactory;
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
            InitUIRoot();
            InitHUD();
            InitGenerator();

            GameObject player = InitPlayer();
            InitEnemy(player.transform);
            CameraFollow(player);
            
            HideCurtain(DestroyCurtain);
        }

        private void InitEnemy(Transform player) => 
            _enemyFactory.CreateEnemy(player, RandomEnemySpawnPoint() + Vector3.up);

        private Vector3 RandomEnemySpawnPoint() => 
            _levelStaticData.Geometry.EnemySpawnPoint[Random.Range(0, _levelStaticData.Geometry.EnemySpawnPoint.Count)];

        private void InitHUD() => 
            _uiFactory.LoadHUD();

        private void InitUIRoot() => 
            _uiFactory.LoadUIRoot();

        private void InitGenerator() => 
            _levelFactory.LoadGenerator(RandomGeneratorSpawnPoint());

        private Vector3 RandomGeneratorSpawnPoint() => 
            _levelStaticData.Geometry.GeneratorSpawnPoint[Random.Range(0, _levelStaticData.Geometry.GeneratorSpawnPoint.Count)];

        private GameObject InitPlayer() => 
            _playerFactory.CreatePlayer(PlayerTypeId.Default, RandomPlayerSpawnPoint() + Vector3.up);

        private Vector3 RandomPlayerSpawnPoint() => 
            _levelStaticData.Geometry.PlayerSpawnPoints[Random.Range(0, _levelStaticData.Geometry.PlayerSpawnPoints.Count)];

        private static void CameraFollow(GameObject player) => 
            SceneManager.GetActiveScene().FindComponentInRootGameObjects<CameraFollow>().Target = player.transform;

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