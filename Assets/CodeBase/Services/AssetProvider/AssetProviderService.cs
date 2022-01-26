using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Services.Input.AssetProvider
{
    public class AssetProviderService : IAssetProviderService
    {
        public LoadingCurtain LoadLoadingMenuCurtain() => 
            Resources.Load<LoadingCurtain>(AssetPath.CurtainsLoadingMenuPath);
    }
}