using System;
using UnityEngine;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class GeneratorSessionData
    {
        public const float MaxPower = 100;
        public const float MinPower = 0;

        public float Power;
        public float PowerSpeedChange;

        public Action<float> ChangePower;

        public void AddPower(float value)
        {
            Power = Mathf.Clamp(Power + value, MinPower, MaxPower);
            ChangePower?.Invoke(Power);
        }

        public void ReducePower(float value)
        {
            Power = Mathf.Clamp(Power - value, MinPower, MaxPower);
            ChangePower?.Invoke(Power);
        }
    }
}