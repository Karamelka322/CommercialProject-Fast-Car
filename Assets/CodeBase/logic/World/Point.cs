using UnityEngine;

namespace CodeBase.Logic.World
{
    [RequireComponent(typeof(Transform))]
    public class Point : MonoBehaviour
    {
        public Color Color;
        public float Range;
        
        public Vector3 WorldPosition => transform.position;
        public Quaternion WorldRotation => transform.rotation;
        public Vector3 LocalPosition => transform.localPosition;
    }
}