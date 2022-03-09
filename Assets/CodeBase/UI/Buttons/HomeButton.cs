using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;

namespace CodeBase.UI.Buttons
{
    public class HomeButton : UIButton
    {
        private IGameStateMachine _gameStateMachine;
        
        public void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        protected override void OnClickButton() => 
            _gameStateMachine.Enter<UnloadLevelState, LoadMenuState>();
    }
}