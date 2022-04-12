using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        private IExitState _exitState;
        
        public GameStateMachine(DiContainer diContainer, ICorutineRunner corutineRunner, IUpdatable updatable)
        {
            _states = new Dictionary<Type, IExitState>
            {
                [typeof(BootstrapState)] = new BootstrapState(diContainer, this, corutineRunner, updatable),
                
                [typeof(LoadPersistentDataState)] = diContainer.Instantiate<LoadPersistentDataState>(),
                
                [typeof(LoadMenuState)] = diContainer.Instantiate<LoadMenuState>(),
                [typeof(UnloadMenuState)] = diContainer.Instantiate<UnloadMenuState>(),
                
                [typeof(LoadLevelState)] = diContainer.Instantiate<LoadLevelState>(),
                [typeof(LoopLevelState)] = diContainer.Instantiate<LoopLevelState>(),
                [typeof(ReplayLevelState)] = diContainer.Instantiate<ReplayLevelState>(),
                [typeof(UnloadLevelState)] = diContainer.Instantiate<UnloadLevelState>(),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TUnloadState, TNextState>() where TUnloadState : class, IUnloadState where TNextState : class, IState
        {
            IUnloadState state = ChangeState<TUnloadState>();
            state.Unload<TNextState>();
        }
        
        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _exitState?.Exit();

            TState state = GetState<TState>();
            _exitState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState => 
            _states[typeof(TState)] as TState;
    }
}