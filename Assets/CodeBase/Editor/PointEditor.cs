using CodeBase.Logic.World;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Point))]
    public class PointEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Selected | GizmoType.InSelectionHierarchy)]
        private static void RenderCustomGizmo(Point point, GizmoType gizmoType)
        {
            Gizmos.color = point.Color;
            Gizmos.DrawSphere(point.WorldPosition, point.Range);
        }
    }
}