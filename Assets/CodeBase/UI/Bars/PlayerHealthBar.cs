using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReadWrite;

namespace CodeBase.UI
{
    public class PlayerHealthBar : UIBar, IStreamingReadData, ISingleReadData
    {
        private float _maxHealth;
        
        public void SingleReadData(PlayerPersistentData persistentData) => 
            _maxHealth = persistentData.SessionData.PlayerData.MaxHealth;

        public void StreamingReadData(PlayerPersistentData persistentData) => 
            Fill = persistentData.SessionData.PlayerData.Health / _maxHealth;
    }
}