using CodeBase.Logic.Menu;
using CodeBase.Mediator;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class SettingsButton : UIButton
    {
        private IMenuMediator _mediator;

        [Inject]
        public void Construct(IMenuMediator mediator)
        {
            _mediator = mediator;
        }

        protected override void OnClickButton() => 
            _mediator.ChangeMenuState(MenuState.Settings);
    }
}