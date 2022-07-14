using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class SettingsPersistentData
    {
        public RenderSettingsPersistentData RenderSettingsData;
        public InputSettingsPersistentData InputSettingsData;
        
        public SettingsPersistentData()
        {
            RenderSettingsData = new RenderSettingsPersistentData();
            InputSettingsData = new InputSettingsPersistentData();
        }
    }
}