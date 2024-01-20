using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameScene : SceneBase
{
    protected override async UniTask OnInitialize(object args)
    {
        await base.OnInitialize(args);
        Debug.Log("GameScene OnInitialize");

        // 仮処理
        if (args is not Stage stage)
        {
            stage = MainSystem.Instance.MasterData.StageData.FirstOrDefault();
        }

        await GameManager.Instance.Init(stage);
    }
}
