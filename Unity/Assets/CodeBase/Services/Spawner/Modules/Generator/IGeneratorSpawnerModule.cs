using System.Threading.Tasks;
using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface IGeneratorSpawnerModule
    {
        void SetConfig(GeneratorSpawnConfig config);
        Task SpawnGenerator();
        void Clear();
    }
}