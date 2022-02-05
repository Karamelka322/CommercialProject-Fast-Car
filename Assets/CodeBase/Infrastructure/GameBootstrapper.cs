using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICorutineRunner, IUpdatable
    {
        private Game _game;

        public event Action OnUpdate;

        private void Awake()
        {
            _game = new Game(this, this);
            DontDestroyOnLoad(this);
        }

        private void Update() => 
            OnUpdate?.Invoke();
    }
}