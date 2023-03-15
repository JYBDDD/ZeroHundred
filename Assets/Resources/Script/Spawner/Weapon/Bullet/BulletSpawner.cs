using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : WeaponSpawnerBase
{
    public static float ShootRange = 0.5f;       // 발사 간격   (BulletItemC 에서 사용중)

    protected override void Awake()
    {
        base.Awake();

        clipPath = "Art/Sound/Effect/Player/PlayerProjectile/PlayerProjectileShot";
        muzzlePath = "Weapon/Bullet/Muzzle";
        projectilePath = "Weapon/Bullet/PlayerProjectile";
        lookQut = Quaternion.Euler(-90, 0, 0);
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
        yield return null;

        while (true)
        {
            GameManager.Sound.Play(clipPath);
            GameManager.Resource.Instantiate(muzzlePath, transform.position, lookQut, muzzleH_P);
            GameManager.Resource.Instantiate(projectilePath, transform.position, identity, playerB_P);
            yield return new WaitForSeconds(shootRange);
        }
    }
}
