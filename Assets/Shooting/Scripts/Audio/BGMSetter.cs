using UnityEngine;

public class BGMSetter : MonoBehaviour
{
    [SerializeField] AudioClip bgmClip;

    void Start()
    {
        AudioManager.Instance.PlayBGM(bgmClip);
    }
}
