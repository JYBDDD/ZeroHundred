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
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 4);       // BossMissile ������ ó��
        }
        if (other.transform.CompareTag("PlayerProjectile") || other.transform.CompareTag("Missile"))      // �����̻����� �÷��̾ �߻��ϴ� Projectile�� �������� �ʰ� ��� ������
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            if (Random.Range(0, 3) >= 2)      // �Ҹ��� �ʹ� ��ġ�� �ʵ��� �������� �Ҹ������� ���� �ʵ��� ����
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

            checkBase.Stat.AttackDamage(other.gameObject.GetComponent<BaseController>().Stat, 2);     // Missile ������ ó��
            checkBase.animBool = true;
            checkBase.StateHit_Enemy(other.gameObject.name);
        }
        if (other.gameObject.CompareTag("EnemyWeaponD"))        // �ı������� ������Ʈ�� ������ ����
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/MissileHit", transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
        }

    }
}
