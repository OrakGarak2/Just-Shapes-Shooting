using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStage : MonoBehaviour
{
    [SerializeField] List<Button> stageButtonList = new List<Button>(); // 1스테이지는 항상 활성화인 상태이므로 리스트에서 제외
    [SerializeField] List<Image> stageToStage = new List<Image>();

    void Start()
    {
        if(PlayerPrefs.HasKey("Stage"))
        {
            Color color = Color.white;
            color.a = 1f;

            for (int i = 0; i < PlayerPrefs.GetInt("Stage"); i++)
            {
                stageButtonList[i].interactable = true;
                stageToStage[i].color = color;
            }
        }
        else
        {
            
        }
        
    }

}
