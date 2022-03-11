using System;
using System.Collections;
using CodeBase.Data.Static.Level;
using CodeBase.Extension;
using CodeBase.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Random
{
    public class RandomService : IRandomService
    {
        private readonly ICorutineRunner _corutineRunner;
        
        private LevelStaticData _config;
        
        private SpawnPointData[] _capsuleSpawnPoints;
        private SpawnPointData[] _enemySpawnPoints;

        public RandomService(ICorutineRunner corutineRunner)
        {
            _corutineRunner = corutineRunner;
        }

        public void SetConfig(LevelStaticData levelConfig)
        {
            _config = levelConfig;

            _capsuleSpawnPoints = levelConfig.Spawn.Capsule.CapsuleSpawnPoints.ConvertSpawnPointDataToPointData();
            _enemySpawnPoints = levelConfig.Spawn.Enemy.EnemySpawnPoints.ConvertSpawnPointDataToPointData();
        }

        public PointData CapsuleSpawnPoint() => 
            _capsuleSpawnPoints.GetUnblockedSpawnPoints().Random();

        public int GetNumberUnlockedCapsuleSpawnPoints() => 
            _capsuleSpawnPoints.GetNumberUnlockedSpawnPoints();

        public PointData EnemySpawnPoint() => 
            _enemySpawnPoints.GetUnblockedSpawnPoints().Random();

        public int GetNumberInlockedEnemySpawnPoints() => 
            _enemySpawnPoints.GetNumberUnlockedSpawnPoints();

        public Vector3 PlayerSpawnPoint() => 
            _config.Level.PlayerSpawnPoints.Random().Position + Vector3.up;

        public PointData GeneratorSpawnPoint() => 
            _config.Spawn.Generator.GeneratorSpawnPoints.Random();

        public void BindObjectToSpawnPoint(Object obj, PointData point) => 
            _corutineRunner.StartCoroutine(BindObject(obj, _capsuleSpawnPoints, point));

        public void BindTimeToSpawnPoint(float time, PointData point) => 
            _corutineRunner.StartCoroutine(BindTime(time, _enemySpawnPoints, point));

        public void Clenup()
        {
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