using CodeBase.UI.Windows;

namespace CodeBase.Services.Window
{
    public interface IWindowService : IService
    {
        void Register<T>(T window) where T : UIWindow;
        void Unregister<T>(T window) where T : UIWindow;
        bool CheckWindow<T>() where T : UIWindow;
        void ShowWindow<T>() where T : UIWindow;
        void CleanUp();
    }
}