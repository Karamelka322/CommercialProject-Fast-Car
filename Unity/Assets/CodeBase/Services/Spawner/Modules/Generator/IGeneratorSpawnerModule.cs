using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface IGeneratorSpawnerModule
    {
        void SetConfig(GeneratorSpawnConfig config);
        void SpawnGenerator();
        void Clear();
    }
}