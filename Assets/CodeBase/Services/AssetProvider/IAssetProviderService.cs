using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.Services.AssetProvider
{
    public interface IAssetProviderService : IService
    {
        LoadingCurtain LoadLoadingMenuCurtain();
        LoadingCurtain LoadLoadingLevelCurtain();

        PlayerStaticData[] LoadPlayerStaticData();
        LevelStaticData[] LoadLevelStaticData();

        GameObject LoadUIRoot();
        GameObject LoadMainButtonInMenu();
        SkipButton LoadSkipButton();
        GameObject LoadSettingsInMenu();
        GameObject LoadGarageInMenu();

        HUD LoadHUD();
        GameObject LoadJoystickInput();
        GameObject LoadButtonsInput();
        GameObject LoadAreasInput();
    }
}