using CodeBase.Extension;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows
{
    public class SessionTimeDisplay : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        private ILevelMediator _levelMediator;

        [Inject]
        private void Construct(ILevelMediator levelMediator)
        {
            _levelMediator = levelMediator;
        }

        private void Awake() => 
            Display();

        private void Display() => 
            _text.text = "Time - " + _levelMediator.StopwatchTime().ConvertToDateTime();
    }
}