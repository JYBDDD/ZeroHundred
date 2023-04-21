using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletSpawner : WeaponSpawnerBase
{
    protected override void Awake()
    {
        base.Awake();

        clipPath = "Art/Sound/Effect/Player/PlayerProjectile/PlayerProjectileShot";
        projectilePath = "Weapon/Bullet/PlayerProjectile";
        shootRange = 0.2f;
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
            if (Random.Range(0, 3) >= 2)      // 소리가 너무 겹치지 않도록 랜덤으로 소리생성이 되지 않도록 설정
                GameManager.Sound.Play(clipPath);

            GameManager.Resource.Instantiate(projectilePath, transform.position, identity, playerB_P);
            yield return new WaitForSeconds(shootRange);
        }
    }
}
