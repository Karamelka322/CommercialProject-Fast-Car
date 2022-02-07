using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class SessionPersistentData
    {
        public PlayerSessionData PlayerSessionData;

        public SessionPersistentData()
        {
            PlayerSessionData = new PlayerSessionData();
        }
    }
}