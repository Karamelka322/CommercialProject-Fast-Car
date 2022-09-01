using CodeBase.Logic.Item;
using CodeBase.Logic.World;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerHook : MonoBehaviour
    {
        [SerializeField]
        private Area _captureArea;

        [SerializeField] 
        private Point _captureCenter;

        private Energy _energy;

        private void OnEnable() => 
            _captureArea.OnAreaEnter += OnAreaEnter;

        private void OnDisable() => 
            _captureArea.OnAreaEnter -= OnAreaEnter;

        private void OnAreaEnter(Collider collider)
        {
            if (_energy == null && IsEnergy(collider, out _energy)) 
                _energy.Raise(_captureCenter.transform);
        }

        private static bool IsEnergy(Collider collider, out Energy energy) => 
            collider.TryGetComponent(out energy);
    }
}