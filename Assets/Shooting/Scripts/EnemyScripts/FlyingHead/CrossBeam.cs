using System.Collections;
using UnityEngine;

public class CrossBeam : MonoBehaviour
{
    [SerializeField] GameObject xBeam;
    [SerializeField] GameObject yBeam;

    public bool isForewarning;

    [SerializeField] float nextBeamWaitTime;

    [SerializeField] Vector2 xBeamPos;
    [SerializeField] Vector2 yBeamPos;

    [SerializeField] SpriteRenderer xBeamSpriteRenderer;
    [SerializeField] SpriteRenderer yBeamSpriteRenderer;

    [SerializeField] float forewarningColorA = 0.3f;

    [SerializeField] Animator animator;

    [SerializeField] Color color;

    void Start()
    {
        xBeamSpriteRenderer = xBeam.GetComponent<SpriteRenderer>();
        yBeamSpriteRenderer = yBeam.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.enabled = false;

        StartCoroutine(Co_Fire());
    }

    IEnumerator Co_Fire()
    {
        Vector2 playerPos;
        WaitForSeconds waitForSecond = new WaitForSeconds(1f);
        WaitForSeconds waitForNextBeam = new WaitForSeconds(nextBeamWaitTime);

        while (gameObject.activeSelf)
        {
            playerPos = GameManager.Instance.trPlayer.position;

            xBeamPos = playerPos;
            xBeamPos.x = 0f;
            xBeam.transform.position = xBeamPos;

            yBeamPos = playerPos;
            yBeamPos.y = 0f;
            yBeam.transform.position = yBeamPos;

            isForewarning = true;

            color.a = forewarningColorA;
            xBeamSpriteRenderer.color = color;
            yBeamSpriteRenderer.color = color;

            xBeam.SetActive(true);
            yBeam.SetActive(true);

            yield return waitForSecond;

            isForewarning = false;

            color.a = 1f;
            xBeamSpriteRenderer.color = color;
            yBeamSpriteRenderer.color = color;
            animator.enabled = true;

            yield return waitForNextBeam;
        }
    }

    void AnimatorInactivation()
    {
        animator.enabled = false;
        xBeam.SetActive(false);
        yBeam.SetActive(false);
    }
}
