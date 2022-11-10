using System;
using System.Collections;
using CodeBase.Data.Static.Level;
using CodeBase.Extension;
using CodeBase.Infrastructure;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Random
{
    [UsedImplicitly]
    public class RandomService : IRandomService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        private LevelStaticData _config;
        
        private SpawnPointData[] _capsuleSpawnPoints;
        private SpawnPointData[] _enemySpawnPoints;

        public RandomService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void SetConfig(LevelStaticData levelConfig)
        {
            _config = levelConfig;

            _capsuleSpawnPoints = levelConfig.Spawn.energy.SpawnPoints.ConvertSpawnPointDataToPointData();
            _enemySpawnPoints = levelConfig.Spawn.Enemy.SpawnPoints.ConvertSpawnPointDataToPointData();
        }

        public PointData EnergySpawnPoint() => 
            _capsuleSpawnPoints.GetUnblockedSpawnPoints().Random();

        public int GetNumberUnlockedCapsuleSpawnPoints() => 
            _capsuleSpawnPoints.GetNumberUnlockedSpawnPoints();

        public PointData EnemySpawnPoint() => 
            _enemySpawnPoints.GetUnblockedSpawnPoints().Random();

        public int GetNumberUnlockedEnemySpawnPoints() => 
            _enemySpawnPoints.GetNumberUnlockedSpawnPoints();

        public Vector3 PlayerSpawnPoint() => 
            _config.Spawn.Player.SpawnPoints.Random().Position + Vector3.up;

        public PointData GeneratorSpawnPoint() => 
            _config.Spawn.Generator.GeneratorSpawnPoints.Random();

        public void BindObjectToSpawnPoint(Object obj, PointData point) => 
            _coroutineRunner.StartCoroutine(BindObject(obj, _capsuleSpawnPoints, point));

        public void BindTimeToSpawnPoint(float time, PointData point) => 
            _coroutineRunner.StartCoroutine(BindTime(time, _enemySpawnPoints, point));

        public void Reset()
        {
            for (int i = 0; i < _capsuleSpawnPoints.Length; i++) 
                _capsuleSpawnPoints[i].IsLocked = false;
            
            for (int i = 0; i < _enemySpawnPoints.Length; i++) 
                _enemySpawnPoints[i].IsLocked = false;
        }
        
        public void CleanUp()
        {
            _config = null;
            
            _capsuleSpawnPoints = Array.Empty<SpawnPointData>();
            _enemySpawnPoints = Array.Empty<SpawnPointData>();
        }
        
        private static IEnumerator BindObject(Object obj, SpawnPointData[] spawnPointDatas, PointData point)
        {
            spawnPointDatas.BlockSpawnPoint(point);
            
            while (obj != null)
                yield return null;

            spawnPointDatas.UnlockSpawnPoint(point);
        }

        private static IEnumerator BindTime(float time, SpawnPointData[] spawnPointDatas, PointData point)
        {
            spawnPointDatas.BlockSpawnPoint(point);
            
            yield return new WaitForSeconds(time);

            spawnPointDatas.UnlockSpawnPoint(point);
        }
    }
}