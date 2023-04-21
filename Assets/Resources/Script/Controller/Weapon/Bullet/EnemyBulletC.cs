using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyBulletC : WeaponBase,IWeaponTrail
{
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
        rigid = GetComponent<Rigidbody>();
        WeaponTrail_UniRx();
    }

    private void OnEnable()
    {
        Inheritance();
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

    public void WeaponTrail_UpdateNecessary()
    {
        time += Time.deltaTime;
        if(time > 0.1f)
        {
            trailRenderer.emitting = true;
            timeBool = true;
        }
    }

    public void WeaponTrail_UniRx()
    {
        this.UpdateAsObservable().Where(_ => timeBool == false).Subscribe(_ => WeaponTrail_UpdateNecessary());
    }
}
