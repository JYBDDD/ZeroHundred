using System;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected BaseController checkBase;
    protected Action hitAction;

    [Header("MuzzleHit")]
    protected Transform muzzleHitT;
    protected string bulletHitPath;

    protected virtual void Initialize()
    {
        muzzleHitT = GameManager.MuzzleOfHitParent.transform;
        bulletHitPath = "Weapon/Bullet/BulletHit";
    }

    protected void Inheritance()
    {
        GameManager.Resource.DestroyObject_UniRx(this, gameObject);
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

            // 카메라 쉐이크 처리
            if(other.gameObject.CompareTag("Player") && !gameObject.name.Contains("Player"))
            {
                CameraShake.shakeCam.HitPlayer_Shake();
            }

            // Hp가 0일경우 실행
            if (HPZeroCheck(thisComponent) == true)
            {
                if(thisType == Define.ObjType.Enemy)
                {
                    thisComponent.EnemyDead();
                }
                if(thisType == Define.ObjType.Player)
                {
                    GameManager.Instance.EndGame_NoticeObserver();
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
    protected void WeaponDestroyD<T>(T o,GameObject obj) where T : MonoBehaviour
    {
        GameManager.Resource.DestroyObject_UniRx(o, obj);
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
