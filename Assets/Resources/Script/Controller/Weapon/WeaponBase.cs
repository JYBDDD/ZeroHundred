using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    /// <summary>
    /// 일정 거리 이상 멀어질시 오브젝트 삭제
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
