using TMPro;
using UnityEngine;

public class FrameRateDebugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _realtimeFrameRate;
    [SerializeField] private TextMeshProUGUI _maxFrameRate;
    [SerializeField] private TextMeshProUGUI _minFrameRate;

    private static int RealtimeFrameRate => (int)(1f / Time.deltaTime);
    
    private float _maxFrameRateCounter = float.MinValue;
    private float _minFrameRateCounter = float.MaxValue;

    private void Update()
    {
        float frameRate = RealtimeFrameRate;

        if (_maxFrameRateCounter < frameRate) 
            UpdateMaxFrameRate(frameRate);

        if (_minFrameRateCounter > frameRate)
            UpdateMinFrameRate(frameRate);

        OutputRealtimeFrameRate(frameRate);
    }

    private void UpdateMaxFrameRate(float frameRate)
    {
        _maxFrameRateCounter = frameRate;
        _maxFrameRate.text = $"Max: {_maxFrameRateCounter.ToString()}";
    }

    private void UpdateMinFrameRate(float frameRate)
    {
        _minFrameRateCounter = frameRate;
        _minFrameRate.text = $"Mix: {_minFrameRateCounter.ToString()}";
    }

    private void OutputRealtimeFrameRate(float frameRate) => 
        _realtimeFrameRate.text = $"Frame Rate: {frameRate.ToString()}";
}