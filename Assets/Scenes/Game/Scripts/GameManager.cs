using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Init,
    Start,
    Play,
    End,
    Result,
}

public class GameManager : SingletonBehaviourSceneOnly<GameManager>
{
    [SerializeField]
    private TimerController _timerController;

    private GameState _gameState;
    public GameState GameState => _gameState;

    public void Init()
    {
        _gameState = GameState.Init;

        // 仮の時間
        _timerController.Init(10, GameEnd);

        GameStart();
    }

    private void GameStart()
    {
        _gameState = GameState.Start;

        _timerController.StartTimer();
    }

    private void GamePlay()
    {
        _gameState = GameState.Play;
    }

    private void GameEnd()
    {
        _gameState = GameState.End;

        Debug.Log("GameEnd");
    }

    private void Result()
    {
        _gameState = GameState.Result;
    }
}
