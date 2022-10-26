using System.Threading.Tasks;
using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface ISpawnerService : IService
    {
        void SetConfig(LevelStaticData levelStaticData);
        void CleanUp();
        void SpawnOnUpdate();
        void Reset();
        Task SpawnOnLoaded();
        Task LoadResourcesAsync();
    }
}