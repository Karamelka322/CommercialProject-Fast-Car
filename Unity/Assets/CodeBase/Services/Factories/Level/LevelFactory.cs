using System.Threading.Tasks;
using CodeBase.Data.Static.Level;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Logic.Level.Generator;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories.Level
{
    [UsedImplicitly]
    public class LevelFactory : ILevelFactory
    {
        private readonly DiContainer _diContainer;

        public LevelFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async Task LoadGeneratorAsync(PointData spawnPoint)
        {
            GameObject prefab = await LoadResourcesGeneratorAsync();

            GameObject generator = Instantiate(prefab, spawnPoint);
            Register(generator);

            _diContainer.Resolve<ILevelMediator>().Construct(generator.GetComponent<GeneratorPrefab>());
        }

        public async Task<GameObject> LoadResourcesGeneratorAsync()
        {
            GeneratorSpawnConfig config = _diContainer
                .Resolve<IPersistentDataService>()
                .PlayerData.SessionData.LevelData.CurrentLevelConfig.Spawn.Generator;
            
            GameObject prefab = await _diContainer
                .Resolve<IAssetManagementService>()
                .LoadAsync<GameObject>(config.PrefabReference);

            return prefab;
        }

        public async Task<GameObject> LoadEnergyAsync(PointData spawnPoint)
        {
            GameObject prefab = await LoadResourcesEnergyAsync();
            return Instantiate(prefab, spawnPoint);
        }
        
        public async Task<GameObject> LoadResourcesEnergyAsync()
        {
            EnergySpawnConfig config = _diContainer
                .Resolve<IPersistentDataService>()
                .PlayerData.SessionData.LevelData.CurrentLevelConfig.Spawn.energy;

            GameObject prefab = await _diContainer
                .Resolve<IAssetManagementService>()
                .LoadAsync<GameObject>(config.PrefabReference);

            return prefab;
        }
        
        private GameObject Instantiate(Object prefab, PointData spawnPoint) => 
            _diContainer.InstantiatePrefab(prefab, spawnPoint.Position, spawnPoint.Rotation, null);

        private void Register(in GameObject gameObject)
        {
            _diContainer.Resolve<IReadWriteDataService>().Register(gameObject);
            _diContainer.Resolve<IReplayService>().Register(gameObject);
            _diContainer.Resolve<IDefeatService>().Register(gameObject);
        }
    }
}