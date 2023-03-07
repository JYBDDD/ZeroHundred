using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombingAttack : WeaponBase
{
    public static bool BombingHitShake = false;

    private bool isbool;
    private float time = 0;

    private void OnEnable()
    {
        isbool = true;
        time = 0;
        GameManager.Sound.Play("Art/Sound/Effect/Enemy/Bombing/BombingExplosion");
    }

    private void Update()
    {
        if (isbool == true)
            time += Time.deltaTime;

        if(time > 0.1f)         // OnEnable 되었을때 0.1초까지만 타격 판정
            isbool = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void DamageProcess(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isbool)
        {
            // 삭제 시켜주는 구문은 따로 넣어놓음
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 30);       // Bombing 데미지 처리

            BombingHitShake = true;
        }
    }
}
