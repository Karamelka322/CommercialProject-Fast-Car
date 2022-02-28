using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class LevelSessionData
    {
        public GeneratorSessionData GeneratorData;
        
        public LevelSessionData()
        {
            GeneratorData = new GeneratorSessionData();
        }
    }
}