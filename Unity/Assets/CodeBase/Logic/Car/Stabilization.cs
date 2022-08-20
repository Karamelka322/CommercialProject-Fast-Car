using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Stabilization
    {
        private readonly CarInfo _info;
        private readonly CarProperty _property;
        private readonly Transform _transform;

        private Vector3 _rotation;

        public Stabilization(CarProperty property, CarInfo info, Transform transform)
        {
            _property = property;
            _info = info;
            _transform = transform;
        }

        public void Update()
        {
            if(_info.IsGrounded == false)
                Stabilize();
        }

        private void Stabilize()
        {
            _rotation = UnityEditor.TransformUtils.GetInspectorRotation(_transform);

            float nextRotationX = Mathf.Clamp(_rotation.x, -_property.MaxRotationX, _property.MaxRotationX);
            float nextRotationZ = Mathf.Clamp(_rotation.z, -_property.MaxRotationZ, _property.MaxRotationZ);

            _rotation.x = Mathf.Lerp(_rotation.x, nextRotationX, Time.deltaTime * _property.SpeedStabilization);
            _rotation.z = Mathf.Lerp(_rotation.z, nextRotationZ, Time.deltaTime * _property.SpeedStabilization);

            UnityEditor.TransformUtils.SetInspectorRotation(_transform, _rotation);
        }
    }
}