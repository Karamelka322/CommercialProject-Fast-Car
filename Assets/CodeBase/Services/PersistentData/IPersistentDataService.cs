using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentDataService : IService
    {
        PlayerData PlayerData { get; set; }
    }
}