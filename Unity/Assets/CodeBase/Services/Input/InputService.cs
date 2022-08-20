using JetBrains.Annotations;

namespace CodeBase.Services.Input
{
    [UsedImplicitly]
    public class InputService : IInputService
    {
        public IInputVariant InputVariant { get; private set; }
        
        public void RegisterInput(IInputVariant inputVariant) => 
            InputVariant = inputVariant;

        public void CleanUp() => 
            InputVariant = null;
    }
}