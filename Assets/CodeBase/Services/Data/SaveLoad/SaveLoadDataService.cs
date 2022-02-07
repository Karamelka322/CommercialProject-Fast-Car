using CodeBase.Data.Perseistent;
using CodeBase.Data.Perseistent.Developer;
using CodeBase.Extension;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadDataService : ISaveLoadDataService
    {
        private const string PlayerDataKey = "PlayerData";
        private const string DeveloperDataKey = "DeveloperData";

        private readonly IPersistentDataService _progressService;
        
        public SaveLoadDataService(IPersistentDataService progressService)
        {
            _progressService = progressService;
        }

        public PlayerPersistentData LoadPlayerData() => 
            PlayerPrefs.GetString(PlayerDataKey).DeserializeFromJson<PlayerPersistentData>();

        public DeveloperPersistentData LoadDeveloperData() => 
            PlayerPrefs.GetString(DeveloperDataKey).DeserializeFromJson<DeveloperPersistentData>();

        public void SavePlayerData() => 
            PlayerPrefs.SetString(PlayerDataKey, _progressService.PlayerData.SerializeToJson());
    }
}