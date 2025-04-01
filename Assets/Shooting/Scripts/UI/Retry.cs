using UnityEngine.SceneManagement;
using UnityEngine;

public class Retry : MonoBehaviour
{
    public void SceneReLoad()
    {
        Time.timeScale = 1.0f;
        Scene scene = SceneManager.GetActiveScene();
        LoadSceneManager.LoadScene(scene.name);
    }
}
