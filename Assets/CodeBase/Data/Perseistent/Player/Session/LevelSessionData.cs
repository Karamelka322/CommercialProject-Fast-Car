using System;
using CodeBase.Data.Static.Level;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class LevelSessionData
    {
        public LevelStaticData CurrentLevelConfig;
        public GeneratorSessionData GeneratorData;
        
        public LevelSessionData()
        {
            GeneratorData = new GeneratorSessionData();
        }
    }
}