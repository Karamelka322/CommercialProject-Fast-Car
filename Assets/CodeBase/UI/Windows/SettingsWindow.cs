using CodeBase.Logic.Menu;
using CodeBase.Mediator;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] 
        private ButtonWrapper _closeButton;

        private IMediator _mediator;

        public void Construct(IMediator mediator) => 
            _mediator = mediator;

        private void Start() => 
            _closeButton.OnClick += CloseWindow;

        private void OnDestroy() => 
            _closeButton.OnClick -= CloseWindow;

        private void CloseWindow()
        {
            _mediator.ChangeMenuState(MenuState.MainMenu);
            Destroy(gameObject);
        }
    }
}