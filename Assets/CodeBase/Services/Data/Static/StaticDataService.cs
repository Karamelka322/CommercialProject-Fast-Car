using CodeBase.Data;
using CodeBase.Data.Static;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Input;
using CodeBase.StaticData;
using UnityEngine;

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

        public GameObject ForInput(InputTypeId typeId)
        {
            return typeId switch
            {
                InputTypeId.Joystick => _assetProviderService.LoadJoystickInput(),
                InputTypeId.Buttons => _assetProviderService.LoadButtonsInput(),
                InputTypeId.Areas => _assetProviderService.LoadAreasInput(),
                _ => default
            };
        }

        public LevelStaticData ForLevel(LevelTypeId typeId)
        {
            LevelStaticData[] staticDatas = _assetProviderService.LoadLevelStaticData();

            for (int i = 0; i < staticDatas.Length; i++)
            {
                if (staticDatas[i].Type == typeId)
                    return staticDatas[i];
            }

            return default;
        }
    }
}