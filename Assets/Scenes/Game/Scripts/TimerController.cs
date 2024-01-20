using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timerText;

    private bool _timerStarted;
    private float _currentTime;

    public void Init(float maxTime)
    {
        _timerStarted = false;
        _currentTime = maxTime;
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
            GameManager.Instance.GameOver();
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

    public void StopTimer()
    {
        _timerStarted = false;
    }
}
