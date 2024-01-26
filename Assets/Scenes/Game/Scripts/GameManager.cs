using System.Threading;
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
    [SerializeField]
    private SpriteRenderer _stageBg;

    [SerializeField]
    private OwnerController _ownerController;

    [SerializeField]
    private CatController _catController;

    private GameState _gameState;
    public GameState GameState => _gameState;

    private GameUIManager _gameUIManager;
    private AddressableManager _addressableManager;
    private ThingController _thingController;
    private CancellationTokenSource _cancellationTokenSource;

    private readonly int CLEAR_BONUS = 1000;

    public async UniTask Init(Stage stage)
    {
        _gameState = GameState.Init;
        _gameUIManager = GameUIManager.Instance;
        _addressableManager = MainSystem.Instance.AddressableManager;
        _cancellationTokenSource = new CancellationTokenSource();

        _gameUIManager.Init(stage);
        var instance = await _addressableManager.InstantiateAsync(stage.thing_address);
        _thingController = instance.GetComponent<ThingController>();
        _stageBg.sprite = await _addressableManager.LoadAssetAsync<Sprite>(stage.bg_address);
        _ownerController.Init(stage.owner_group_id, _cancellationTokenSource.Token).Forget();
        _catController.Init();

        GameStart();
    }

    private void GameStart()
    {
        _gameState = GameState.Start;

        _gameUIManager.StartTimer();
        _ownerController.StartAction().Forget();
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
        _cancellationTokenSource.Cancel();
        MainSystem.Instance.PlayerData.AddNextStage();
        Result(ConstAssetAddress.NextStageButton, CLEAR_BONUS);
    }

    public void GameOver()
    {
        _gameState = GameState.GameOver;

        Debug.Log("GameOver");

        _cancellationTokenSource.Cancel();
        Result(ConstAssetAddress.RetryStageButton);
    }

    private void Result(string address, int bonus = 0)
    {
        _gameState = GameState.Result;
        Debug.Log("Result");

        _gameUIManager.OpenResultView(bonus, address);
    }

    public void CheckScratch()
    {
        if (_thingController.IsScratching)
        {
            GameOver();
        }
    }

    public void CatNailSharpener()
    {
        _catController.NailSharpener();
    }

    public void CatSleep()
    {
        _catController.Sleep();
    }
}