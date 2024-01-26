using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Cysharp.Threading.Tasks;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField]
    private Button _startButton, _titleViewButton;

    [SerializeField]
    private TextMeshProUGUI _totalCatDegree;

    [SerializeField]
    private TitleView _titleView;

    [SerializeField]
    private GameObject _controlCanvas;

    private Tween _startButtonTween;

    public async UniTask Init()
    {
        await _titleView.Init();

        _titleView.gameObject.SetActive(false);

        _startButton.onClick.AddListener(OnClickStartButton);
        _titleViewButton.onClick.AddListener(OnClickTitleViewButton);

        _startButtonTween = _startButton.transform
            .DOScale(1.2f, 0.4f)
            .SetLoops(-1, LoopType.Yoyo);

        _totalCatDegree.text = MainSystem.Instance.PlayerData.cat_degree.score.ToString();
    }

    private void OnClickStartButton()
    {
        _startButtonTween.Kill();
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Game, fadeType: FadeType.ColorWhite);
    }

    private void OnClickTitleViewButton()
    {
        _controlCanvas.SetActive(false);
        _titleView.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _startButtonTween.Kill();
    }
}
