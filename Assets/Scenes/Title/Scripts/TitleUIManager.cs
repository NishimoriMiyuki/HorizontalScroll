using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    public void Init()
    {
        _startButton.onClick.AddListener(OnClickStartButton);
    }

    private void OnClickStartButton()
    {
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Game, fadeType: FadeType.ColorWhite);
    }
}
