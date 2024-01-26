using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private TextMeshProUGUI _totalCatDegree;

    private Tween _startButtonTween;

    public void Init()
    {
        _startButton.onClick.AddListener(OnClickStartButton);
        _startButtonTween = _startButton.transform
            .DOScale(1.2f, 0.4f)
            .SetLoops(-1, LoopType.Yoyo);

        _totalCatDegree.text = MainSystem.Instance.PlayerData.CatDegree.ToString();
    }

    private void OnClickStartButton()
    {
        _startButtonTween.Kill();
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Game, fadeType: FadeType.ColorWhite);
    }
}
