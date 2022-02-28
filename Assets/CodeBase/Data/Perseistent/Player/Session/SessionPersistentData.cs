using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class SessionPersistentData
    {
        public PlayerSessionData PlayerData;
        public LevelSessionData LevelData;

        public float StopwatchTime;
        
        public SessionPersistentData()
        {
            PlayerData = new PlayerSessionData();
            LevelData = new LevelSessionData();
        }
        
        public void Reset()
        {
            StopwatchTime = 0;
            PlayerData.Health = PlayerData.MaxHealth;
            LevelData.GeneratorData.Power = GeneratorSessionData.MaxPower;
        }
    }
}