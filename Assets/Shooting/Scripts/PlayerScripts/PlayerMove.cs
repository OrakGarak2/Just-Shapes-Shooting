using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2D;
    PlayerHp playerHp;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerHp = GetComponent<PlayerHp>();
    }

    void Update()
    {
        if (playerHp.isPushed) { return; }

        PlayerMoving();
        PlayerRotate();
    }

    /// <summary>
    /// Player 이동
    /// (방향키로 이동)
    /// </summary>
    void PlayerMoving()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rb2D.velocity = movement * moveSpeed;
    }

    /// <summary>
    /// Player 각도 변경
    /// (wasd로 변경)
    /// </summary>
    void PlayerRotate()
    {
        if (Input.GetKeyDown(KeyCode.W))
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (Input.GetKeyDown(KeyCode.A))
            this.transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (Input.GetKeyDown(KeyCode.S))
            this.transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (Input.GetKeyDown(KeyCode.D))
            this.transform.rotation = Quaternion.Euler(0, 0, 270);
    }
}
