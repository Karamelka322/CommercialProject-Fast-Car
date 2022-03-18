using CodeBase.Data.Static.Enemy;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Logic.Item;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Services.AssetProvider
{
    public interface IAssetProviderService : IService
    {
        LoadingCurtain LoadMenuCurtain();
        LoadingCurtain LoadLevelCurtain();

        PlayerStaticData[] LoadPlayerStaticData();
        LevelStaticData[] LoadLevelStaticData();
        EnemyStaticData[] LoadEnemies();

        GameObject LoadUIRoot();
        GameObject LoadMainButtonInMenu();
        SkipButton LoadSkipButton();
        SettingsWindow LoadSettingsWindow();
        GarageWindow LoadGarageWindow();

        HUD LoadHUD();
        GameObject LoadJoystickInput();
        GameObject LoadButtonsInput();
        GameObject LoadAreasInput();
        GameObject LoadGenerator();
        Capsule LoadCapsule();
        GameObject LoadPauseWindow();
        GameObject LoadTimer();
        GameObject LoadDefeatWindow();
        GameObject LoadVictoryWindow();
    }
}