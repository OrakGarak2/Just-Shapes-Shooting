using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 마우스 포인터가 버튼 위에 올라갔을 때의 버튼 위치
    /// </summary>
    [SerializeField] private Vector2 btnTransformOnPointer;

    /// <summary>
    /// 마우스 포인터가 버튼 위에 올라가지 않았을 때의 버튼 위치
    /// </summary>
    [SerializeField] private Vector2 btnTransformOffPointer;

    /// <summary>
    /// 마우스 포인터가 버튼 위에 올라갔을 때의 버튼의 x값
    /// </summary>
    private const float onPointerPosX = 50f;

    /// <summary>
    /// 버튼이 움직이는 속도
    /// </summary>
    [SerializeField] private float btnSpeed;

    /// <summary>
    /// 마우스 포인터가 버튼 위에 있는지 없는지 알려주는 변수
    /// </summary>
    [SerializeField] private bool isOnPointer;

    /// <summary>
    /// 애니메이션이 끝났는지 알려주는 변수
    /// </summary>
    [SerializeField] private bool isAnimFinished = false;
     
    /// <summary>
    /// 마우스 포인터가 버튼 위에 있을 때의 버튼의 색(없을 때는 하얀색)
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
            if (GetComponent<RectTransform>().anchoredPosition != btnTransformOnPointer && isOnPointer) // 이 버튼의 위치와 btnTransformOnMouse의 위치가 다르다면
            {
                GetComponent<RectTransform>().anchoredPosition =
                    Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, btnTransformOnPointer, btnSpeed * Time.deltaTime);
            }
            else if (GetComponent<RectTransform>().anchoredPosition != btnTransformOffPointer && !isOnPointer) // 이 버튼의 위치와 btnTransformOffMouse의 위치가 다르다면
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
