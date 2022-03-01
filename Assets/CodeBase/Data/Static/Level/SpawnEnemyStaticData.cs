using System;
using CodeBase.Data.Static.Enemy;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class SpawnEnemyStaticData
    {
        [GUIColor(0.8f, 0.8f, 0), BoxGroup("Data", false)]
        public EnemyTypeId EnemyType;
        
        [GUIColor(0.8f, 0.8f, 0), BoxGroup("Data", false)]
        public EnemyDifficultyTypeId DifficultyType;
        
        [GUIColor(1f, 0.5f, 0f), BoxGroup("Data", false), MinMaxSlider(0, 100)]
        public Vector2 Period;
        
        [GUIColor(1f, 0.5f, 0f), InlineProperty(LabelWidth = 30), BoxGroup("Data", false), PropertyRange(0, "MaxValueRange")]
        public float Range;

#if UNITY_EDITOR

        [UsedImplicitly]
        private float MaxValueRange() => 
            Period.y;

#endif
    }
}