using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _bgmSource, _seSource;

    private CancellationToken _cancellationToken;
    private string _currentBgmKey;

    private void Awake()
    {
        _cancellationToken = this.GetCancellationTokenOnDestroy();
    }

    public async UniTaskVoid PlayBgm(string key, bool enforceRestart = false, bool isOneShot = false)
    {
        // 現在のbgmと同じかつ最初から再生しない
        if (_currentBgmKey == key && !enforceRestart)
        {
            return;
        }

        if (_currentBgmKey != null)
        {
            if (_bgmSource.isPlaying)
            {
                _bgmSource.Stop();
            }
            _bgmSource.time = 0;
            _bgmSource.clip = null;

            MainSystem.Instance.AddressableManager.ReleaseAsset(_currentBgmKey);
        }

        _currentBgmKey = key;
        var clip = await MainSystem.Instance.AddressableManager.LoadAssetAsync<AudioClip>(key);
        if (clip == null)
        {
            return;
        }
        _bgmSource.clip = clip;
        _bgmSource.loop = !isOneShot;
        _bgmSource.Play();
    }

    public async UniTaskVoid PlaySe(string key)
    {
        var clip = await MainSystem.Instance.AddressableManager.LoadAssetAsync<AudioClip>(key);
        _seSource.PlayOneShot(clip);

        await UniTask.Delay(TimeSpan.FromSeconds(clip.length), cancellationToken: _cancellationToken);
        MainSystem.Instance.AddressableManager.ReleaseAsset(key);
    }
}