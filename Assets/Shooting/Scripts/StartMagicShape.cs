using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMagicShape : MonoBehaviour
{
    [SerializeField] string magicShapeName;

    void Start()
    {
        if(magicShapeName != GameManager.Instance.curMagicShape.ToString())
        {
            gameObject.SetActive(false);
        }
    }
}
