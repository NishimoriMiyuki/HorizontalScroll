using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

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
    private Image _stateImage;

    private OwnerState _ownerState;
    private List<Owner> _ownerDataList = new();
    private AddressableManager _addressableManager;
    private CancellationToken _ct;

    private Sprite _workIcon;
    private Sprite _feelDisabledIcon;
    private Sprite _monitorIcon;

    public async UniTaskVoid Init(int ownerGroupId, CancellationToken ct)
    {
        _ownerState = OwnerState.Init;
        Debug.Log("OwnerState.Init");

        _ct = ct;
        _addressableManager = MainSystem.Instance.AddressableManager;

        _ownerDataList = MainSystem.Instance.MasterData.OwnerData
            .Where(owner => owner.group_id == ownerGroupId)
            .OrderBy(owner => owner.order)
            .ToList();

        _workIcon = await _addressableManager.LoadAssetAsync<Sprite>(ConstAssetAddress.WorkIcon);
        _feelDisabledIcon = await _addressableManager.LoadAssetAsync<Sprite>(ConstAssetAddress.FeelDisabledIcon);
        _monitorIcon = await _addressableManager.LoadAssetAsync<Sprite>(ConstAssetAddress.MonitorIcon);

        Work();
    }

    private void Work()
    {
        _stateImage.sprite = _workIcon;

        _ownerState = OwnerState.Work;
        Debug.Log("OwnerState.Work");
    }

    private void FeelDisabled()
    {
        _stateImage.sprite = _feelDisabledIcon;

        _ownerState = OwnerState.FeelDisabled;
        Debug.Log("OwnerState.FeelDisabled");
    }

    private async UniTaskVoid Monitor(float seconds)
    {
        _stateImage.sprite = _monitorIcon;

        // Iconが切り替わるタイミングでDragすると、Iconが切り替わる前にGameOverになってしまうので1フレ待ってる
        await UniTask.NextFrame(cancellationToken: _ct);

        _ownerState = OwnerState.Monitor;
        Debug.Log("OwnerState.Monitor");

        float elapsedTime = 0f;

        while (elapsedTime < seconds)
        {
            GameManager.Instance.CheckScratch();
            await UniTask.NextFrame(cancellationToken: _ct);
            elapsedTime += Time.deltaTime;
        }
    }

    public async UniTaskVoid StartAction()
    {
        // 全ての行動が終わった回数を数える
        int allActionCompletedCount = 0;

        while (true)
        {
            var owner = _ownerDataList[allActionCompletedCount];

            await UniTask.Delay(TimeSpan.FromSeconds(owner.work_time), cancellationToken: _ct);
            FeelDisabled();
            await UniTask.Delay(TimeSpan.FromSeconds(owner.waiting_time), cancellationToken: _ct);
            Monitor(owner.monitor_time).Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(owner.monitor_time), cancellationToken: _ct);
            Work();

            allActionCompletedCount++;

            // もし、全ての行動回数がデータ以上になったら回数リセット
            if (allActionCompletedCount >= _ownerDataList.Count)
            {
                allActionCompletedCount = 0;
            }
        }
    }
}