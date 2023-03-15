using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    [SerializeField]
    private float shootRange;       // �߻� ����
    [SerializeField]
    private int shootCount;         // Projectile �߻� ����
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
        while (true)
        {
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyProjectile/EnemyProjectileShot");
            GameManager.Resource.Instantiate("Weapon/Bullet/Muzzle", transform.position, Quaternion.Euler(90, 0, 0), GameManager.MuzzleOfHitParent.transform);
            GameObject bullet = GameManager.Resource.Instantiate("Weapon/Bullet/EnemyProjectile", transform.position, Quaternion.identity, GameManager.EnemyBulletParent.transform);
            Vector3 targetVec = GameManager.Player.player.transform.position - transform.position;      // �÷��̾� ��ġ��

            Movement2D twoD = bullet.GetComponent<Movement2D>();
            if(twoD != null)
                twoD.MoveDirection(targetVec);      // Projectile�� �÷��̾ ���Ͽ� �߻�

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
