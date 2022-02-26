using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public int hp;
    public int maxHp;
    public int dropScore;

    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            hp = Mathf.Clamp(hp, 0, maxHp);
        }
    }

    public int DropScore
    {
        get => dropScore;
        set
        {
            dropScore = value;
            dropScore = Mathf.Clamp(dropScore, 0, 9999999);     // �ִ� ��ӽ��ھ�� ��Ʈ��ũ �ִ� ������ ������ ������
        }
    }

    public void AttackDamage(Stat HitObejct,int attackDamage)
    {
        HitObejct.Hp -= attackDamage;
    }
}
