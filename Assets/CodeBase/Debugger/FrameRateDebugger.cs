using UnityEngine;
using TMPro;

namespace CodeBase.Debugger
{
    public class FrameRateDebugger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _realtimeFrameRate;
        [SerializeField] private TextMeshProUGUI _maxFrameRate;
        [SerializeField] private TextMeshProUGUI _minFrameRate;

        private static float RealtimeFrameRate => 1f / Time.deltaTime;
    
        private int _maxFrameRateCounter = int.MinValue;
        private int _minFrameRateCounter = int.MaxValue;

        private const int _speedFrequency = 7;
        private int _farameRate;

        private void Update()
        {
            int frameRate = (int)RealtimeFrameRate;

            if (_maxFrameRateCounter < frameRate) 
                OutputMaxFrameRate(frameRate);

            if (_minFrameRateCounter > frameRate)
                OutputMinFrameRate(frameRate);

            OutputRealtimeFrameRate(frameRate);
        }

        private void OutputMaxFrameRate(int maxFrameRate)
        {
            _maxFrameRateCounter = maxFrameRate;
            _maxFrameRate.text = $"Max: {_maxFrameRateCounter.ToString()}";
        }

        private void OutputMinFrameRate(int minFrameRate)
        {
            _minFrameRateCounter = minFrameRate;
            _minFrameRate.text = $"Min: {_minFrameRateCounter.ToString()}";
        }

        private void OutputRealtimeFrameRate(int frameRate)
        {
            _farameRate = (int)Mathf.Lerp(_farameRate, frameRate, Time.deltaTime * _speedFrequency);
            _realtimeFrameRate.text = $"Frame Rate: {_farameRate.ToString()}";
        }
    }
}