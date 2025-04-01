using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    [SerializeField] Button btnQuit;

    void Start()
    {
        btnQuit = GetComponent<Button>();
        btnQuit.onClick.AddListener(Quit);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
