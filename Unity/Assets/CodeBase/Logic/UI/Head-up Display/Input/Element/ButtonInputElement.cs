using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Services.Input.Element
{
    public class ButtonInputElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [ReadOnly]
        public bool Click;

        public event Action Enabled;
        public event Action Disabled;

        public void OnPointerDown(PointerEventData eventData)
        {
            Enabled?.Invoke();
            Click = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Disabled?.Invoke();
            Click = false;
        }

        private void OnDisable()
        {
            if(Click)
                Disabled?.Invoke();
            
            Click = false;
        }
    }
}