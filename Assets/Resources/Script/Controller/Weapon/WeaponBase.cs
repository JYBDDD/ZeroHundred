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

            // ������ ó��
            DamageProcess(other);

            // Hp�� 0�ϰ�� ����
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

    /// <summary>
    /// �ǰ� ��ü ������ ó��
    /// </summary>
    protected virtual void DamageProcess(Collider other) {}

    /// <summary>
    /// HP�� 0���� üũ
    /// </summary>
    private bool HPZeroCheck(BaseController bc)
    {
        if (bc.Stat.Hp <= 0)
            return true;
        else
            return false;
    }
}
