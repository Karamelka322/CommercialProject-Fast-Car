using CodeBase.Data.Perseistent;
using CodeBase.Data.Static;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Toggles;
using UnityEngine;

namespace CodeBase.UI
{
    public class InputSettings : MonoBehaviour, ISingleWriteData
    {
        [SerializeField] 
        private InputSettingToggle _joystick;
        
        [SerializeField]
        private InputSettingToggle _buttons;
        
        [SerializeField]
        private InputSettingToggle _areas;

        private InputTypeId CurrentInput;

        public void Construct(IPersistentDataService persistentDataService) => 
            SwitchInputSetting(persistentDataService.PlayerData.SettingsData.InputType);

        public void SingleWriteData(PlayerPersistentData persistentData) => 
            persistentData.SettingsData.InputType = CurrentInput;

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
            CurrentInput = typeId;

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