using System;
using CodeBase.Logic.Menu;
using CodeBase.Mediator;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Window;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Scene.Menu
{
    public class MenuUIViewer : MonoBehaviour
    {
        [SerializeField]
        private MenuMediator _mediator;

        [SerializeField] 
        private MenuStates _menuStates;

        private IWindowService _windowService;
        private IUIFactory _factory;

        public void Construct(IWindowService windowService, IUIFactory factory)
        {
            _windowService = windowService;
            _factory = factory;
        }

        private void Start() => 
            _menuStates.OnChangeState += OnChangeState;

        private void OnDestroy() => 
            _menuStates.OnChangeState -= OnChangeState;

        private void OnChangeState(MenuState state)
        {
            switch (state)
            {
                case MenuState.Intro:
                {
                    ViewSkipButton();
                }
                    break;
                
                case MenuState.MainMenu:
                {
                    ShowMainMenuWindow();
                } 
                    break;

                case MenuState.Garage:
                {
                    ShowGarageWindow();
                }
                    break;

                case MenuState.Settings:
                {
                    ShowSettingsWindow();
                }
                    break;

                case MenuState.PlayGame:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void ShowMainMenuWindow()
        {
            if (_windowService.CheckWindow<MainMenuWindow>())
                _windowService.ShowWindow<MainMenuWindow>();
            else
                _factory.LoadMainMenuWindow(_mediator);
        }

        private void ShowSettingsWindow()
        {
            if (_windowService.CheckWindow<SettingsWindow>())
                _windowService.ShowWindow<SettingsWindow>();
            else
                _factory.LoadSettingsWindow(_mediator);
        }

        private void ShowGarageWindow()
        {
            if (_windowService.CheckWindow<SettingsWindow>())
                _windowService.ShowWindow<SettingsWindow>();
            else
                _factory.LoadGarageWindow(_mediator);
        }

        private void ViewSkipButton() => 
            _factory.LoadSkipButton(_mediator);
    }
}