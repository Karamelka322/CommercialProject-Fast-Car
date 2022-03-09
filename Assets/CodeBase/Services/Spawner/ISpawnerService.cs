using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface ISpawnerService : IService
    {
        void SetConfig(LevelStaticData levelConfig);
        void Clenup();
        void RealTimeSpawn();
        void Reset();
    }
}