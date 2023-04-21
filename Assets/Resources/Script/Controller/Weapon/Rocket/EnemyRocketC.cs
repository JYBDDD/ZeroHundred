using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketC : WeaponBase
{
    [SerializeField]
    private TrailRenderer trailRenderer;
    private float time = 0;
    private bool timeBool = false;

    Rigidbody rigid;        // Boss1Controller ���� ����ϴ� ���� AddForce �ʱ�ȭ ��
    const string roketHitPath = "Weapon/Rocket/RocketExplosion";
    const string roketHitSoundPath = "Art/Sound/Effect/Enemy/EnemyRoket/EnemyRoketExplosion";

    private void Awake()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void Inheritance()
    {
        base.Inheritance();
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        trailRenderer.emitting = false;
        Inheritance();
    }

    private void OnDisable()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0, 0));      // �̵����� �ʱ�ȭ
        time = 0;
        timeBool = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void DamageProcess(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Player"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate(roketHitPath, transform.position, Quaternion.identity, muzzleHitT);

            GameManager.Sound.Play(roketHitSoundPath);
            GameManager.Player.playerController.Stat.AttackDamage(obj.GetComponent<PlayerControllerEx>().Stat, 10);        // ���� ������ ó��
        }

        if (obj.CompareTag("PlayerProjectile") || obj.CompareTag("Missile")) // �÷��̾� projectile , Missile �� �¾����� ����
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate(roketHitPath, transform.position, Quaternion.identity, muzzleHitT);
            GameManager.Sound.Play(roketHitSoundPath);
        }
    }

    private void Update()
    {
        if (time < 0.1f)
            time += Time.deltaTime;
        if (time > 0.1f && timeBool == false)
        {
            trailRenderer.emitting = true;
            timeBool = true;
        }
    }
}
