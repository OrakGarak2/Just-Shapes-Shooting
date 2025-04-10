using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class TitleMagicShape : MonoBehaviour
{
    [SerializeField] MagicShape thisMagicShape;

    void Start()
    {
        if(thisMagicShape != GameManager.Instance.curMagicShape)
        {
            gameObject.SetActive(false);
        }
    }
}
