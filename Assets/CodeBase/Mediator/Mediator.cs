using CodeBase.Data.Static.Player;
using CodeBase.Logic.Menu;
using CodeBase.Scene.Menu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Mediator
{
    internal class Mediator : MonoBehaviour, IMediator
    {
        [Required, SceneObjectsOnly, SerializeField] private MenuUIViewer _menuUIViewer; public MenuUIViewer MenuUIViewer => _menuUIViewer;
        [Required, SceneObjectsOnly, SerializeField] private MenuAnimator _menuAnimator; public MenuAnimator MenuAnimator => _menuAnimator;
        [Required, SceneObjectsOnly, SerializeField] private Garage _garage; public Garage Garage => _garage;
        
        public void ChangePlayerCar(PlayerTypeId playerTypeId) => _garage.ChangePlayerCar(playerTypeId);
        public void StartPlayAnimator(bool isFirstPlay) => _menuAnimator.StartPlayAnimator(isFirstPlay);

        [PropertySpace]
        
        [Button] public void RebindMenuAnimator() => _menuAnimator.Rebind();
        [Button] public void PlayCloseMenu() => _menuAnimator.PlayCloseMenu();
        [Button] public void PlayOpenSettings() => _menuAnimator.PlayOpenSettings();
        [Button] public void PlayOpenGarage() => _menuAnimator.PlayOpenGarage();
    }

    public interface IMediator
    {
        MenuUIViewer MenuUIViewer { get; }
        MenuAnimator MenuAnimator { get; }
        Garage Garage { get; }
        void ChangePlayerCar(PlayerTypeId playerTypeId);
        void RebindMenuAnimator();
        void StartPlayAnimator(bool isFirstPlay);
        void PlayCloseMenu();
        void PlayOpenSettings();
        void PlayOpenGarage();
    }
}