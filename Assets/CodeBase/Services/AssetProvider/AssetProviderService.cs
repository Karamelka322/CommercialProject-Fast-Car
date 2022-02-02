using CodeBase.StaticData.Player;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.Services.AssetProvider
{
    public class AssetProviderService : IAssetProviderService
    {
        public LoadingCurtain LoadLoadingMenuCurtain() => 
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

        public LoadingCurtain LoadLoadingLevelCurtain() => 
            Resources.Load<LoadingCurtain>(AssetPath.CurtainsLoadingLevelPath);

        public GameObject LoadUIRoot() => 
            Resources.Load<GameObject>(AssetPath.UIRootPath);

        public GameObject LoadMainButtonInMenu() => 
            Resources.Load<GameObject>(AssetPath.MainButtonInMenuPath);

        public SkipButton LoadSkipButton() => 
            Resources.Load<SkipButton>(AssetPath.SkipButtonPath);

        public PlayerStaticData[] LoadPlayerStaticData() => 
            Resources.LoadAll<PlayerStaticData>(AssetPath.PlayerStaticDataPath);
    }
}