using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    public float maxEnemyHp;
    public float enemyHp;
    [SerializeField] Image bossHpBar;
    [SerializeField] bool isCoroutine = false;

    public bool enemyGracePeriod = false;

    [SerializeField] AudioClip hitSound;

    private void Start()
    {
        enemyHp = maxEnemyHp;
    }

    /// <summary>
    /// Enemy�� ���ݹ޾��� �� bulletDmg��ŭ enemyHp�� ���ҽ�Ű�� �Լ�
    /// </summary>
    /// <param name="bulletDamage">bulletDmg</param>
    public void enemyAttacked(float bulletDamage)
    {
        if (enemyGracePeriod || !gameObject.activeSelf) { return; }

        enemyHp -= bulletDamage;
        AudioManager.Instance.PlayEffect(hitSound);
        
        if (!isCoroutine)
        {
            StartCoroutine(Co_HpBar());
        }
    }

    void EnemyDeath()
    {
        gameObject.SetActive(false);
        LoadSceneManager.LoadScene("02.StageSelect");
    }

    IEnumerator Co_HpBar()
    {
        isCoroutine = true;
        float elapsedTime = 0f;
        while(elapsedTime < 1f)
        {
            bossHpBar.fillAmount = Mathf.Lerp(bossHpBar.fillAmount, enemyHp / maxEnemyHp, elapsedTime / 10f);
            elapsedTime += Time.deltaTime;

            if(enemyHp <= 0 || elapsedTime >= 1f) { break; }

            yield return null;
        }

        bossHpBar.fillAmount = enemyHp / maxEnemyHp;

        if (enemyHp <= 0)
        {
            EnemyDeath();
        }
        isCoroutine = false;
    }
}
