using CodeBase.Infrastructure;

namespace CodeBase.UI.Buttons
{
    public class ReplayButton : UIButton
    {
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        protected override void OnClickButton()
        {
            //_gameStateMachine.Enter<ReplayLevelState>();
        }
    }
}