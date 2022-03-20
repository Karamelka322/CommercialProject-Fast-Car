using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Menu;
using CodeBase.Mediator;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class PlayButton : UIButton
    {
        private IGameStateMachine _stateMachine;
        private IMenuMediator _mediator;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IMenuMediator mediator)
        {
            _stateMachine = stateMachine;
            _mediator = mediator;
        }

        protected override void OnClickButton()
        {
            _stateMachine.Enter<UnloadMenuState, LoadLevelState>();
            _mediator.ChangeMenuState(MenuState.PlayGame);
        }
    }
}