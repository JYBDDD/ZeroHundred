using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletC : WeaponBase
{
    public static bool bulletHitShake = false;  // 카메라 쉐이크(Projectile)

    [SerializeField]
    private TrailRenderer trailRenderer;
    private float time = 0;
    private bool timeBool = false;

    Rigidbody rigid;        // Boss1Controller 에서 사용하는 패턴 AddForce 초기화 용

    AudioClip[] audioClips;         // Projectile 타격 사운드 4개를 랜덤값으로 설정하여 무작위 발생

    private void Awake()
    {
        audioClips = Resources.LoadAll<AudioClip>("Art/Sound/Effect/Enemy/EnemyProjectile/EnemyProjectileHit");
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0, 0));      // 이동방향 초기화

        trailRenderer.emitting = false;
        time = 0;
        timeBool = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void DamageProcess(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Bullet/PlayerHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            bulletHitShake = true;

            GameManager.Sound.Play(audioClips[Random.Range(0, 4)]);
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 1);       // EnemyProjectile 데미지 처리
        }

        if (other.gameObject.CompareTag("PlayerGuard"))     // 플레이어 스킬에 막히도록 설정
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Bullet/PlayerHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
        }
    }

    private void Update()
    {
        WeaponDestroyD();

        if (time < 0.1f)
            time += Time.deltaTime;
        if (time > 0.1f && timeBool == false)
        {
            trailRenderer.emitting = true;
            timeBool = true;
        }

    }
    
}
