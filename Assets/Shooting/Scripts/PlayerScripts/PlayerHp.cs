using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public float currentHp;
    public float maxHp;
    public Image HpBar;

    /// <summary>
    /// MagicShape 밖으로 나갔을 때 입을 피해
    /// </summary>
    public float outMagicShapeDamage;

    /// <summary>
    /// Player가가 MagicShape 밖으로 나갔는지 여부
    /// </summary>
    private bool isOutMagicShape = false;

    /// <summary>
    /// 무적 상태 지속 시간
    /// </summary>
    float gracePeriodTime = 1.5f;

    /// <summary>
    /// 무적 상태 여부
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

        magicShapeLayer = LayerData.magicShapeLayer;
        wallLayer = LayerData.wallLayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == magicShapeLayer) // MagicShape 안으로 들어옴.
        {
            isOutMagicShape = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) // MagicShape 밖으로 나감.
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
    /// Player가 피해를 입는 것을 확인함.
    /// </summary>
    /// <param name="damage">Player가 입을 피해</param>
    /// <param name="pushed">Player가 밀쳐질 것인지 여부</param>
    public void CheckDamage(float damage, bool pushed)
    {
        if (gracePeriod) return;

        currentHp -= damage;
        isPushed = pushed;
        gracePeriod = true;
        playerColor.a = 0.3f;
        this.GetComponent<SpriteRenderer>().color = playerColor;
        AudioManager.Instance.PlayEffect(hitSound);

        StartCoroutine(UpdateHPBar());

    }

    /// <summary>
    /// HP바를 업데이트하는 코루틴
    /// </summary>
    IEnumerator UpdateHPBar()
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
            AudioManager.Instance.StopBGM();
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
