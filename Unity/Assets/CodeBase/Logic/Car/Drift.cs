using System;

namespace CodeBase.Logic.Car
{
    [Serializable]
    public class Drift
    {
        private readonly CarProperty _property;

        public Drift(CarProperty property)
        {
            _property = property;
        }
    }
}