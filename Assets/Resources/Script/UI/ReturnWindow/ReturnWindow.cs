using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnWindow : MonoBehaviour
{
    [SerializeField]
    GameObject returnWindow;

    private bool onBool;
    private void OnEnable()
    {
        onBool = false;
    }

    private void Update()
    {
        OnWindow();
    }

    private void OnWindow()     // 플레이어 체력이 0일시 한번 실행
    {
        if(GameManager.Player.playerController.Stat.Hp <= 0 && onBool == false)
        {
            returnWindow.SetActive(true);
            onBool = true;
        }
    }
}
