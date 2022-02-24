using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class ReplayLevelState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public ReplayLevelState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}