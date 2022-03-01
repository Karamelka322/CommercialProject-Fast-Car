using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface ISpawnerService : IService
    {
        void Enable();
        void Disable();
        void SetConfig(LevelStaticData levelConfig);
        void Clenup();
    }
}