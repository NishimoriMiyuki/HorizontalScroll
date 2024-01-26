using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : SingletonBehaviourSceneOnly<GameUIManager>
{
    [SerializeField]
    private CatDegree _catDegree;

    [SerializeField]
    private TimerController _timerController;

    [SerializeField]
    private RectTransform _resultCanvas;

    [SerializeField]
    private ResultView _resultView;

    [SerializeField]
    private Button _titleButton;

    private int _currentDragNumber;

    private Stage _stage;

    public void Init(Stage stage)
    {
        _resultCanvas.gameObject.SetActive(false);

        _stage = stage;

        _currentDragNumber = 0;
        _catDegree.Init(stage.required_number_scratches);
        _timerController.Init(stage.max_time);

        _titleButton.onClick.AddListener(OnClickTitleButton);
    }

    public void DragCount()
    {
        if (GameManager.Instance.GameState != GameState.Play)
        {
            return;
        }

        _currentDragNumber++;
        CatDegreeUpdate();
    }

    public void StartTimer()
    {
        _timerController.StartTimer();
    }

    public void StopTimer()
    {
        _timerController.StopTimer();
    }

    public void OpenResultView(int bonus, string address)
    {
        _resultCanvas.gameObject.SetActive(true);
        _resultView.SetButtonImage(address);

        // ゲームオーバー時はタイムボーナスが入らない
        if (bonus == 0)
        {
            _resultView.SetScore(0, _currentDragNumber, bonus).Forget();
            return;
        }
        _resultView.SetScore(_timerController.RestTime, _currentDragNumber, bonus).Forget();
    }

    private void CatDegreeUpdate()
    {
        _catDegree.SliderUpdate(_currentDragNumber);

        if (_currentDragNumber == _stage.required_number_scratches)
        {
            GameManager.Instance.GameClear();
        }
    }

    private void OnClickTitleButton()
    {
        GameManager.Instance.CancellationTokenSource.Cancel();
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Title, fadeType: FadeType.ColorWhite);
    }
}
