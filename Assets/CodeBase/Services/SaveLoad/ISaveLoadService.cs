using CodeBase.Data;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerData LoadPlayerData();
        void SavePlayerData();
    }
}