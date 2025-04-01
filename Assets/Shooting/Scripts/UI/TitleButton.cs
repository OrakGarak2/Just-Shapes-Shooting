using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 마우스 커서를 버튼 위에 가져다 댔을 때 버튼이 이동할 위치
    /// </summary>
    [SerializeField] private Vector2 btnTransformOnPointer;

    /// <summary>
    /// 마우스 커서를 버튼 위에서 땠을 때 버튼이 이동할 위치
    /// </summary>
    [SerializeField] private Vector2 btnTransformOffPointer;

    /// <summary>
    /// 마우스 커서를 버튼 위에 가져다 댔을 때 버튼이 이동할 위치의 X좌표
    /// </summary>
    private const float onPointerPosX = 50f;

    /// <summary>
    /// 버튼의 이동 속도
    /// </summary>
    [SerializeField] private float btnSpeed;

    /// <summary>
    /// ���콺 �����Ͱ� ��ư ���� �ִ��� ������ �˷��ִ� ����
    /// </summary>
    [SerializeField] private bool isOnPointer;

    /// <summary>
    /// �ִϸ��̼��� �������� �˷��ִ� ����
    /// </summary>
    [SerializeField] private bool isAnimFinished = false;
     
    /// <summary>
    /// ���콺 �����Ͱ� ��ư ���� ���� ���� ��ư�� ��(���� ���� �Ͼ��)
    /// </summary>
    [SerializeField] private Color onPointerColor = new Color();

    public void SetButton()
    {
        btnTransformOffPointer = GetComponent<RectTransform>().anchoredPosition;
        btnTransformOnPointer = new Vector2(onPointerPosX, btnTransformOffPointer.y);
        btnSpeed = 10.0f;
        isAnimFinished = true;
    }

    private void Update() {
        if (!isAnimFinished) return;

        if (GetComponent<RectTransform>().anchoredPosition != btnTransformOnPointer && isOnPointer)
        {
            GetComponent<RectTransform>().anchoredPosition =
                Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, btnTransformOnPointer, btnSpeed * Time.deltaTime);
        }
        else if (GetComponent<RectTransform>().anchoredPosition != btnTransformOffPointer && !isOnPointer)
        {
            GetComponent<RectTransform>().anchoredPosition =
                Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, btnTransformOffPointer, btnSpeed * Time.deltaTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isAnimFinished) { return; }

        isOnPointer = true;
        GetComponent<Image>().color = onPointerColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isAnimFinished) { return; }

        isOnPointer = false;
        GetComponent<Image>().color = Color.white;
    }
}
