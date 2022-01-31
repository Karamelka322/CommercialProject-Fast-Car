using System;
using CodeBase.Scene.Menu;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Services.Factories.UI
{
    public interface IUIFactory : IService
    {
        LoadingCurtain LoadLoadingMenuCurtain();
        LoadingCurtain LoadLoadingLevelCurtain();
        GameObject LoadMainButtonInMenu(MenuAnimator menuAnimator);
        GameObject LoadUIRoot();
        GameObject LoadSkipButton(MenuAnimator menuAnimator);
        LoadingCurtain LoadingCurtain { get; }
        GameObject LoadSettingsInMenu(Action backEvent);
        GameObject LoadGarageInMenu(Action backEvent);
    }
}