using System.Threading.Tasks;
using CodeBase.UI;

using UnityEngine;

namespace CodeBase.Services.Factories.UI
{
    public interface IUIFactory : IService
    {
        LoadingCurtain LoadMenuCurtain();
        LoadingCurtain LoadLevelCurtain();
        void LoadUIRoot();
        void LoadSkipButton();
        void LoadMainMenuWindow();
        void LoadSettingsWindow();
        void LoadGarageWindow();
        void LoadHUD();
        void LoadPauseWindow();
        GameObject LoadTimer();
        void LoadDefeatWindow();
        void LoadVictoryWindow();
        Task LoadAllResourcesForLevel();
    }
}