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

    public enum MenuState
    {
        Intro = 0,
        MainMenu = 1,
        Garage = 2,
        Settings = 3,
        PlayGame = 4,
    }
}