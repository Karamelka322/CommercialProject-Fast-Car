using System;
using CodeBase.Data.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Toggles
{
    public class InputSettingToggle : UIToggle
    {
        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private InputTypeId _type;
        
        public Action<InputTypeId> OnSelectToggle;
        
        private bool _isOn;
        public override bool IsOn
        {
            get => _isOn;
            set => Switch(value);
        }

        private void Switch(bool value)
        {
            if(_isOn == value)
                return;
            
            _isOn = value;
            
            if(value)
            {
                _image.color = Color.yellow;
                OnSelectToggle?.Invoke(_type);
            }
            else
            {
                _image.color = Color.white;
            }
        }
    }
}