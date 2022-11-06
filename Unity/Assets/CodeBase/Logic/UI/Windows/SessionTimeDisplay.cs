using CodeBase.Extension;
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

        private IPersistentDataService _persistentDataService;

        [Inject]
        private void Construct(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        private void Awake() => 
            Display();

        private void Display() => 
            _text.text = "Time - " + _persistentDataService.PlayerData.SessionData.LevelData.StopwatchTime.ConvertToDateTime();
    }
}