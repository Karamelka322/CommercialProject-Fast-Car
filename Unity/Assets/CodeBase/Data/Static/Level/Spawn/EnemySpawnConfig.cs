using System;
using CodeBase.Data.Static.Enemy;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class EnemySpawnConfig
    {
        [Title("Config", titleAlignment: TitleAlignments.Right), GUIColor(1f, 0.5f, 0f), BoxGroup("Data", false), MinMaxSlider(0, 100), PropertyOrder(1)]
        public Vector2 Period;

        [GUIColor(1f, 0.5f, 0f), InlineProperty(LabelWidth = 30), BoxGroup("Data"), PropertyRange(0.1f, "MaxValueRange"), SuffixLabel("@(int)((Period.y - Period.x)/(Range))", true), PropertyOrder(1)]
        public float Range = 0.1f;

        [Title("Asset", titleAlignment: TitleAlignments.Right), BoxGroup("Data"), Required("Empty"), PropertyOrder(0)] 
        public AssetReferenceGameObject PrefabReference;

        [HideInInspector, NonSerialized] 
        public bool IsLocked;
        
        
#if UNITY_EDITOR

        [ShowIf("NotEmpty"), BoxGroup("Data"), InlineEditor(InlineEditorModes.LargePreview), ShowInInspector, ReadOnly, PropertyOrder(0)]
        private GameObject Prefab => PrefabReference.editorAsset;
        
        [UsedImplicitly]
        private bool NotEmpty() => 
            PrefabReference.editorAsset;
        
        [UsedImplicitly]
        private float MaxValueRange() => 
            Mathf.Clamp(Period.y - Period.x, 0.1f, float.MaxValue);

#endif
    }
}