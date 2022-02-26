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
        childText.enabled = true;       // UI�� OFF �ǰ� �ٽ� ����� �ٽ� true�� ����
        childTextBool = true;
    }

    public void ScoreLookButton()
    {
        childTextBool = !childTextBool;

        if (childTextBool)             // Ŭ����
        {
            AlfaUpColor();
            childText.enabled = true;
        }
        if (!childTextBool)             // Ŭ����
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
