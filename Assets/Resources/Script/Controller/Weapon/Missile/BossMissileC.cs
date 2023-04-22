using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BossMissileC : MissileBase
{
    private float speed = 0.0f;             // �����̻��Ͽ� Movement2D ������� �ʰ� ��ü������ ������
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

    private void GuidedMissile()        // ���� �̻���   
    {
        if (transform.position.z > 0.45f)        // �� �ڷ� �̵��� ������ ��� ����
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, 
                Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
            if (Random.Range(0, 3) >= 2)      // �Ҹ��� �ʹ� ��ġ�� �ʵ��� �������� �Ҹ������� ���� �ʵ��� ����
                GameManager.Sound.Play(ObjSound_P.EnemyMissileExplosion);
            return;
        }

        waitTime += Time.deltaTime;
        if (waitTime < 0.7f) 
        {
            speed = Time.deltaTime;
            transform.Translate(-transform.forward * speed, Space.World);         // �ڷ� �̵�
        }
        else           
        {
            speed += Time.deltaTime / 40f;
            transform.Translate(transform.forward * speed, Space.World);

            lookVec = GameManager.Player.player.transform.position - transform.position;

            Quaternion qut = Quaternion.LookRotation(lookVec);          // �̻��� forward�� Ÿ���� ����
            transform.rotation = Quaternion.Slerp(transform.rotation, qut, Time.deltaTime * 6f);        // ������ ���̴� ���� Slerp�� ����
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
