using UnityEngine;

namespace CodeBase
{
    [RequireComponent(typeof(Transform))]
    public class Point : MonoBehaviour
    {
        public Vector3 WorldPosition => transform.position;
    }
}