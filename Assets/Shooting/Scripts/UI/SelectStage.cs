using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Button btnStage;

    void Start()
    {
        btnStage = GetComponent<Button>();
        btnStage.onClick.AddListener(SceneChange);
    }

    void SceneChange()
    {
        LoadSceneManager.LoadScene(sceneName);
    }
}
