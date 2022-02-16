namespace CodeBase.Infrastructure.States
{
    public interface IState : IEnterState, IExitState
    {
        
    }

    public interface IUpdateableState : IState
    {
        void OnUpdate();
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