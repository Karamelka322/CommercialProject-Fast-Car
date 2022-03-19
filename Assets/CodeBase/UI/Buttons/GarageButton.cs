using CodeBase.Logic.Menu;
using CodeBase.Mediator;

namespace CodeBase.UI.Buttons
{
    public class GarageButton : UIButton
    {
        private IMenuMediator _mediator;

        public void Construct(IMenuMediator mediator) => 
            _mediator = mediator;

        protected override void OnClickButton() => 
            _mediator.ChangeMenuState(MenuState.Garage);
    }
}