using CodeBase.Data.Static.Enemy;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Logic.Item;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.Services.AssetProvider
{
    public class AssetProviderService : IAssetProviderService
    {
        public LoadingCurtain LoadMenuCurtain() => 
            Resources.Load<LoadingCurtain>(AssetPath.CurtainsLoadingMenuPath);

        public GameObject LoadSettingsInMenu() => 
            Resources.Load<GameObject>(AssetPath.SettingsInMenuPath);

        public GameObject LoadGarageInMenu() => 
            Resources.Load<GameObject>(AssetPath.GarageInMenuPath);

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

        public GameObject LoadPauseWindow() => 
            Resources.Load<GameObject>(AssetPath.PauseWindowPath);

        public GameObject LoadTimer() => 
            Resources.Load<GameObject>(AssetPath.TimerPath);

        public GameObject LoadDefeatWindow() => 
            Resources.Load<GameObject>(AssetPath.DefeatWindowPath);

        public EnemyStaticData[] LoadEnemies() => 
            Resources.LoadAll<EnemyStaticData>(AssetPath.EnemiesPath);

        public LoadingCurtain LoadLevelCurtain() => 
            Resources.Load<LoadingCurtain>(AssetPath.CurtainsLoadingLevelPath);

        public GameObject LoadUIRoot() => 
            Resources.Load<GameObject>(AssetPath.UIRootPath);

        public GameObject LoadMainButtonInMenu() => 
            Resources.Load<GameObject>(AssetPath.MainButtonInMenuPath);

        public SkipButton LoadSkipButton() => 
            Resources.Load<SkipButton>(AssetPath.SkipButtonPath);

        public PlayerStaticData[] LoadPlayerStaticData() => 
            Resources.LoadAll<PlayerStaticData>(AssetPath.PlayerStaticDataPath);

        public LevelStaticData[] LoadLevelStaticData() => 
            Resources.LoadAll<LevelStaticData>(AssetPath.LevelStaticDataPath);
    }
}