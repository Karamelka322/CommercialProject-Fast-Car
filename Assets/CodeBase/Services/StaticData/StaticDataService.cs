using CodeBase.Services.AssetProvider;
using CodeBase.StaticData.Player;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProviderService _assetProviderService;

        public StaticDataService(IAssetProviderService assetProviderService)
        {
            _assetProviderService = assetProviderService;
        }

        public PlayerStaticData ForPlayer(PlayerTypeId typeId)
        {
            PlayerStaticData[] staticDatas = _assetProviderService.LoadPlayerStaticData();

            for (int i = 0; i < staticDatas.Length; i++)
            {
                if (staticDatas[i].Type == typeId)
                    return staticDatas[i];
            }

            return default;
        }
    }
}