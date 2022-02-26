using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoketSpawner : MonoBehaviour
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
        yield return null;

        while (true)
        {
            GameObject bullet = GameManager.Resource.Instantiate("Weapon/Rocket/Rocket Projectile", transform.position, Quaternion.identity, GameManager.EnemyBulletParent.transform);
            bullet.GetComponent<Movement2D>().MoveDirection(GameManager.Player.player.transform.position - transform.position);      // Projectile�� �÷��̾ ���Ͽ� �߻�

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
