using CodeBase.Extension;
using CodeBase.Services.Replay;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class StopwatchDisplay : MonoBehaviour, IReplayHandler
    {
        [SerializeField] 
        private Text _text;

        public float Time { get; private set; }
        
        public void Show(float time)
        {
            Time = time;
            _text.text = Time.ConvertToDateTime();
        }

        public void OnReplay() => 
            _text.text = "00:00:00";
    }
}