using System;
using CodeBase.Data.Static.Player;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class RewardCarData
    {
        [UsedImplicitly]
        public bool UsingRewardCar;

        [OnValueChanged("OnPlayerTypeIdChanged"), GUIColor(0.8f, 0.8f, 0)]
        public PlayerTypeId Type;
        
#if UNITY_EDITOR

        [ShowIf("@UsingRewardCar"),ShowInInspector, InlineEditor(InlineEditorModes.LargePreview), ReadOnly]
        private GameObject Preview;

        [OnInspectorInit]
        private void OnEnable() => 
            Preview = GetPlayerPreview();

        private void OnPlayerTypeIdChanged() => 
            Preview = GetPlayerPreview();

        private GameObject GetPlayerPreview()
        {
            PlayerStaticData[] staticData = Resources.LoadAll<PlayerStaticData>("Player");

            for (int i = 0; i < staticData.Length; i++)
            {
                if (staticData[i].Type == Type) 
                    return staticData[i].Prefab.gameObject;
            }

            return default;
        }
        
#endif
    }
}