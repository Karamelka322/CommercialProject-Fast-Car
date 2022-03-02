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
            LevelData.CurrentLevelConfig = null;
            
            StopwatchTime = 0;
            PlayerData.Health = 0;
            PlayerData.MaxHealth = 0;
            LevelData.GeneratorData.Power = 0;
        }
    }
}