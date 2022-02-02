using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Services.Input.Element
{
    public class AreaInputElement : MonoBehaviour, IAreaInputElement, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] 
        private string _id;
        
        public string Id => _id;
        public bool Value { get; private set; }

        public void OnPointerDown(PointerEventData eventData) => 
            Value = true;

        public void OnPointerUp(PointerEventData eventData) => 
            Value = false;
    }
}