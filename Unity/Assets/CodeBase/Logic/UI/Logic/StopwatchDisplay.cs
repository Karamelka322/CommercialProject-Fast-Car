using CodeBase.Data.Perseistent;
using CodeBase.Extension;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Replay;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class StopwatchDisplay : MonoBehaviour, IStreamingReadData, IReplayHandler
    {
        [SerializeField] 
        private Text _text;

        public void StreamingReadData(PlayerPersistentData persistentData) => 
            _text.text = persistentData.SessionData.LevelData.StopwatchTime.ConvertToDateTime();

        public void OnReplay() => 
            _text.text = "00:00:00";
    }
}