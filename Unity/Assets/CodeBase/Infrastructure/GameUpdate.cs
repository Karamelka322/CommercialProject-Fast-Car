using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameUpdate : MonoBehaviour, IUpdatable, ICoroutineRunner
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;

        private void Awake() => 
            DontDestroyOnLoad(this);

        private void Update() => 
            OnUpdate?.Invoke();

        private void FixedUpdate() => 
            OnFixedUpdate?.Invoke();
    }
}