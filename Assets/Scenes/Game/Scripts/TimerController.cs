using System;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timerText;

    private bool _timerStarted;
    private float _currentTime;

    private Action _timerEndedCallback;

    public void Init(float maxTime, Action timerEndedCallback)
    {
        _timerStarted = false;
        _currentTime = maxTime;
        _timerEndedCallback = timerEndedCallback;
    }

    private void Update()
    {
        if (!_timerStarted)
        {
            return;
        }

        if (_currentTime <= 0)
        {
            _timerStarted = false;
            _timerEndedCallback.Invoke();
        }

        _currentTime -= Time.deltaTime;

        string minutes = ((int)_currentTime / 60).ToString("00");
        string seconds = (_currentTime % 60).ToString("00");

        _timerText.text = minutes + ":" + seconds;
    }

    public void StartTimer()
    {
        _timerStarted = true;
    }
}
