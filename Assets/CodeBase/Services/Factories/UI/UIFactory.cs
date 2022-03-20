using CodeBase.Data.Static;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Replay;
using CodeBase.Services.StaticData;
using CodeBase.UI;
using CodeBase.UI.Buttons;
using CodeBase.UI.Windows;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Factories.UI
{
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
            LoadingCurtain prefab = _diContainer.Resolve<IAssetProviderService>().LoadMenuCurtain();
            return _diContainer.InstantiatePrefabForComponent<LoadingCurtain>(prefab.gameObject);
        }

        public LoadingCurtain LoadLevelCurtain()
        {
            LoadingCurtain prefab = _diContainer.Resolve<IAssetProviderService>().LoadLevelCurtain();
            return _diContainer.InstantiatePrefabForComponent<LoadingCurtain>(prefab.gameObject);
        }

        public void LoadMainMenuWindow()
        {
            MainMenuWindow prefab = _diContainer.Resolve<IAssetProviderService>().LoadMainMenuWindow(); 
            _diContainer.InstantiatePrefab(prefab.gameObject, UIRoot);
        }

        public void LoadSettingsWindow()
        {
            SettingsWindow prefab = _diContainer.Resolve<IAssetProviderService>().LoadSettingsWindow();
            _diContainer.InstantiatePrefab(prefab, UIRoot);
        }

        public void LoadGarageWindow()
        {
            GarageWindow prefab = _diContainer.Resolve<IAssetProviderService>().LoadGarageWindow();
            _diContainer.InstantiatePrefab(prefab, UIRoot);
        }

        public void LoadHUD()
        {
            HUD hudPrefab = _diContainer.Resolve<IAssetProviderService>().LoadHUD();
            HUD hud = InstantiateRegisterWindow(hudPrefab);

            InputTypeId inputType = _diContainer.Resolve<IPersistentDataService>().PlayerData.SettingsData.InputType;
            GameObject prefab = _diContainer.Resolve<IStaticDataService>().ForInput(inputType);
            GameObject inputVariant = Object.Instantiate(prefab, hud.InputContainer);
            
            _diContainer.Resolve<IInputService>().RegisterInput(inputType, inputVariant);
        }

        public void LoadPauseWindow()
        {
            GameObject prefab = _diContainer.Resolve<IAssetProviderService>().LoadPauseWindow();
            InstantiateRegisterWindow(prefab, UIRoot);
        }

        public GameObject LoadTimer()
        {
            GameObject prefab = _diContainer.Resolve<IAssetProviderService>().LoadTimer();
            return Object.Instantiate(prefab, UIRoot);
        }

        public void LoadDefeatWindow()
        {
            GameObject prefab = _diContainer.Resolve<IAssetProviderService>().LoadDefeatWindow();
            InstantiateRegisterWindow(prefab, UIRoot);
        }

        public void LoadVictoryWindow()
        {
            GameObject prefab = _diContainer.Resolve<IAssetProviderService>().LoadVictoryWindow();
            InstantiateRegisterWindow(prefab, UIRoot);
        }

        public void LoadSkipButton()
        {
            SkipButton prefab = _diContainer.Resolve<IAssetProviderService>().LoadSkipButton();
            _diContainer.InstantiatePrefab(prefab, UIRoot);
        }

        public void LoadUIRoot()
        {
            GameObject prefab = _diContainer.Resolve<IAssetProviderService>().LoadUIRoot();
            UIRoot = Object.Instantiate(prefab).transform;
        }

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
    }
}