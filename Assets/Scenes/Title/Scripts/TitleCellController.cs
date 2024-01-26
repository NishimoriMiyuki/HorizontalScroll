using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TitleCellController : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    public async UniTask Init(Title titleData)
    {
        _image.sprite = await MainSystem.Instance.AddressableManager.LoadAssetAsync<Sprite>(titleData.name_address);

        if (!MainSystem.Instance.PlayerData.titles.Any(t => t.title_id == titleData.id))
        {
            _image.color = Color.gray;
        }
    }
}
