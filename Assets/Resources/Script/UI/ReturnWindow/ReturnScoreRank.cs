using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnScoreRank : MonoBehaviour
{
    [SerializeField]
    Text myBestScore;           // �ְ� ����
    [SerializeField]
    Text score;                 // ���� ����

    private void OnEnable()
    {
        score.text = $"{GameManager.SCORE}";                                // ���� ����
        myBestScore.text = $"{GameManager.BackendMain.BestMyScore()}";      // �ְ� ����
        GameManager.BackendMain.ONInsertUserInfo();
    }




}
