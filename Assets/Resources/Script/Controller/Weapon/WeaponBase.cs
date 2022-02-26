using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    /// <summary>
    /// ���� �Ÿ� �̻� �־����� ������Ʈ ����
    /// </summary>
    protected void WeaponDestroyD()
    {
        float cameraDistance = (Camera.main.transform.position - transform.position).magnitude;

        if (cameraDistance >= 11.5f | GameManager.Player.playerController.Stat.Hp <= 0)
        {
            GameManager.Pool.Push(gameObject);
        }
    }
}
