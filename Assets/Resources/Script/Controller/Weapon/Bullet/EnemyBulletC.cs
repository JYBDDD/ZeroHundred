using UnityEngine;

public class EnemyBulletC : WeaponBase
{
    public static bool bulletHitShake = false;  // ī�޶� ����ũ(Projectile)

    [SerializeField]
    private TrailRenderer trailRenderer;
    private float time = 0;
    private bool timeBool = false;

    Rigidbody rigid;        // Boss1Controller ���� ����ϴ� ���� AddForce �ʱ�ȭ ��
    AudioClip[] audioClips; // Projectile Ÿ�� ���� 4���� ���������� �����Ͽ� ������ �߻�
    const string projectileHitPath = "Art/Sound/Effect/Enemy/EnemyProjectile/EnemyProjectileHit";

    private void Awake()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        audioClips = Resources.LoadAll<AudioClip>(projectileHitPath);
        AddHitAction(() => { GameManager.Resource.Instantiate("Weapon/Bullet/PlayerHit", transform.position, Quaternion.identity, muzzleHitT); });
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
            HitActionInvoke();

            bulletHitShake = true;

            GameManager.Sound.Play(audioClips[UnityEngine.Random.Range(0, 4)]);
            PlayerControllerEx pEx = other.GetComponent<PlayerControllerEx>();
            pEx.Stat.AttackDamage(pEx.Stat, 1);       // EnemyProjectile ������ ó��
        }

        if (other.gameObject.CompareTag("PlayerGuard"))     // �÷��̾� ��ų�� �������� ����
        {
            GameManager.Pool.Push(gameObject);
            HitActionInvoke();
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
