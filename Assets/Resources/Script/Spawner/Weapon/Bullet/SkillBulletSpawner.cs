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
            if (Random.Range(0, 3) >= 2)      // �Ҹ��� �ʹ� ��ġ�� �ʵ��� �������� �Ҹ������� ���� �ʵ��� ����
                GameManager.Sound.Play(clipPath);

            GameManager.Resource.Instantiate(projectilePath, transform.position, identity, playerB_P);
            yield return new WaitForSeconds(shootRange);
        }
    }
}
