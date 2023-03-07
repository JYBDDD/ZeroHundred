using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketC : WeaponBase
{
    public static bool RocketHitShake = false;  // 카메라 쉐이크(Projectile)

    [SerializeField]
    private TrailRenderer trailRenderer;
    private float time = 0;
    private bool timeBool = false;

    Rigidbody rigid;        // Boss1Controller 에서 사용하는 패턴 AddForce 초기화 용
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        trailRenderer.emitting = false;
    }

    private void OnDisable()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0, 0));      // 이동방향 초기화
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
            GameManager.Resource.Instantiate("Weapon/Rocket/RocketExplosion", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            RocketHitShake = true;

            GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyRoket/EnemyRoketExplosion");
            GameManager.Player.playerController.Stat.AttackDamage(other.gameObject.GetComponent<PlayerControllerEx>().Stat, 10);        // 로켓 데미지 처리
        }

        if (other.gameObject.CompareTag("PlayerProjectile") || other.gameObject.CompareTag("Missile")) // 플레이어 projectile , Missile 에 맞았을시 삭제
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Rocket/RocketExplosion", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyRoket/EnemyRoketExplosion");
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
