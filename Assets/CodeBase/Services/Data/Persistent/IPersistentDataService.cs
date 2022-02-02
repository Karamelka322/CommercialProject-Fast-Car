using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentDataService : IService
    {
        PlayerPersistentData PlayerData { get; set; }
    }
}