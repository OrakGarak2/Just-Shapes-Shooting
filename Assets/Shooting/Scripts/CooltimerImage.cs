using UnityEngine;
using UnityEngine.UI;

public class CooltimerImage : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.cooltimeImage = this.GetComponent<Image>();
    }
}
