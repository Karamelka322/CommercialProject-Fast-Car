using System;
using CodeBase.Data.Static.Level;
using CodeBase.Extension;
using CodeBase.Logic.Item;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Services.Spawner
{
    public class CapsuleSpawnerModule : ICapsuleSpawnerModule
    {
        private readonly IRandomService _randomService;
        private readonly ILevelFactory _levelFactory;

        private CapsuleSpawnConfig _config;
        private Capsule[] _capsules;

        public CapsuleSpawnerModule(ILevelFactory levelFactory, IRandomService randomService)
        {
            _randomService = randomService;
            _levelFactory = levelFactory;
        }

        public void SetConfig(CapsuleSpawnConfig config)
        {
            _config = config;
            _capsules = new Capsule[_config.Quantity];
        }

        public void TrySpawnCapsule()
        {
            if (IsSpawnedCapsule())
                SpawnCapsule();
        }

        public void Clear()
        {
            _config = null;
            _capsules = Array.Empty<Capsule>();
        }
        
        private void SpawnCapsule() => 
            _capsules[_capsules.GetEmptyIndex()] = LoadCapsule();

        private bool IsSpawnedCapsule() => 
            _config != null && _config.UsingCapsule && _capsules.NumberEmptyIndexes() != 0 && _randomService.GetNumberUnlockedCapsuleSpawnPoints() > 0;

        private Capsule LoadCapsule()
        {
            PointData spawnPoint = _randomService.CapsuleSpawnPoint();
            
            Capsule capsule = _levelFactory.LoadCapsule(spawnPoint + Vector3.up);
            _randomService.BindObjectToSpawnPoint(capsule.gameObject, spawnPoint);
            
            return capsule;
        }
    }
}