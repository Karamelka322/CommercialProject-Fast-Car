using System;
using CodeBase.Data.Static.Level;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class ProgressPersistentData
    {
        public KeyValue<LevelTypeId, bool>[] Levels;
    }
}