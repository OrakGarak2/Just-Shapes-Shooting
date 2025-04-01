using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform intermediatePoint;
    public Vector2 endPoint;

    [SerializeField] float gizmoDetail;
    [SerializeField] float foodDamage;
    List<Vector2> _gizmoPoints = new List<Vector2>();

    [SerializeField] int playerLayer;

    void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnEnable() {
        StartCoroutine(Co_BezierCurves());
    }

    IEnumerator Co_BezierCurves(float duration = 1.0f)
    {
        float time = 0f;

        while (time < 1f)
        {
            Vector2 p1 = Vector2.Lerp(startPoint.position, intermediatePoint.position, time);
            Vector2 p2 = Vector2.Lerp(intermediatePoint.position, endPoint, time);
            transform.position = Vector2.Lerp(p1, p2, time);

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

    void OnDrawGizmos()
    {
        _gizmoPoints.Clear();

        if (startPoint == null || intermediatePoint == null || endPoint == null || gizmoDetail <= 0) { return; }

        for (int i = 0; i < gizmoDetail; i++)
        {
            float t = (i / gizmoDetail);
            Vector2 p3 = Vector2.Lerp(startPoint.position, intermediatePoint.position, t);
            Vector2 p4 = Vector2.Lerp(intermediatePoint.position, endPoint, t);
            _gizmoPoints.Add(Vector2.Lerp(p3, p4, t));
        }

        for (int i = 0; i < _gizmoPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(_gizmoPoints[i], _gizmoPoints[i + 1]);
        }
        Gizmos.DrawLine(_gizmoPoints[_gizmoPoints.Count - 1], endPoint);
    }
}
