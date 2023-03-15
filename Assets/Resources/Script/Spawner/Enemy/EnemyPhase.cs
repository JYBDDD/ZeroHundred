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
        WeightRandom();
        _time = 0.0f;
    }

    private void FixedUpdate()
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
