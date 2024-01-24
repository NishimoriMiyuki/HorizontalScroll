using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeScoreText, _scratchScoreText, _bonusScoreText, _totalScoreText;

    [SerializeField]
    private Button _titleButton;

    private Tween _titleButtonTween;

    private void Awake()
    {
        _timeScoreText.text = null;
        _scratchScoreText.text = null;
        _bonusScoreText.text = null;
        _totalScoreText.text = null;

        _titleButton.onClick.AddListener(OnClickTitleButton);
    }

    public async UniTaskVoid SetScore(int restTime, int dragNumber, int bonus)
    {
        _titleButton.gameObject.SetActive(false);

        int timeBonus = restTime * 5;
        int dragNumberBonus = dragNumber * 2;

        await ScoreAnim(_timeScoreText, timeBonus);
        await ScoreAnim(_scratchScoreText, dragNumberBonus);
        await ScoreAnim(_bonusScoreText, bonus);
        await ScoreAnim(_totalScoreText, (timeBonus + dragNumberBonus + bonus));

        _titleButton.gameObject.SetActive(true);
        _titleButtonTween = _titleButton.transform
            .DOScale(1.2f, 0.4f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void OnClickTitleButton()
    {
        _titleButtonTween.Kill();
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Title, fadeType: FadeType.ColorWhite);
    }

    private async UniTask ScoreAnim(TextMeshProUGUI scoreText, int score)
    {
        float duration = 0.5f;

        int randomValue = Random.Range(0, 9999);
        await DOTween.To(() => randomValue, x => randomValue = x, score, duration)
            .OnUpdate(() => scoreText.text = randomValue.ToString());
    }
}
