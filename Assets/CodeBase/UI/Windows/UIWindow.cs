using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public abstract class UIWindow : MonoBehaviour
    {
        [SerializeField] 
        private ButtonWrapper _closeButton;

        private void Start() =>
            OnOpen();

        private void OnEnable() => 
            _closeButton.OnClick += OnClose;

        private void OnDisable() => 
            _closeButton.OnClick -= OnClose;

        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }
        public virtual void Show() { }
        public virtual void Unshow() { }
    }
}