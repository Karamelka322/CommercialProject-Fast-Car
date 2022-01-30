using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.Input;

namespace CodeBase.Infrastructure
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
    }
}