using System.Collections;
using UnityEngine;

public class TitleButtonGroup : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixPositionAfterAnimation()
    { 
        foreach (var titleButton in GetComponentsInChildren<TitleButton>())
        {
            titleButton.SetButton();
        }

        animator.enabled = false;
    }
}
