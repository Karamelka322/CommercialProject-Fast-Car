namespace CodeBase.Infrastructure.States
{
    public interface IState : IEnterState, IExitState
    {
        
    }

    public interface IUpdateableState : IState
    {
        void OnUpdate();
    }

    public interface IUnloadState : IExitState
    {
        void Unload<TState>() where TState : class, IState;
    }
    
    public interface IEnterState
    {
        void Enter();
    }

    public interface IExitState
    {
        void Exit();
    }
}