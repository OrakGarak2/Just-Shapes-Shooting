using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
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
        AudioManager.Instance.volumeMaster = (volumeMaster + conditioningValue) / conditioningValue; // ����ġ�� -80�� 0���� �ٲٱ� ���� 80�� ���ϰ� 0~1�� �Ǽ��� ����� ���� 80�� ��������.

        if (volumeMaster == sliderMaster.minValue)
        {
            volumeMaster = -conditioningValue;
            audioMixer.SetFloat("Master", -conditioningValue); // ���Ұ�
        }
        else audioMixer.SetFloat("Master", volumeMaster);
    }

    public void BGM_Volume()
    {
        volumeBGM = sliderBGM.value;
        AudioManager.Instance.volumeBGM = (volumeBGM + conditioningValue) / conditioningValue; // ����ġ�� -80�� 0���� �ٲٱ� ���� 80�� ���ϰ� 0~1�� �Ǽ��� ����� ���� 80�� ��������.

        if (volumeBGM == sliderBGM.minValue)
        {
            volumeBGM = -conditioningValue;
            audioMixer.SetFloat("BGM", -conditioningValue); // ���Ұ�
        }
        else audioMixer.SetFloat("BGM", volumeBGM);
    }

    public void SFX_Volume()
    {
        volumeSFX = sliderSFX.value;
        AudioManager.Instance.volumeSFX = (volumeSFX + conditioningValue) / conditioningValue; // ����ġ�� -80�� 0���� �ٲٱ� ���� 80�� ���ϰ� 0~1�� �Ǽ��� ����� ���� 80�� ��������.

        if (volumeSFX == sliderSFX.minValue)
        {
            volumeSFX = -conditioningValue;
            audioMixer.SetFloat("SFX", -conditioningValue); // ���Ұ�
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
