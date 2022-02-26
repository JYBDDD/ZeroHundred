using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnScoreRank : MonoBehaviour
{
    [SerializeField]
    Text myBestScore;           // 최고 점수
    [SerializeField]
    Text score;                 // 현재 점수

    private void OnEnable()
    {
        score.text = $"{GameManager.SCORE}";                                // 현재 점수
        myBestScore.text = $"{GameManager.BackendMain.BestMyScore()}";      // 최고 점수
        GameManager.BackendMain.ONInsertUserInfo();
    }




}
