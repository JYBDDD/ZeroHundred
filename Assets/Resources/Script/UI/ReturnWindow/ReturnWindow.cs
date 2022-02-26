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

    private void OnWindow()     // �÷��̾� ü���� 0�Ͻ� �ѹ� ����
    {
        if(GameManager.Player.playerController.Stat.Hp <= 0 && onBool == false)
        {
            returnWindow.SetActive(true);
            onBool = true;
        }
    }
}
