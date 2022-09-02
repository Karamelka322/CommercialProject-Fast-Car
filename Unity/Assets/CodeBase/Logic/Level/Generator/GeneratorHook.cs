using System;
using CodeBase.Logic.Item;
using CodeBase.Logic.World;
using UnityEngine;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorHook : MonoBehaviour
    {
        [SerializeField] 
        private Area _captureArea;

        [SerializeField] 
        private Point _capturePoint;

        public event Action<Energy> OnEnergyLift;
        
        private void OnEnable() => 
            _captureArea.OnAreaEnter += OnCaptureAreaEnter;

        private void OnDisable() => 
            _captureArea.OnAreaEnter -= OnCaptureAreaEnter;

        private void OnCaptureAreaEnter(Collider collider)
        {
            if (IsEnergy(collider, out Energy energy)) 
                LiftEnergy(energy);
        }
        
        private static bool IsEnergy(Collider collider, out Energy energy) => 
            collider.TryGetComponent(out energy);
        
        private void LiftEnergy(Energy energy)
        {
            energy.Raise(_capturePoint.transform);
            
            OnEnergyLift?.Invoke(energy);
            
            Destroy(energy.gameObject, 1f);
        }
    }
}