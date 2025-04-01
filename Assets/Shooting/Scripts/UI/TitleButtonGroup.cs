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

        // �ִϸ��̼��� ���� �� ��ġ�� �����մϴ�.
        for (int i = 0; i < transform.childCount; i++)
        {
            childTransform = transform.GetChild(i);
            titleButton = childTransform.GetComponent<TitleButton>();

            titleButton.SetButton();
        }

        animator.enabled = false;
    }
}
