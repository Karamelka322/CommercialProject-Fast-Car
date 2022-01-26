namespace CodeBase.Infrastructure.States
{
    public interface IState : IEnterState, IExitState
    {
        
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