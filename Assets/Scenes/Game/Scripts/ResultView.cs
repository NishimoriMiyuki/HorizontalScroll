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
        await ScoreAnim(_timeScoreText, restTime);
        await ScoreAnim(_scratchScoreText, dragNumber);
        await ScoreAnim(_bonusScoreText, bonus);
        await ScoreAnim(_totalScoreText, (restTime + dragNumber + bonus));
    }

    public void OnClickTitleButton()
    {
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Title, fadeType: FadeType.ColorWhite);
    }

    private async UniTask ScoreAnim(TextMeshProUGUI scoreText, int score)
    {
        float duration = 0.8f;

        int randomValue = Random.Range(0, 9999);
        await DOTween.To(() => randomValue, x => randomValue = x, score, duration)
            .OnUpdate(() => scoreText.text = randomValue.ToString());
    }
}
