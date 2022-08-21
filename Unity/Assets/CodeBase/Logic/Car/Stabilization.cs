using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    public class Stabilization
    {
        private readonly CarProperty _property;
        private readonly Transform _transform;

        private Vector3 _rotation;

        public Stabilization(CarProperty property, Transform transform)
        {
            _property = property;
            _transform = transform;
        }

        public void Stabilize()
        {
            _rotation = _transform.localEulerAngles;
            _rotation.ClampEuler(_property.MaxRotationX, _property.MaxRotationZ);
            
            _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(_rotation), Time.deltaTime * _property.SpeedStabilization);
        }
    }
}