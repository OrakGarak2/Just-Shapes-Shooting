using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public float currentHp;
    public float maxHp;
    public Image HpBar;

    /// <summary>
    /// MagicShape ������ ������ �� �޴� ������
    /// </summary>
    public float outMagicShapeDamage;

    /// <summary>
    /// Player�� MagicShape ������ �������� Ȯ���ϴ� �Լ�
    /// </summary>
    private bool isOutMagicShape = false;

    /// <summary>
    /// �÷��̾� ���� ������ �ð�
    /// </summary>
    float gracePeriodTime = 1.5f;

    /// <summary>
    /// ���� �������� Ȯ���ϴ� ����
    /// </summary>
    bool gracePeriod = false;

    [SerializeField] private float pushedSpeed;
    public bool isPushed;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private AudioClip hitSound;
    Color playerColor;

    int magicShapeLayer;
    int wallLayer;

    void Start()
    {
        GameManager.Instance.trPlayer = this.transform;
        playerColor = GetComponent<SpriteRenderer>().color;
        maxHp = 100f;
        currentHp = maxHp;
        outMagicShapeDamage = 5f;

        gracePeriodTime = 1.5f;
        pushedSpeed = 25f;

        magicShapeLayer = LayerMask.NameToLayer("MagicShape");
        wallLayer = LayerMask.NameToLayer("Wall");
    }

    private void Update()
    {
        // ġƮŰ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            maxHp = 1000f;
            currentHp = maxHp;
            HpBar.fillAmount = currentHp / maxHp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == magicShapeLayer) // MagicShape ������ ��
        {
            isOutMagicShape = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) // MagicShape ������ ����
    {
        if (HpBar == null || !gameObject.activeSelf) return;

        if (other.gameObject.layer == magicShapeLayer)
        {
            isOutMagicShape = true;
            StartCoroutine(Co_OutMagicShape());
        }
    }

    IEnumerator Co_OutMagicShape()
    {
        WaitForSeconds persistTime = new WaitForSeconds(1f);

        HpBar.color = Color.magenta;
        while (isOutMagicShape)
        {
            if (currentHp - outMagicShapeDamage <= 0)
            {
                currentHp = 1f;
                break;
            }

            currentHp -= outMagicShapeDamage;
            HpBar.fillAmount = currentHp / maxHp;

            yield return persistTime;
        }
        HpBar.color = Color.white;
    }

    /// <summary>
    /// Player�� ���� ������� ���� ��Ȳ���� Ȯ���ϰ�
    /// ������� ���� ��Ȳ�̸� �������ŭ Hp�� ���ҽ�Ű�� �Լ�
    /// </summary>
    /// <param name="damage">Player�� ���� �����</param>
    /// <param name="pushed">Player�� ���������� �ƴ��� Ȯ���ϴ� �Ű�����</param>
    public void CheckDamage(float damage, bool pushed)
    {
        if (gracePeriod) return;

        currentHp -= damage;
        isPushed = pushed;
        gracePeriod = true;
        playerColor.a = 0.3f;
        this.GetComponent<SpriteRenderer>().color = playerColor;
        AudioManager.Instance.PlayEffect(hitSound);

        StartCoroutine(Cooltimer());

    }

    /// <summary>
    /// Cooltime ���� ���� ���� ����
    /// </summary>
    IEnumerator Cooltimer()
    {
        float elapsedTime = 0;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            HpBar.fillAmount = Mathf.Lerp(HpBar.fillAmount, currentHp / maxHp, elapsedTime / gracePeriodTime);
    
            if (isPushed) 
            {
                transform.Translate(Vector2.down * pushedSpeed * Time.deltaTime);
            }

            if (elapsedTime >= gracePeriodTime || currentHp <= 0) break;

            yield return null;
        }

        gracePeriod = false;
        isPushed = false;
        HpBar.fillAmount = currentHp / maxHp;
        playerColor.a = 1f;
        this.GetComponent<SpriteRenderer>().color = playerColor;

        if(currentHp <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == wallLayer)
        {
            isPushed = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == wallLayer)
        {
            isPushed = false;
        }
    }
}
