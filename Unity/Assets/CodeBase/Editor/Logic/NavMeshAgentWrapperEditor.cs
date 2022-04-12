using CodeBase.Logic.Enemy;
using UnityEditor;
using UnityEngine.AI;

namespace CodeBase.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NavMeshAgentWrapper))]
    public class NavMeshAgentWrapperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            NavMeshAgentWrapper wrapper = target as NavMeshAgentWrapper;

            if (wrapper.isActiveAndEnabled && wrapper.TryGetComponent(out NavMeshAgent navMeshAgent))
            {
                ResetParameters(navMeshAgent);
                EditorUtility.SetDirty(navMeshAgent);
            }
        }

        private static void ResetParameters(NavMeshAgent navMeshAgent)
        {
            navMeshAgent.baseOffset = 0;
            navMeshAgent.speed = 0;
            navMeshAgent.angularSpeed = 0;
            navMeshAgent.acceleration = 0;
            navMeshAgent.stoppingDistance = 0;

            navMeshAgent.autoBraking = false;
            navMeshAgent.autoTraverseOffMeshLink = false;
            navMeshAgent.autoRepath = false;
        }
    }
}