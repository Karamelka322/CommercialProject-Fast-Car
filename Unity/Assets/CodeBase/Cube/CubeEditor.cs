using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [CustomEditor(typeof(Cube))]
    public class CubeEditor : OdinEditor
    {
        private const int SizeGizmo = 10;

        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
        private static void RenderCustomGizmo(Cube cube, GizmoType gizmoType)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(cube.transform.position, cube.transform.position + cube.StartForward * SizeGizmo);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(cube.transform.position, cube.transform.position + cube.transform.forward * SizeGizmo);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(cube.transform.position, cube.transform.position + cube.MaxForward * SizeGizmo);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(cube.transform.position, cube.transform.position + cube.MinForward * SizeGizmo);
            
            Handles.color = Color.green;
            Handles.DrawWireDisc(cube.transform.position, cube.transform.up, SizeGizmo);
        }
    }
}