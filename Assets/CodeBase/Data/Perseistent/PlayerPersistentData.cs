using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class PlayerPersistentData
    {
        public InputPersistentData InputData;
        public LevelPersistentData LevelData;

        public PlayerPersistentData()
        {
            InputData = new InputPersistentData();
            LevelData = new LevelPersistentData();
        }
    }
}