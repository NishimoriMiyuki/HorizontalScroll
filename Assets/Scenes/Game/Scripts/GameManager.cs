using Cysharp.Threading.Tasks;
using UnityEngine;

public enum GameState
{
    Init,
    Start,
    Play,
    GameClear,
    GameOver,
    Result,
}

public class GameManager : SingletonBehaviourSceneOnly<GameManager>
{
    private GameState _gameState;
    public GameState GameState => _gameState;

    public async UniTask Init(Stage stage)
    {
        _gameState = GameState.Init;

        GameUIManager.Instance.Init(stage);
        await MainSystem.Instance.AddressableManager.InstantiateAsync(stage.thing_address);

        GameStart();
    }

    private void GameStart()
    {
        _gameState = GameState.Start;

        GameUIManager.Instance.StartTimer();
    }

    private void GamePlay()
    {
        _gameState = GameState.Play;
    }

    private void GameClear()
    {
        _gameState = GameState.GameClear;
    }

    public void GameOver()
    {
        _gameState = GameState.GameOver;

        Debug.Log("GameOver");
    }

    private void Result()
    {
        _gameState = GameState.Result;
    }
}
