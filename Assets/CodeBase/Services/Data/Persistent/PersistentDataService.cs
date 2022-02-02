using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public class PersistentDataService : IPersistentDataService
    {
        public PlayerPersistentData PlayerData { get; set; }
    }
}