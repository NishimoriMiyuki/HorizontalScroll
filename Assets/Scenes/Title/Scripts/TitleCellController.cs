using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TitleCellController : MonoBehaviour
{
    [SerializeField]
    private Image _titleImage, _possessionImage;

    public async UniTask Init(Title titleData)
    {
        _titleImage.sprite = await MainSystem.Instance.AddressableManager.LoadAssetAsync<Sprite>(titleData.name_address);
        _possessionImage.gameObject.SetActive(true);

        if (!MainSystem.Instance.PlayerData.titles.Any(t => t.title_id == titleData.id))
        {
            _titleImage.color = Color.gray;
            _possessionImage.gameObject.SetActive(false);
        }
    }
}
