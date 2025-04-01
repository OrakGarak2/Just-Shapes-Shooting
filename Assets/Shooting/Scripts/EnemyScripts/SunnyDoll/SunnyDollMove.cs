using UnityEngine;

public class SunnyDollMove : MonoBehaviour
{
    [Header("속도, 길이")]
    [SerializeField][Range(0f, 10f)] private float speed = 1f;
    [SerializeField][Range(0f, 10f)] private float yPosLength = 1f;

    [Header("RunningTime과 Position")]
    [SerializeField] private float runningTime = 0f;
    [SerializeField] private float xPos = 0f;
    [SerializeField] private float initialYPos = 0f;

    void Start()
    {
        xPos = this.transform.position.x;
        initialYPos = this.transform.position.y;
    }

    void Update()
    {
        runningTime += Time.deltaTime * speed;
        float yPos = initialYPos + Mathf.Sin(runningTime) * yPosLength;
        this.transform.position = new Vector2(xPos, yPos);
    }
}
