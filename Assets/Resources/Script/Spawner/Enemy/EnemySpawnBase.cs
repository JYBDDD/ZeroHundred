using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBase : MonoBehaviour
{
    [SerializeField]
    StageData spawnData;

    [SerializeField]
    GameObject bossSpawn;       // ���� ���� 

    public List<GameObject> ObjectList = new List<GameObject>();


    /// �� ������Ʈ ���� Ȯ��
    protected int enemy1_1Percent;
    protected int enemy1_2Percent;
    protected int enemy2Percent;
    protected int enemy3Percent;
    protected int enemy4Percent;
    protected int enemy5Percent;
    protected int enemyAll_100Percent;

    /// �� ������Ʈ Ȯ�� ���� �ð�
    protected float _timePercentCheck;
    protected float timeCheck;
    protected int checkInt;

    /// �� ������Ʈ ���� ���� ����
    protected int instantiateCount;

    /// �� ������Ʈ ���� �ӵ� ����
    protected float minSpeed;     // �ְ� ���� �ӵ�
    protected float maxSpeed;     // ���� ���� �ӵ�

    void EnemySpawnSystem(string objectPath,int objectCount)
    {
        int count = 0;

        while(objectCount > count)
        {
            count++;
            Vector3 randSpawn = new Vector3(Random.Range(spawnData.LimitMin.x, spawnData.LimitMax.x), Random.Range(spawnData.LimitMin.y, spawnData.LimitMax.y));        // ���� ���� ��ġ
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

    protected void SpawnSystem_PerCentManageMent()      // �� ���� Ȯ�� ���
    {
        _timePercentCheck += Time.deltaTime;
        timeCheck = _timePercentCheck;

        if (timeCheck > 30 && checkInt == 0) 
        {
            enemy1_1Percent = 40;
            enemy1_2Percent = 25;
            enemy2Percent = 10;
            enemy3Percent = 10;
            enemy4Percent = 10;
            enemy5Percent = 5;
            checkInt++;

            WeightRandom();
        }
        if(timeCheck > 60 && checkInt == 1)
        {
            enemy1_1Percent = 30;
            enemy1_2Percent = 20;
            enemy2Percent = 10;
            enemy3Percent = 15;
            enemy4Percent = 15;
            enemy5Percent = 10;
            checkInt++;

            WeightRandom();
        }
        if(timeCheck > 90 && checkInt == 2)
        {
            enemy1_1Percent = 15;
            enemy1_2Percent = 15;
            enemy2Percent = 15;
            enemy3Percent = 20;
            enemy4Percent = 20;
            enemy5Percent = 15;
            checkInt++;

            WeightRandom();
        }
        if(timeCheck > 120 && checkInt == 3)
        {
            enemy1_1Percent = 5;
            enemy1_2Percent = 10;
            enemy2Percent = 25;
            enemy3Percent = 20;
            enemy4Percent = 20;
            enemy5Percent = 20;
            checkInt++;

            WeightRandom();
        }
        if(timeCheck > 150 && checkInt == 4)                 // ���� ���� ����
        {
            checkInt++;
            bossSpawn.SetActive(true);
            gameObject.SetActive(false);                // ���������� EnemyPhase �ӽ� ����
        }

    }
    protected int SpawnSystem_Counting()               // �� ���� ���� ���
    {
        int randInt = Random.Range(0, 100);


        if (randInt < 3)            // 3% Ȯ���� 5�� ��ȯ
            instantiateCount = 5;
        if (randInt < 11)           // 8% Ȯ���� 4�� ��ȯ
            instantiateCount = 4;
        if (randInt < 25)           // 14% Ȯ���� 3�� ��ȯ
            instantiateCount = 3;
        if (randInt < 50)           // 25% Ȯ���� 2�� ��ȯ
            instantiateCount = 2;
        else
            instantiateCount = 1;   // 50% Ȯ���� 1�� ��ȯ

        return instantiateCount;

    }

    protected float SpawnSystem_InstantiateSpeed()       // �� ���� �ӵ� ���
    {
        float randFloat = Random.Range(minSpeed,maxSpeed);

        if(_timePercentCheck == 0.0f)
        {
            minSpeed -= 0.05f;
            maxSpeed -= 0.1f;
            //Debug.Log($" �� ���� �ӵ� :{randFloat}");
        }

        return randFloat;
    }

    protected void WeightRandom()    // ����ġ ����
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
