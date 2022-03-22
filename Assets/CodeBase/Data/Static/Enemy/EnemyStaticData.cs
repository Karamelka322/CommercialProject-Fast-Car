using CodeBase.Logic.Car;
using CodeBase.Logic.Enemy;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.Static.Enemy
{
    [CreateAssetMenu(menuName = "Static Data/Enemy", fileName = "Enemy", order = 51)]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyType;
        public EnemyDifficultyTypeId DifficultyType;

        [Space, Required("Empty")] 
        public AssetReferenceGameObject PrefabReference;

#if UNITY_EDITOR

        [ShowIf("IsCarType"), BoxGroup("Config"), InlineEditor(InlineEditorModes.LargePreview), ShowInInspector, ReadOnly, PropertyOrder(0)]
        private GameObject Prefab => PrefabReference.editorAsset;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("SetHealth"), MinValue(0), InfoBox("It works here auto-save data in prefab"), DelayedProperty, PropertyOrder(1)]
        private int Health;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("SetMotorPower"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int MotorPower;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("SetAcceleration"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int Acceleration;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("SetSteerAngle"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int SteerAngle;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("SetSpeedRotation"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int SpeedRotation;

        private void OnEnable()
        {
            Health = Prefab.GetComponent<EnemyHealth>().Health;
            MotorPower = Prefab.GetComponent<Motor>().Power;
            Acceleration = Prefab.GetComponent<Motor>().Acceleration;
            SteerAngle = Prefab.GetComponent<SteeringGear>().SteerAngle;
            SpeedRotation = Prefab.GetComponent<SteeringGear>().SpeedRotation;
        }

        [UsedImplicitly]
        private bool IsCarType() => 
            EnemyType == EnemyTypeId.Car && PrefabReference != null;
        
        [UsedImplicitly]
        private void SetSpeedRotation()
        {
            SteeringGear steeringGear = Prefab.GetComponent<SteeringGear>();
            steeringGear.SpeedRotation = SpeedRotation;
            EditorUtility.SetDirty(steeringGear);
            
            AssetDatabase.SaveAssets();
        }

        [UsedImplicitly]
        private void SetSteerAngle()
        {
            SteeringGear steeringGear = Prefab.GetComponent<SteeringGear>();
            steeringGear.SteerAngle = SteerAngle;
            EditorUtility.SetDirty(steeringGear);
            
            AssetDatabase.SaveAssets();
        }

        [UsedImplicitly]
        private void SetAcceleration()
        {
            Motor motor = Prefab.GetComponent<Motor>();
            motor.Acceleration = Acceleration;
            EditorUtility.SetDirty(motor);
            
            AssetDatabase.SaveAssets();
        }

        [UsedImplicitly]
        private void SetMotorPower()
        {
            Motor motor = Prefab.GetComponent<Motor>();
            motor.Power = MotorPower;
            EditorUtility.SetDirty(motor);
            
            AssetDatabase.SaveAssets();
        }

        [UsedImplicitly]
        private void SetHealth()
        {
            EnemyHealth health = Prefab.GetComponent<EnemyHealth>();
            health.Health = Health;
            EditorUtility.SetDirty(health);
            
            AssetDatabase.SaveAssets();
        }
#endif
    }
}