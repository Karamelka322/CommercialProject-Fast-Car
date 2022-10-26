using System;
using System.Threading.Tasks;
using CodeBase.Data.Static.Level;
using CodeBase.Extension;
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
        private GameObject[] _capsules;

        public CapsuleSpawnerModule(ILevelFactory levelFactory, IRandomService randomService)
        {
            _randomService = randomService;
            _levelFactory = levelFactory;
        }

        public void SetConfig(CapsuleSpawnConfig config)
        {
            _config = config;
            _capsules = new GameObject[_config.Quantity];
        }

        public async Task LoadResourcesAsync() => 
            await _levelFactory.LoadResourcesCapsuleAsync();

        public async Task TrySpawnCapsule()
        {
            if (IsSpawnedCapsule())
                await SpawnCapsule();
        }

        public void Clear()
        {
            _config = null;
            _capsules = Array.Empty<GameObject>();
        }
        
        private async Task SpawnCapsule() => 
            _capsules[_capsules.GetEmptyIndex()] = await LoadCapsuleAsync();

        private bool IsSpawnedCapsule() => 
            _capsules.NumberEmptyIndexes() != 0 && _randomService.GetNumberUnlockedCapsuleSpawnPoints() > 0;

        private async Task<GameObject> LoadCapsuleAsync()
        {
            PointData spawnPoint = _randomService.CapsuleSpawnPoint();
            
            GameObject capsule = await _levelFactory.LoadCapsuleAsync(spawnPoint + Vector3.up);
            _randomService.BindObjectToSpawnPoint(capsule, spawnPoint);

            return capsule;
        }
    }
}