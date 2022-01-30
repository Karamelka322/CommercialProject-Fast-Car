using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;

namespace CodeBase.UI.Buttons
{
    public class PlayButton : UIButton
    {
        private IGameStateMachine _stateMachine;

        public void Construct(IGameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        protected override void OnClickButton() => 
            _stateMachine.Enter<LoadLevelState>();
    }
}