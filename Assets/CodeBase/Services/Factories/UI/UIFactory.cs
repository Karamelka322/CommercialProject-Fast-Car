using CodeBase.Services.Input.AssetProvider;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Services.Input.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProviderService _assetProvider;

        public LoadingCurtain LoadingCurtain { get; private set; }
        
        public UIFactory(IAssetProviderService assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public LoadingCurtain LoadLoadingMenuCurtain() => 
            LoadingCurtain = Object.Instantiate(_assetProvider.LoadLoadingMenuCurtain());
    }
}