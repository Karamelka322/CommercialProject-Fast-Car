using CodeBase.Data.Static.Enemy;
using CodeBase.Logic.Car;
using CodeBase.Logic.Enemy;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EnemyStaticData))]
    public class EnemyStaticDataEditor : OdinEditor
    {
        private EnemyStaticData _staticData;
        
        private int _health;
        private int _motorPower;
        private int _acceleration;
        private int _steerAngle;
        private int _speedRotation;

        protected override void OnEnable() => 
            _staticData = target as EnemyStaticData;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if(_staticData.Prefab == false)
                return;
            
            EditorGUILayout.Space();
            SetHealth();

            if (_staticData.EnemyType == EnemyTypeId.Car)
            {
                SetMotorPower();
                SetAcceleration();
                SetSteerAngle();
                SetSpeedRotation();
            }
        }

        private void SetSpeedRotation()
        {
            SteeringGear steeringGear = _staticData.Prefab.GetComponent<SteeringGear>();
            _speedRotation = Mathf.Clamp(EditorGUILayout.IntField("Wheels Turning Speed", steeringGear.SpeedRotation), 0, int.MaxValue);
            steeringGear.SpeedRotation = _speedRotation;
            EditorUtility.SetDirty(steeringGear);
        }

        private void SetSteerAngle()
        {
            SteeringGear steeringGear = _staticData.Prefab.GetComponent<SteeringGear>();
            _steerAngle = Mathf.Clamp(EditorGUILayout.IntField("Wheels Steer Angle", steeringGear.SteerAngle), 0, int.MaxValue);
            steeringGear.SteerAngle = _steerAngle;
            EditorUtility.SetDirty(steeringGear);
        }

        private void SetAcceleration()
        {
            Motor motor = _staticData.Prefab.GetComponent<Motor>();
            _acceleration = Mathf.Clamp(EditorGUILayout.IntField("Speed Acceleration", motor.Acceleration), 0, int.MaxValue);
            motor.Acceleration = _acceleration;
            EditorUtility.SetDirty(motor);
        }

        private void SetMotorPower()
        {
            Motor motor = _staticData.Prefab.GetComponent<Motor>();
            _motorPower = Mathf.Clamp(EditorGUILayout.IntField("Motor Power", motor.Power), 0, int.MaxValue);
            motor.Power = _motorPower;
            EditorUtility.SetDirty(motor);
        }

        private void SetHealth()
        {
            EnemyHealth health = _staticData.Prefab.GetComponent<EnemyHealth>();
            _health = Mathf.Clamp(EditorGUILayout.IntField("Health", health.Health), 0, int.MaxValue);
            health.Health = _health;
            EditorUtility.SetDirty(health);
        }
    }
}