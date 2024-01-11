using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickButton : MonoBehaviour
{ 
    [SerializeField]
    Text childText;
    bool childTextBool;

    Image myImg;

    private void Awake()
    {
        myImg = GetComponent<Image>();
    }

    private void OnEnable()         
    {
        childText.enabled = true;       // UI가 OFF 되고 다시 실행시 다시 true로 변경
        childTextBool = true;
    }

    public void ScoreLookButton()
    {
        childTextBool = !childTextBool;

        if (childTextBool)             // 클릭후
        {
            AlfaUpColor();
            childText.enabled = true;
        }
        if (!childTextBool)             // 클릭전
        {
            AlfaDownColor();
            childText.enabled = false;
        }
    }

    private void AlfaDownColor()
    {
        Color color = myImg.color;
        color.a = 0;
        myImg.color = color;
    }

    private void AlfaUpColor()
    {
        Color color = myImg.color;
        color.a = 255;
        myImg.color = color;
    }

}
