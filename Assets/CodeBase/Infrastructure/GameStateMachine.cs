using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Input.Factories.UI;
using CodeBase.Services.Input.LoadScene;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IExitState _exitState;
        
        public GameStateMachine(AllServices services, ICorutineRunner corutineRunner)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, corutineRunner),
                [typeof(LoadMenuState)] = new LoadMenuState(this, services.Single<ISceneLoaderService>(), services.Single<IUIFactory>())
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        
        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _exitState?.Exit();

            TState state = GetState<TState>();
            _exitState = state;
            
            return  state;
        }

        private TState GetState<TState>() where TState : class, IExitState => 
            _states[typeof(TState)] as TState;
    }
}