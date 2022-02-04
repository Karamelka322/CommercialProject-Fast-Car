using CodeBase.logic.Player;
using CodeBase.Services.Tween;
using UnityEngine;

namespace CodeBase.logic
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] 
        private Area _capturArea;

        [SerializeField] 
        private Point _capturePoin;

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
            _tweenService.Move<Capsule>(capsule.transform, _capturePoin.WorldPosition);
            Destroy(capsule.gameObject, 1f);
        }
    }
}