using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentDataService : IService
    {
        PlayerPersistentData PlayerData { get; set; }
        DeveloperPersistentData DeveloperData { get; set; }
    }
}