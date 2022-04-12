using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _gameStateMachine;

        public Game(in DiContainer diContainer, ICorutineRunner corutineRunner, IUpdatable updatable)
        {
            _gameStateMachine = new GameStateMachine(diContainer, corutineRunner, updatable);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}