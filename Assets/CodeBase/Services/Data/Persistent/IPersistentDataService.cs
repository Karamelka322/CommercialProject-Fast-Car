using CodeBase.Data;
using CodeBase.Data.Perseistent;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentDataService : IService
    {
        PlayerPersistentData PlayerData { get; set; }
    }
}