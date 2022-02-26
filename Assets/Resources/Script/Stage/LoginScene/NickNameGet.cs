using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameGet : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();

        GameManager.BackendMain.GetMyNewData();
    }

    private void OnEnable()
    {
        text.text = $"{"<color=#FFE400>" + "Nick" + "</color>" + "<color=#ffffff>" + " :  " + "</color>"}{ GameManager.BackendMain.GetNick()}";
    }

}
