using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class Stabilization : MonoBehaviour
    {
        [SerializeField] 
        private Rigidbody _rigidbody;

        [SerializeField] 
        private Vector3 _intensity;
        
        private IUpdatable _updatable;

        public void Construct(IUpdatable updatable) =>
            _updatable = updatable;

        private void Start() => 
            _updatable.OnUpdate += OnUpdate;

        private void OnDisable() => 
            _updatable.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            _rigidbody.AddRelativeTorque(_intensity);
        }
    }
}