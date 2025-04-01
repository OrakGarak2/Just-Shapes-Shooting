using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// ���콺 �����Ͱ� ��ư ���� �ö��� ���� ��ư ��ġ
    /// </summary>
    [SerializeField] private Vector2 btnTransformOnPointer;

    /// <summary>
    /// ���콺 �����Ͱ� ��ư ���� �ö��� �ʾ��� ���� ��ư ��ġ
    /// </summary>
    [SerializeField] private Vector2 btnTransformOffPointer;

    /// <summary>
    /// ���콺 �����Ͱ� ��ư ���� �ö��� ���� ��ư�� x��
    /// </summary>
    private const float onPointerPosX = 50f;

    /// <summary>
    /// ��ư�� �����̴� �ӵ�
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

        StartCoroutine(Co_ButtonMove());
    }

    IEnumerator Co_ButtonMove()
    {
        while (true)
        {
            if (GetComponent<RectTransform>().anchoredPosition != btnTransformOnPointer && isOnPointer) // �� ��ư�� ��ġ�� btnTransformOnMouse�� ��ġ�� �ٸ��ٸ�
            {
                GetComponent<RectTransform>().anchoredPosition =
                    Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, btnTransformOnPointer, btnSpeed * Time.deltaTime);
            }
            else if (GetComponent<RectTransform>().anchoredPosition != btnTransformOffPointer && !isOnPointer) // �� ��ư�� ��ġ�� btnTransformOffMouse�� ��ġ�� �ٸ��ٸ�
            {
                GetComponent<RectTransform>().anchoredPosition =
                    Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, btnTransformOffPointer, btnSpeed * Time.deltaTime);
            }

            yield return null;
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
