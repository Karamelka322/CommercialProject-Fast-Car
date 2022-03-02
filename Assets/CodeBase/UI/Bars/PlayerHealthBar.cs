using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReadWrite;

namespace CodeBase.UI
{
    public class PlayerHealthBar : UIBar, IStreamingReadData
    {
        public void StreamingReadData(PlayerPersistentData persistentData) => 
            Fill = persistentData.SessionData.PlayerData.Health / persistentData.SessionData.PlayerData.MaxHealth;
    }
}