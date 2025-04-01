using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Camera m_camera;

    /// <summary>
    /// ī�޶� ����.
    /// </summary>
    /// <param name="duration">ī�޶� ��鸮�� �ð�</param>
    /// <param name="roughness">ī�޶� ��鸲�� ��ĥ��</param>
    /// <param name="magnitude">ī�޶��� ������ ����</param>
    public IEnumerator Shake(float duration, float roughness, float magnitude)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f; // �帥 �ð�
        float tick = Random.Range(-10f, 10f); // 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime / halfDuration;

            tick += Time.deltaTime * roughness;
            m_camera.transform.position = new Vector3(
                Mathf.PerlinNoise(tick, 0) - 0.5f, // X�࿡ ���� PerlinNoise�� ����(PerlinNoise�� 1 ~ 0 ������ ���� ��ȯ�ϱ� ������ �߾Ӱ��� 0���� �����ֱ� ���� 0.5�� ����.)
                Mathf.PerlinNoise(0, tick) - 0.5f, // Y�࿡ ���� PerlinNoise�� ����(���� ����)
                -10f) * magnitude * Mathf.PingPong(elapsed, halfDuration);    // ������ ũ�⸦ �����ϱ� ���� magnitude�� ���ϰ�,
                                                                              // Mathf.PingPong�� ���� ī�޶� �󿡼� ��, �¿��� ��� �Դٰ����ϰ� �����, 
                                                                              // ���� ������ ���� ��������.

            yield return null;
        }

        Vector3 vector3 = m_camera.transform.position;
        vector3.z = -10f;
        m_camera.transform.position = vector3;
    }
}
