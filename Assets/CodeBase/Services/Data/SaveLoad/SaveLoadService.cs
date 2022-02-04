using CodeBase.Data;
using CodeBase.Data.Perseistent;
using CodeBase.Extension;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerDataKey = "PlayerData";
        
        private readonly IPersistentDataService _progressService;

        public SaveLoadService(IPersistentDataService progressService)
        {
            _progressService = progressService;
        }

        public PlayerPersistentData LoadPlayerData() => 
            PlayerPrefs.GetString(PlayerDataKey).DeserializeFromJson<PlayerPersistentData>();

        public void SavePlayerData() => 
            PlayerPrefs.SetString(PlayerDataKey, _progressService.PlayerData.SerializeToJson());
    }
}