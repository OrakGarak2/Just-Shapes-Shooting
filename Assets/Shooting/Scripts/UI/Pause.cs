using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button btnToTitle;
    [SerializeField] Button btnToSelect;

    void Start()
    {
        btnToTitle.onClick.AddListener(ToTitle);
        btnToSelect.onClick.AddListener(ToSelect);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pausePanel.activeSelf) 
            {
                if (Time.timeScale == 0) return;
                OnPause();
            }
            else
            {
                OffPause();
            }
        }
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        AudioManager.Instance.PauseBGM();
        pausePanel.SetActive(true);
    }

    public void OffPause()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.UnPauseBGM();
        pausePanel.SetActive(false);
    }

    public void ToTitle()
    {
        Time.timeScale = 1f;
        LoadSceneManager.LoadScene("00.Title");
    }

    public void ToSelect()
    {
        Time.timeScale = 1f;
        LoadSceneManager.LoadScene("02.StageSelect");
    }
}
