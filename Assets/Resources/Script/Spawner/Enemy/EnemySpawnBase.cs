using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBase : MonoBehaviour
{
    [SerializeField]
    StageData spawnData;

    [SerializeField]
    GameObject bossSpawn;       // 보스 스폰 

    [SerializeField]
    SpawnPercentData percentData;

    public List<GameObject> ObjectList = new List<GameObject>();


    /// 적 오브젝트 생성 확률
    [SerializeField,Header("스폰 초기 확률")] protected int enemy1_1Percent;
    [SerializeField] protected int enemy1_2Percent;
    [SerializeField] protected int enemy2Percent;
    [SerializeField] protected int enemy3Percent;
    [SerializeField] protected int enemy4Percent;
    [SerializeField] protected int enemy5Percent;
    protected int enemyAll_100Percent = 0;

    /// 적 오브젝트 확률 변동 시간
    protected float _timePercentCheck = 0;
    protected float timeCheck = 0;
    protected int checkInt = 0;

    /// 적 오브젝트 생성 갯수 변동
    protected int instantiateCount = 1;

    /// 적 오브젝트 생성 속도 변동
    [SerializeField,Header("스폰 속도")] protected float minSpeed;     // 최고 생성 속도
    [SerializeField] protected float maxSpeed;     // 최저 생성 속도

    void EnemySpawnSystem(string objectPath,int objectCount)
    {
        int count = 0;

        while(objectCount > count)
        {
            count++;
            Vector3 randSpawn = new Vector3(Random.Range(spawnData.LimitMin.x, spawnData.LimitMax.x), Random.Range(spawnData.LimitMin.y, spawnData.LimitMax.y));        // 랜덤 스폰 위치
            GameManager.Resource.Instantiate(objectPath, randSpawn, Quaternion.Euler(90, 0, 180), GameManager.EnemyObjectParent.transform);
        }
    }

    protected void Enemy1_1Spawner(int spawnCount)
    {
        EnemySpawnSystem("Enemy/Enemy1_1", spawnCount);
    }
    protected void Enemy1_2Spawner(int spawnCount)
    {
        EnemySpawnSystem("Enemy/Enemy1_2", spawnCount);
    }
    protected void Enemy2Spawner(int spawnCount)
    {
        EnemySpawnSystem("Enemy/Enemy2", spawnCount);
    }
    protected void Enemy3Spawner(int spawnCount)
    {
        EnemySpawnSystem("Enemy/Enemy3", spawnCount);
    }
    protected void Enemy4Spawner(int spawnCount)
    {
        EnemySpawnSystem("Enemy/Enemy4", spawnCount);
    }
    protected void Enemy5Spawner(int spawnCount)
    {
        EnemySpawnSystem("Enemy/Enemy5", spawnCount);
    }

    private void SpawnSet(float checkTime,int checkPhase,bool boss = false)
    {
        if(boss == false)
        {
            if (timeCheck > checkTime && checkInt == checkPhase)
            {

                enemy1_1Percent = percentData.enemy1_1Per[checkPhase];
                enemy1_2Percent = percentData.enemy1_2Per[checkPhase];
                enemy2Percent = percentData.enemy2Per[checkPhase];
                enemy3Percent = percentData.enemy3Per[checkPhase];
                enemy4Percent = percentData.enemy4Per[checkPhase];
                enemy5Percent = percentData.enemy5Per[checkPhase];
                checkInt++;

                WeightRandom();
            }
        }
        else
        {
            if (timeCheck > checkTime && checkInt == checkPhase)                 // 보스 생성 구문
            {
                checkInt++;
                bossSpawn.SetActive(true);
                gameObject.SetActive(false);                // 보스생성후 EnemyPhase 임시 정지
            }
        }
    }

    protected void SpawnSystem_PerCentManageMent()      // 적 스폰 확률 계산
    {
        _timePercentCheck += Time.deltaTime;
        timeCheck = _timePercentCheck;

        SpawnSet(30, 0);
        SpawnSet(60, 1);
        SpawnSet(90, 2);
        SpawnSet(120, 3);
        SpawnSet(150, 4, true);
    }
    protected int SpawnSystem_Counting()               // 적 스폰 갯수 계산
    {
        int randInt = Random.Range(0, 100);


        if (randInt < 3)            // 3% 확률로 5번 소환
            instantiateCount = 5;
        if (randInt < 11)           // 8% 확률로 4번 소환
            instantiateCount = 4;
        if (randInt < 25)           // 14% 확률로 3번 소환
            instantiateCount = 3;
        if (randInt < 50)           // 25% 확률로 2번 소환
            instantiateCount = 2;
        else
            instantiateCount = 1;   // 50% 확률로 1번 소환

        return instantiateCount;

    }

    protected float SpawnSystem_InstantiateSpeed()       // 적 스폰 속도 계산
    {
        float randFloat = Random.Range(minSpeed,maxSpeed);

        if(_timePercentCheck == 0.0f)
        {
            minSpeed -= 0.05f;
            maxSpeed -= 0.1f;
            //Debug.Log($" 적 스폰 속도 :{randFloat}");
        }

        return randFloat;
    }

    protected void WeightRandom()    // 가중치 랜덤
    {
        enemyAll_100Percent = 0;

        for (int i = 0; i < ObjectList.Count; i++)  
        {
            if (i == 0)
                enemyAll_100Percent += enemy1_1Percent;
            if (i == 1)
                enemyAll_100Percent += enemy1_2Percent;
            if (i == 2)
                enemyAll_100Percent += enemy2Percent;
            if (i == 3)
                enemyAll_100Percent += enemy3Percent;
            if (i == 4)
                enemyAll_100Percent += enemy4Percent;
            if (i == 5)
                enemyAll_100Percent += enemy5Percent;
        }
    }

    protected void TotalSpawnSystem()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(enemyAll_100Percent * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < ObjectList.Count; i++)
        {
            if (i == 0)
            {
                weight += enemy1_1Percent;
                if (selectNum <= weight)
                {
                    Enemy1_1Spawner(SpawnSystem_Counting());
                    break;
                }
            }
            if (i == 1)
            {
                weight += enemy1_2Percent;
                if (selectNum <= weight)
                {
                    Enemy1_2Spawner(SpawnSystem_Counting());
                    break;
                }
            }
            if (i == 2)
            {
                weight += enemy2Percent;
                if (selectNum <= weight)
                {
                    Enemy2Spawner(SpawnSystem_Counting());
                    break;
                }
            }
            if (i == 3)
            {
                weight += enemy3Percent;
                if (selectNum <= weight)
                {
                    Enemy3Spawner(SpawnSystem_Counting());
                    break;
                }
            }
            if (i == 4)
            {
                weight += enemy4Percent;
                if (selectNum <= weight)
                {
                    Enemy4Spawner(SpawnSystem_Counting());
                    break;
                }
            }
            if (i == 5)
            {
                weight += enemy5Percent;
                if (selectNum <= weight)
                {
                    Enemy5Spawner(SpawnSystem_Counting());
                    break;
                }
            }
        }
    }
}
