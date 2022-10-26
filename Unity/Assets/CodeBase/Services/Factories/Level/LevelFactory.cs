using System.Threading.Tasks;
using CodeBase.Data.Static.Level;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.Victory;
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

        public async Task LoadGeneratorAsync(PointData spawnPoint) => 
            InstantiateRegister(await LoadResourcesGeneratorAsync(), spawnPoint);

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

        public async Task<GameObject> LoadCapsuleAsync(PointData spawnPoint)
        {
            GameObject capsule = await LoadResourcesCapsuleAsync();
            InstantiateRegister(capsule, spawnPoint);
            return capsule;
        }
        
        public async Task<GameObject> LoadResourcesCapsuleAsync()
        {
            CapsuleSpawnConfig config = _diContainer
                .Resolve<IPersistentDataService>()
                .PlayerData.SessionData.LevelData.CurrentLevelConfig.Spawn.Capsule;

            GameObject prefab = await _diContainer
                .Resolve<IAssetManagementService>()
                .LoadAsync<GameObject>(config.PrefabReference);

            return prefab;
        }
        
        private void InstantiateRegister(Object prefab, PointData spawnPoint) => 
            Register(Instantiate(prefab, spawnPoint));

        private GameObject Instantiate(Object prefab, PointData spawnPoint) => 
            _diContainer.InstantiatePrefab(prefab, spawnPoint.Position, spawnPoint.Rotation, null);

        private void Register(in GameObject gameObject)
        {
            _diContainer.Resolve<IReadWriteDataService>().Register(gameObject);
            _diContainer.Resolve<IReplayService>().Register(gameObject);
            _diContainer.Resolve<IDefeatService>().Register(gameObject);
            _diContainer.Resolve<IVictoryService>().Register(gameObject);
        }
    }
}