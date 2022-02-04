using System;
using UnityEngine;

namespace CodeBase.logic.Player
{
    public abstract class Item : MonoBehaviour
    {
        public event Action OnLifting;
        
        private bool _raised;

        public bool Raised
        {
            get => _raised;
            
            set
            {
                _raised = value;
                
                if(value)
                {
                    OnLifting?.Invoke();
                }
            }
        }
    }
}