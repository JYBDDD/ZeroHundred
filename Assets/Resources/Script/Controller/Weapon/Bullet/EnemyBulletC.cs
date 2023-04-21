using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyBulletC : WeaponBase,IWeaponTrail
{
    [SerializeField]
    private TrailRenderer trailRenderer;
    private float time = 0;
    private bool timeBool = false;

    Rigidbody rigid;        // Boss1Controller 에서 사용하는 패턴 AddForce 초기화 용
    AudioClip[] audioClips; // Projectile 타격 사운드 4개를 랜덤값으로 설정하여 무작위 발생
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
            HitActionInvoke();

            GameManager.Sound.Play(audioClips[UnityEngine.Random.Range(0, 4)]);
            PlayerControllerEx pEx = other.GetComponent<PlayerControllerEx>();
            pEx.Stat.AttackDamage(pEx.Stat, 1);       // EnemyProjectile 데미지 처리
        }

        if (other.gameObject.CompareTag("PlayerGuard"))     // 플레이어 스킬에 막히도록 설정
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
