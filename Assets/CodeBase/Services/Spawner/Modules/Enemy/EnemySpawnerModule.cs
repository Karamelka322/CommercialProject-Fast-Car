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
        private readonly ICorutineRunner _corutineRunner;
        private readonly IRandomService _randomService;
        private readonly IEnemyFactory _enemyFactory;

        private EnemySpawnConfig[] _spawnData;
        private LevelStaticData _config;

        private float StopwatchTime => _persistentDataService.PlayerData.SessionData.StopwatchTime;

        public EnemySpawnerModule(IRandomService randomService, IEnemyFactory enemyFactory, IPersistentDataService persistentDataService, ICorutineRunner corutineRunner)
        {
            _persistentDataService = persistentDataService;
            _corutineRunner = corutineRunner;
            _randomService = randomService;
            _enemyFactory = enemyFactory;
        }

        public void SetConfig(LevelStaticData levelConfig)
        {
            if(levelConfig.Enemy.UsingEnemy == false)
                return;
            
            _config = levelConfig;
            _spawnData = Copy(levelConfig.Enemy.Enemies);
        }

        public void TrySpawnEnemy()
        {
            if (IsSpawnedEnemy(out List<EnemySpawnConfig> spawnEnemyStaticData)) 
                SpawnEnemy(spawnEnemyStaticData);
        }

        public void ClenupModule()
        {
            _config = null;
            _spawnData = Array.Empty<EnemySpawnConfig>();
        }

        private bool IsSpawnedEnemy(out List<EnemySpawnConfig> enemySpawnConfig)
        {
            if (_config != null && _config.Enemy.UsingEnemy && _randomService.GetNumberInlockedEnemySpawnPoints() > 0)
            {
                enemySpawnConfig = GetSpawnConfigs();
                return true;
            }
            else
            {
                enemySpawnConfig = null;
                return false;
            }
        }

        private void SpawnEnemy(IReadOnlyList<EnemySpawnConfig> enemySpawnConfigs)
        {
            for (int i = 0; i < enemySpawnConfigs.Count; i++)
            {
                PointData spawnPoint = _randomService.EnemySpawnPoint();
                _enemyFactory.CreateEnemy(enemySpawnConfigs[i].EnemyType, enemySpawnConfigs[i].DifficultyType, spawnPoint + Vector3.up);
                _randomService.BindTimeToSpawnPoint(TimeSpawnEnemy, spawnPoint);

                BlockSpawnConfigForWhile(enemySpawnConfigs[i]);       
            }
        }

        private List<EnemySpawnConfig> GetSpawnConfigs()
        {
            List<EnemySpawnConfig> _configs = new List<EnemySpawnConfig>();

            for (int i = 0; i < _spawnData.Length; i++)
            {
                if (_spawnData[i].Period.x <= StopwatchTime && _spawnData[i].Period.y >= StopwatchTime && _spawnData[i].IsLocked == false)
                {
                    _configs.Add(_spawnData[i]);
                }
            }

            if (_configs.Count == 0 && _spawnData[_spawnData.Length - 1].Period.y < StopwatchTime) 
                SetInfinityPeriod();

            return _configs;
        }

        private void SetInfinityPeriod() => 
            _spawnData[_spawnData.Length - 1].Period.y = int.MaxValue;

        private void BlockSpawnConfigForWhile(in EnemySpawnConfig enemySpawnConfig) => 
            _corutineRunner.StartCoroutine(BlockSpawnConfig(enemySpawnConfig));

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