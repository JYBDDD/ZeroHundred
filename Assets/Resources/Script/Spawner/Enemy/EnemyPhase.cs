using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase : EnemySpawnBase
{
    float _time;

    private void Awake()
    {
        // 오브젝트 리스트 Add
        ObjectList.AddRange(Resources.LoadAll<GameObject>("Prefabs/Enemy"));

        // 적 오브젝트 생성확률 (EnemySpawnBase)
        enemy1_1Percent = 50;
        enemy1_2Percent = 25;
        enemy2Percent = 15;
        enemy3Percent = 10;
        enemy4Percent = 0;
        enemy5Percent = 0;
        enemyAll_100Percent = 0;
        WeightRandom();

        // 적 오브젝트 확률 변동 시간 (EnemySpawnBase) & 카운트
        _timePercentCheck = 0.0f;
        checkInt = 0;

        // 적 오브젝트 생성 갯수 변동 (EnemySpawnBase)
        instantiateCount = 1;

        // 적 오브젝트 생성 속도 변동 (EnemySpawnBase)
        minSpeed = 3f;
        maxSpeed = 5;

        _time = 0.0f;
    }

    protected void Update()
    {
        // 재생성 시간 보다 클시 즉시 생성
        SpawnSystem_PerCentManageMent();
        _time += Time.deltaTime;
        if (_time > SpawnSystem_InstantiateSpeed())
        {
            _time = 0;
            TotalSpawnSystem();
        }

    }




}
