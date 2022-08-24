using CodeBase.Data.Static;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Logic.Player;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Replay;
using CodeBase.Services.StaticData;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using CodeBase.UI.Windows;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Factories.UI
{
    [UsedImplicitly]
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _diContainer;

        private Transform UIRoot;

        public UIFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public LoadingCurtain LoadMenuCurtain()
        {
            LoadingCurtain prefab = LoadAsset<LoadingCurtain>(AssetPath.MenuCurtainPath);
            return _diContainer.InstantiatePrefabForComponent<LoadingCurtain>(prefab.gameObject);
        }

        public LoadingCurtain LoadLevelCurtain()
        {
            LoadingCurtain prefab = LoadAsset<LoadingCurtain>(AssetPath.LevelCurtainPath);
            return _diContainer.InstantiatePrefabForComponent<LoadingCurtain>(prefab.gameObject);
        }

        public void LoadMainMenuWindow()
        {
            MainMenuWindow prefab = LoadAsset<MainMenuWindow>(AssetPath.MainMenuWindowPath);
            _diContainer.InstantiatePrefab(prefab.gameObject, UIRoot);
        }

        public void LoadSettingsWindow()
        {
            SettingsWindow prefab = LoadAsset<SettingsWindow>(AssetPath.SettingsWindowPath);
            _diContainer.InstantiatePrefab(prefab, UIRoot);
        }

        public void LoadGarageWindow()
        {
            GarageWindow prefab = LoadAsset<GarageWindow>(AssetPath.GarageWindowPath);
            _diContainer.InstantiatePrefab(prefab, UIRoot);
        }

        public void LoadHUD()
        {
            HUD prefab = LoadAsset<HUD>(AssetPath.HUDPath);
            HUD hub = InstantiateRegisterWindow(prefab);

            LoadInputVariant(GetInputType(), hub.InputContainer);
            
            _diContainer.Resolve<ILevelMediator>().Construct(hub);
        }

        public void LoadPauseWindow()
        {
            GameObject prefab = LoadAsset<GameObject>(AssetPath.PauseWindowPath);
            InstantiateRegisterWindow(prefab, UIRoot);
        }

        public GameObject LoadTimer()
        {
            GameObject prefab = LoadAsset<GameObject>(AssetPath.TimerPath);
            return Object.Instantiate(prefab, UIRoot);
        }

        public void LoadDefeatWindow()
        {
            GameObject prefab = LoadAsset<GameObject>(AssetPath.DefeatWindowPath);
            InstantiateRegisterWindow(prefab, UIRoot);
        }

        public void LoadVictoryWindow()
        {
            GameObject prefab = LoadAsset<GameObject>(AssetPath.VictoryWindowPath);
            InstantiateRegisterWindow(prefab, UIRoot);
        }

        public void LoadSkipButton()
        {
            SkipButton prefab = LoadAsset<SkipButton>(AssetPath.SkipButtonPath);
            _diContainer.InstantiatePrefab(prefab, UIRoot);
        }

        public void LoadUIRoot()
        {
            GameObject prefab = LoadAsset<GameObject>(AssetPath.UIRootPath);
            UIRoot = Object.Instantiate(prefab).transform;
        }

        private void LoadInputVariant(InputTypeId inputType, Transform parent)
        {
            GameObject prefab = _diContainer.Resolve<IStaticDataService>().ForInput(inputType);
            
            IInputVariant inputVariant = _diContainer.InstantiatePrefab(prefab, parent).GetComponent<IInputVariant>();
            
            _diContainer.Resolve<IInputService>().RegisterInput(inputVariant);
        }

        private InputTypeId GetInputType() => 
            _diContainer.Resolve<IPersistentDataService>().PlayerData.SettingsData.InputSettingsData.InputType;

        private T InstantiateRegisterWindow<T>(T prefab) where T : MonoBehaviour
        {
            T monoBehaviour = _diContainer.InstantiatePrefabForComponent<T>(prefab);

            _diContainer.Resolve<IReadWriteDataService>().Register(monoBehaviour.gameObject);
            _diContainer.Resolve<IReplayService>().Register(monoBehaviour.gameObject);
            
            return monoBehaviour;
        }

        private void InstantiateRegisterWindow(Object prefab, Transform parent)
        {
            GameObject obj = _diContainer.InstantiatePrefab(prefab, parent);
            _diContainer.Resolve<IReplayService>().Register(obj);
        }

        private T LoadAsset<T>(string assetPath) where T : Object =>
            _diContainer.Resolve<IAssetManagementService>().Load<T>(assetPath);
    }
}