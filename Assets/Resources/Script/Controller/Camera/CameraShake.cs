using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 cameraPos;

    [SerializeField, Range(0.01f, 0.1f)]
    float shakeRange = 0.05f;
    float originRange = 0.0f;           // 쉐이크 기본값
    [SerializeField, Range(0.1f, 1f)]
    float duration = 0.1f;
    float originDuration = 0.0f;        // 지속시간 기본값
    float _time = 0;

    private void Awake()
    {
        cameraPos = mainCamera.transform.position;
        originRange = shakeRange;
        originDuration = duration;

        GameManager.Sound.Play("Art/Sound/BGM/GameScene_BGM", Define.Sound.bgm);
    }

    private void FixedUpdate()
    {
        if(EnemyBulletC.bulletHitShake == true | EnemyRocketC.RocketHitShake == true | EnemyMissileC.MissileHitShake == true | EnemyBombingAttack.BombingHitShake == true)
            Shake();
    }

    public void Shake()
    {
        Invoke("StartShake",duration);
    }

    private void StartShake()
    {
        if (EnemyBombingAttack.BombingHitShake == true)
        {
            shakeRange = 0.075f;
            duration = 0.15f;
        }

        if (EnemyRocketC.RocketHitShake == true)
        {
            shakeRange = 0.07f;
            duration = 0.14f;
        }

        if(EnemyMissileC.MissileHitShake == true)
        {
            shakeRange = 0.065f;
            duration = 0.13f;
        }


        _time += Time.deltaTime;

        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;

        if (_time > duration)
        {
            StopShake();
            _time = 0;
        }
    }

    private void StopShake()
    {
        CancelInvoke("StartShake");
        EnemyBulletC.bulletHitShake = false;
        EnemyRocketC.RocketHitShake = false;
        EnemyMissileC.MissileHitShake = false;
        EnemyBombingAttack.BombingHitShake = false;
        mainCamera.transform.position = cameraPos;
        shakeRange = originRange;
        duration = originDuration;

    }


}
