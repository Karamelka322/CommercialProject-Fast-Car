using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data.Static.Level;
using CodeBase.Infrastructure;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Services.Spawner
{
    public class EnemySpawnerModule : IEnemySpawnerModule
    {
        private const float TimeSpawnEnemy = 2f;

        private readonly IPersistentDataService _persistentDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IRandomService _randomService;
        private readonly IEnemyFactory _enemyFactory;

        private EnemySpawnConfig[] _configs;
        
        private float StopwatchTime => _persistentDataService.PlayerData.SessionData.LevelData.StopwatchTime;

        public EnemySpawnerModule(IRandomService randomService, IEnemyFactory enemyFactory, IPersistentDataService persistentDataService, ICoroutineRunner coroutineRunner)
        {
            _persistentDataService = persistentDataService;
            _coroutineRunner = coroutineRunner;
            _randomService = randomService;
            _enemyFactory = enemyFactory;
        }

        public void SetConfig(EnemiesSpawnConfig config) => 
            _configs = Copy(config.Enemies);

        public void TrySpawnEnemy()
        {
            if (IsSpawnedEnemy(out List<EnemySpawnConfig> spawnEnemyStaticData)) 
                SpawnEnemy(spawnEnemyStaticData);
        }

        public void Clear() => 
            _configs = Array.Empty<EnemySpawnConfig>();

        public void ResetModule()
        {
            foreach (EnemySpawnConfig config in _configs)
                config.IsLocked = false;
        }

        private bool IsSpawnedEnemy(out List<EnemySpawnConfig> enemySpawnConfig)
        {
            if (_randomService.GetNumberInlockedEnemySpawnPoints() > 0)
            {
                enemySpawnConfig = GetSpawnConfigs();
                return true;
            }

            enemySpawnConfig = null;
            return false;
        }

        private async void SpawnEnemy(List<EnemySpawnConfig> enemySpawnConfigs)
        {
            for (int i = 0; i < enemySpawnConfigs.Count; i++)
            {
                PointData spawnPoint = _randomService.EnemySpawnPoint();
                await _enemyFactory.CreateEnemy(enemySpawnConfigs[i].EnemyType, enemySpawnConfigs[i].DifficultyType, spawnPoint + Vector3.up);
                _randomService.BindTimeToSpawnPoint(TimeSpawnEnemy, spawnPoint);

                if(IsBlockSpawnConfig(enemySpawnConfigs[i])) 
                    BlockSpawnConfigForWhile(enemySpawnConfigs[i]);
            }
        }

        private static bool IsBlockSpawnConfig(EnemySpawnConfig config) => 
            config.Period.y - config.Period.x > config.Range;

        private List<EnemySpawnConfig> GetSpawnConfigs()
        {
            List<EnemySpawnConfig> _configs = new List<EnemySpawnConfig>();

            for (int i = 0; i < this._configs.Length; i++)
            {
                if (this._configs[i].Period.x <= StopwatchTime && this._configs[i].Period.y >= StopwatchTime && this._configs[i].IsLocked == false)
                {
                    _configs.Add(this._configs[i]);
                }
            }

            return _configs;
        }

        private void BlockSpawnConfigForWhile(in EnemySpawnConfig enemySpawnConfig) => 
            _coroutineRunner.StartCoroutine(BlockSpawnConfig(enemySpawnConfig));

        private static IEnumerator BlockSpawnConfig(EnemySpawnConfig enemySpawnConfig)
        {
            enemySpawnConfig.IsLocked = true;

            yield return new WaitForSeconds(enemySpawnConfig.Range);
            
            enemySpawnConfig.IsLocked = false;
        }

        private static EnemySpawnConfig[] Copy(in EnemySpawnConfig[] targetArray)
        {
            EnemySpawnConfig[] currentArray = new EnemySpawnConfig[targetArray.Length];

            for (int i = 0; i < currentArray.Length; i++)
            {
                currentArray[i] = new EnemySpawnConfig()
                {
                    EnemyType = targetArray[i].EnemyType,
                    DifficultyType = targetArray[i].DifficultyType,
                    IsLocked = false,
                    Period = targetArray[i].Period,
                    Range = targetArray[i].Range,
                };
            }

            return currentArray;
        }
    }
}