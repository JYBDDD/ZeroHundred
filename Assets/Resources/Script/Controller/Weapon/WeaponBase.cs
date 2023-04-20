using System;
using UnityEngine;

public class WeaponBase : MonoBehaviour//,IHit
{
    protected BaseController checkBase;
    protected Action hitAction;

    [Header("MuzzleHit")]
    protected Transform muzzleHitT;
    protected string bulletHitPath;


    // 모든 무기가 부모로 삼고있는 WeaponBase에 Weapon[]로 적이 사용할수있는 패턴을 담고
    // 게임 시작시 WeaponBase의 자식 클래스를 하나 만들어서 미리 SerializeField값으로 지정한 String값으로 무기를 판별하여 실행시키도록 할것 
    // 보스가 아닌 모든 적객체는 해당 웨폰을 모두 실행하며
    // 보스는 삽입되어있는 패턴을 랜덤값으로 실행시킨다(기존의 BossController 로직 존재) 

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
