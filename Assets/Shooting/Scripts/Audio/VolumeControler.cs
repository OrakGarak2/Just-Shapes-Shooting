using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeControler : MonoBehaviour
{
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;

    [SerializeField] float volumeMaster;
    [SerializeField] float volumeBGM;
    [SerializeField] float volumeSFX;

    [SerializeField] float conditioningValue = 80f;

    public AudioMixer audioMixer;

    private void Start()
    {
        float[] volumes = new float[3];
        volumes[0] = AudioManager.Instance.volumeMaster;
        volumes[1] = AudioManager.Instance.volumeBGM;
        volumes[2] = AudioManager.Instance.volumeSFX;

        for (int i = 0; i < volumes.Length; i++)
        {
            if (volumes[i] == -conditioningValue) 
            {
                volumes[i] = sliderMaster.minValue;
            }
        }

        sliderMaster.value = volumes[0];
        sliderBGM.value = volumes[1];
        sliderSFX.value = volumes[2];
    }

    public void Master_Volume()
    {
        volumeMaster = sliderMaster.value;
        // 볼륨 값을 -80에서 0으로 변환하기 위해 80을 더하고 0~1 범위의 실수로 만들기 위해 80으로 나눔.
        AudioManager.Instance.volumeMaster = (volumeMaster + conditioningValue) / conditioningValue;

        if (volumeMaster == sliderMaster.minValue)
        {
            volumeMaster = -conditioningValue;
            audioMixer.SetFloat("Master", -conditioningValue); // 음소거
        }
        else audioMixer.SetFloat("Master", volumeMaster);
    }

    public void BGM_Volume()
    {
        volumeBGM = sliderBGM.value;
        // 볼륨 값을 -80에서 0으로 변환하기 위해 80을 더하고 0~1 범위의 실수로 만들기 위해 80으로 나눔.
        AudioManager.Instance.volumeBGM = (volumeBGM + conditioningValue) / conditioningValue;

        if (volumeBGM == sliderBGM.minValue)
        {
            volumeBGM = -conditioningValue;
            audioMixer.SetFloat("BGM", -conditioningValue); // 음소거
        }
        else audioMixer.SetFloat("BGM", volumeBGM);
    }

    public void SFX_Volume()
    {
        volumeSFX = sliderSFX.value;
        // 볼륨 값을 -80에서 0으로 변환하기 위해 80을 더하고 0~1 범위의 실수로 만들기 위해 80으로 나눔.
        AudioManager.Instance.volumeSFX = (volumeSFX + conditioningValue) / conditioningValue;

        if (volumeSFX == sliderSFX.minValue)
        {
            volumeSFX = -conditioningValue;
            audioMixer.SetFloat("SFX", -conditioningValue); // 음소거
        }
        else audioMixer.SetFloat("SFX", volumeSFX);
    }

    public void SaveVolume()
    {
        AudioManager.Instance.volumeMaster = volumeMaster;
        AudioManager.Instance.volumeBGM = volumeBGM;
        AudioManager.Instance.volumeSFX = volumeSFX;

        PlayerPrefs.SetFloat("VolumeMaster", volumeMaster);
        PlayerPrefs.SetFloat("VolumeBGM", volumeBGM);
        PlayerPrefs.SetFloat("VolumeSFX", volumeSFX);
    }
}
