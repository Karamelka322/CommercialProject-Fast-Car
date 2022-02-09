using CodeBase.Data.Perseistent;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private PlayerSessionData _playerSessionData;

        public void Construct(PlayerSessionData playerSessionData) => 
            _playerSessionData = playerSessionData;

        public void AddHealth(float value) => 
            _playerSessionData.AddHealth(value);

        public void ReduceHealth(float value) => 
            _playerSessionData.ReduceHealth(value);
    }
}