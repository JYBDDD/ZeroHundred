using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item ", menuName = "ScriptableItem")]
public class ScriptableObjectC : ScriptableObject
{
    public string ItemName;         // ������ �̸�

    public float ShootRangeUp;      // �߻� �ӵ� Up                  -> �⺻ �ӵ� 0.5f;
    public int ShootCountUp;        // �߻� ���� Up                  -> �⺻ �߻� ���� 1; (�̻��Ͽ��� ���)
    public int HpUp;                // ü�� ȸ��                     -> (�÷��̾� ü�� ȸ��)

    public int SkillCountUp;        // �÷��̾� ��ų ���� Up          -> �÷��̾� ��ų ���� (�ִ� 3��)
}
