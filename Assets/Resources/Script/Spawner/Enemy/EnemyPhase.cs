using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase : EnemySpawnBase
{
    float _time;

    private void Awake()
    {
        // ������Ʈ ����Ʈ Add
        ObjectList.AddRange(Resources.LoadAll<GameObject>("Prefabs/Enemy"));

        // �� ������Ʈ ����Ȯ�� (EnemySpawnBase)
        WeightRandom();
        _time = 0.0f;
    }

    private void FixedUpdate()
    {
        // ����� �ð� ���� Ŭ�� ��� ����
        SpawnSystem_PerCentManageMent();
        _time += Time.deltaTime;
        if (_time > SpawnSystem_InstantiateSpeed())
        {
            _time = 0;
            TotalSpawnSystem();
        }
    }
}
