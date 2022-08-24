using TMPro;
using UnityEngine;

namespace CodeBase.UI.Input
{
    public class AbilityBar : UIBar
    {
        [SerializeField] 
        private TextMeshProUGUI _textCounter;

        public override float Value
        {
            get => _image.fillAmount;
            
            set
            {
                _textCounter.text = $"{(int) (value * 100)}%";
                _image.fillAmount = value;
            }
        }
    }
}