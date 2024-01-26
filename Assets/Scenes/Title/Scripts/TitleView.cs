using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField]
    private TitleCellController _titleCellController;

    [SerializeField]
    private RectTransform _content;

    [SerializeField]
    private Button _backButton;

    public async UniTask Init()
    {
        await CreateCell();
        _backButton.onClick.AddListener(OnClickBackButton);
    }

    private async UniTask CreateCell()
    {
        foreach (var title in MainSystem.Instance.MasterData.TitleData)
        {
            var instance = Instantiate(_titleCellController, _content);
            await instance.Init(title);
        }
    }

    private void OnClickBackButton()
    {
        MainSystem.Instance.AppSceneManager.ChangeScene(ConstSceneName.Title, fadeType: FadeType.ColorWhite);
    }
}
