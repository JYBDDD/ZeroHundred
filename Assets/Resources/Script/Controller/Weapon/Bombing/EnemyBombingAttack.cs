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

        if(time > 0.1f)         // OnEnable �Ǿ����� 0.1�ʱ����� Ÿ�� ����
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
            // ���� �����ִ� ������ ���� �־����
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 30);       // Bombing ������ ó��

            BombingHitShake = true;
        }
    }
}
