using CodeBase.Logic.Car;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(Car))]
    public class CarEditor : OdinEditor
    {
        private const int _sizeLineDirectionDrift = 2;
        
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
        private static void RenderCustomGizmo(Car car, GizmoType gizmoType)
        {
            if(car.Property.Slip < 1)
            {
                Vector3 position = car.transform.position;
                
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(position, car.Property.DirectionDrift * _sizeLineDirectionDrift + position);
                
                Gizmos.color = Color.red;
                Gizmos.DrawLine(position, car.transform.forward * _sizeLineDirectionDrift + position);
            }
        }
    }
}