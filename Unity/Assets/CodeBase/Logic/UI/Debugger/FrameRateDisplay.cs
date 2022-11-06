using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Debugger
{
    public class FrameRateDisplay : MonoBehaviour
    {
        [SerializeField] private Text _realtimeFrameRate;

        private static float RealtimeFrameRate => 1f / Time.deltaTime;
    
        private const int _speedFrequency = 3;
        private int _frameRate;

        private void Awake() => 
            DontDestroyOnLoad(this);

        private void Update() => 
            OutputRealtimeFrameRate();

        private void OutputRealtimeFrameRate()
        {
            _frameRate = (int)Mathf.Lerp(_frameRate, RealtimeFrameRate, Time.unscaledDeltaTime * _speedFrequency);
            _realtimeFrameRate.text = $"FPS: {_frameRate.ToString()}";
        }
    }
}