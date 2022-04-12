using System;
using UnityEngine;

namespace CodeBase.Logic.Menu
{
    public class MenuStates : MonoBehaviour
    {
        private MenuState _currentState;
        
        public event Action<MenuState> OnChangeState;

        public MenuState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnChangeState?.Invoke(value);
            }
        }
    }
}