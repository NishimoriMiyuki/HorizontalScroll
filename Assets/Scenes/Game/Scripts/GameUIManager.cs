using UnityEngine;

public class GameUIManager : SingletonBehaviourSceneOnly<GameUIManager>
{
    [SerializeField]
    private CatDegree _catDegree;

    [SerializeField]
    private TimerController _timerController;

    private int _currentDragNumber;

    private Stage _stage;

    public void Init(Stage stage)
    {
        _stage = stage;

        _currentDragNumber = 0;
        _catDegree.Init(stage.required_number_scratches);
        _timerController.Init(stage.max_time);
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

    private void CatDegreeUpdate()
    {
        _catDegree.SliderUpdate(_currentDragNumber);

        if (_currentDragNumber == _stage.required_number_scratches)
        {
            GameManager.Instance.GameClear();
        }
    }
}
