using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [ExecuteInEditMode]
    public class Cube : MonoBehaviour
    {
        [ValidateInput(nameof(InputNowAngle))]
        public float NowAngle;
        
        public float MaxAngle;

        [ReadOnly]
        public Vector3 StartForward;

        [ReadOnly]
        public Vector3 MaxForward;

        [ReadOnly]
        public Vector3 MinForward;

        [ReadOnly] 
        public Vector3 Axis;
        
        private float Offset => StartForward.z > 0 ? 1 : -1;

        private void Update() => 
            Rotation();

        [Button("Reset Forward")]
        private void ResetForward()
        {
            StartForward = transform.forward;
            Axis = transform.up;
            NowAngle = 0;
            
            MaxForward = GetMaxForward();
            MinForward = GetMinForward();
        }

        private void OnValidate()
        {
            MaxForward = GetMaxForward();
            MinForward = GetMinForward();
        }

        private Vector3 GetMaxForward()
        {
            return new()
            {
                x = StartForward.magnitude 
                    * Mathf.Cos(((MaxAngle + Vector3.Angle(Vector3.right, StartForward)) * Offset) * Mathf.PI / 180),
                
                y = StartForward.y,
                
                z = StartForward.magnitude 
                    * Mathf.Sin(((MaxAngle + Vector3.Angle(Vector3.right, StartForward)) * Offset) * Mathf.PI / 180)
            };
        }

        private Vector3 GetMinForward()
        {
            return new()
            {
                x = StartForward.magnitude 
                    * Mathf.Cos(((-MaxAngle + Vector3.Angle(Vector3.right, StartForward)) * Offset) * Mathf.PI / 180),
                
                y = StartForward.y,
                
                z = StartForward.magnitude 
                    * Mathf.Sin(((-MaxAngle + Vector3.Angle(Vector3.right, StartForward)) * Offset) * Mathf.PI / 180)
            };
        }

        private void Rotation()
        {
            Quaternion nextRotation = Quaternion.AngleAxis(NowAngle + Vector3.Angle(Vector3.forward, StartForward) *
                (StartForward.normalized.x > 0 ? 1 : -1), transform.up);

            Vector3 rotation = nextRotation.eulerAngles;
            rotation.x = transform.localEulerAngles.x;
            rotation.z = transform.localEulerAngles.z;
            
            transform.localEulerAngles = rotation;
        }

        private bool InputNowAngle()
        {
            NowAngle = Mathf.Clamp(NowAngle, -MaxAngle, MaxAngle);
            return NowAngle <= MaxAngle && NowAngle >= -MaxAngle;
        }
    }
}