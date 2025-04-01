using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Filled Image를 사용하여 씬을 로드하는 클래스.
public class LoadSceneManager : MonoBehaviour
{
    // 다음에 로드할 씬의 이름을 저장하는 정적 변수.
    public static string nextScene;

    // 에디터에서 Filled Image UI 요소를 연결하기 위한 직렬화된 필드.
    [SerializeField] Image progressBar;

    // 페이드 효과를 위한 CanvasGroup을 연결하기 위한 직렬화된 필드.
    [SerializeField] CanvasGroup fadeCanvasGroup;

    // 페이드 인/아웃 지속 시간을 설정하기 위한 직렬화된 필드.
    [SerializeField] float fadeDuration = 1.0f;

    private void Awake()
    {
        // 비동기 씬 로드를 시작하는 코루틴을 시작함.
        StartCoroutine(LoadScene());
    }

    // 다음 씬을 설정하고 로딩 씬으로 전환하는 정적 메서드.
    public static void LoadScene(string sceneName)
    {
        // 다음에 로드할 씬의 이름을 설정함.
        nextScene = sceneName;
        // Filled Image를 표시하기 위해 로딩 씬을 로드함.
        SceneManager.LoadScene("99.LoadingScene");
    }

    // 다음 씬을 비동기적으로 로드하는 코루틴.
    IEnumerator LoadScene()
    {
        // 페이드 아웃 효과를 실행함.
        yield return StartCoroutine(FadeOut());

        // 씬이 로드되기 시작했는지 확인하기 위해 한 프레임을 기다림.
        yield return new WaitForSeconds(0.5f);

        // 다음 씬을 비동기적으로 로드하고 자동 활성화를 방지함.
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        // Filled Image 애니메이션을 제어하기 위한 타이머.
        float timer = 0.0f;

        // 씬이 로드될 때까지 루프를 반복함.
        while (!op.isDone)
        {
            // 다음 프레임을 기다림.
            yield return null;
            // 마지막 프레임 이후 경과된 시간만큼 타이머를 증가시킴.
            timer += Time.deltaTime;

            // 씬이 90% 로딩 진행도에 도달하지 않은 경우.
            if (op.progress < 0.9f)
            {
                // 로딩 진행도에 맞춰 Filled Image 채우기 양을 부드럽게 애니메이션화함.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                // 채우기 양이 로딩 진행도에 도달하면 타이머를 리셋함.
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else // 씬이 거의 로드 완료된 경우 (90% 이상).
            {
                // Filled Image 채우기 양을 최대로 부드럽게 애니메이션화함.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                // Filled Image가 가득 차면 페이드 인 효과를 실행하고 씬 활성화를 허용한 후 루프를 종료함.
                if (progressBar.fillAmount == 1.0f)
                {
                    yield return new WaitForSeconds(0.5f);
                    yield return StartCoroutine(FadeIn());
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    // 페이드 인 효과를 위한 코루틴.
    IEnumerator FadeIn()
    {
        // 페이드 인을 시작할 때 캔버스 그룹의 알파 값을 1로 설정함.
        fadeCanvasGroup.alpha = 1f;
        float timer = 0f;

        // 설정된 페이드 인 지속 시간 동안 알파 값을 0으로 감소시킴.
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - (timer / fadeDuration);
            yield return null;
        }

        // 페이드 인이 완료되면 알파 값을 0으로 설정함.
        fadeCanvasGroup.alpha = 0f;
    }

    // 페이드 아웃 효과를 위한 코루틴.
    IEnumerator FadeOut()
    {
        // 페이드 아웃을 시작할 때 캔버스 그룹의 알파 값을 0으로 설정함.
        fadeCanvasGroup.alpha = 0f;
        float timer = 0f;

        // 설정된 페이드 아웃 지속 시간 동안 알파 값을 1로 증가시킴.
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = timer / fadeDuration;
            yield return null;
        }

        // 페이드 아웃이 완료되면 알파 값을 1로 설정함.
        fadeCanvasGroup.alpha = 1f;
    }
}