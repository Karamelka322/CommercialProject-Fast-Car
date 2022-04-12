using System;
using CodeBase.Data.Perseistent;
using CodeBase.Data.Static.Player;
using CodeBase.Mediator;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.UI.Buttons;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Logic
{
    public class SwitchPlayerCar : MonoBehaviour, ISingleReadData, ISingleWriteData
    {
        [SerializeField] 
        private ButtonWrapper _closeButton;
        
        [SerializeField] 
        private ButtonWrapper _switchLeft;
        
        [SerializeField] 
        private ButtonWrapper _switchRight;

        [SerializeField] 
        private ButtonWrapper _select;
        
        private PlayerTypeId _currentPlayerType;
        private PlayerTypeId _selectedPlayerType;
        
        private IMenuMediator _mediator;

        [Inject]
        public void Constuct(IMenuMediator mediator)
        {
            _mediator = mediator;
        }

        public void Start()
        {
            _switchLeft.OnClick += OnSwitchLeft;
            _switchRight.OnClick += OnSwitchRight;
            _select.OnClick += OnSelect;
            _closeButton.OnClick += OnClickCloseButton;
        }

        public void OnDestroy()
        {
            _switchLeft.OnClick -= OnSwitchLeft;
            _switchRight.OnClick -= OnSwitchRight;
            _select.OnClick -= OnSelect;
            _closeButton.OnClick -= OnClickCloseButton;
        }

        public void SingleReadData(PlayerPersistentData persistentData)
        {
            _currentPlayerType = persistentData.ProgressData.CurrentPlayer;
            _selectedPlayerType = persistentData.ProgressData.CurrentPlayer;
            
            HideAndShowButtons();
        }

        public void SingleWriteData(PlayerPersistentData persistentData) => 
            persistentData.ProgressData.CurrentPlayer = _currentPlayerType;

        private void OnSwitchLeft()
        {
            if (IsSwitchLeft())
            {
                _selectedPlayerType--;

                SwitchCar(_selectedPlayerType);
                HideAndShowButtons();
            }
        }

        private void OnSwitchRight()
        {
            if (IsSwitchRight())
            {
                _selectedPlayerType++;

                SwitchCar(_selectedPlayerType);
                HideAndShowButtons();
            }
        }

        private void OnClickCloseButton()
        {
            if (_currentPlayerType != _selectedPlayerType) 
                SwitchCar(_currentPlayerType);
        }

        private void OnSelect()
        {
            _currentPlayerType = _selectedPlayerType;
            _select.Disable();;
        }

        private void SwitchCar(PlayerTypeId playerTypeId) => 
            _mediator.ChangePlayerCar(playerTypeId);

        private void HideAndShowButtons()
        {
            HideAndShowSelectButton();
            HideAndShowSwitchLeftButton();
            HideAndShowSwitchRightButton();
        }

        private bool IsSwitchLeft() => 
            _selectedPlayerType != 0;

        private bool IsSwitchRight() => 
            _selectedPlayerType != (PlayerTypeId)Enum.GetNames(typeof(PlayerTypeId)).Length - 1;

        private void HideAndShowSwitchLeftButton()
        {
            if (_selectedPlayerType == 0)
                _switchLeft.Disable();
            else
                _switchLeft.Enable();
        }

        private void HideAndShowSwitchRightButton()
        {
            if (_selectedPlayerType == (PlayerTypeId)Enum.GetNames(typeof(PlayerTypeId)).Length - 1)
                _switchRight.Disable();
            else
                _switchRight.Enable();
        }

        private void HideAndShowSelectButton()
        {
            if (_currentPlayerType == _selectedPlayerType)
                _select.Disable();
            else
                _select.Enable();
        }
    }
}