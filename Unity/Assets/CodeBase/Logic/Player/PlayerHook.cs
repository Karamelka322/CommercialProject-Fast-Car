using System;
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

        public Action EnergyCapture;
        
        private Energy _energy;

        private void OnEnable() => 
            _captureArea.OnAreaEnter += OnAreaEnter;

        private void OnDisable() => 
            _captureArea.OnAreaEnter -= OnAreaEnter;

        private void OnAreaEnter(Collider collider)
        {
            if (IsEnergyCatch(collider))
            {
                _energy.Raise(_captureCenter.transform);
                EnergyCapture?.Invoke();
            }
        }

        private bool IsEnergyCatch(Collider collider) => 
            _energy == null && collider.TryGetComponent(out _energy);
    }
}