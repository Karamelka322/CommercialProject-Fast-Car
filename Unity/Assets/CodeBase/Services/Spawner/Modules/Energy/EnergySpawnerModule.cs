using System;
using System.Threading.Tasks;
using CodeBase.Data.Static.Level;
using CodeBase.Extension;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Services.Spawner
{
    public class EnergySpawnerModule
    {
        private readonly IRandomService _randomService;
        private readonly ILevelFactory _levelFactory;

        private EnergySpawnConfig _config;
        private GameObject[] _energies;

        public Action<GameObject> OnSpawnEnergy;

        public EnergySpawnerModule(ILevelFactory levelFactory, IRandomService randomService)
        {
            _randomService = randomService;
            _levelFactory = levelFactory;
        }

        public void SetConfig(EnergySpawnConfig config)
        {
            _config = config;
            _energies = new GameObject[_config.Quantity];
        }

        public async Task LoadResourcesAsync() => 
            await _levelFactory.LoadResourcesEnergyAsync();

        public async Task TrySpawnEnergy()
        {
            if (IsSpawnedEnergy())
                await SpawnEnergy();
        }

        public void Clear()
        {
            _config = null;
            _energies = Array.Empty<GameObject>();
        }
        
        private async Task SpawnEnergy()
        {
            GameObject energy = await LoadEnergyAsync();
            _energies[_energies.GetEmptyIndex()] = energy;

            OnSpawnEnergy?.Invoke(energy);
        }
        
        private bool IsSpawnedEnergy() => 
            _energies.NumberEmptyIndexes() != 0 && _randomService.GetNumberUnlockedCapsuleSpawnPoints() > 0;

        private async Task<GameObject> LoadEnergyAsync()
        {
            PointData spawnPoint = _randomService.EnergySpawnPoint();
            
            GameObject energy = await _levelFactory.LoadEnergyAsync(spawnPoint + Vector3.up);
            _randomService.BindObjectToSpawnPoint(energy, spawnPoint);
            
            return energy;
        }
    }
}