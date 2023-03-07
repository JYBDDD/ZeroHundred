using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletC : WeaponBase
{
    public static bool bulletHitShake = false;  // ī�޶� ����ũ(Projectile)

    [SerializeField]
    private TrailRenderer trailRenderer;
    private float time = 0;
    private bool timeBool = false;

    Rigidbody rigid;        // Boss1Controller ���� ����ϴ� ���� AddForce �ʱ�ȭ ��

    AudioClip[] audioClips;         // Projectile Ÿ�� ���� 4���� ���������� �����Ͽ� ������ �߻�

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
        gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0, 0));      // �̵����� �ʱ�ȭ

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
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 1);       // EnemyProjectile ������ ó��
        }

        if (other.gameObject.CompareTag("PlayerGuard"))     // �÷��̾� ��ų�� �������� ����
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
