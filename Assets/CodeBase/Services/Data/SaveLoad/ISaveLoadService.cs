using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerPersistentData LoadPlayerData();
        DeveloperPersistentData LoadDeveloperData();
        void SavePlayerData();
    }
}