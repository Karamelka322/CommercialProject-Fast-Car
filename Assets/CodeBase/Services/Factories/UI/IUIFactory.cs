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
        void LoadSkipButton(IMediator mediator);
        void LoadMainButtonInMenu(IMediator mediator);
        void LoadSettingsInMenu(IMediator mediator);
        void LoadGarageWindow(IMediator mediator);
        void LoadHUD();
        void LoadPauseWindow();
        GameObject LoadTimer();
        void LoadDefeatWindow();
        void LoadVictoryWindow();
    }
}