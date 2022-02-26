using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    GameObject PauseWindow;

    bool isBool = false;

    public void ClickPauseButton()
    {
        isBool = !isBool;

        if(isBool == false)             // isBool ���� �ݴ�� �ۿ��ϵ��� ����
        {
            GameManager.Sound.Play("Art/Sound/Effect/UI/UITap");
            PauseWindow.SetActive(false);
            Time.timeScale = 1;
        }

        if(isBool == true)
        {
            GameManager.Sound.Play("Art/Sound/Effect/UI/UITap");
            PauseWindow.SetActive(true);
            Time.timeScale = 0;
        }
    }

    
}