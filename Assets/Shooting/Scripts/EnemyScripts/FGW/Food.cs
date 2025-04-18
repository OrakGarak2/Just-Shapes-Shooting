using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform middlePoint;
    public Vector2 endPoint;

    [SerializeField] float foodDamage;
    [SerializeField] int playerLayer;

    void Awake()
    {
        playerLayer = LayerData.playerLayer;
    }

    private void OnEnable() {
        StartCoroutine(Co_BezierCurve(startPoint.position, middlePoint.position, endPoint));
    }

    IEnumerator Co_BezierCurve(Vector2 start, Vector2 middle, Vector2 end, float duration = 1.0f)
    {
        float time = 0f;

        while (time < 1f)
        {
            /* 
            Vector2 p1 = Vector2.Lerp(startPoint.position, intermediatePoint.position, time);
            Vector2 p2 = Vector2.Lerp(intermediatePoint.position, endPoint, time);
            transform.position = Vector2.Lerp(p1, p2, time); 
            */

            // Lerp를 계속 호출하는 위의 코드보다 
            // 곱셈과 덧셈 연산만을 사용하는 아래의 코드가 더 효율적임.

            transform.position = (1 - time) * (1 - time) * start
                                + 2 * (1 - time) * time * middle
                                + time * time * end;

            time += Time.deltaTime / duration;

            if (!gameObject.activeSelf) break;

            yield return null;
        }

        transform.position = startPoint.position;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            collision.GetComponent<PlayerHp>().CheckDamage(foodDamage, false);
            gameObject.SetActive(false);
        }
    }
}
