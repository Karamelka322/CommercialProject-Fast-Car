using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
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
using CodeBase.Services.Spawner;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        private IExitState _exitState;
        
        public GameStateMachine(AllServices services, ICorutineRunner corutineRunner, IUpdatable updatable)
        {
            _states = new Dictionary<Type, IExitState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, corutineRunner, updatable),
                
                [typeof(LoadPersistentDataState)] = new LoadPersistentDataState(
                    services.Single<IGameStateMachine>(),
                    services.Single<IPersistentDataService>(),
                    services.Single<ISaveLoadDataService>()),
                
                [typeof(LoadMenuState)] = new LoadMenuState(
                    services.Single<ISceneLoaderService>(),
                    services.Single<IUIFactory>(),
                    services.Single<IPlayerFactory>(),
                    services.Single<IPersistentDataService>()),
                
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
                    services.Single<IPauseService>(),
                    services.Single<ISpawnerService>()),
                
                [typeof(LoopLevelState)] = new LoopLevelState(
                    services.Single<IUIFactory>(),
                    services.Single<IPersistentDataService>(),
                    services.Single<IReadWriteDataService>(),
                    services.Single<ITweenService>(),
                    services.Single<IPauseService>(),
                    services.Single<ISpawnerService>(),
                    services.Single<IUpdateService>(),
                    services.Single<IDefeatService>(),
                    services.Single<IVictoryService>()),
                
                [typeof(ReplayLevelState)] = new ReplayLevelState(
                    services.Single<IGameStateMachine>(),
                    services.Single<IReplayService>(),
                    services.Single<IUIFactory>(),
                    services.Single<IPauseService>(),
                    services.Single<IPersistentDataService>()),
                
                [typeof(UnloadLevelState)] = new UnloadLevelState(
                    services.Single<IGameStateMachine>(),
                    services.Single<ISpawnerService>(),
                    services.Single<IInputService>(),
                    services.Single<IVictoryService>(),
                    services.Single<IDefeatService>(),
                    services.Single<IReplayService>(),
                    services.Single<IPersistentDataService>(),
                    services.Single<IReadWriteDataService>(),
                    services.Single<IPauseService>(),
                    services.Single<IRandomService>(),
                    services.Single<ISaveLoadDataService>()),
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