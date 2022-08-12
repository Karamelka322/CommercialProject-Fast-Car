using System.Threading.Tasks;
using CodeBase.Data.Static.Level;
using CodeBase.Logic.Item;
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

        public async Task LoadGenerator(PointData spawnPoint)
        {
            GeneratorSpawnConfig config = _diContainer
                .Resolve<IPersistentDataService>()
                .PlayerData.SessionData.LevelData.CurrentLevelConfig.Spawn.Generator;
            
            GameObject prefab = await _diContainer
                .Resolve<IAssetManagementService>()
                .LoadAsync<GameObject>(config.PrefabReference);

            InstantiateRegister(prefab, spawnPoint);
        }

        public async Task<GameObject> LoadCapsule(PointData spawnPoint)
        {
            CapsuleSpawnConfig config = _diContainer
                .Resolve<IPersistentDataService>()
                .PlayerData.SessionData.LevelData.CurrentLevelConfig.Spawn.Capsule;

            GameObject capsule = await _diContainer
                .Resolve<IAssetManagementService>()
                .LoadAsync<GameObject>(config.PrefabReference);
            
            InstantiateRegister(capsule, spawnPoint);

            return capsule;
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