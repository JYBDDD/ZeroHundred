using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletC : WeaponBase
{
    [SerializeField]
    private TrailRenderer trailRenderer;
    private float time = 0;
    private bool timeBool = false;

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
            GameManager.Resource.Instantiate("Weapon/Bullet/BulletHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            GameManager.Player.playerController.Stat.AttackDamage(other.gameObject.GetComponent<BaseController>().Stat, 1);     // Projectile 데미지 처리
            GameManager.Sound.Play("Art/Sound/Effect/Player/PlayerProjectile/PlayerProjectileHit");
            other.gameObject.GetComponent<BaseController>().animBool = true;
        }

        if (other.gameObject.CompareTag("EnemyWeaponD"))            // 파괴가능한 오브젝트에 맞을시 삭제
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Bullet/BulletHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
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
