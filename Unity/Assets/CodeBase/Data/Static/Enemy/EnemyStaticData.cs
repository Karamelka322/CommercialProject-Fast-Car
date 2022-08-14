using System.Runtime.CompilerServices;
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
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int MotorPowerForward;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int MotorPowerBackwards;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int Acceleration;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int SteerAngle;
        
        [ShowIf("IsCarType"), BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty, PropertyOrder(1)]
        private int SpeedRotation;

        private void OnEnable()
        {
            Health = Prefab.GetComponent<EnemyHealth>().Health;
            
            MotorPowerForward = Prefab.GetComponent<Car>().Property.TorqueForward;
            MotorPowerBackwards = Prefab.GetComponent<Car>().Property.TorqueBack;
            Acceleration = Prefab.GetComponent<Car>().Property.Acceleration;
            SteerAngle = Prefab.GetComponent<Car>().Property.SteeringAngle;
            SpeedRotation = Prefab.GetComponent<Car>().Property.SpeedSteering;
        }

        [UsedImplicitly]
        private bool IsCarType() => 
            EnemyType == EnemyTypeId.Car && PrefabReference != null;
        
        [UsedImplicitly]
        private void UpdateParameters()
        {
            Car car = Prefab.GetComponent<Car>();
            car.Property.SpeedSteering = SpeedRotation;
            car.Property.SteeringAngle = SteerAngle;
            car.Property.Acceleration = Acceleration;
            car.Property.TorqueForward = MotorPowerForward;
            car.Property.TorqueBack = MotorPowerBackwards;
            
            EditorUtility.SetDirty(car);
            
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