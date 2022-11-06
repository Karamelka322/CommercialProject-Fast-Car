using CodeBase.Extension;
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

        [Inject]
        private void Construct(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        private void Awake() => 
            Display();

        private void Display()
        {
            float recordTime = _persistentDataService.PlayerData.ProgressData.RecordTime;
            float stopwatchTime = _persistentDataService.PlayerData.SessionData.LevelData.StopwatchTime;
            
            _text.text = "Record - " + (recordTime > stopwatchTime ? recordTime.ConvertToDateTime() : stopwatchTime.ConvertToDateTime());
        }
    }
}