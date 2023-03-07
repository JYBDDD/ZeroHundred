using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_2Controller : BaseController
{
    float _gtime = 0.0f;
    float lookVec = 0.0f;
    Quaternion lookQut = Quaternion.identity;

    protected override void OnEnable()
    {
        base.OnEnable();
        _gtime = 0;

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Enemy1_2Stat");      // ���� �ҷ�����  (PC ����)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Enemy1_2Stat");       // ����

        ObjectList.Add(gameObject);
        StartCoroutine(MoveRoutin_Enemy1_1());
    }
    private void OnDisable()
    {
        gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, -1, 0));
        ObjectList.Remove(gameObject);
        StopCoroutine(MoveRoutin_Enemy1_1());
    }

    private void Start()
    {
        //StatSet(true);        // ���� ����
    }

    IEnumerator MoveRoutin_Enemy1_1()
    {
        yield return null;

        bool isbool = true;
        while (isbool)
        {
            float randRange = Random.Range(3, 7);
            yield return new WaitForSeconds(randRange);                                  // ���� �ð� ���
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0, 0));   // �̵� ���� 

            yield return new WaitForSeconds(2f);                                         // 2���� ��� ����
            isbool = false;
        }
    }

    private void FixedUpdate()
    {
        DestroyObject();
        MoveDirect();
        StateHit("SwitchColor1");
    }

    private void MoveDirect()
    {
        if (_gtime < 3f)
        {
            _gtime += Time.deltaTime;
            float _time = 1.5f;
            if (_gtime > _time)
            {
                int count = 0;
                if (count == 0)
                {
                    lookVec = gameObject.transform.position.x - GameManager.Player.player.transform.position.x;   // �÷��̾� ��ġ�� ���� �ٸ� ��
                    lookQut = (lookVec >= 0) ? Quaternion.Euler(145, -20, 180) : Quaternion.Euler(145, 20, 180);
                    count++;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, lookQut, Time.deltaTime * 2);
            }
        }


    }
}
