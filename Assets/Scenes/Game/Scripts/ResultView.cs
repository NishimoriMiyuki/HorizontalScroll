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
    private Button _titleButton, _gameButton;

    [SerializeField]
    private Image _gameButtonImage;

    private Tween _titleButtonTween, _gameButtonTween;

    private void Awake()
    {
        _timeScoreText.text = null;
        _scratchScoreText.text = null;
        _bonusScoreText.text = null;
        _totalScoreText.text = null;

        _titleButton.onClick.AddListener(OnClickTitleButton);
        _gameButton.onClick.AddListener(OnClickGameButton);
    }

    public void SetButtonImage(string address)
    {
        UniTask.Void(async () =>
        {
            _gameButtonImage.sprite = await MainSystem.Instance.AddressableManager.LoadAssetAsync<Sprite>(address);
        });
    }

    public async UniTaskVoid SetScore(int restTime, int dragNumber, int bonus)
    {
        ButtonsSetActive(false);

        int timeBonus = restTime * 5;
        int dragNumberBonus = dragNumber * 2;
        int totalScore = timeBonus + dragNumberBonus + bonus;

        await ScoreAnim(_timeScoreText, timeBonus);
        await ScoreAnim(_scratchScoreText, dragNumberBonus);
        await ScoreAnim(_bonusScoreText, bonus);
        await ScoreAnim(_totalScoreText, totalScore);

        ButtonsSetActive(true);

        _titleButtonTween = _titleButton.transform
            .DOScale(1.2f, 0.4f)
            .SetLoops(-1, LoopType.Yoyo);

        _gameButtonTween = _gameButton.transform
            .DOScale(1.2f, 0.4f)
            .SetLoops(-1, LoopType.Yoyo);

        MainSystem.Instance.PlayerData.AddScore(totalScore);
        MainSystem.Instance.PlayerData.AddTitle();
    }

    private void OnClickTitleButton()
    {
        TweensKill();
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Title, fadeType: FadeType.ColorWhite);
    }

    private void OnClickGameButton()
    {
        TweensKill();
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Game, fadeType: FadeType.ColorWhite);
    }

    private async UniTask ScoreAnim(TextMeshProUGUI scoreText, int score)
    {
        float duration = 0.5f;

        int randomValue = Random.Range(0, 9999);
        await DOTween.To(() => randomValue, x => randomValue = x, score, duration)
            .OnUpdate(() => scoreText.text = randomValue.ToString());
    }

    private void TweensKill()
    {
        _gameButtonTween.Kill();
        _titleButtonTween.Kill();
    }

    private void ButtonsSetActive(bool isActive)
    {
        _titleButton.gameObject.SetActive(isActive);
        _gameButton.gameObject.SetActive(isActive);
    }
}
