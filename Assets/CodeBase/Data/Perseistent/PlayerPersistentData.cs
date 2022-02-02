using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerPersistentData
    {
        public InputPersistentData InputData;

        public PlayerPersistentData()
        {
            InputData = new InputPersistentData();
        }
    }
}