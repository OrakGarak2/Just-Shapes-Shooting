using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Camera m_camera;

    /// <summary>
    /// 카메라를 흔든다.
    /// </summary>
    /// <param name="duration">카메라가 흔들리는 시간</param>
    /// <param name="roughness">카메라 흔들림의 거칠기</param>
    /// <param name="magnitude">카메라의 움직임 범위</param>
    public IEnumerator Shake(float duration, float roughness, float magnitude)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f; // 흐른 시간
        float tick = Random.Range(-10f, 10f); // 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime / halfDuration;

            tick += Time.deltaTime * roughness;
            m_camera.transform.position = new Vector3(
                Mathf.PerlinNoise(tick, 0) - 0.5f, // X축에 대한 PerlinNoise값 생성(PerlinNoise는 1 ~ 0 까지의 값을 반환하기 때문에 중앙값을 0으로 맞춰주기 위해 0.5를 뺀다.)
                Mathf.PerlinNoise(0, tick) - 0.5f, // Y축에 대한 PerlinNoise값 생성(위와 동일)
                -10f) * magnitude * Mathf.PingPong(elapsed, halfDuration);    // 진동의 크기를 조정하기 위해 magnitude를 곱하고,
                                                                              // Mathf.PingPong을 통해 카메라가 상에서 하, 좌에서 우로 왔다갔다하게 만들고, 
                                                                              // 점점 진동의 폭이 좁아진다.

            yield return null;
        }

        Vector3 vector3 = m_camera.transform.position;
        vector3.z = -10f;
        m_camera.transform.position = vector3;
    }
}
