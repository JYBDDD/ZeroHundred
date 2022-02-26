using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPanelProgress : MonoBehaviour
{
    RectTransform rectTransform;
    int playerCurrentHp;
    int plyerOriginHp;
    int currentHp;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        playerCurrentHp = GameManager.Player.playerController.Stat.Hp;
        if (plyerOriginHp != playerCurrentHp)            // ü���� �Ҹ�Ǿ������� ȣ��
            HpPanelSet();
    }

    private void HpPanelSet()
    {
        plyerOriginHp = GameManager.Player.playerController.Stat.Hp;        // playerCurrentHp �� �ٽ� ���� ������

        // PlayerHp : 0     ->   z�� 100  
        // PlayerHp : 100   ->   z�� 0

        currentHp = (-GameManager.Player.playerController.Stat.Hp) + 100;       // ���� ü��
        rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, new Quaternion(0, 0, currentHp, 0), Time.deltaTime / 10f);
        //Debug.Log(GameManager.Player.playerController.Stat.Hp);
    }

}
