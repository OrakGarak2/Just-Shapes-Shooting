using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Button btnStart;

    private void Start()
    {
        btnStart = GetComponent<Button>();

        btnStart.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        LoadSceneManager.LoadScene("02.StageSelect");
    }
}
