using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class PlayerPersistentData
    {
        public InputPersistentData InputData;
        public LevelPersistentData LevelData;
        public SessionPersistentData SessionData;

        public PlayerPersistentData()
        {
            InputData = new InputPersistentData();
            LevelData = new LevelPersistentData();
            SessionData = new SessionPersistentData();
        }
    }
}