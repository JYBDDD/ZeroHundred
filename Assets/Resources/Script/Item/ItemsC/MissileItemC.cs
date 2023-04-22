using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileItemC : ItemBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Player"))
        {
            if (MissileSpawner.ActiveBool == false)       // �̻��Ͻ����ʰ� Ȱ��ȭ ���� ���� ���¶�� Ȱ��ȭ (�ѹ���)
            {
                MissileSpawner.ActiveBool = true;
                GameManager.Sound.Play(ObjSound_P.ItemGet);
                GameManager.Pool.Push(gameObject);
            }

            else
            {
                if (MissileSpawner.ShootCount >= 3)          // �̻��� �߻� ������ 3�� �̻��̶�� �߻� �ӵ� ���
                {
                    MissileSpawner.ShootRange -= scriptableObjectC.ShootRangeUp;
                    GameManager.Sound.Play(ObjSound_P.ItemGet);
                    GameManager.Pool.Push(gameObject);
                }

                else                                        // �̻��� �߻� ���� ����
                {
                    MissileSpawner.ShootCount += scriptableObjectC.ShootCountUp;
                    GameManager.Sound.Play(ObjSound_P.ItemGet);
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
