using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class PlayerPersistentData
    {
        public SettingsPersistentData SettingsData;
        public ProgressPersistentData ProgressData;
        public SessionPersistentData SessionData;

        public PlayerPersistentData()
        {
            SettingsData = new SettingsPersistentData();
            ProgressData = new ProgressPersistentData();
            SessionData = new SessionPersistentData();
        }
    }
}