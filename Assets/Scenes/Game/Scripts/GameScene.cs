using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameScene : SceneBase
{
    protected override async UniTask OnInitialize(object args)
    {
        await base.OnInitialize(args);
        Debug.Log("GameScene OnInitialize");

        await GameManager.Instance.Init(MainSystem.Instance.PlayerData.NextStage.MasterStage);
    }
}
