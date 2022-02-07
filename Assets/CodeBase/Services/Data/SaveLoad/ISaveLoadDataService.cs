using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadDataService : IService
    {
        PlayerPersistentData LoadPlayerData();
        DeveloperPersistentData LoadDeveloperData();
        void SavePlayerData();
    }
}