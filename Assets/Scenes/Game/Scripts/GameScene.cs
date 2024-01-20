using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameScene : SceneBase
{
    [SerializeField]
    private GameManager _gameManager;

    protected override async UniTask OnInitialize(object args)
    {
        await base.OnInitialize(args);
        Debug.Log("GameScene OnInitialize");

        _gameManager.Init();
    }
}
