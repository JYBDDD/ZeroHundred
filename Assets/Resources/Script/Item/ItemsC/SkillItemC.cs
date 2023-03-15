using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemC : ItemBase
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
            if (GameManager.Player.playerController.PlayerSkillCount < 3)        // 플레이어 스킬카운트가 최대개수를 넘지 않는다면 습득
            {
                GameManager.Sound.Play("Art/Sound/Effect/Item/ItemGet");
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
