using System.Collections;
using JustShapesAndShooting.ObjectPool;
using UnityEngine;

public class SolarEnergy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1f; // ������ ��ȭ�� �ɸ��� �ð�
    [SerializeField] private float rotateDuration = 1f; // ȸ���� �ɸ��� �ð�

    [Header("SolarEnergy")]
    [SerializeField] private float solarEnergyDamage = 30f; // Player���� ���� �����
    [SerializeField] private float solarEnergySpeed = 10f; // SolarEnergy�� �̵� �ӵ�

    private Transform playerTransform;
    private Quaternion beginningRotate;
    private SpriteRenderer spriteRenderer;

    private int playerLayer;
    private int wallLayer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameManager.Instance.trPlayer;
        beginningRotate = transform.rotation;

        playerLayer = LayerMask.NameToLayer("Player");
        wallLayer = LayerMask.NameToLayer("Wall");
    }

    void OnEnable()
    {
        StartCoroutine(Invigoration());
    }

    /// <summary>
    /// ������Ʈ�� Fade In�ϰ�, Player�� �ٶ󺸰� ����� �ڷ�ƾ
    /// </summary>
    private IEnumerator Invigoration()
    {
        float elapsedTime = 0f; // ��� �ð�
        Color color = spriteRenderer.color;
        color.a = 0; // ������ 0���� �ʱ�ȭ
        spriteRenderer.color = color; // ��������Ʈ �������� ����

        Quaternion initialRotation = transform.rotation; // �ʱ� ȸ���� ����
        Vector2 direction = (playerTransform.position - transform.position).normalized; // Player�� ���� ���� ���
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction); // ��ǥ ȸ���� ���

        while (elapsedTime < fadeDuration/* && elapsedTime < rotateDuration*/)
        {
            elapsedTime += Time.deltaTime; // ��� �ð� ����
            
            if(elapsedTime < fadeDuration)
            {    
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // color.a�� ���� 0�� 1 ���̿� �ִ��� Ȯ���ϰ� ��� ��, 0 �Ǵ� 1�� ��ȯ
                                                                     // ������ ���
                spriteRenderer.color = color; // ��������Ʈ �������� ����
            }
            if (elapsedTime < rotateDuration)
            {
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotateDuration); // ȸ���� ���
            }

            yield return null;
        }

        StartCoroutine(SolarEnergyMove()); // SolarEnergy �̵� ����
    }

    /// <summary>
    /// SolarEnergy �̵� �ڷ�ƾ
    /// </summary>
    IEnumerator SolarEnergyMove()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(Vector2.up * solarEnergySpeed * Time.deltaTime); // SolarEnergy �̵�

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == playerLayer)
        {
            col.GetComponent<PlayerHp>().CheckDamage(solarEnergyDamage, false); // Player���� ���ظ� ����
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.layer == wallLayer)
        {
            ObjectPoolManager.Instance.Release(PoolEnum.SolarEnergy, gameObject);
        }
    }
}