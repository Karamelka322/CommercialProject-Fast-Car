using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IExitState _exitState;
        
        public GameStateMachine(AllServices services, ICorutineRunner corutineRunner)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, corutineRunner),
                [typeof(LoadPersistentDataState)] = new LoadPersistentDataState(this, services.Single<IPersistentDataService>(), services.Single<ISaveLoadService>()),
                [typeof(LoadMenuState)] = new LoadMenuState(services.Single<ISceneLoaderService>(), services.Single<IUIFactory>()),
                [typeof(LoadLevelState)] = new LoadLevelState(services.Single<ISceneLoaderService>(), services.Single<IUIFactory>(), services.Single<IPlayerFactory>(), services.Single<IInputService>()),
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