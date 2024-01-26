using System.Threading;
using UnityEngine;

public enum CatState
{
    Sleep = 0,
    NailSharpener, // 爪とぎ
}

public class CatController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sleepSprite, _nailSharpenerSprite;

    private CatState _catState;
    public CatState CatState => _catState;

    private CancellationToken _ct;

    public const string PARAMETER_STATE_NUM = "StateNum";

    public void Init()
    {
        Sleep();
    }

    public void Sleep()
    {
        _catState = CatState.Sleep;

        _sleepSprite.gameObject.SetActive(true);
        _nailSharpenerSprite.gameObject.SetActive(false);
    }

    public void NailSharpener()
    {
        _catState = CatState.NailSharpener;

        _sleepSprite.gameObject.SetActive(false);
        _nailSharpenerSprite.gameObject.SetActive(true);
    }
}
