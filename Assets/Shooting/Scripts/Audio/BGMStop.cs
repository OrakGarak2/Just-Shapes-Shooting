using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStop : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance.StopBGM();
    }
}