using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : WeaponSpawnerBase
{
    int count = 0;

    protected override void Awake()
    {
        base.Awake();

        muzzlePath = "Weapon/Bullet/Muzzle";
        projectilePath = "Weapon/Missile/EnemyMissileAttack";
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
        yield return null;

        while (true)
        {
            GameManager.Resource.Instantiate(muzzlePath, transform.position, lookQut, muzzleH_P);
            GameManager.Resource.Instantiate(projectilePath, transform.position, lookQut, enemyB_P);

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
