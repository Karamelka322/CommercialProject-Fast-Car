using CodeBase.Data.Perseistent;
using CodeBase.Extension;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Replay;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class Stopwatch : MonoBehaviour, IStreamingReadData, IReplayHandler
    {
        [SerializeField] 
        private TextMeshProUGUI _textMeshPro;

        public void StreamingReadData(PlayerPersistentData persistentData) => 
            _textMeshPro.text = persistentData.SessionData.StopwatchTime.ConvertToDateTime();

        public void OnReplay() => 
            _textMeshPro.text = "00:00:00";
    }
}