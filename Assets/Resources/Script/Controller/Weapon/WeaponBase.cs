using System;
using UnityEngine;

public class WeaponBase : MonoBehaviour//,IHit
{
    protected BaseController checkBase;
    protected Action hitAction;

    [Header("MuzzleHit")]
    protected Transform muzzleHitT;
    protected string bulletHitPath;


    // ��� ���Ⱑ �θ�� ����ִ� WeaponBase�� Weapon[]�� ���� ����Ҽ��ִ� ������ ���
    // ���� ���۽� WeaponBase�� �ڽ� Ŭ������ �ϳ� ���� �̸� SerializeField������ ������ String������ ���⸦ �Ǻ��Ͽ� �����Ű���� �Ұ� 
    // ������ �ƴ� ��� ����ü�� �ش� ������ ��� �����ϸ�
    // ������ ���ԵǾ��ִ� ������ ���������� �����Ų��(������ BossController ���� ����) 

    protected virtual void Initialize()
    {
        muzzleHitT = GameManager.MuzzleOfHitParent.transform;
        bulletHitPath = "Weapon/Bullet/BulletHit";
    }

    protected void AddHitAction(Action action)
    {
        if(hitAction == null)
            hitAction += action;
    }

    protected void HitActionInvoke()
    {
        hitAction.Invoke();
    }

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
