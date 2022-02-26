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
        if (plyerOriginHp != playerCurrentHp)            // 체력이 소모되었을떄만 호출
            HpPanelSet();
    }

    private void HpPanelSet()
    {
        plyerOriginHp = GameManager.Player.playerController.Stat.Hp;        // playerCurrentHp 와 다시 값을 맞춰줌

        // PlayerHp : 0     ->   z값 100  
        // PlayerHp : 100   ->   z값 0

        currentHp = (-GameManager.Player.playerController.Stat.Hp) + 100;       // 현재 체력
        rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, new Quaternion(0, 0, currentHp, 0), Time.deltaTime / 10f);
        //Debug.Log(GameManager.Player.playerController.Stat.Hp);
    }

}
