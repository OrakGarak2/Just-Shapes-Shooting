using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class FlyingHeadPatterns : MonoBehaviour
{
    [Header("Enemy Patterns")]
    [SerializeField] float preGameWaitTime;
    [SerializeField] float nextAttackWaitTime;
    [SerializeField] int beforeRan = 0;
    [SerializeField] GameObject crossBeam;
    [SerializeField] GameObject forewarnningUltraBeam;
    [SerializeField] AudioClip beamSound;
    [SerializeField] AudioClip headFireSound;

    [Header("Enemy Hp")]
    [SerializeField] EnemyHp enemyHp;
    [SerializeField] float healthRatio;

    [Header("Enemy Phase")]
    [SerializeField] float phase2Hp = 0.5f;
    [SerializeField] bool onPhase2 = false;

    [Header("Camera Shake")]
    [SerializeField] CameraShake cameraShake;
    [SerializeField] float duration;    // 카메라가 흔들리는 시간
    [SerializeField] float roughness;   // 카메라 흔들림의 거칠기
    [SerializeField] float magnitude;   // 카메라의 움직임 범위

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip clipHeadFire;
    [SerializeField] AnimationClip clipUltraBeam;
    [SerializeField] int spinId;
    [SerializeField] int headFireId;

    [Header("Player Side")]
    [SerializeField] PlayerMove playerMove;
    [SerializeField] GameObject Spawner;
    [SerializeField] float speedMagnification = 5f;

    void Start()
    {
        enemyHp = GetComponent<EnemyHp>();
        animator = GetComponent<Animator>();
        cameraShake = GetComponent<CameraShake>();
        playerMove = GameManager.Instance.trPlayer.GetComponent<PlayerMove>();

        spinId = Animator.StringToHash("Spin");
        headFireId = Animator.StringToHash("HeadFire");

        StartCoroutine(Co_StartPattern());
    }

    IEnumerator Co_StartPattern()
    {
        yield return new WaitForSeconds(preGameWaitTime);

        healthRatio = enemyHp.enemyHp / enemyHp.maxEnemyHp;

        crossBeam.SetActive(true);

        int hideId = Animator.StringToHash("Hide");

        animator.SetTrigger(hideId);

        float timer = 0;

        while (!onPhase2)
        {
            while (timer < nextAttackWaitTime)
            {
                timer += Time.deltaTime;

                healthRatio = enemyHp.enemyHp / enemyHp.maxEnemyHp;

                if (healthRatio <= phase2Hp)
                {
                    onPhase2 = true;
                    break;
                }

                yield return null;
            }

            timer = 0;

            animator.SetTrigger(hideId);

            yield return null;
        }
    }

    IEnumerator Co_UltraBeamForewarning()
    {
        forewarnningUltraBeam.SetActive(true);

        yield return new WaitForSeconds(2f);

        forewarnningUltraBeam.SetActive(false);
        enemyHp.enemyGracePeriod = false;
        AudioManager.Instance.PlayEffect(beamSound);
        animator.SetTrigger("Fire");
    }

    void HideTo()
    {
        if (onPhase2)
        {
            StartCoroutine(Co_UltraBeamForewarning());
        }
        else
        {
            animator.SetTrigger(spinId);
        }
    }

    void Fire()
    {
        int ran;

        do
        {
            ran = Random.Range(0, 4);
        } while (beforeRan == ran);
        beforeRan = ran;

        animator.SetInteger(headFireId, ran);

        AudioManager.Instance.PlayEffect();
    }

    void UltraBeam()
    {
        duration = clipUltraBeam.length;
        StartCoroutine(cameraShake.Shake(duration, roughness, magnitude));
    }

    void Pahse2()
    {
        Spawner.GetComponent<MagicShapeSpawn>().curMagicShape.SetActive(false);
        playerMove.moveSpeed *= speedMagnification;
        GameManager.Instance.trPlayer.GetComponent<TrailRenderer>().enabled = true;
        AudioManager.Instance.SetEffect(headFireSound);
    }
}
