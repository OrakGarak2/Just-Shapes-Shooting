using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rigidbody2D;
    PlayerHp playerHp;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerHp = GetComponent<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHp.isPushed) { return; }

        PlayerMoving();
        PlayerRotate();
    }

    /// <summary>
    /// Player의 이동
    /// (방향키로 이동)
    /// </summary>
    void PlayerMoving()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rigidbody2D.velocity = movement * moveSpeed;
    }

    /// <summary>
    /// Player의 방향 변환
    /// (wasd로 변환)
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
