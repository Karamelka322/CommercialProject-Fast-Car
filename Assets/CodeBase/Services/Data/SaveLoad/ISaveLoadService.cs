using CodeBase.Data;
using CodeBase.Data.Perseistent;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerPersistentData LoadPlayerData();
        void SavePlayerData();
    }
}