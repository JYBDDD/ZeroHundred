using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBase : WeaponBase
{
    AudioClip[] enemyClips;
    AudioClip playerClip;

    protected void Awake()
    {
        enemyClips = Resources.LoadAll<AudioClip>("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileHit");
        playerClip = Resources.Load<AudioClip>("Art/Sound/Effect/Player/PlayerMissile/PlayerMissileHit");
    }

    protected void EnemyMissileProcess(Collider other) 
    {
        if (other.transform.CompareTag("Player"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.Euler(-90, 0, 0), GameManager.MuzzleOfHitParent.transform);

            EnemyMissileC.MissileHitShake = true;

            GameManager.Sound.Play(enemyClips[Random.Range(0, 3)]);
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 4);       // BossMissile 데미지 처리
        }
        if (other.transform.CompareTag("PlayerProjectile") || other.transform.CompareTag("Missile"))      // 유도미사일은 플레이어가 발사하는 Projectile이 삭제되지 않고 계속 유지됨
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            if (Random.Range(0, 3) >= 2)      // 소리가 너무 겹치지 않도록 랜덤으로 소리생성이 되지 않도록 설정
                GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion");
        }
    }

    protected virtual void PlayerMissileProcess(Collider other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/MissileHit", transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            GameManager.Sound.Play(playerClip);
            checkBase = other.gameObject.GetComponent<BaseController>();

            checkBase.Stat.AttackDamage(other.gameObject.GetComponent<BaseController>().Stat, 2);     // Missile 데미지 처리
            checkBase.animBool = true;
            checkBase.StateHit_Enemy(other.gameObject.name);
        }
        if (other.gameObject.CompareTag("EnemyWeaponD"))        // 파괴가능한 오브젝트에 맞을시 삭제
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/MissileHit", transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
        }

    }
}
