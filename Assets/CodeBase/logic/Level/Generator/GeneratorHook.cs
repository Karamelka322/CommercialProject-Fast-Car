using System;
using CodeBase.Logic.Item;
using CodeBase.Logic.World;
using CodeBase.Services.Tween;
using UnityEngine;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorHook : MonoBehaviour
    {
        [SerializeField] 
        private Area _capturArea;

        [SerializeField] 
        private Point _capturPoin;

        public event Action<Capsule> OnCapsuleLift;
        private ITweenService _tweenService;
        
        public void Construct(ITweenService tweenService) => 
            _tweenService = tweenService;

        private void OnEnable() => 
            _capturArea.OnAreaEnter += OnCapturAreaEnter;

        private void OnDisable() => 
            _capturArea.OnAreaEnter -= OnCapturAreaEnter;

        private void OnCapturAreaEnter(Collider collider)
        {
            if (IsCapsule(collider, out Capsule capsule)) 
                LiftCapsule(capsule);
        }
        
        private static bool IsCapsule(Collider collider, out Capsule capsule) => 
            collider.TryGetComponent(out capsule);

        private void LiftCapsule(Capsule capsule)
        {
            capsule.transform.parent = null;
            
            _tweenService.Move<Capsule>(capsule.transform, _capturPoin.WorldPosition);
            OnCapsuleLift?.Invoke(capsule);
            
            Destroy(capsule.gameObject, 1f);
        }
    }
}