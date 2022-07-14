using System;
using CodeBase.Data.Static;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class InputSettingsPersistentData
    {
        public InputTypeId InputType;
        
        private InputTypeId _inputTypeDefault = InputTypeId.Buttons;
        
        public InputSettingsPersistentData()
        {
            InputType = _inputTypeDefault;
        }
    }
}