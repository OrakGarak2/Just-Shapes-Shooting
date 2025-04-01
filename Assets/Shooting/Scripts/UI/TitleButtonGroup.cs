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
        TitleButton titleButton;
        Transform childTransform;

        // 애니메이션이 끝난 후 위치를 고정합니다.
        for (int i = 0; i < transform.childCount; i++)
        {
            childTransform = transform.GetChild(i);
            titleButton = childTransform.GetComponent<TitleButton>();

            titleButton.SetButton();
        }

        animator.enabled = false;
    }
}
