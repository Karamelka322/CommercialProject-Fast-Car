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
        void Unload<T>() where T : class, IState;
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