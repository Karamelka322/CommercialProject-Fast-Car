using CodeBase.Data.Static.Level;
using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.Services.Random
{
    public class RandomService : IRandomService
    {
        private LevelStaticData _config;

        public void SetConfig(LevelStaticData levelConfig) => 
            _config = levelConfig;

        public Vector3 CapsuleSpawnPoint() => 
            _config.Capsule.CapsuleSpawnPoints.Random() + Vector3.up;

        public Vector3 PlayerSpawnPoint() => 
            _config.PlayerSpawnPoints.Random() + Vector3.up;

        public Vector3 GeneratorSpawnPoint() => 
            _config.Generator.GeneratorSpawnPoints.Random();

        public Vector3 EnemySpawnPoint() => 
            _config.Enemy.EnemySpawnPoints.Random() + Vector3.up;
    }
}