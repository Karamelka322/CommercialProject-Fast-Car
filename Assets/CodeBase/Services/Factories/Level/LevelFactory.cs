using CodeBase.Logic.Item;
using CodeBase.Logic.Level.Generator;
using CodeBase.Logic.Player;
using CodeBase.Logic.World;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;
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

        public GameObject LoadGenerator(PointData spawnPoint)
        {
            GameObject prefab = _diContainer.Resolve<IAssetProviderService>().LoadGenerator();
            return InstantiateRegister(prefab, spawnPoint);
        }

        public Capsule LoadCapsule(PointData spawnPoint)
        {
            Capsule prefab = _diContainer.Resolve<IAssetProviderService>().LoadCapsule();
            return InstantiateRegister(prefab, spawnPoint);
        }

        private Capsule InstantiateRegister(Capsule prefab, PointData spawnPoint)
        {
            Capsule capsule = Object.Instantiate(prefab, spawnPoint.Position, spawnPoint.Rotation);
            
            _diContainer.Resolve<IReplayService>().Register(capsule.gameObject);
            
            return capsule;
        }

        private GameObject InstantiateRegister(Object prefab, PointData spawnPoint)
        {
            GameObject gameObject = _diContainer.InstantiatePrefab(prefab, spawnPoint.Position, spawnPoint.Rotation, null);
            
            _diContainer.Resolve<IReadWriteDataService>().Register(gameObject);
            _diContainer.Resolve<IReplayService>().Register(gameObject);
            _diContainer.Resolve<IDefeatService>().Register(gameObject);
            _diContainer.Resolve<IVictoryService>().Register(gameObject);
            
            return gameObject;
        }
    }
}