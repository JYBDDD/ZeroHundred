using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBase : WeaponBase
{
    AudioClip[] enemyClips;
    AudioClip playerClip;

    protected const string missileHitPath = "Weapon/Missile/MissileHit";
    protected const string enemyMissileHitPath = "Weapon/Missile/EnemyMissileHit";
    protected const string enemyMissileExplosionPath = "Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion";

    protected void Awake()
    {
        enemyClips = Resources.LoadAll<AudioClip>("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileHit");
        playerClip = Resources.Load<AudioClip>("Art/Sound/Effect/Player/PlayerMissile/PlayerMissileHit");
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected void EnemyMissileProcess(Collider other) 
    {
        Transform t = other.transform;
        if (t.CompareTag("Player"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate(enemyMissileHitPath, transform.position, Quaternion.Euler(-90, 0, 0), muzzleHitT);

            PlayerControllerEx pEx = other.GetComponent<PlayerControllerEx>();
            GameManager.Sound.Play(enemyClips[Random.Range(0, 3)]);
            pEx.Stat.AttackDamage(pEx.Stat, 4);       // BossMissile 데미지 처리
        }
        if (t.CompareTag("PlayerProjectile") || t.CompareTag("Missile"))      // 유도미사일은 플레이어가 발사하는 Projectile이 삭제되지 않고 계속 유지됨
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate(enemyMissileHitPath, transform.position, Quaternion.identity, muzzleHitT);

            if (Random.Range(0, 3) >= 2)      // 소리가 너무 겹치지 않도록 랜덤으로 소리생성이 되지 않도록 설정
                GameManager.Sound.Play(enemyMissileExplosionPath);
        }
    }

    protected virtual void PlayerMissileProcess(Collider other) 
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate(missileHitPath, transform.position, Quaternion.identity, muzzleHitT);

            GameManager.Sound.Play(playerClip);
            checkBase = obj.GetComponent<BaseController>();

            checkBase.Stat.AttackDamage(checkBase.Stat, 2);     // Missile 데미지 처리
            checkBase.animBool = true;
            checkBase.StateHit_Enemy(other.gameObject.name);
        }
        if (obj.CompareTag("EnemyWeaponD"))        // 파괴가능한 오브젝트에 맞을시 삭제
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate(missileHitPath, transform.position, Quaternion.identity, muzzleHitT);
        }

    }
}
