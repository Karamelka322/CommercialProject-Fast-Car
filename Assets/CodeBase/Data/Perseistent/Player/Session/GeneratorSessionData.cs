using System;

namespace CodeBase.Data.Perseistent
{
    [Serializable]
    public class GeneratorSessionData
    {
        public const float MaxPower = 100;
        public const float MinPower = 0;

        public float Power;
        public float PowerSpeedChange;
    }
}