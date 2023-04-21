using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileC : MissileBase
{
    private float speed = 0.0f;             // �����̻��Ͽ� Movement2D ������� �ʰ� ��ü������ ������
    Vector3 lookVec = Vector3.zero;
    private float waitTime = 0.0f;

    protected override void Inheritance()
    {
        base.Inheritance();
    }

    private void OnEnable()
    {
        waitTime = 0;
        speed = 0;
        GameManager.Sound.Play(ObjSound_P.EnemyMisslieShot);
        Inheritance();
    }

    private void FixedUpdate()
    {
        GuidedMissile();
    }

    private void GuidedMissile()        // ���� �̻���   
    {
        if(transform.position.z > 0.45f)        // �� �ڷ� �̵��� ������ ��� ����
        {
            if (Random.Range(0, 3) >= 2)      // �Ҹ��� �ʹ� ��ġ�� �ʵ��� �������� �Ҹ������� ���� �ʵ��� ����
                GameManager.Sound.Play(ObjSound_P.EnemyMissileExplosion);
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
            return;
        }

        waitTime += Time.deltaTime;
        if(waitTime < 1.5f)     // 1.5�ʰ� �̻��� ���� �ӷ� ����
        {
            speed = Time.deltaTime;
            transform.Translate(transform.forward * speed, Space.World);         // forward �������� �̵�
        }
        else                    // 1.5���� �ӷ� ��� ����
        {
            speed += Time.deltaTime / 10f;
            transform.Translate(transform.forward * speed, Space.World);       
        }

        lookVec = GameManager.Player.player.transform.position - transform.position;

        Quaternion qut = Quaternion.LookRotation(lookVec);          // �̻��� forward�� Ÿ���� ����
        transform.rotation = Quaternion.Slerp(transform.rotation, qut, Time.deltaTime * 2f);        // ������ ���̴� ���� Slerp�� ����
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void DamageProcess(Collider other)
    {
        EnemyMissileProcess(other);
    }
}
