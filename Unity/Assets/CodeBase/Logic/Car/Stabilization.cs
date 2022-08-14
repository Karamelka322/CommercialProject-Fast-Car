using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Stabilization
    {
        private readonly Transform _transform;
        private readonly CarProperty _property;

        private Vector3 _rotation;
        
        public Stabilization(Transform transform, CarProperty property)
        {
            _transform = transform;
            _property = property;
        }

        public void Stabilize()
        {
            _rotation = UnityEditor.TransformUtils.GetInspectorRotation(_transform);

            _rotation.x = Mathf.Clamp(_rotation.x, -_property.MaxRotationX, _property.MaxRotationX);
            _rotation.z = Mathf.Clamp(_rotation.z, -_property.MaxRotationZ, _property.MaxRotationZ);

            UnityEditor.TransformUtils.SetInspectorRotation(_transform, _rotation);
        }
    }
}