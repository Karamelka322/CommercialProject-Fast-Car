using System;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Extension;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class ProgressPersistentData
    {
        public KeyValue<PlayerTypeId, bool>[] Players;
        public KeyValue<LevelTypeId, bool>[] Levels;

        public PlayerTypeId CurrentPlayer;
        public LevelTypeId CurrentLevel;

        private const PlayerTypeId CurrentPlayerDefault = PlayerTypeId.Demon;
        private const LevelTypeId CurrentLevelDefault = LevelTypeId.Level_1;
        
        public ProgressPersistentData()
        {
            Players = KeyValueExtension.New<PlayerTypeId, bool>(Enum.GetNames(typeof(PlayerTypeId)));
            Levels = KeyValueExtension.New<LevelTypeId, bool>(Enum.GetNames(typeof(LevelTypeId)));

            CurrentPlayer = CurrentPlayerDefault;
            CurrentLevel = CurrentLevelDefault;

            Players.SetValueToKey(CurrentPlayer, true);
            Levels.SetValueToKey(CurrentLevel, true);
        }
    }
}