using CodeBase.StaticData.Player;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.Services.AssetProvider
{
    public interface IAssetProviderService : IService
    {
        LoadingCurtain LoadLoadingMenuCurtain();
        LoadingCurtain LoadLoadingLevelCurtain();
        GameObject LoadUIRoot();
        GameObject LoadMainButtonInMenu();
        SkipButton LoadSkipButton();
        PlayerStaticData[] LoadPlayerStaticData();
        GameObject LoadSettingsInMenu();
        GameObject LoadGarageInMenu();
    }
}