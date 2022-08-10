using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface ICapsuleSpawnerModule
    {
        void SetConfig(CapsuleSpawnConfig config);
        void TrySpawnCapsule();
        void Clear();
    }
}