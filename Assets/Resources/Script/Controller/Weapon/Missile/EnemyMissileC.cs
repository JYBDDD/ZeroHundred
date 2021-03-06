using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileC : WeaponBase
{
    public static bool MissileHitShake = false;

    private float speed = 0.0f;             // 유도미사일에 Movement2D 사용하지 않고 자체적으로 구현함
    Vector3 lookVec = Vector3.zero;
    private float waitTime = 0.0f;

    AudioClip[] audioClips;         // 미사일 폭발 사운드 3개를 랜덤값으로 설정하여 무작위 발생

    private void Awake()
    {
        audioClips = Resources.LoadAll<AudioClip>("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileHit");
    }

    private void OnEnable()
    {
        waitTime = 0;
        speed = 0;
        GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileShot");
    }

    private void FixedUpdate()
    {
        WeaponDestroyD();
        GuidedMissile();
    }

    private void GuidedMissile()        // 유도 미사일   
    {
        if(transform.position.z > 0.45f)        // 맵 뒤로 이동시 폭발후 즉시 삭제
        {
            if (Random.Range(0, 3) >= 2)      // 소리가 너무 겹치지 않도록 랜덤으로 소리생성이 되지 않도록 설정
                GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion");
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
            return;
        }

        waitTime += Time.deltaTime;
        if(waitTime < 1.5f)     // 1.5초간 미사일 일정 속력 유지
        {
            speed = Time.deltaTime;
            transform.Translate(transform.forward * speed, Space.World);         // forward 방향으로 이동
        }
        else                    // 1.5초후 속력 계속 증가
        {
            speed += Time.deltaTime / 10f;
            transform.Translate(transform.forward * speed, Space.World);       
        }

        lookVec = GameManager.Player.player.transform.position - transform.position;

        Quaternion qut = Quaternion.LookRotation(lookVec);          // 미사일 forward는 타겟을 향함
        transform.rotation = Quaternion.Slerp(transform.rotation, qut, Time.deltaTime * 2f);        // 방향이 꺽이는 값을 Slerp로 조절
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.Euler(-90,0,0), GameManager.MuzzleOfHitParent.transform);

            MissileHitShake = true;

            GameManager.Sound.Play(audioClips[Random.Range(0,3)]);
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 4);       // EnemyMissile 데미지 처리
        }
        if(other.transform.CompareTag("PlayerProjectile") | other.transform.CompareTag("Missile"))      // 유도미사일이 플레이어가 발사하는 Projectile이 삭제되지 않고 계속 유지됨
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            if (Random.Range(0, 3) >= 2)      // 소리가 너무 겹치지 않도록 랜덤으로 소리생성이 되지 않도록 설정
                GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion");
        }
    }

}
