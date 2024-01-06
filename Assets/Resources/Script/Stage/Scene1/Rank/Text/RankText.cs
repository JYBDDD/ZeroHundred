using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankText : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void OnEnable()
    {
        text.text = $"{GameManager.NetworkMain.BestMyRank()}";
    }
}
