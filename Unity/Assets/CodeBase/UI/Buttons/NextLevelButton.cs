using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class NextLevelButton : UIButton
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        protected override void OnClickButton() => 
            _gameStateMachine.Enter<UnloadLevelState, LoadLevelState>();
    }
}