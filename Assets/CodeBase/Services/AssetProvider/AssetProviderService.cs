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
    public class AssetProviderService : IAssetProviderService
    {
        public LoadingCurtain LoadMenuCurtain() => 
            Resources.Load<LoadingCurtain>(AssetPath.CurtainsLoadingMenuPath);

        public HUD LoadHUD() => 
            Resources.Load<HUD>(AssetPath.HUDPath);

        public GameObject LoadJoystickInput() => 
            Resources.Load<GameObject>(AssetPath.JoystickInputPath);

        public GameObject LoadButtonsInput() => 
            Resources.Load<GameObject>(AssetPath.ButtonsInputPath);

        public GameObject LoadAreasInput() => 
            Resources.Load<GameObject>(AssetPath.AreasInputPath);

        public GameObject LoadGenerator() => 
            Resources.Load<GameObject>(AssetPath.GeneratorPath);

        public Capsule LoadCapsule() => 
            Resources.Load<Capsule>(AssetPath.CapsulePath);

        public GameObject LoadTimer() => 
            Resources.Load<GameObject>(AssetPath.TimerPath);

        

        public GarageWindow LoadGarageWindow() => 
            Resources.Load<GarageWindow>(AssetPath.GarageWindowPath);

        public SettingsWindow LoadSettingsWindow() => 
            Resources.Load<SettingsWindow>(AssetPath.SettingsWindowPath);

        public GameObject LoadPauseWindow() => 
            Resources.Load<GameObject>(AssetPath.PauseWindowPath);

        public GameObject LoadDefeatWindow() => 
            Resources.Load<GameObject>(AssetPath.DefeatWindowPath);

        public GameObject LoadVictoryWindow() => 
            Resources.Load<GameObject>(AssetPath.VictoryWindowPath);

        public MainMenuWindow LoadMainMenuWindow() => 
            Resources.Load<MainMenuWindow>(AssetPath.MainMenuWindowPath);

        
        
        public EnemyStaticData[] LoadEnemies() => 
            Resources.LoadAll<EnemyStaticData>(AssetPath.EnemiesStaticDataPath);

        public LoadingCurtain LoadLevelCurtain() => 
            Resources.Load<LoadingCurtain>(AssetPath.CurtainsLoadingLevelPath);

        public GameObject LoadUIRoot() => 
            Resources.Load<GameObject>(AssetPath.UIRootPath);

        public SkipButton LoadSkipButton() => 
            Resources.Load<SkipButton>(AssetPath.SkipButtonPath);

        public PlayerStaticData[] LoadPlayerStaticData() => 
            Resources.LoadAll<PlayerStaticData>(AssetPath.PlayerStaticDataPath);

        public LevelStaticData[] LoadLevelStaticData() => 
            Resources.LoadAll<LevelStaticData>(AssetPath.LevelStaticDataPath);
    }
}