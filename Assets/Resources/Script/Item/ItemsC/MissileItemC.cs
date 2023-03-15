using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileItemC : ItemBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Player"))
        {
            if (MissileSpawner.ActiveBool == false)       // 미사일스포너가 활성화 되지 않은 상태라면 활성화 (한번만)
            {
                MissileSpawner.ActiveBool = true;
                GameManager.Sound.Play("Art/Sound/Effect/Item/ItemGet");
                GameManager.Pool.Push(gameObject);
            }

            else
            {
                if (MissileSpawner.ShootCount >= 3)          // 미사일 발사 갯수가 3개 이상이라면 발사 속도 상승
                {
                    MissileSpawner.ShootRange -= scriptableObjectC.ShootRangeUp;
                    GameManager.Sound.Play("Art/Sound/Effect/Item/ItemGet");
                    GameManager.Pool.Push(gameObject);
                }

                else                                        // 미사일 발사 갯수 증가
                {
                    MissileSpawner.ShootCount += scriptableObjectC.ShootCountUp;
                    GameManager.Sound.Play("Art/Sound/Effect/Item/ItemGet");
                    GameManager.Pool.Push(gameObject);
                }
            }
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
