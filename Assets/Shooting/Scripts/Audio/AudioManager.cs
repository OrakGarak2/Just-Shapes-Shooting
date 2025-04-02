using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource effectSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master"))   audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("Master"));
        if (PlayerPrefs.HasKey("BGM"))      audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGM"));
        if (PlayerPrefs.HasKey("SFX"))      audioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PauseBGM()
    {
        bgmSource.Pause();
    }

    public void UnPauseBGM()
    {
        bgmSource.UnPause();
    }

    public void PlayEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void SetEffect(AudioClip clip)
    {
        effectSource.clip = clip;
    }

    public void PlayEffect()
    {
        effectSource.Play();
    }

    public void SetVolume(string type, float volume)
    {
        audioMixer.SetFloat(type, volume);
        PlayerPrefs.SetFloat(type, volume);
    }
}
