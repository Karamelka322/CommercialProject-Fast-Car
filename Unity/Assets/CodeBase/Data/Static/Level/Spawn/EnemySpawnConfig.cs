using System;
using CodeBase.Data.Static.Enemy;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class EnemySpawnConfig
    {
        [GUIColor(0.8f, 0.8f, 0), BoxGroup("Data", false)]
        public EnemyTypeId EnemyType;
        
        [GUIColor(0.8f, 0.8f, 0), BoxGroup("Data", false)]
        public EnemyDifficultyTypeId DifficultyType;
        
        [GUIColor(1f, 0.5f, 0f), BoxGroup("Data", false), MinMaxSlider(0, 100)]
        public Vector2 Period;
        
        [GUIColor(1f, 0.5f, 0f), InlineProperty(LabelWidth = 30), BoxGroup("Data", false), PropertyRange(0.1f, "MaxValueRange"), SuffixLabel("@(int)((Period.y - Period.x)/(Range))", true)]
        public float Range = 0.1f;

        [HideInInspector, NonSerialized] 
        public bool IsLocked;
        
        
#if UNITY_EDITOR

        [UsedImplicitly]
        private float MaxValueRange() => 
            Mathf.Clamp(Period.y - Period.x, 0.1f, float.MaxValue);

#endif
    }
}