using UnityEngine;
using UnityEngine.UI;
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

    public void Init()
    {
        _titleView.Init();

        _titleView.gameObject.SetActive(false);

        _startButton.onClick.AddListener(OnClickStartButton);
        _titleViewButton.onClick.AddListener(OnClickTitleViewButton);

        _totalCatDegree.text = MainSystem.Instance.PlayerData.cat_degree.score.ToString();
    }

    private void OnClickStartButton()
    {
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Game, fadeType: FadeType.ColorWhite);
    }

    private void OnClickTitleViewButton()
    {
        _controlCanvas.SetActive(false);
        _titleView.gameObject.SetActive(true);
    }
}
