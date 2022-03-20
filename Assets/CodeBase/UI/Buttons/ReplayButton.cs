using CodeBase.Infrastructure;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class ReplayButton : UIButton
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        protected override void OnClickButton() => 
            _gameStateMachine.Enter<ReplayLevelState>();
    }
}