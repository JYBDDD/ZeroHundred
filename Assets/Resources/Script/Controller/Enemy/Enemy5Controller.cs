using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Controller : BaseController
{
    [SerializeField]
    private StageData stageData;

    private void OnEnable()
    {
        Invoke("Enemy2ReturnState", 0);            // �ǰ��� ���� �̺��� ����

        animation = GetComponent<Animation>();

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Enemy5Stat");    // ���� �ҷ�����  (PC ����)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Enemy5Stat");       // ����

        ObjectList.Add(gameObject);
        StartCoroutine(BombWarning());

        GameManager.Sound.Play("Art/Sound/Effect/Enemy/Enemy5Move/Enemy5MoveSound");
    }

    private void OnDisable()
    {
        ObjectList.Remove(gameObject);
        StopCoroutine(BombWarning());
    }

    private void Start()
    {
        //StatSet(true);        // ���� ����
    }

    private void FixedUpdate()
    {
        DestroyObject();
        ObjectDead();
        StateHit("SwitchColor2", "Enemy2ReturnState");
    }

    IEnumerator BombWarning()
    {
        yield return null;

        bool isbool = true;

        if(isbool)
        {
            yield return new WaitForSeconds(1f);

            Vector3 randVec = new Vector3(Random.Range(stageData.LimitMin.x, stageData.LimitMax.x), Random.Range(stageData.LimitMin.y, stageData.LimitMax.y));
            GameManager.Resource.Instantiate("Weapon/Bombing/Bombing", randVec, Quaternion.identity, GameManager.EnemyBulletParent.transform);
            isbool = false;
        }
    }
}
