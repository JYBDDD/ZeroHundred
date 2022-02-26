using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileSpawner : MonoBehaviour
{
    [SerializeField]
    private float shootRange;       // 발사 간격
    [SerializeField]
    private int shootCount;         // Projectile 발사 갯수
    int count = 0;

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
            GameManager.Resource.Instantiate("Weapon/Bullet/Muzzle", transform.position, Quaternion.Euler(-90, 0, 0), GameManager.MuzzleOfHitParent.transform);
            GameObject bullet = GameManager.Resource.Instantiate("Weapon/Missile/BossMissileAttack", transform.position, Quaternion.Euler(-90, 0, 0), GameManager.EnemyBulletParent.transform);

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
