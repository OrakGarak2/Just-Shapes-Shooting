using UnityEngine;

public class SaveStage : MonoBehaviour
{
    [SerializeField] int stageLevel;

    void StageSave()
    {
        if (PlayerPrefs.HasKey("Stage"))
        {
            if(stageLevel > PlayerPrefs.GetInt("Stage"))
            {
                PlayerPrefs.SetInt("Stage", stageLevel);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Stage", stageLevel);
        }
    }
}
