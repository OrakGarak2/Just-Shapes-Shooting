using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource effectSource;

    public float volumeMaster;
    public float volumeBGM;
    public float volumeSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey("VolumeMaster"))
            {
                volumeMaster = PlayerPrefs.GetFloat("VolumeMaster");
                volumeBGM = PlayerPrefs.GetFloat("VolumeBGM");
                volumeSFX = PlayerPrefs.GetFloat("VolumeSFX");
            }
            else
            {
                volumeMaster = -20f;
                volumeBGM = -10f;
                volumeSFX = -10f;
            }

            audioMixer.SetFloat("Master", volumeMaster);
            audioMixer.SetFloat("BGM", volumeBGM);
            audioMixer.SetFloat("SFX", volumeSFX);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if(bgmSource != null)
            bgmSource.Stop();
    }

    public void PlayEffect(AudioClip clip)
    {
        // PlayOneShot �޼��� ���
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
}
