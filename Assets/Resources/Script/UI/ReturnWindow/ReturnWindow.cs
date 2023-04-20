using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnWindow : MonoBehaviour,IEndGameObserver
{
    [SerializeField]
    GameObject returnWindow;

    private void Awake()
    {
        GameManager.Instance.EndGame_AddObserver(this);
    }

    public void EndGame_Notice()
    {
        returnWindow.SetActive(true);
    }
}
