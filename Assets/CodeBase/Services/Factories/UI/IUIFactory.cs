using CodeBase.UI;

namespace CodeBase.Services.Input.Factories.UI
{
    public interface IUIFactory : IService
    {
        LoadingCurtain LoadLoadingMenuCurtain();
        LoadingCurtain LoadingCurtain { get; }
    }
}