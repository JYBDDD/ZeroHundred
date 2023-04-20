using UnityEngine;
using Path;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake shakeCam;

    public Camera mainCamera;
    Vector3 cameraPos;

    [SerializeField, Range(0.01f, 0.1f)]
    float shakeRange = 0.05f;
    float originRange = 0.0f;           // 쉐이크 기본값
    [SerializeField, Range(0.1f, 1f)]
    float duration = 0.1f;
    float originDuration = 0.0f;        // 지속시간 기본값
    Coroutine _cor = null;

    private void Awake()
    {
        shakeCam = this;
        cameraPos = mainCamera.transform.position;
        originRange = shakeRange;
        originDuration = duration;

        GameManager.Sound.Play(SceneSound_P.GameSceneBGM, Define.Sound.bgm);
    }

    public void HitPlayer_Shake()
    {
        if (_cor != null)
            StopCoroutine(_cor);

        StartCoroutine(StartShake());
    }

    IEnumerator StartShake()
    {
        int rand = UnityEngine.Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                shakeRange = 0.075f;
                duration = 0.15f;
                break;
            case 1:
                shakeRange = 0.07f;
                duration = 0.14f;
                break;
            case 2:
                shakeRange = 0.065f;
                duration = 0.13f;
                break;
            default:
                break;
        }
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;

        float time = 0;

        while (true)
        {
            time += Time.deltaTime;
            if (time > duration)
            {
                StopShake();
                yield break;
            }

            yield return null;
        }
    }

    private void StopShake()
    {
        StopAllCoroutines();
        _cor = null;
        mainCamera.transform.position = cameraPos;
        shakeRange = originRange;
        duration = originDuration;
    }
}
