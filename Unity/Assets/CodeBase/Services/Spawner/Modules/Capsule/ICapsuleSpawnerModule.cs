using System.Threading.Tasks;
using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface ICapsuleSpawnerModule
    {
        void SetConfig(CapsuleSpawnConfig config);
        Task TrySpawnCapsule();
        void Clear();
        Task LoadResourcesAsync();
    }
}