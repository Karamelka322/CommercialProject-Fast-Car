using CodeBase.Data.Static.Enemy;
using CodeBase.Data.Static.Level;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Services.Spawner
{
    public class EnemySpawnerModule : IEnemySpawnerModule
    {
        private const float TimeSpawnEnemy = 2f;
        
        private readonly IRandomService _randomService;
        private readonly IEnemyFactory _enemyFactory;
        
        private LevelStaticData _config;

        public EnemySpawnerModule(IRandomService randomService, IEnemyFactory enemyFactory)
        {
            _randomService = randomService;
            _enemyFactory = enemyFactory;
        }

        public void SetConfig(LevelStaticData levelConfig) => 
            _config = levelConfig;

        public void TrySpawnEnemy()
        {
            if (IsSpawnedEnemy())
                SpawnEnemy();
        }

        public void ClenupModule() => 
            _config = null;

        private bool IsSpawnedEnemy() => 
            _config != null && _randomService.GetNumberInlockedEnemySpawnPoints() > 0;

        private void SpawnEnemy()
        {
            PointData spawnPoint = _randomService.EnemySpawnPoint();
            _enemyFactory.CreateEnemy(EnemyTypeId.Car, EnemyDifficultyTypeId.VeryEasy, spawnPoint + Vector3.up);
            _randomService.BindTimeToSpawnPoint(TimeSpawnEnemy, spawnPoint);
        }
    }
}