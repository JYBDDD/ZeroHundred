using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerBulletC : WeaponBase,IWeaponTrail
{
    [SerializeField]
    private TrailRenderer trailRenderer;
    private float time = 0;
    private bool timeBool = false;

    const string projectileHitPath = "Art/Sound/Effect/Player/PlayerProjectile/PlayerProjectileHit";

    private void Awake()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        AddHitAction(() => { GameManager.Resource.Instantiate(bulletHitPath, transform.position, Quaternion.identity, muzzleHitT); });
        WeaponTrail_UniRx();
    }

    private void OnEnable()
    {
        Inheritance();
    }

    private void OnDisable()
    {
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Pool.Push(gameObject);
            HitActionInvoke();

            checkBase = other.gameObject.GetComponent<BaseController>();
            GameManager.Player.playerController.Stat.AttackDamage(checkBase.Stat, 1);     // Projectile 데미지 처리
            GameManager.Sound.Play(projectileHitPath);

            checkBase.animBool = true;
            checkBase.StateHit_Enemy(other.gameObject.name);
        }

        if (other.gameObject.CompareTag("EnemyWeaponD"))            // 파괴가능한 오브젝트에 맞을시 삭제
        {
            GameManager.Pool.Push(gameObject);
            HitActionInvoke();
        }
    }

    public void WeaponTrail_UpdateNecessary()
    {
        time += Time.deltaTime;
        if (time > 0.1f)
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
