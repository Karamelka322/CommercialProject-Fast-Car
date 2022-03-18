using CodeBase.Services.Factories.UI;
using UnityEngine;

namespace CodeBase.Scene.Menu
{
    public class MenuUIViewer : MonoBehaviour
    {
        [SerializeField]
        private Mediator.Mediator _mediator;

        [SerializeField]
        private MenuAnimator _menuAnimator;

        private IUIFactory _factory;

        private GameObject _currentUI;

        public void Construct(IUIFactory factory) => 
            _factory = factory;

        private void Awake()
        {
            _menuAnimator.StartPlayOpenMenu += ViewSkipButton;
            _menuAnimator.StartPlayIdleMenu += ViewUIInMenu;
            _menuAnimator.StartPlayCloseMenu += DestroyCurrentUI;

            _menuAnimator.StartPlayOpenSettings += ViewUIInSettings;
            _menuAnimator.StartPlayCloseSettings += DestroyCurrentUI;
            
            _menuAnimator.StartPlayOpenGarage += ViewUIInGarage;
            _menuAnimator.StartPlayCloseGarage += DestroyCurrentUI;
        }

        private void OnDestroy()
        {
            _menuAnimator.StartPlayOpenMenu -= ViewSkipButton;
            _menuAnimator.StartPlayIdleMenu -= ViewUIInMenu;
            _menuAnimator.StartPlayCloseMenu -= DestroyCurrentUI;
            
            _menuAnimator.StartPlayOpenSettings -= ViewUIInSettings;
            _menuAnimator.StartPlayCloseSettings -= DestroyCurrentUI;
            
            _menuAnimator.StartPlayOpenGarage -= ViewUIInGarage;
            _menuAnimator.StartPlayCloseGarage -= DestroyCurrentUI;
        }

        private void ViewUIInMenu()
        {
            if(_currentUI == null)
                _currentUI = _factory.LoadMainButtonInMenu(_mediator);
        }

        private void ViewUIInSettings()
        {
            DestroyCurrentUI();
            _currentUI = _factory.LoadSettingsInMenu(_mediator, _menuAnimator.PlayCloseSettings);
        }

        private void ViewUIInGarage()
        {
            DestroyCurrentUI();
            _currentUI = _factory.LoadGarageWindow(_mediator, _menuAnimator.PlayCloseGarage);
        }

        private void ViewSkipButton() => 
            _factory.LoadSkipButton(_menuAnimator);

        private void DestroyCurrentUI() => 
            Destroy(_currentUI);
    }
}