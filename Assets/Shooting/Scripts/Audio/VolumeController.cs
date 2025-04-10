using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private string audioType;

    private readonly float muteValue = -80f;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();

        if(PlayerPrefs.HasKey(audioType))
        {
            float savedVolume = PlayerPrefs.GetFloat(audioType);

            if(savedVolume == muteValue)
            {
                volumeSlider.value = volumeSlider.minValue;
            }
            else
            {
                volumeSlider.value = savedVolume;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(volumeSlider.value == volumeSlider.minValue)
        {
            AudioManager.Instance.SetVolume(audioType, muteValue);
        }
        else
        {
            AudioManager.Instance.SetVolume(audioType, volumeSlider.value);
        }
    }
}
