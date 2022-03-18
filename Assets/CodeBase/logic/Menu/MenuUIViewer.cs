using System;
using CodeBase.Logic.Menu;
using CodeBase.Mediator;
using CodeBase.Services.Factories.UI;
using UnityEngine;

namespace CodeBase.Scene.Menu
{
    public class MenuUIViewer : MonoBehaviour
    {
        [SerializeField]
        private MenuMediator _mediator;

        [SerializeField] 
        private MenuStates _menuStates;

        private IUIFactory _factory;

        public void Construct(IUIFactory factory) => 
            _factory = factory;

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
                    ViewUIInMenu();
                } 
                    break;

                case MenuState.Garage:
                {
                    ViewUIInGarage();
                }
                    break;

                case MenuState.Settings:
                {
                    ViewUIInSettings();
                }
                    break;

                case MenuState.PlayGame:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void ViewUIInMenu() => 
            _factory.LoadMainButtonInMenu(_mediator);

        private void ViewUIInSettings() => 
            _factory.LoadSettingsInMenu(_mediator);

        private void ViewUIInGarage() => 
            _factory.LoadGarageWindow(_mediator);

        private void ViewSkipButton() => 
            _factory.LoadSkipButton(_mediator);
    }
}