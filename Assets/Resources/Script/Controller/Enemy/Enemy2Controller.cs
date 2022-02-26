using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : BaseController
{
    private void OnEnable()
    {
        Invoke("Enemy2ReturnState", 0);            // �ǰ��� ���� �̺��� ����

        animation = GetComponent<Animation>();

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Enemy2Stat");    // ���� �ҷ�����  (PC ����)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Enemy2Stat");       // ����

        ObjectList.Add(gameObject);
    }

    private void OnDisable()
    {
        ObjectList.Remove(gameObject);
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
}
