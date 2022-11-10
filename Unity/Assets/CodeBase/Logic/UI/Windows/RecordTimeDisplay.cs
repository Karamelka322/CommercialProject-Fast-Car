using CodeBase.Extension;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows
{
    public class RecordTimeDisplay : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        private IPersistentDataService _persistentDataService;
        private ILevelMediator _levelMediator;

        [Inject]
        private void Construct(IPersistentDataService persistentDataService, ILevelMediator levelMediator)
        {
            _levelMediator = levelMediator;
            _persistentDataService = persistentDataService;
        }

        private void Awake() => 
            Display();

        private void Display()
        {
            float recordTime = _persistentDataService.PlayerData.ProgressData.RecordTime;
            float stopwatchTime = _levelMediator.StopwatchTime();
            
            _text.text = "Record - " + (recordTime > stopwatchTime ? recordTime.ConvertToDateTime() : stopwatchTime.ConvertToDateTime());
        }
    }
}