using CodeBase.Services.Factories.UI;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.Scene.Menu
{
    public class MenuUIViewer : MonoBehaviour
    {
        [SerializeField]
        private MenuAnimator _menuAnimator;

        private IUIFactory _factory;

        private SkipButton _skipButton;
        private GameObject _currentUI;

        public void Construct(IUIFactory factory)
        {
            _factory = factory;
        }

        private void Awake()
        {
            _menuAnimator.StartPlayIntro += ViewSkipButton;
            _menuAnimator.IdleInMenu += ViewUIInMenu;
        }

        private void OnDestroy()
        {
            _menuAnimator.StartPlayIntro -= ViewSkipButton;
            _menuAnimator.IdleInMenu -= ViewUIInMenu;
        }

        private void ViewUIInMenu()
        {
            if(_skipButton != null)
                DestroySkipButton();
            
            if(_currentUI == null)
                _currentUI = _factory.LoadMainButtonInMenu();
        }

        private void ViewSkipButton()
        {
            _skipButton = _factory.LoadSkipButton();
            _skipButton.Click += SkipIntro;
        }

        private void SkipIntro()
        {
            _skipButton.Click -= SkipIntro;
            _menuAnimator.SkipIntro();

            DestroySkipButton();
        }

        private void DestroySkipButton() => 
            Destroy(_skipButton.gameObject);
    }
}