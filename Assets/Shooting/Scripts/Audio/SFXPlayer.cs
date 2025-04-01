using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioClip clipSFX;

    public void PlaySFX()
    {
        AudioManager.Instance.PlayEffect(clipSFX);
    }
}
