using CodeBase.Logic.Menu;
using CodeBase.Mediator;

namespace CodeBase.UI.Buttons
{
    public class SkipButton : UIButton
    {
        private const float LifeTime = 15;
        
        private IMenuMediator _mediator;

        protected override void Awake()
        {
            base.Awake();
            Invoke(nameof(OnClickButton), LifeTime);
        }

        public void Construct(IMenuMediator mediator) => 
            _mediator = mediator;

        protected override void OnClickButton()
        {
            _mediator.ChangeMenuState(MenuState.MainMenu);
            Destroy(gameObject);
        }
    }
}