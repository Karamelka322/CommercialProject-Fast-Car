namespace CodeBase.Services.Replay
{
    public interface IReplayHandler
    {
        string name { get; }
        void OnReplay();
    }
}