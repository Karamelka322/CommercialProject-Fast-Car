using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Toggles
{
    public class UIToggle : MonoBehaviour, IPointerDownHandler
    {
        public virtual bool IsOn { get; set; }

        public void OnPointerDown(PointerEventData eventData) =>
            IsOn = !IsOn;
    }
}