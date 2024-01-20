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

    private GameUIManager _gameUIManager;

    public readonly int CLEAR_BONUS = 1000;
    private readonly int GAMEOVER_BONUS = 0;

    public async UniTask Init(Stage stage)
    {
        _gameState = GameState.Init;
        _gameUIManager = GameUIManager.Instance;

        _gameUIManager.Init(stage);
        await MainSystem.Instance.AddressableManager.InstantiateAsync(stage.thing_address);

        GameStart();
    }

    private void GameStart()
    {
        _gameState = GameState.Start;

        _gameUIManager.StartTimer();
        GamePlay();
    }

    private void GamePlay()
    {
        _gameState = GameState.Play;
    }

    public void GameClear()
    {
        _gameState = GameState.GameClear;

        Debug.Log("GameClear");

        _gameUIManager.StopTimer();
        Result(CLEAR_BONUS);
    }

    public void GameOver()
    {
        _gameState = GameState.GameOver;

        Debug.Log("GameOver");
        Result(GAMEOVER_BONUS);
    }

    private void Result(int bonus)
    {
        _gameState = GameState.Result;

        Debug.Log("Result");

        _gameUIManager.OpenResultView(bonus);
    }
}
