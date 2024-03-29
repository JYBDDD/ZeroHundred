using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReCoveryItemC : ItemBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Player.playerController.Stat.Hp < 100)       // 플레이어의 체력이 최대체력이 아닐시만 습득
            {
                GameManager.Sound.Play(ObjSound_P.ItemGet);
                GameManager.Player.playerController.Stat.Hp += scriptableObjectC.HpUp;
                GameManager.Pool.Push(gameObject);
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
