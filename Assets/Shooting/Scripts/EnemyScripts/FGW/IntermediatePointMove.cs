using UnityEngine;

public class IntermediatePointMove : MonoBehaviour
{
    [Header("속도, 길이")]
    [SerializeField][Range(0f, 10f)] private float speed = 1f;
    [SerializeField][Range(0f, 10f)] private float xPosLength = 1f;

    [Header("RunningTime과 Position")]
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
