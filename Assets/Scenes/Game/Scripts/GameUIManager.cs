using UnityEngine;

public class GameUIManager : SingletonBehaviourSceneOnly<GameUIManager>
{
    [SerializeField]
    private CatDegree _catDegree;

    [SerializeField]
    private TimerController _timerController;

    private int _currentDragNumber;

    public void Init(Stage stage)
    {
        _currentDragNumber = 0;
        _catDegree.Init(stage.required_number_scratches);
        _timerController.Init(stage.max_time);
    }

    public void DragCount()
    {
        _currentDragNumber++;
        _catDegree.SliderUpdate(_currentDragNumber);
    }

    public void StartTimer()
    {
        _timerController.StartTimer();
    }
}
