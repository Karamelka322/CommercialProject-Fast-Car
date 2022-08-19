using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _gameStateMachine;

        public Game(in DiContainer diContainer, ICoroutineRunner coroutineRunner, IUpdatable updatable)
        {
            _gameStateMachine = new GameStateMachine(diContainer, coroutineRunner, updatable);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}