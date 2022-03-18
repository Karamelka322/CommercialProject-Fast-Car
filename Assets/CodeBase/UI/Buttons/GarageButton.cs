using CodeBase.Mediator;

namespace CodeBase.UI.Buttons
{
    public class GarageButton : UIButton
    {
        private IMediator _mediator;

        public void Construct(IMediator mediator) => 
            _mediator = mediator;

        protected override void OnClickButton() => 
            _mediator.PlayOpenGarage();
    }
}