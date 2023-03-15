using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : WeaponSpawnerBase
{
    public static float ShootRange = 4.0f;      // �߻� ����   (MissileItemC ���� �����)
    public static int ShootCount = 1;           // Projectile �߻� ����   (MissileItemC ���� �����)
    int count = 0;

    public static bool ActiveBool = false;      // MissileItemC ���� SetActive ȣ������� �����
    private int activeCount = 0;                // Count�� 1 �����Ͻ� �ѹ��� ȣ�� ��Ű���� �� (Update)

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
