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
        
        [Space, Required("Empty"), ValidateInput("CheckType", "Not correct type")]
        public PlayerPrefab Prefab;
        
        [InlineEditor(InlineEditorModes.LargePreview), Required("Empty"), ValidateInput("CheckType", "Not correct type")]
        public PlayerPreview Preview;
        
#if UNITY_EDITOR
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetHealth"), MinValue(0), InfoBox("It works here auto-save data in prefab"), DelayedProperty]
        private float Health;

        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetMotorPowerForward"), MinValue(0), DelayedProperty]
        private int MotorPowerForward;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetMotorPowerBackwards"), MinValue(0), DelayedProperty]
        private int MotorPowerBackwards;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetAcceleration"), MinValue(0), DelayedProperty]
        private int Acceleration;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetSteerAngle"), MinValue(0), DelayedProperty]
        private int SteerAngle;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetSpeedRotation"), MinValue(0), DelayedProperty]
        private int SpeedRotation;

        private void OnEnable()
        {
            Health = Prefab.GetComponent<PlayerHealth>().Health;
            MotorPowerForward = Prefab.GetComponent<Motor>().PowerForward;
            MotorPowerBackwards = Prefab.GetComponent<Motor>().PowerBackwards;
            Acceleration = Prefab.GetComponent<Motor>().Acceleration;
            SteerAngle = Prefab.GetComponent<SteeringGear>().SteerAngle;
            SpeedRotation = Prefab.GetComponent<SteeringGear>().SpeedRotation;
        }

        [UsedImplicitly]
        public bool CheckType() =>
            (Prefab != null && Type == Prefab.Type) && (Preview != null && Type == Preview.Type);

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
        private void SetMotorPowerForward()
        {
            Motor motor = Prefab.GetComponent<Motor>();
            motor.PowerForward = MotorPowerForward;
            EditorUtility.SetDirty(motor);
            
            AssetDatabase.SaveAssets();
        }
        
        [UsedImplicitly]
        private void SetMotorPowerBackwards()
        {
            Motor motor = Prefab.GetComponent<Motor>();
            motor.PowerBackwards = MotorPowerBackwards;
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