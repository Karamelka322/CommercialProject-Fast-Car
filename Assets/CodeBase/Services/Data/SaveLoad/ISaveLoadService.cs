using CodeBase.Data;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerPersistentData LoadPlayerData();
        void SavePlayerData();
    }
}