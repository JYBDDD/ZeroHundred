using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemC : ItemBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Player.playerController.PlayerSkillCount < 3)        // �÷��̾� ��ųī��Ʈ�� �ִ밳���� ���� �ʴ´ٸ� ����
            {
                GameManager.Sound.Play(ObjSound_P.ItemGet);
                GameManager.Player.playerController.PlayerSkillCount++;
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
