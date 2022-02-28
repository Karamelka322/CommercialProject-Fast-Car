using System.Collections.Generic;
using CodeBase.Data.Static.Level;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Services.Random
{
    public class RandomService : IRandomService
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStaticDataService _staticDataService;
        
        private LevelStaticData _levelStaticData;

        public RandomService(IPersistentDataService persistentDataService, IStaticDataService staticDataService)
        {
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
        }

        public Vector3 CapsuleSpawnPoint()
        {
            List<Vector3> capsuleSpawnPoints = TryLoadCapsuleSpawnPoints();
            return capsuleSpawnPoints[UnityEngine.Random.Range(0, capsuleSpawnPoints.Count)]  + Vector3.up;
        }

        public Vector3 PlayerSpawnPoint()
        {
            List<Vector3> playerSpawnPoints = TryLoadPlayerSpawnPoints();
            return playerSpawnPoints[UnityEngine.Random.Range(0, playerSpawnPoints.Count)] + Vector3.up;
        }

        public Vector3 GeneratorSpawnPoint()
        {
            List<Vector3> generatorSpawnPoints = TryLoadGeneratorSpawnPoints();
            return generatorSpawnPoints[UnityEngine.Random.Range(0, generatorSpawnPoints.Count)];
        }

        public Vector3 EnemySpawnPoint()
        {
            List<Vector3> enemySpawnPoint = TryLoadEnemuSpawnPoints();
            return enemySpawnPoint[UnityEngine.Random.Range(0, enemySpawnPoint.Count)] + Vector3.up;
        }
        
        private List<Vector3> TryLoadEnemuSpawnPoints() => 
            IsCurrentLevelStaticData() ? (_levelStaticData = LoadLevelStaticData()).Enemy.EnemySpawnPoints : _levelStaticData.Enemy.EnemySpawnPoints;

        private List<Vector3> TryLoadGeneratorSpawnPoints() => 
            IsCurrentLevelStaticData() ? (_levelStaticData = LoadLevelStaticData()).Generator.GeneratorSpawnPoints : _levelStaticData.Generator.GeneratorSpawnPoints;

        private List<Vector3> TryLoadPlayerSpawnPoints() => 
            IsCurrentLevelStaticData() ? (_levelStaticData = LoadLevelStaticData()).PlayerSpawnPoints : _levelStaticData.PlayerSpawnPoints;

        private List<Vector3> TryLoadCapsuleSpawnPoints() => 
            IsCurrentLevelStaticData() ? (_levelStaticData = LoadLevelStaticData()).Capsule.CapsuleSpawnPoints : _levelStaticData.Capsule.CapsuleSpawnPoints;

        private bool IsCurrentLevelStaticData() => 
            _levelStaticData == null || _levelStaticData.LevelType == _persistentDataService.PlayerData.ProgressData.LevelType;

        private LevelStaticData LoadLevelStaticData() => 
            _staticDataService.ForLevel(_persistentDataService.PlayerData.ProgressData.LevelType);
    }
}