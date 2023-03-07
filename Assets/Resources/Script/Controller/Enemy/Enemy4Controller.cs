using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Controller : BaseController
{
    protected override void OnEnable()
    {
        base.OnEnable();

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Enemy4Stat");    // ���� �ҷ�����  (PC ����)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Enemy4Stat");       // ����

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
        StateHit("SwitchColor2");
    }
}
