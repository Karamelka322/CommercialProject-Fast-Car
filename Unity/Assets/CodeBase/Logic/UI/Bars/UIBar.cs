using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(Image))]
    public abstract class UIBar : MonoBehaviour
    {
        [SerializeField] 
        protected Image _image;

        public abstract float Value { get; set; }
    }
}