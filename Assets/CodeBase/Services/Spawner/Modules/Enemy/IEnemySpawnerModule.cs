using CodeBase.Data.Static.Level;

namespace CodeBase.Services.Spawner
{
    public interface IEnemySpawnerModule
    {
        void SetConfig(LevelStaticData levelConfig);
        void TrySpawnEnemy();
        void ClenupModule();
    }
}