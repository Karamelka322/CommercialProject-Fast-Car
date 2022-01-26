using CodeBase.Infrastructure.States;
using CodeBase.Services.Input.LoadScene;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _gameStateMachine;

        public Game(ICorutineRunner corutineRunner)
        {
            AllServices services = new AllServices();
            
            _gameStateMachine = new GameStateMachine(services, corutineRunner);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}