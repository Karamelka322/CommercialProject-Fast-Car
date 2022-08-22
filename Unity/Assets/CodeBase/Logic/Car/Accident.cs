using System;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Collider))]
    public class Accident : MonoBehaviour
    {
        private const string Ground = "Ground";
        
        public event Action Start;
        public event Action Stop;

        public bool Crash;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer(Ground))
            {
                Crash = true;
                Start?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer(Ground))
            {
                Crash = false;
                Stop?.Invoke();
            }
        }
    }
}