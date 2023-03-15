using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : WeaponSpawnerBase
{
    int count = 0;

    protected override void Awake()
    {
        base.Awake();

        clipPath = "Art/Sound/Effect/Enemy/EnemyProjectile/EnemyProjectileShot";
        muzzlePath = "Weapon/Bullet/Muzzle";
        projectilePath = "Weapon/Bullet/EnemyProjectile";
        lookQut = Quaternion.Euler(90, 0, 0);
    }

    private void OnEnable()
    {
        StartCoroutine(ShootRoutin());
    }

    private void OnDisable()
    {
        StopCoroutine(ShootRoutin());
    }

    IEnumerator ShootRoutin()
    {
        while (true)
        {
            GameManager.Sound.Play(clipPath);
            GameManager.Resource.Instantiate(muzzlePath, transform.position, lookQut, muzzleH_P);
            GameObject bullet = GameManager.Resource.Instantiate(projectilePath, transform.position, identity, enemyB_P);
            Vector3 targetVec = GameManager.Player.player.transform.position - transform.position;      // 플레이어 위치값

            Movement2D twoD = bullet.GetComponent<Movement2D>();
            if(twoD != null)
                twoD.MoveDirection(targetVec);      // Projectile을 플레이어를 향하여 발사

            yield return new WaitForSeconds(0.2f);
            count++;
            if (count >= shootCount)
            {
                count = 0;
                yield return new WaitForSeconds(shootRange);
            }
        }
    }
}
