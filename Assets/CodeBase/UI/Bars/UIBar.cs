using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(Image))]
    public abstract class UIBar : MonoBehaviour
    {
        [SerializeField] 
        private Image _image;

        public float Fill
        {
            get => _image.fillAmount;
            set => _image.fillAmount = value;
        }
    }
}