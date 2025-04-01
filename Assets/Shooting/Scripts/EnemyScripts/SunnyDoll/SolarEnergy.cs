using System.Collections;
using JustShapesAndShooting.ObjectPool;
using UnityEngine;

public class SolarEnergy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1f; // 나타나는 시간
    [SerializeField] private float rotateDuration = 1f; // 회전하는 시간

    [Header("SolarEnergy")]
    [SerializeField] private float solarEnergyDamage = 30f;
    [SerializeField] private float solarEnergySpeed = 10f;

    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    private int playerLayer;
    private int wallLayer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameManager.Instance.trPlayer;

        playerLayer = LayerMask.NameToLayer("Player");
        wallLayer = LayerMask.NameToLayer("Wall");
    }

    void OnEnable()
    {
        StartCoroutine(Invigoration());
    }

    /// <summary>
    /// SolarEnergy가 Fade In하고, Player를 바라보게 만드는 코루틴
    /// </summary>
    private IEnumerator Invigoration()
    {
        float elapsedTime = 0f; // 경과 시간
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        Quaternion initialRotation = transform.rotation; // 초기 회전값 저장
        Vector2 direction = (playerTransform.position - transform.position).normalized; // Player를 향하는 벡터 계산
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction); // Player를 바라보는 각도 계산

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            
            if(elapsedTime < fadeDuration)
            {    
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // Fade In
                spriteRenderer.color = color;
            }
            if (elapsedTime < rotateDuration)
            {
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotateDuration); // 부드럽게 회전전
            }

            yield return null;
        }

        StartCoroutine(SolarEnergyMove()); // SolarEnergy 발사
    }

    /// <summary>
    /// SolarEnergy 발사
    /// </summary>
    IEnumerator SolarEnergyMove()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(Vector2.up * solarEnergySpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == playerLayer)
        {
            col.GetComponent<PlayerHp>().CheckDamage(solarEnergyDamage, false);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.layer == wallLayer)
        {
            ObjectPoolManager.Instance.Release(PoolEnum.SolarEnergy, gameObject);
        }
    }
}