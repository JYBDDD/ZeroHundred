using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletSpawner : MonoBehaviour
{
    public float shootRange = 0.5f;

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
                GameManager.Sound.Play("Art/Sound/Effect/Player/PlayerProjectile/PlayerProjectileShot");

            GameManager.Resource.Instantiate("Weapon/Bullet/PlayerProjectile", transform.position, Quaternion.identity, GameManager.PlayerBulletParent.transform);
            yield return new WaitForSeconds(shootRange);
        }
    }
}
