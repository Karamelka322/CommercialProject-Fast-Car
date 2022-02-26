using System;
using CodeBase.Data.Static.Enemy;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class SpawnEnemyStaticData
    {
        public EnemyTypeId EnemyType;
        public EnemyDifficultyTypeId DifficultyType;
        public MaxMinValue Period;
        public MaxMinValue Range;
    }
}