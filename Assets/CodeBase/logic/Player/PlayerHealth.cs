using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReaderWriter;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerHealth : MonoBehaviour, IReadData, IWriteData
    {
        private float _health;
        
        public void ReadData(PlayerPersistentData persistentData) => 
            _health = persistentData.SessionData.PlayerSessionData.Health;

        public void WriteData(PlayerPersistentData persistentData) => 
            persistentData.SessionData.PlayerSessionData.Health = _health;
    }
}