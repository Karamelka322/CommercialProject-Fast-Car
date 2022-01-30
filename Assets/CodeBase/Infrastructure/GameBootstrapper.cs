using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICorutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            DontDestroyOnLoad(this);
        }
    }
}