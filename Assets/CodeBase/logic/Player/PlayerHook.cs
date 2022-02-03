using UnityEngine;

namespace CodeBase.logic.Player
{
    public class PlayerHook : MonoBehaviour
    {
        [SerializeField]
        private Area _captureArea;

        [SerializeField] 
        private Point _captureCenter;

        private void OnEnable() => 
            _captureArea.OnAreaEnter += OnAreaEnter;

        private void OnDisable() => 
            _captureArea.OnAreaEnter -= OnAreaEnter;

        private void OnAreaEnter(Collider collider)
        {
            Debug.Log(collider.name);
            
            if (IsItem(collider, out Item item)) 
                LiftItem(item);
        }

        private static bool IsItem(Collider collider, out Item item) => 
            collider.TryGetComponent(out item);

        private void LiftItem(Item item)
        {
            item.Raised = true;
            
            item.transform.parent = _captureCenter.transform;
            item.transform.localPosition = Vector3.zero;
        }
    }
}