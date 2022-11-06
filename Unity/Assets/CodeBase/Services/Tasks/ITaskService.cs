namespace CodeBase.Services.Tasks
{
    public interface ITaskService : IService
    {
        void Initialize();
        void CleanUp();
    }
}