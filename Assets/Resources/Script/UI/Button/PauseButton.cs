using Path;
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

        if(isBool == false)             // isBool 서로 반대로 작용하도록 설정
        {
            GameManager.Sound.Play(UI_P.UITap);
            PauseWindow.SetActive(false);
            Time.timeScale = 1;
        }

        if(isBool == true)
        {
            GameManager.Sound.Play(UI_P.UITap);
            PauseWindow.SetActive(true);
            Time.timeScale = 0;
        }
    }

    
}
