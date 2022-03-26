using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface IGeneratorSpawnerModule
    {
        void TrySpawnGenerator();
        void SetConfig(LevelStaticData levelConfig);
        void Clear();
    }
}