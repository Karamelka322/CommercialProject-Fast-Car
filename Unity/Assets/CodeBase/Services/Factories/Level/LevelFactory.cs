using CodeBase.Logic.Item;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.Victory;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.Services.Factories.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly DiContainer _diContainer;

        public LevelFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async void LoadGenerator(AssetReference PrefabReference,PointData spawnPoint)
        {
            GameObject prefab = await _diContainer.Resolve<IAssetMenagementService>()
                .Load<GameObject>(PrefabReference);

            InstantiateRegister(prefab, spawnPoint);
        }

        public Capsule LoadCapsule(PointData spawnPoint)
        {
            Capsule prefab = LoadAsset<Capsule>(AssetPath.CapsulePath);
            return InstantiateRegister(prefab, spawnPoint);
        }

        private Capsule InstantiateRegister(Capsule prefab, PointData spawnPoint)
        {
            Capsule capsule = Object.Instantiate(prefab, spawnPoint.Position, spawnPoint.Rotation);
            
            _diContainer.Resolve<IReplayService>().Register(capsule.gameObject);
            
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
        
        private T LoadAsset<T>(string assetPath) where T : Object => 
            _diContainer.Resolve<IAssetMenagementService>().Load<T>(assetPath);
    }
}