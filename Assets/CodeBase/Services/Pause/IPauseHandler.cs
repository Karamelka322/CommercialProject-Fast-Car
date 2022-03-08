using CodeBase.Services.Defeat;

namespace CodeBase.Services.Pause
{
    public interface IPauseHandler : IHandler
    {
        void OnEnabledPause();
        void OnDisabledPause();
    }
}