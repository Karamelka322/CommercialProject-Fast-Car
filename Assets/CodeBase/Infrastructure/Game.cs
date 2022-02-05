using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _gameStateMachine;

        public Game(ICorutineRunner corutineRunner, IUpdatable updatable)
        {
            AllServices services = new AllServices();
            
            _gameStateMachine = new GameStateMachine(services, corutineRunner, updatable);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}