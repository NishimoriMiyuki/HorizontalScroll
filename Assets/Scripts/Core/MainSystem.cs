using UnityEngine;

public class MainSystem : SingletonBehaviour<MainSystem>
{
    [SerializeField]
    private AppSceneManager _appSceneManager;
    public AppSceneManager AppSceneManager => _appSceneManager;

    [SerializeField]
    private FadeManager _fadeManager;
    public FadeManager FadeManager => _fadeManager;

    [SerializeField]
    private AddressableManager _addressableManager;
    public AddressableManager AddressableManager => _addressableManager;

    [SerializeField]
    private SoundManager _soundManager;
    public SoundManager SoundManager => _soundManager;
}
