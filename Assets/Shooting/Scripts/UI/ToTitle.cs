using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToTitle : MonoBehaviour
{
    [SerializeField] Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GoTitle);
    }

    void GoTitle()
    {
        LoadSceneManager.LoadScene("00.Title");
    }
}
