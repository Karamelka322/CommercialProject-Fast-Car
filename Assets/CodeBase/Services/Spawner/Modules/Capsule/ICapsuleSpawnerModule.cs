using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface ICapsuleSpawnerModule
    {
        void SetConfig(LevelStaticData levelConfig);
        void TrySpawnCapsule();
        void Clear();
    }
}