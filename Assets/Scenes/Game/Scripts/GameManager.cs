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

public class GameManager : MonoBehaviour
{
    private GameState _gameState;
    public GameState GameState => _gameState;

    public void Init()
    {
        _gameState = GameState.Init;
    }

    private void GameStart()
    {
        _gameState = GameState.Start;
    }

    private void GamePlay()
    {
        _gameState = GameState.Play;
    }

    private void GameEnd()
    {
        _gameState = GameState.End;
    }

    private void Result()
    {
        _gameState = GameState.Result;
    }
}
