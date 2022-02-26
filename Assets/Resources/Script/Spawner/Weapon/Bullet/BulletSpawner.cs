using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static float shootRange = 0.5f;       // 발사 간격   (BulletItemC 에서 사용중)

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
            GameManager.Sound.Play("Art/Sound/Effect/Player/PlayerProjectile/PlayerProjectileShot");
            GameManager.Resource.Instantiate("Weapon/Bullet/Muzzle", transform.position, Quaternion.Euler(-90,0,0),GameManager.MuzzleOfHitParent.transform);
            GameManager.Resource.Instantiate("Weapon/Bullet/PlayerProjectile", transform.position, Quaternion.identity, GameManager.PlayerBulletParent.transform);
            yield return new WaitForSeconds(shootRange);
        }
    }
}
