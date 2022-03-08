using CodeBase.Data.Static.Enemy;
using CodeBase.Logic.Car;
using CodeBase.Logic.Enemy;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Defeat;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Pause;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.StaticData;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;

namespace CodeBase.Services.Factories.Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly IUpdateService _updateService;
        private readonly IPauseService _pauseService;
        private readonly IReplayService _replayService;
        private readonly IPlayerFactory _playerFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IDefeatService _defeatService;
        private readonly IVictoryService _victoryService;

        public EnemyFactory(
            IAssetProviderService assetProviderService,
            IUpdateService updateService,
            IPauseService pauseService,
            IReplayService replayService,
            IPlayerFactory playerFactory,
            IStaticDataService staticDataService,
            IDefeatService defeatService, 
            IVictoryService victoryService)
        {
            _assetProviderService = assetProviderService;
            _updateService = updateService;
            _pauseService = pauseService;
            _replayService = replayService;
            _playerFactory = playerFactory;
            _staticDataService = staticDataService;
            _defeatService = defeatService;
            _victoryService = victoryService;
        }

        public void CreateEnemy(EnemyTypeId enemyType, EnemyDifficultyTypeId difficultyType, PointData spawnPoint)
        {
            GameObject prefab = _staticDataService.ForEnemy(enemyType, difficultyType).Prefab.gameObject;
            GameObject enemy = InstantiateRegister(prefab, spawnPoint);
         
            if(enemy.TryGetComponent(out EnemyMovement movement))
                movement.Construct(_updateService);
            
            enemy.GetComponentInChildren<NavMeshAgentWrapper>()?.Construct(_updateService, _playerFactory.Player.transform);
            
            foreach (Wheel wheel in enemy.GetComponentsInChildren<Wheel>()) 
                wheel.Construct(_updateService);
        }

        private GameObject InstantiateRegister(GameObject prefab, PointData spawnPoint)
        {
            GameObject gameObject = Object.Instantiate(prefab, spawnPoint.Position, spawnPoint.Rotation);
            _pauseService.Register(gameObject);
            _replayService.Register(gameObject);
            _defeatService.Register(gameObject);
            _victoryService.Register(gameObject);
            return gameObject;
        }
    }
}