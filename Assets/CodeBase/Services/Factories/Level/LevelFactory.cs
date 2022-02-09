using CodeBase.Logic.Level.Generator;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Tween;
using UnityEngine;

namespace CodeBase.Services.Factories.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IAssetProviderService _assetProviderService;
        private readonly ITweenService _tweenService;

        public LevelFactory(IAssetProviderService assetProviderService, ITweenService tweenService, IPersistentDataService persistentDataService)
        {
            _assetProviderService = assetProviderService;
            _tweenService = tweenService;
            _persistentDataService = persistentDataService;
        }

        public void LoadGenerator(Vector3 at)
        {
            GameObject prefab = _assetProviderService.LoadGenerator();
            GameObject generator = Object.Instantiate(prefab, at, Quaternion.identity);

            generator.GetComponentInChildren<GeneratorHook>().Construct(_tweenService);
            generator.GetComponentInChildren<GeneratorPower>().Construct(_persistentDataService.PlayerData.SessionData.GeneratorData);
        }
    }
}