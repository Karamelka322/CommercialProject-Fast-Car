using System;
using CodeBase.Data.Perseistent;
using CodeBase.Data.Static;
using CodeBase.UI.Toggles;
using UnityEngine;

namespace CodeBase.UI
{
    public class InputSettings : MonoBehaviour
    {
        [SerializeField] 
        private InputSettingToggle _joystick;
        
        [SerializeField]
        private InputSettingToggle _buttons;
        
        [SerializeField]
        private InputSettingToggle _areas;

        private SettingsPersistentData _settingsData;

        public void Construct(SettingsPersistentData settingsData) => 
            _settingsData = settingsData;

        private void Start() => 
            SwitchInputSetting(_settingsData.InputType);

        private void OnEnable()
        {
            _joystick.OnSelectToggle += SwitchInputSetting;
            _buttons.OnSelectToggle += SwitchInputSetting;
            _areas.OnSelectToggle += SwitchInputSetting;
        }

        private void OnDisable()
        {
            _joystick.OnSelectToggle -= SwitchInputSetting;
            _buttons.OnSelectToggle -= SwitchInputSetting;
            _areas.OnSelectToggle -= SwitchInputSetting;
        }

        private void SwitchInputSetting(InputTypeId typeId)
        {
            _settingsData.InputType = typeId;

            if (typeId == InputTypeId.Joystick)
            {
                _joystick.IsOn = true;
                _buttons.IsOn = false;
                _areas.IsOn = false;
            }
            else if (typeId == InputTypeId.Buttons)
            {
                _joystick.IsOn = false;
                _buttons.IsOn = true;
                _areas.IsOn = false;
            }
            else if(typeId == InputTypeId.Areas)
            {
                _joystick.IsOn = false;
                _buttons.IsOn = false;
                _areas.IsOn = true;
            }
        }
    }
}