using CodeBase.Logic.Car;
using CodeBase.Logic.Player;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Data.Static.Player
{
    [CreateAssetMenu(menuName = "Static Data/Player", fileName = "Player", order = 51)]
    public class PlayerStaticData : ScriptableObject
    {
        public PlayerTypeId Type;
        
        [Space, InlineEditor(InlineEditorModes.LargePreview), Required("Empty")]
        public PlayerPrefab Prefab;
        
#if UNITY_EDITOR
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetHealth"), MinValue(0), InfoBox("It works here auto-save data in prefab")]
        private float Health;

        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetMotorPower"), MinValue(0)]
        private int MotorPower;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetAcceleration"), MinValue(0)]
        private int Acceleration;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetSteerAngle"), MinValue(0)]
        private int SteerAngle;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetSpeedRotation"), MinValue(0)]
        private int SpeedRotation;

        private bool isFirstLoad;
        
        private void OnValidate()
        {
            if(isFirstLoad)
                return;
            
            Health = Prefab.GetComponent<PlayerHealth>().Health;
            MotorPower = Prefab.GetComponent<Motor>().Power;
            Acceleration = Prefab.GetComponent<Motor>().Acceleration;
            SteerAngle = Prefab.GetComponent<SteeringGear>().SteerAngle;
            SpeedRotation = Prefab.GetComponent<SteeringGear>().SpeedRotation;

            isFirstLoad = true;
        }

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
            PlayerHealth health = Prefab.GetComponent<PlayerHealth>();
            health.Health = Health;
            EditorUtility.SetDirty(health);
            
            AssetDatabase.SaveAssets();
        }
        
#endif
    }
}