using System;
using CodeBase.Data.Static.Player;
using CodeBase.Mediator;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Buttons;
using UnityEngine;

namespace CodeBase.UI.Logic
{
    public class SwitchPlayerCar : MonoBehaviour
    {
        [SerializeField] 
        private ButtonWrapper _black;
        
        [SerializeField] 
        private ButtonWrapper _switchLeft;
        
        [SerializeField] 
        private ButtonWrapper _switchRight;

        [SerializeField] 
        private ButtonWrapper _select;
        
        private IPersistentDataService _persistentDataService;
        private PlayerTypeId _playerType;
        private IMediator _mediator;

        private PlayerTypeId CurrentPlayerType
        {
            get => _persistentDataService.PlayerData.ProgressData.CurrentPlayer;
            set => _persistentDataService.PlayerData.ProgressData.CurrentPlayer = value;
        }

        public void Constuct(IPersistentDataService persistentDataService, IMediator mediator)
        {
            _persistentDataService = persistentDataService;
            _mediator = mediator;
            _playerType = CurrentPlayerType;
            
            HideAndShowButtons();
        }

        public void Start()
        {
            _switchLeft.OnClick += OnSwitchLeft;
            _switchRight.OnClick += OnSwitchRight;
            _select.OnClick += OnSelect;
            _black.OnClick += OnClickBlack;
        }

        public void OnDestroy()
        {
            _switchLeft.OnClick -= OnSwitchLeft;
            _switchRight.OnClick -= OnSwitchRight;
            _select.OnClick -= OnSelect;
            _black.OnClick -= OnClickBlack;
        }

        private void OnSwitchLeft()
        {
            if (IsSwitchLeft())
            {
                _playerType--;

                SwitchCar(_playerType);
                HideAndShowButtons();
            }
        }

        private void OnSwitchRight()
        {
            if (IsSwitchRight())
            {
                _playerType++;

                SwitchCar(_playerType);
                HideAndShowButtons();
            }
        }

        private void OnClickBlack()
        {
            if (IsHideSelectButton() == false) 
                SwitchCar(CurrentPlayerType);
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
            _playerType != 0;

        private bool IsHideSwitchLeftButton() => 
            _playerType == 0;

        private void HideSwitchLeftButton() => 
            _switchLeft.Disable();

        private void ShowSwitchLeftButton() => 
            _switchLeft.Enable();

        private void HideAndShowSwitchLeftButton()
        {
            if (IsHideSwitchLeftButton())
                HideSwitchLeftButton();
            else
                ShowSwitchLeftButton();
        }


        private bool IsSwitchRight() => 
            _playerType != (PlayerTypeId)Enum.GetNames(typeof(PlayerTypeId)).Length - 1;

        private bool IsHideSwitchRightButton() => 
            _playerType == (PlayerTypeId)Enum.GetNames(typeof(PlayerTypeId)).Length - 1;

        private void HideSwitchRightButton() => 
            _switchRight.Disable();

        private void ShowSwitchRightButton() => 
            _switchRight.Enable();

        private void HideAndShowSwitchRightButton()
        {
            if (IsHideSwitchRightButton())
                HideSwitchRightButton();
            else
                ShowSwitchRightButton();
        }


        private void OnSelect()
        {
            CurrentPlayerType = _playerType;
            HideSelectButton();
        }

        private bool IsHideSelectButton() => 
            CurrentPlayerType == _playerType;

        private void HideSelectButton() => 
            _select.Disable();

        private void ShowSelectButton() => 
            _select.Enable();

        private void HideAndShowSelectButton()
        {
            if (IsHideSelectButton())
                HideSelectButton();
            else
                ShowSelectButton();
        }
    }
}