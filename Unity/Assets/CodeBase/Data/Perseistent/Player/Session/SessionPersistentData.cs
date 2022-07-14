using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class SessionPersistentData
    {
        public PlayerSessionData PlayerData;
        public LevelSessionData LevelData;

        public SessionPersistentData()
        {
            PlayerData = new PlayerSessionData();
            LevelData = new LevelSessionData();
        }
        
        public void CleanUp()
        {
            PlayerData.CleanUp();
            LevelData.CleanUp();
        }
    }
}