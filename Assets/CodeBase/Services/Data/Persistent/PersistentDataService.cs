using CodeBase.Data;
using CodeBase.Data.Perseistent;

namespace CodeBase.Services.PersistentProgress
{
    public class PersistentDataService : IPersistentDataService
    {
        public PlayerPersistentData PlayerData { get; set; }
    }
}