namespace CodeBase.Services.Pause
{
    public interface IPauseHandler
    {
        string name { get; }
        void OnEnabledPause();
        void OnDisabledPause();
    }
}