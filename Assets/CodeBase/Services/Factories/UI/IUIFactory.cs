using CodeBase.Mediator;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Services.Factories.UI
{
    public interface IUIFactory : IService
    {
        LoadingCurtain LoadMenuCurtain();
        LoadingCurtain LoadLevelCurtain();
        void LoadUIRoot();
        void LoadSkipButton(IMenuMediator mediator);
        void LoadMainMenuWindow(IMenuMediator mediator);
        void LoadSettingsWindow(IMenuMediator mediator);
        void LoadGarageWindow(IMenuMediator mediator);
        void LoadHUD();
        void LoadPauseWindow();
        GameObject LoadTimer();
        void LoadDefeatWindow();
        void LoadVictoryWindow();
    }
}