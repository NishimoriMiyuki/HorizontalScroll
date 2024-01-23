using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public enum OwnerState
{
    Init,
    Work, // 仕事
    FeelDisabled, // 異変を感じる
    Monitor, // 監視
}

public class OwnerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _stateSprite;

    private OwnerState _ownerState;
    private List<Owner> _ownerDataList = new();
    private AddressableManager _addressableManager;
    private CancellationToken _ct;

    public void Init(int ownerGroupId, CancellationToken ct)
    {
        _ownerState = OwnerState.Init;
        Debug.Log("OwnerState.Init");

        _ct = ct;
        _addressableManager = MainSystem.Instance.AddressableManager;

        _ownerDataList = MainSystem.Instance.MasterData.OwnerData
            .Where(owner => owner.group_id == ownerGroupId)
            .OrderBy(owner => owner.order)
            .ToList();

        Work().Forget();
    }

    private async UniTaskVoid Work()
    {
        _ownerState = OwnerState.Work;
        Debug.Log("OwnerState.Work");

        _stateSprite.sprite = await _addressableManager.LoadAssetAsync<Sprite>(ConstAssetAddress.WorkIcon);
    }

    private async UniTaskVoid FeelDisabled()
    {
        _ownerState = OwnerState.FeelDisabled;
        Debug.Log("OwnerState.FeelDisabled");

        _stateSprite.sprite = await _addressableManager.LoadAssetAsync<Sprite>(ConstAssetAddress.FeelDisabledIcon);
    }

    private async UniTaskVoid Monitor(float seconds)
    {
        _ownerState = OwnerState.Monitor;
        Debug.Log("OwnerState.Monitor");

        _stateSprite.sprite = await _addressableManager.LoadAssetAsync<Sprite>(ConstAssetAddress.MonitorIcon);

        float elapsedTime = 0f;

        while (elapsedTime < seconds)
        {
            GameManager.Instance.CheckScratch();
            await UniTask.NextFrame(cancellationToken: _ct);
            elapsedTime += Time.deltaTime;
            Debug.Log("check中");
        }
    }

    public async UniTask StartAction()
    {
        // 全ての行動が終わった回数を数える
        int allActionCompletedCount = 0;

        while (true)
        {
            var owner = _ownerDataList[allActionCompletedCount];

            await UniTask.Delay(TimeSpan.FromSeconds(owner.work_time), cancellationToken: _ct);
            FeelDisabled().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(owner.waiting_time), cancellationToken: _ct);
            Monitor(owner.monitor_time).Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(owner.monitor_time), cancellationToken: _ct);
            Work().Forget();

            allActionCompletedCount++;

            // もし、全ての行動回数がデータ以上になったら回数リセット
            if (allActionCompletedCount >= _ownerDataList.Count)
            {
                allActionCompletedCount = 0;
            }
        }
    }
}