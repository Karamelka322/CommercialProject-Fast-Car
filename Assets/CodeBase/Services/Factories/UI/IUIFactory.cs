using System;
using CodeBase.Mediator;
using CodeBase.Scene.Menu;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Services.Factories.UI
{
    public interface IUIFactory : IService
    {
        LoadingCurtain LoadMenuCurtain();
        LoadingCurtain LoadLevelCurtain();
        void LoadUIRoot();
        void LoadSkipButton(MenuAnimator menuAnimator);
        GameObject LoadMainButtonInMenu(IMediator mediator);
        GameObject LoadSettingsInMenu(IMediator mediator, Action backEvent);
        GameObject LoadGarageWindow(IMediator mediator, Action backEvent);
        void LoadHUD();
        void LoadPauseWindow();
        GameObject LoadTimer();
        void LoadDefeatWindow();
        void LoadVictoryWindow();
    }
}