using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberHammerAttack : MonoBehaviour
{
    [SerializeField] List<Sprite> numberList = new List<Sprite>();
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float numberChangingTime = 5f;

    [SerializeField] Transform hammer;
    [SerializeField] GameObject warning;
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;

    [SerializeField] float rotateSpeed = 500f;

    [SerializeField] AudioClip numberChangeSound;
    [SerializeField] AudioClip numberConfirmSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPos = hammer.position;
        endPos = new Vector2(startPos.x, -startPos.y);

        numberChangingTime = 2f;

        StartCoroutine(Co_HammerAttack());
    }

    IEnumerator Co_HammerAttack()
    {
        while(gameObject.activeSelf)
        {
            hammer.position = startPos;
            hammer.rotation = Quaternion.identity;

            float timer = 0f;
            int numCount = Random.Range(1, numberList.Count);

            float waitTime = 10f * Time.deltaTime;
            WaitForSeconds wait = new WaitForSeconds(waitTime);

            AudioManager.Instance.SetEffect(numberChangeSound);

            while (timer < numberChangingTime)
            {
                numCount++;
                if (numCount >= numberList.Count)
                {
                    numCount = 1;
                }

                spriteRenderer.sprite = numberList[numCount];
                AudioManager.Instance.PlayEffect();

                timer += waitTime;

                yield return wait;
            }

            AudioManager.Instance.PlayEffect(numberConfirmSound);
            warning.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            warning.SetActive(false);

            timer = 0f;
            float overallTime = (float)(10 - numCount) * 5f;
            hammer.GetComponent<EnemyBump>().bumpDamage = numCount * 10;

            while (timer < 1f)
            {
                timer += Time.deltaTime;
                hammer.position = Vector2.Lerp(hammer.position, endPos, timer / overallTime);
                hammer.Rotate(0, 0, numCount * rotateSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}
