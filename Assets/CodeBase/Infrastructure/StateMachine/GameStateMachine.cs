using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.Player;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.LoadScene;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IExitState _exitState;
        
        public GameStateMachine(AllServices services, ICorutineRunner corutineRunner, IUpdatable updatable)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, corutineRunner, updatable),
                
                [typeof(LoadPersistentDataState)] = new LoadPersistentDataState(
                    services.Single<IGameStateMachine>(),
                    services.Single<IPersistentDataService>(),
                    services.Single<ISaveLoadDataService>(),
                    services.Single<IStaticDataService>()),
                
                [typeof(LoadMenuState)] = new LoadMenuState(
                    services.Single<ISceneLoaderService>(),
                    services.Single<IUIFactory>(),
                    services.Single<IPauseService>(),
                    services.Single<IReplayService>()),
                
                [typeof(LoadLevelState)] = new LoadLevelState(
                    services.Single<IGameStateMachine>(),
                    services.Single<ISceneLoaderService>(),
                    services.Single<IUIFactory>(),
                    services.Single<IPlayerFactory>(),
                    services.Single<IPersistentDataService>(),
                    services.Single<IStaticDataService>(),
                    services.Single<ILevelFactory>(),
                    services.Single<IRandomService>(),
                    services.Single<IUpdateService>(),
                    services.Single<IReadWriteDataService>(),
                    services.Single<IPauseService>()),
                
                [typeof(LoopLevelState)] = new LoopLevelState(
                    services.Single<ILevelFactory>(),
                    services.Single<IEnemyFactory>(),
                    services.Single<IUIFactory>(),
                    services.Single<IRandomService>(),
                    services.Single<IPersistentDataService>(),
                    services.Single<IUpdateService>(),
                    services.Single<IReadWriteDataService>(),
                    services.Single<IInputService>(),
                    services.Single<IPlayerFactory>(),
                    services.Single<ITweenService>(),
                    services.Single<IPauseService>()),
                
                [typeof(ReplayLevelState)] = new ReplayLevelState(
                    services.Single<IGameStateMachine>(),
                    services.Single<IReplayService>(),
                    services.Single<IUIFactory>(),
                    services.Single<IPauseService>()),
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