using System.Threading.Tasks;
using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface IEnemySpawnerModule
    {
        void SetConfig(EnemiesSpawnConfig config);
        void TrySpawnEnemy();
        void Clear();
        void ResetModule();
        Task LoadResourcesAsync();
    }
}