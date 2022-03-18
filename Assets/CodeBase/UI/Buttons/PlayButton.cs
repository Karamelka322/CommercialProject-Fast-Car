using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Menu;
using CodeBase.Mediator;

namespace CodeBase.UI.Buttons
{
    public class PlayButton : UIButton
    {
        private IGameStateMachine _stateMachine;
        private IMediator _mediator;

        public void Construct(IGameStateMachine stateMachine, IMediator mediator)
        {
            _stateMachine = stateMachine;
            _mediator = mediator;
        }

        protected override void OnClickButton()
        {
            _stateMachine.Enter<LoadLevelState>();
            _mediator.ChangeMenuState(MenuState.PlayGame);
        }
    }
}