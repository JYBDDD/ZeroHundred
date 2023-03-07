using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : BaseController
{
    protected override void OnEnable()
    {
        base.OnEnable();

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Enemy3Stat");    // ½ºÅÈ ºÒ·¯¿À±â  (PC Àü¿ë)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Enemy3Stat");       // ½ºÅÈ

        ObjectList.Add(gameObject);
    }

    private void OnDisable()
    {
        ObjectList.Remove(gameObject);
    }

    private void Start()
    {
        //StatSet(true);        // ½ºÅÈ »ý¼º
    }

    private void FixedUpdate()
    {
        DestroyObject();
        StateHit("SwitchColor2");
    }

}
