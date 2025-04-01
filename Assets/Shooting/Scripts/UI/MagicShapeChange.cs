using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class MagicShapeChange : MonoBehaviour
{
    [SerializeField] private Button btnMagicShapeChange;
    [SerializeField] List<GameObject> objMagicShapes = new List<GameObject>();

    [SerializeField] private float fadeSpeed = 1f;

    void Start()
    {
        btnMagicShapeChange = GetComponent<Button>();
        btnMagicShapeChange.onClick.AddListener(ChangeCurMagicShape);
    }

    void ChangeCurMagicShape()
    {
        if(GameManager.Instance != null)
        {
            if (Enum.GetValues(typeof(MagicShape)).Length > objMagicShapes.Count) { return; }

            StartCoroutine(Co_FadeOutObject(objMagicShapes[(int)GameManager.Instance.curMagicShape]));

            if (Enum.GetValues(typeof(MagicShape)).Length > (int)GameManager.Instance.curMagicShape + 1)// enum의 최댓값보다 현재 enum 값 + 1이 작다면
                GameManager.Instance.curMagicShape++;                                                   // curMagicShapes++
            else                                                                                        // enum의 최댓값보다 현재 enum 값 + 1이 크다면
                GameManager.Instance.curMagicShape = 0;                                                 // curMagicShapes = 0
                                                                                                        // (이처럼 순환 구조를 만들기 위해 enum의 마지막 값에서 다시 처음 값으로 돌아가도록 설정해야 함.)

            PlayerPrefs.SetInt("MagicShape", (int)GameManager.Instance.curMagicShape);

            StartCoroutine(Co_FadeInObject(objMagicShapes[(int)GameManager.Instance.curMagicShape]));
        }
    }

    IEnumerator Co_FadeOutObject(GameObject obj)
    {
        btnMagicShapeChange.interactable = false;

        Color color = obj.GetComponent<SpriteRenderer>().color;

        if (color != null)
        {
            color.a = 1;

            while (true)
            {
                color.a -= Time.deltaTime * fadeSpeed;
                obj.GetComponent<SpriteRenderer>().color = color;

                if (color.a <= 0) { break; }

                yield return null;
            }

            obj.SetActive(false);
        }
    }

    IEnumerator Co_FadeInObject(GameObject obj)
    {
        obj.SetActive(true);

        Color color = obj.GetComponent<SpriteRenderer>().color;

        if (color != null)
        {
            color.a = 0;

            while (true)
            {
                color.a += Time.deltaTime * fadeSpeed;
                obj.GetComponent<SpriteRenderer>().color = color;

                if (color.a >= 1) { break; }

                yield return null;
            }
        }

        btnMagicShapeChange.interactable = true;
    }
}
