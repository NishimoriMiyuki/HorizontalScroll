using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public enum CatState
{
    Sleep = 0,
    NailSharpener, // 爪とぎ
}

public class CatController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _stateSprite;

    private CatState _catState;
    public CatState CatState => _catState;

    private CancellationToken _ct;

    public const string PARAMETER_STATE_NUM = "StateNum";

    public void Init(CancellationToken ct)
    {
        _ct = ct;

        Sleep();
    }

    public void Sleep()
    {
        _catState = CatState.Sleep;
        _animator.SetInteger(PARAMETER_STATE_NUM, (int)CatState.Sleep);
    }

    public async UniTaskVoid NailSharpener()
    {
        _catState = CatState.NailSharpener;
        _animator.SetInteger(PARAMETER_STATE_NUM, (int)CatState.NailSharpener);

        float elapsedTime = 0f;
        float seconds = 0.5f;

        while (elapsedTime < seconds)
        {
            await UniTask.NextFrame(cancellationToken: _ct);
            elapsedTime += Time.deltaTime;
        }

        Sleep();
    }
}
