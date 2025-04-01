using UnityEngine;

public class PointGizmo : MonoBehaviour
{
    public Color color = Color.gray;
    public float radius = 1.0f;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
