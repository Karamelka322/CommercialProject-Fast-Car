using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class PlayerSessionData
    {
        public float Health;
        public float MaxHealth;

        public void CleanUp()
        {
            Health = 0;
            MaxHealth = 0;
        }
    }
}