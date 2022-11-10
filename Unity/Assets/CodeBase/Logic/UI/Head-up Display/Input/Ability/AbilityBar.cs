using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Input
{
    public class AbilityBar : UIBar
    {
        [SerializeField] 
        private Text _counter;

        public override float Value
        {
            get => _image.fillAmount;
            
            set
            {
                value = Mathf.Clamp01(value);
                _counter.text = $"{(int) (value * 100)}%";
                _image.fillAmount = value;
            }
        }
    }
}