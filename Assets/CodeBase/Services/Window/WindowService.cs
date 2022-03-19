using System.Collections.Generic;
using System.Linq;
using CodeBase.UI.Windows;

namespace CodeBase.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly Dictionary<string, UIWindow> _windows = new Dictionary<string, UIWindow>();
        
        public void Register<T>(T window) where T : UIWindow
        {
            if (IsSwitchWindow()) 
                _windows.Last().Value.Unshow();

            _windows.Add(typeof(T).Name, window);
        }

        public void Unregister<T>(T window) where T : UIWindow
        {
            _windows.Remove(typeof(T).Name);
            
            if (IsSwitchWindow()) 
                _windows.Last().Value.Show();
        }

        public bool CheckWindow<T>() where T : UIWindow => 
            _windows.ContainsKey(typeof(T).Name);

        public void ShowWindow<T>() where T : UIWindow
        {
            switch (typeof(T).Name)
            {
                case nameof(MainMenuWindow): Show<MainMenuWindow>(); break;
                case nameof(SettingsWindow): Show<SettingsWindow>(); break;
                case nameof(GarageWindow): Show<GarageWindow>(); break;
                default: throw new WindowExeption("Not Showing Window"); 
            }

            void Show<TWindow>() where TWindow : UIWindow
            {
                if(_windows.TryGetValue(typeof(TWindow).Name, out UIWindow window))
                    window.Show();
                else
                    throw new WindowExeption($"Not Showing Window - ({typeof(TWindow).Name})");
            }
        }

        public void Clenup() => 
            _windows.Clear();

        private bool IsSwitchWindow() => 
            _windows.Count > 0;
    }
}