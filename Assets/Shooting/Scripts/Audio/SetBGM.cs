using UnityEngine;

public class SetBGM : MonoBehaviour
{
    [SerializeField] AudioClip bgmClip;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM(bgmClip);
    }
}
