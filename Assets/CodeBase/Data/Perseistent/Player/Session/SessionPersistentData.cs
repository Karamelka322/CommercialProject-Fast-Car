using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class SessionPersistentData
    {
        public PlayerSessionData PlayerData;
        public GeneratorSessionData GeneratorData;

        public SessionPersistentData()
        {
            PlayerData = new PlayerSessionData();
            GeneratorData = new GeneratorSessionData();
        }
        
        public void Reset()
        {
            PlayerData.Health = PlayerData.MaxHealth;
            GeneratorData.Power = GeneratorSessionData.MaxPower;
        }
    }
}