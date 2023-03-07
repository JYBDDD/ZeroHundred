using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        var thisComponent = other.GetComponent<BaseController>();
        if(thisComponent != null)
        {
            var thisType = thisComponent.ObjType;

            // 데미지 처리
            DamageProcess(other);

            // Hp가 0일경우 실행
            if (HPZeroCheck(thisComponent) == true)
            {
                if(thisType == Define.ObjType.Enemy)
                {
                    thisComponent.EnemyDead();
                }
                if(thisType == Define.ObjType.Player)
                {
                    thisComponent.GetComponent<PlayerControllerEx>().PlayerDead();
                }
                if(thisType == Define.ObjType.Boss)
                {
                    Boss1Controller bc = thisComponent.GetComponent<Boss1Controller>();
                    bc.BossDead();
                    bc.BossPartternStop();
                }
                return;
            }
        }
    }

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

    /// <summary>
    /// 피격 객체 데미지 처리
    /// </summary>
    protected virtual void DamageProcess(Collider other) {}

    /// <summary>
    /// HP가 0인지 체크
    /// </summary>
    private bool HPZeroCheck(BaseController bc)
    {
        if (bc.Stat.Hp <= 0)
            return true;
        else
            return false;
    }
}
