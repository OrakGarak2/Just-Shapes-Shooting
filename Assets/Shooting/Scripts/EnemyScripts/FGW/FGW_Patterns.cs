using System.Collections;
using UnityEngine;

public class FGW_Patterns : MonoBehaviour
{
    [Header("Enemy Patterns")]
    [SerializeField] float preGameWaitTime = 1f;
    [SerializeField] Cooking cooking;
    [SerializeField] GameObject objNumber;
    [SerializeField] GameObject EnemyBulletSpawnPointGroup;

    [SerializeField] GameObject intermediatePoint;
    [SerializeField] Vector2 fixedPos = Vector2.zero;

    [Header("Enemy Hp")]
    [SerializeField] EnemyHp enemyHp;
    [SerializeField] float healthRatio;

    [Header("Enemy Phase")]
    [SerializeField] float phase2Hp = 0.5f;
    [SerializeField] bool onNumber = false;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip phaseChangeClip;

    [Header("Enemy Items")]
    [SerializeField] GameObject pan;

    void Start()
    {
        cooking = GetComponent<Cooking>();
        enemyHp = GetComponent<EnemyHp>();

        animator = GetComponent<Animator>();

        StartCoroutine(Co_StartPattern());
    }

    IEnumerator Co_StartPattern()
    {
        yield return new WaitForSeconds(preGameWaitTime);

        cooking.enabled = true;

        while (!onNumber)
        {
            healthRatio = enemyHp.enemyHp / enemyHp.maxEnemyHp;

            if (healthRatio <= phase2Hp)
            {
                animator.SetBool("isPhaseChange", true);
                cooking.enabled = false;
                pan.SetActive(false);

                yield return new WaitForSeconds(phaseChangeClip.length);

                transform.position = fixedPos;
                animator.SetBool("isPhaseChange", false);

                objNumber.SetActive(true);

                onNumber = true;
                EnemyBulletSpawnPointGroup.SetActive(true);
                intermediatePoint.SetActive(false);
            }

            yield return null;
        }
    }

    void PosFix()
    {
        fixedPos = transform.position;
    }
}
