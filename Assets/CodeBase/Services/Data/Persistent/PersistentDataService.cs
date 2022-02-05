using CodeBase.Data;
using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;

namespace CodeBase.Services.PersistentProgress
{
    public class PersistentDataService : IPersistentDataService
    {
        public PlayerPersistentData PlayerData { get; set; }
        public DeveloperPersistentData DeveloperData { get; set; }
    }
}