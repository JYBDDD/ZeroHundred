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
        enemy1_1Percent = 50;
        enemy1_2Percent = 25;
        enemy2Percent = 15;
        enemy3Percent = 10;
        enemy4Percent = 0;
        enemy5Percent = 0;
        enemyAll_100Percent = 0;
        WeightRandom();

        // �� ������Ʈ Ȯ�� ���� �ð� (EnemySpawnBase) & ī��Ʈ
        _timePercentCheck = 0.0f;
        checkInt = 0;

        // �� ������Ʈ ���� ���� ���� (EnemySpawnBase)
        instantiateCount = 1;

        // �� ������Ʈ ���� �ӵ� ���� (EnemySpawnBase)
        minSpeed = 3f;
        maxSpeed = 5;

        _time = 0.0f;
    }

    protected void Update()
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
