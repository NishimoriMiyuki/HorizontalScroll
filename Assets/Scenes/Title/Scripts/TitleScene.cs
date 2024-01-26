using Cysharp.Threading.Tasks;
using UnityEngine;

public class TitleScene : SceneBase
{
    [SerializeField]
    private TitleUIManager _titleUIManager;

    protected async override UniTask OnInitialize(object args)
    {
        await base.OnInitialize(args);
        Debug.Log("TitleScene OnInitialize");

        await _titleUIManager.Init();
    }
}