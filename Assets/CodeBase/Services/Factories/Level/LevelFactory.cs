using CodeBase.Logic.Item;
using CodeBase.Logic.Level.Generator;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.PersistentProgress;
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

        public LevelFactory(
            IAssetProviderService assetProviderService,
            ITweenService tweenService,
            IUpdateService updateService,
            IReadWriteDataService readWriteDataService)
        {
            _assetProviderService = assetProviderService;
            _tweenService = tweenService;
            _updateService = updateService;
            _readWriteDataService = readWriteDataService;
        }

        public GameObject LoadGenerator(Vector3 at)
        {
            GameObject prefab = _assetProviderService.LoadGenerator();
            GameObject generator = InstantiateRegister(at, prefab);

            generator.GetComponentInChildren<GeneratorHook>().Construct(_tweenService);
            generator.GetComponentInChildren<GeneratorPower>().Construct(_updateService);

            return generator;
        }

        public Capsule LoadCapsule(Vector3 at)
        {
            Capsule prefab = _assetProviderService.LoadCapsule();
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        private GameObject InstantiateRegister(Vector3 at, GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            _readWriteDataService.Register(gameObject);
            return gameObject;
        }
    }
}