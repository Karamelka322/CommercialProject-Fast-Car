using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class RenderSettingsPersistentData
    {
        public int MaxFrameRate;
        
        private int _maxFrameRateDefault = 60;
        
        public RenderSettingsPersistentData()
        {
            MaxFrameRate = _maxFrameRateDefault;
        }
    }
}