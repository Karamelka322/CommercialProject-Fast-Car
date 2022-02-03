using UnityEngine;

namespace CodeBase
{
    [RequireComponent(typeof(Transform))]
    public class Point : MonoBehaviour
    {
        public Color Color;
        public float Range;
        
        public Vector3 WorldPosition => transform.position;
    }
}