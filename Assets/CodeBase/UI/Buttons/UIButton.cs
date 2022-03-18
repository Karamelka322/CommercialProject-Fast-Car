using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] 
        private Button _button;

        protected virtual void Awake() => 
            _button.onClick.AddListener(OnClickButton);

        protected abstract void OnClickButton();
    }
}