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
        
        [Space, Required("Empty")]
        public PlayerPrefab Prefab;
        
        [InlineEditor(InlineEditorModes.LargePreview), Required("Empty"), ValidateInput("CheckType", "Not correct type")]
        public PlayerPreview Preview;
        
#if UNITY_EDITOR
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("SetHealth"), MinValue(0), InfoBox("It works here auto-save data in prefab"), DelayedProperty]
        private float Health;

        [BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty]
        private int MotorPowerForward;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty]
        private int MotorPowerBackwards;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty]
        private int Acceleration;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty]
        private int SteerAngle;
        
        [BoxGroup("Config"), ShowInInspector, OnValueChanged("UpdateParameters"), MinValue(0), DelayedProperty]
        private int SpeedRotation;

        private void OnEnable()
        {
            Health = Prefab.GetComponent<PlayerHealth>().Health;
            MotorPowerForward = Prefab.GetComponent<Car>().Property.TorqueForward;
            MotorPowerBackwards = Prefab.GetComponent<Car>().Property.TorqueBack;
            Acceleration = Prefab.GetComponent<Car>().Property.Acceleration;
            SteerAngle = Prefab.GetComponent<Car>().Property.SteeringAngle;
            SpeedRotation = Prefab.GetComponent<Car>().Property.SpeedSteering;
        }
        
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
            PlayerHealth health = Prefab.GetComponent<PlayerHealth>();
            health.Health = Health;
            EditorUtility.SetDirty(health);
            
            AssetDatabase.SaveAssets();
        }
        
#endif
    }
}