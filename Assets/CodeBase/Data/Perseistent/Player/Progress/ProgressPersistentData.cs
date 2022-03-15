using System;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class ProgressPersistentData
    {
        public KeyValue<PlayerTypeId, bool>[] Players;
        public KeyValue<LevelTypeId, bool>[] Levels;

        public PlayerTypeId CurrentPlayer;
        public LevelTypeId CurrentLevel;
    }
}