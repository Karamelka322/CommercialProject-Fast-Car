using CodeBase.Logic.Item;
using CodeBase.Logic.Level.Generator;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Services.Factories.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly ITweenService _tweenService;
        private readonly IUpdateService _updateService;
        private readonly IReadWriteDataService _readWriteDataService;
        private readonly IRandomService _randomService;
        private readonly IReplayService _replayService;

        public LevelFactory(
            IAssetProviderService assetProviderService,
            ITweenService tweenService,
            IUpdateService updateService,
            IReadWriteDataService readWriteDataService,
            IRandomService randomService,
            IReplayService replayService)
        {
            _assetProviderService = assetProviderService;
            _tweenService = tweenService;
            _updateService = updateService;
            _readWriteDataService = readWriteDataService;
            _randomService = randomService;
            _replayService = replayService;
        }

        public GameObject LoadGenerator(PointData spawnPoint)
        {
            GameObject prefab = _assetProviderService.LoadGenerator();
            GameObject generator = InstantiateRegister(prefab, spawnPoint);

            if (generator.TryGetComponent(out GeneratorPrefab generatorPrefab)) 
                generatorPrefab.Construct(_randomService);

            if (generator.TryGetComponent(out GeneratorHook hook)) 
                hook.Construct(_tweenService);
            
            if (generator.TryGetComponent(out GeneratorPower power)) 
                power.Construct(_updateService);

            return generator;
        }

        public Capsule LoadCapsule(PointData spawnPoint)
        {
            Capsule prefab = _assetProviderService.LoadCapsule();
            return InstantiateRegister(prefab, spawnPoint);
        }

        private Capsule InstantiateRegister(Capsule prefab, PointData spawnPoint)
        {
            Capsule capsule = Object.Instantiate(prefab, spawnPoint.Position, spawnPoint.Rotation);
            _replayService.Register(capsule.gameObject);
            return capsule;
        }

        private GameObject InstantiateRegister(GameObject prefab, PointData spawnPoint)
        {
            GameObject gameObject = Object.Instantiate(prefab, spawnPoint.Position, spawnPoint.Rotation);
            _readWriteDataService.Register(gameObject);
            _replayService.Register(gameObject);
            return gameObject;
        }
    }
}