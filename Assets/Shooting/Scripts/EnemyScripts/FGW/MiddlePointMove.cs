using UnityEngine;

public class MiddlePointMove : MonoBehaviour
{
    // 베지어 곡선의 3점 중 중간점의 움직임

    [Header("Speed, X Axis Range")]
    [SerializeField][Range(0f, 10f)] private float speed = 1f;
    [SerializeField][Range(0f, 10f)] private float xPosLength = 1f;

    [Header("RunningTime, Position")]
    [SerializeField] private float runningTime = 0f;
    [SerializeField] private float yPos = 0f;
    [SerializeField] private float initialXPos = 0f;

    void Start()
    {
        yPos = this.transform.position.y;
        initialXPos = this.transform.position.x;
    }

    void Update()
    {
        runningTime += Time.deltaTime * speed;
        float xPos = initialXPos + Mathf.Sin(runningTime) * xPosLength;
        this.transform.position = new Vector2(xPos, yPos);
    }
}
