using System;
using UnityEngine;

namespace CodeBase.Logic.World
{
    [RequireComponent(typeof(Collider))]
    public class Area : MonoBehaviour
    {
        [SerializeField] 
        private Collider _collider;

        public event Action<Collider> OnAreaEnter;
        
        private void Awake() => 
            _collider.isTrigger = true;

        private void OnTriggerEnter(Collider other) => 
            OnAreaEnter?.Invoke(other);
    }
}