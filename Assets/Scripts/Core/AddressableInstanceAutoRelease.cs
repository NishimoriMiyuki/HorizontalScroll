using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableInstanceAutoRelease : MonoBehaviour
{
    private bool _autoRelease = true;

    private void OnDestroy()
    {
        if (_autoRelease)
        {
            Addressables.ReleaseInstance(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        _autoRelease = false;
    }
}