using CodeBase.Services.Defeat;

namespace CodeBase.Services.Victory
{
    public interface IPlayerVictoryHandler : IHandler
    {
        void OnVictory();
    }
}