namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        IInputVariant InputVariant { get; }
        void RegisterInput(IInputVariant inputVariant);
        void CleanUp();
    }
}