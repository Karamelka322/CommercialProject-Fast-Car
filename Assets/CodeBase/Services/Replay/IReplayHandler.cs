using CodeBase.Services.Defeat;

namespace CodeBase.Services.Replay
{
    public interface IReplayHandler : IHandler
    {
        void OnReplay();
    }
}