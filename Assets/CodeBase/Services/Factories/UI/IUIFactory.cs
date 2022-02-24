using System;
using CodeBase.Scene.Menu;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Services.Factories.UI
{
    public interface IUIFactory : IService
    {
        LoadingCurtain LoadMenuCurtain();
        LoadingCurtain LoadLevelCurtain();
        GameObject LoadMainButtonInMenu(MenuAnimator menuAnimator);
        void LoadUIRoot();
        void LoadSkipButton(MenuAnimator menuAnimator);
        LoadingCurtain LoadingCurtain { get; }
        HUD HUD { get; }
        GameObject LoadSettingsInMenu(Action backEvent);
        GameObject LoadGarageInMenu(Action backEvent);
        void LoadHUD(GameObject generator, GameObject player);
        void LoadPauseWindow();
    }
}