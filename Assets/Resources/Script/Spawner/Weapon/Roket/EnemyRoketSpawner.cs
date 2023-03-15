using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoketSpawner : WeaponSpawnerBase
{
    int count = 0;

    protected override void Awake()
    {
        base.Awake();
        projectilePath = "Weapon/Rocket/Rocket Projectile";
    }

    private void OnEnable()
    {
        StartCoroutine(ShootRoutin());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ShootRoutin()
    {
        yield return null;

        while (true)
        {
            GameObject bullet = GameManager.Resource.Instantiate(projectilePath, transform.position, identity, enemyB_P);
            bullet.GetComponent<Movement2D>().MoveDirection(GameManager.Player.player.transform.position - transform.position);      // Projectile을 플레이어를 향하여 발사

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
