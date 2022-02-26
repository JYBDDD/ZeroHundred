using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : BaseController
{
    private void OnEnable()
    {
        Invoke("Enemy2ReturnState", 0);            // 피격후 색상 미변경 방지

        animation = GetComponent<Animation>();

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Enemy3Stat");    // 스탯 불러오기  (PC 전용)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Enemy3Stat");       // 스탯

        ObjectList.Add(gameObject);
    }

    private void OnDisable()
    {
        ObjectList.Remove(gameObject);
    }

    private void Start()
    {
        //StatSet(true);        // 스탯 생성
    }

    private void FixedUpdate()
    {
        DestroyObject();
        ObjectDead();
        StateHit("SwitchColor2", "Enemy2ReturnState");
    }

}
