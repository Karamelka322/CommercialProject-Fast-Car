using CodeBase.UI;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.Services.Factories.UI
{
    public interface IUIFactory : IService
    {
        LoadingCurtain LoadLoadingMenuCurtain();
        LoadingCurtain LoadLoadingLevelCurtain();
        GameObject LoadMainButtonInMenu();
        GameObject LoadUIRoot();
        SkipButton LoadSkipButton();
        LoadingCurtain LoadingCurtain { get; }
    }
}