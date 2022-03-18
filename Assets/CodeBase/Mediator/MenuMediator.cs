using CodeBase.Data.Static.Player;
using CodeBase.Logic.Menu;
using CodeBase.Scene.Menu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Mediator
{
    internal class MenuMediator : MonoBehaviour, IMediator
    {
        [Required, SceneObjectsOnly, SerializeField] private MenuStates _menuStates;
        [Required, SceneObjectsOnly, SerializeField] private MenuUIViewer _menuUIViewer; public MenuUIViewer MenuUIViewer => _menuUIViewer;
        [Required, SceneObjectsOnly, SerializeField] private MenuAnimator _menuAnimator; public MenuAnimator MenuAnimator => _menuAnimator;
        [Required, SceneObjectsOnly, SerializeField] private Garage _garage; public Garage Garage => _garage;
        
        public void ChangePlayerCar(PlayerTypeId playerTypeId) => _garage.ChangePlayerCar(playerTypeId);
        public void ChangeMenuState(MenuState state) => _menuStates.CurrentState = state;
    }

    public interface IMediator
    {
        MenuUIViewer MenuUIViewer { get; }
        MenuAnimator MenuAnimator { get; }
        Garage Garage { get; }
        void ChangePlayerCar(PlayerTypeId playerTypeId);
        void ChangeMenuState(MenuState state);
    }
}