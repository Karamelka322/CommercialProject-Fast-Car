using CodeBase.Services.Tween;
using UnityEngine;

namespace CodeBase.logic.Player
{
    public class PlayerHook : MonoBehaviour
    {
        [SerializeField]
        private Area _captureArea;

        [SerializeField] 
        private Point _captureCenter;

        private ITweenService _tweenService;
        private Item _item;

        public void Construct(ITweenService tweenService) => 
            _tweenService = tweenService;

        private void OnEnable() => 
            _captureArea.OnAreaEnter += OnAreaEnter;

        private void OnDisable() => 
            _captureArea.OnAreaEnter -= OnAreaEnter;

        private void OnAreaEnter(Collider collider)
        {
            if(_item != null)
                return;
            
            if (IsItem(collider, out _item)) 
                LiftItem(_item);
        }

        private static bool IsItem(Collider collider, out Item item) => 
            collider.TryGetComponent(out item);

        private void LiftItem(Item item)
        {
            item.Raised = true;

            _item.transform.parent = _captureCenter.transform;
            _tweenService.Move<Capsule>(item.transform, Vector3.zero, speed: 1, TweenMode.Local);
        }
    }
}