using System;
using CodeBase.Data.Static.Level;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class LevelSessionData
    {
        public LevelStaticData CurrentLevelConfig;
        public GeneratorSessionData GeneratorData;
        
        public float StopwatchTime;
        
        public LevelSessionData()
        {
            GeneratorData = new GeneratorSessionData();
        }

        public void CleanUp()
        {
            CurrentLevelConfig = null;
            GeneratorData.Power = 0;
            StopwatchTime = 0;
        }
    }
}