using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BossMissileC : MissileBase
{
    private float speed = 0.0f;             // 유도미사일에 Movement2D 사용하지 않고 자체적으로 구현함
    Vector3 lookVec = Vector3.zero;
    private float waitTime = 0.0f;

    private void Start()
    {
        this.UpdateAsObservable().Subscribe(_ => GuidedMissile());
    }

    private void OnEnable()
    {
        waitTime = 0;
        speed = 0;
        Inheritance();
    }

    private void GuidedMissile()        // 유도 미사일   
    {
        if (transform.position.z > 0.45f)        // 맵 뒤로 이동시 폭발후 즉시 삭제
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, 
                Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
            if (Random.Range(0, 3) >= 2)      // 소리가 너무 겹치지 않도록 랜덤으로 소리생성이 되지 않도록 설정
                GameManager.Sound.Play(ObjSound_P.EnemyMissileExplosion);
            return;
        }

        waitTime += Time.deltaTime;
        if (waitTime < 0.7f) 
        {
            speed = Time.deltaTime;
            transform.Translate(-transform.forward * speed, Space.World);         // 뒤로 이동
        }
        else           
        {
            speed += Time.deltaTime / 40f;
            transform.Translate(transform.forward * speed, Space.World);

            lookVec = GameManager.Player.player.transform.position - transform.position;

            Quaternion qut = Quaternion.LookRotation(lookVec);          // 미사일 forward는 타겟을 향함
            transform.rotation = Quaternion.Slerp(transform.rotation, qut, Time.deltaTime * 6f);        // 방향이 꺽이는 값을 Slerp로 조절
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        EnemyMissileDestroy(other);

        PlayerGuard(other);
    }

    protected override void DamageProcess(Collider other)
    {
        EnemyMissileProcess(other);
    }
}
