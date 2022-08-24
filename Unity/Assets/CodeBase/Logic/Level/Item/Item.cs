using System;
using UnityEngine;

namespace CodeBase.Logic.Item
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