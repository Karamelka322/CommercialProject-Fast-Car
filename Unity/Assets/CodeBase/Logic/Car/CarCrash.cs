using UnityEngine;

namespace CodeBase.Logic.Car
{
    public abstract class CarCrash : MonoBehaviour
    {
        protected const string Ground = "Ground";

        public bool Crash { get; protected set; }

        protected virtual void OnTriggerEnter(Collider other) { }
        protected virtual void OnTriggerExit(Collider other) { }
    }
}