using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : WeaponSpawnerBase
{
    public static float ShootRange = 4.0f;      // 발사 간격   (MissileItemC 에서 사용중)
    public static int ShootCount = 1;           // Projectile 발사 갯수   (MissileItemC 에서 사용중)
    int count = 0;

    public static bool ActiveBool = false;      // MissileItemC 에서 SetActive 호출용으로 사용중
    private int activeCount = 0;                // Count가 1 이하일시 한번만 호출 시키도록 함 (Update)

    protected override void Awake()
    {
        base.Awake();

        clipPath = "Art/Sound/Effect/Player/PlayerMissile/PlayerMissileReload";
        muzzlePath = "Weapon/Bullet/Muzzle";
        projectilePath = "Weapon/Missile/MissileAttack";
        lookQut = Quaternion.Euler(90, 0, 0);
    }

    private void OnEnable()
    {
        if (ActiveBool == true)
            StartCoroutine(ShootRoutin());
    }

    private void OnDisable()
    {
        StopCoroutine(ShootRoutin());
    }

    public void Update()
    {
        if(ActiveBool == true && activeCount < 1)
        {
            activeCount++;
            StartCoroutine(ShootRoutin());
        }
    }

    IEnumerator ShootRoutin()
    {
        yield return null;

        while (true)
        {
            GameManager.Sound.Play(clipPath);
            GameManager.Resource.Instantiate(muzzlePath, transform.position, lookQut , muzzleH_P);
            GameManager.Resource.Instantiate(projectilePath, transform.position, identity, enemyB_P);

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
