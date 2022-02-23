namespace CodeBase.Services.Pause
{
    public interface IPauseHandler
    {
        void OnEnabledPause();
        void OnDisabledPause();
    }
}